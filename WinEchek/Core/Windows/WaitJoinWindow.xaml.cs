﻿using System;
using System.ServiceModel;
using WinEchek.Network;

namespace WinEchek.Core.Windows
{
    /// <summary>
    ///     Logique d'interaction pour WaitJoinWindow.xaml
    /// </summary>
    public partial class WaitJoinWindow
    {
        public WaitJoinWindow(Uri uri)
        {
            InitializeComponent();
            LabelWait.Content = "Création de la partie";
            NetworkGameServiceHost = new NetworkGameServiceHost(uri);
            NetworkGameServiceHost.Open();
            NetworkGameServiceHost.NetworkGameService.ClientUriReceived += NetworkGameServiceOnClientUriReceived;
            LabelWait.Content = "Attente d'un autre joueur";
        }

        public NetworkGameServiceClient NetworkGameServiceClient { get; set; }
        public NetworkGameServiceHost NetworkGameServiceHost { get; set; }

        private void NetworkGameServiceOnClientUriReceived(Uri uri)
        {
            LabelWait.Content = "Configuration de la partie";

            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            EndpointAddress endpointAddress = new EndpointAddress(uri);

            try
            {
                NetworkGameServiceClient = new NetworkGameServiceClient(basicHttpBinding, endpointAddress);
                NetworkGameServiceClient.Open();
            }
            catch (Exception)
            {
                NetworkGameServiceClient.Close();
                NetworkGameServiceHost.Close();
                DialogResult = false;
            }
            if (NetworkGameServiceClient.State != CommunicationState.Opened)
            {
                NetworkGameServiceClient.Close();
                NetworkGameServiceHost.Close();
                DialogResult = false;
            }

            LabelWait.Content = "Tentative de connexion avec le client";
            DialogResult = Ping();
        }

        private bool Ping()
        {
            string testMessage = "42";
            try
            {
                NetworkGameServiceClient.InnerChannel.OperationTimeout = TimeSpan.FromSeconds(4);
                string received = NetworkGameServiceClient.Echo(testMessage);
                if (received != testMessage)
                {
                    NetworkGameServiceClient.Close();
                    NetworkGameServiceHost.Close();
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                NetworkGameServiceClient.Close();
                NetworkGameServiceHost.Close();
                return false;
            }
        }
    }
}