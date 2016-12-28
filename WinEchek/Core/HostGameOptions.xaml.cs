﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using WinEchek.Core.Windows;
using WinEchek.Model;

namespace WinEchek.Core
{
    /// <summary>
    ///     Logique d'interaction pour HostGameOptions.xaml
    /// </summary>
    public partial class HostGameOptions : UserControl
    {
        private Container _container;
        private MainWindow _mainWindow;

        public HostGameOptions(MainWindow mainWindow, Container container)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _container = container;

            InitComboBoxIp();
        }

        private void InitComboBoxIp()
        {
            // Getting Ip address of local machine...
            // First get the host name of local machine.
            var strHostName = Dns.GetHostName();
            Console.WriteLine("Local Machine's Host Name: " + strHostName);
            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            foreach (IPAddress t in addr)
                ComboBoxIP.Items.Add(t.ToString());
        }

        private void ButtonCreate_OnClick(object sender, RoutedEventArgs e)
        {
            Uri uri =
                new Uri("http://" + ComboBoxIP.SelectedItem + ":" + TextBoxPort.Text + "/" + TextBoxGameName.Text +
                        TextBoxPseudo.Text);
            WaitJoinWindow waitJoinWindow = new WaitJoinWindow(uri);
            if (waitJoinWindow.ShowDialog() == true)
            {
                //créer la partie
            }
            else
            {
                _mainWindow.ShowMessageAsync("Erreur réseau",
                    "Il y a eu un problème lors de la connexion avec l'autre joueur... Vueillez réessayer.",
                    MessageDialogStyle.Affirmative);
            }
        }
    }
}