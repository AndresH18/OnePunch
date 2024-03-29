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
using OnePunch.WPF.ViewModel;

namespace OnePunch.WPF.View.Monster
{
    /// <summary>
    /// Interaction logic for MonstersPage.xaml
    /// </summary>
    public partial class MonstersPage : Page
    {
        public MonstersPage()
        {
            InitializeComponent();
        }

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dockpanel = (DockPanel) Content;
            var vm = (MonstersViewModel) dockpanel.DataContext;
            vm.Refresh();
        }
    }
}
