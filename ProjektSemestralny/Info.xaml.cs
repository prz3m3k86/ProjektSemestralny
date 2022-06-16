﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjektSemestralny
{
    /// <summary>
    /// Logika interakcji dla klasy Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        /// <summary>
        /// Konstruktor inicjalizujący okno.
        /// Blokuje zmiane wielkości okna, ustawia je na środku ekranu.
        /// </summary>
        /// <param name="message"></param>
        public Info(string message)
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            InfoLabel.Content = message;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        // Zamykanie okna
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
