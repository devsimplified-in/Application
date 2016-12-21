﻿using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using WinEchek.Core;
using WinEchek.Core.Persistance;
using WinEchek.Model;

namespace WinEchek.GUI.Core {
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        private MainWindow _mainWindow;
        public Home(MainWindow mw) {
            InitializeComponent();
            _mainWindow = mw;
        }

        private void CreateNewGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainControl.Content = new GameModeSelection(new Container(), _mainWindow);
        }

        private void UseSaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            ILoader loader = new BinaryLoader();

            const string directorySaveName = "Save";
            string fullSavePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + directorySaveName;

            if (!Directory.Exists(fullSavePath))
            {
                Directory.CreateDirectory(fullSavePath);
            }

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = loader.Filter(),
                InitialDirectory = fullSavePath
            };

            if (openFileDialog.ShowDialog() != true) return;

            Container container = loader.Load(openFileDialog.FileName);

            _mainWindow.MainControl.Content = new GameModeSelection(container, _mainWindow);
        }

        private void ContributeButton_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/WinEchek/Application");
        }
    }
}
