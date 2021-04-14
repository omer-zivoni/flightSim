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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowVM vm;
        public MainWindow()
        {
            vm = new MainWindowVM();
            
           
            DataContext = vm;
            InitializeComponent();
        }
        void doneClicked(object sender, RoutedEventArgs e)
        {
            
            Window2 w = new Window2();
            w.Show();
            this.Close();
        }
    }
}
