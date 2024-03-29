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

namespace OnePunch.WPF.View.Hero;

/// <summary>
/// Interaction logic for HeroesPage.xaml
/// </summary>
public partial class HeroesPage : Page
{
    public HeroesPage()
    {
        InitializeComponent();
    }


    private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
    {
        var dockpanel = (DockPanel) Content;
        var vm = (HeroesViewModel) dockpanel.DataContext;
        vm.Refresh();

    }
}