﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Model;

namespace OnePunch.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public Visibility AdminVisibility { get; private set; } = Visibility.Visible;
    public Visibility HeroSVisibility { get; private set; } = Visibility.Visible;
    public Visibility SaitamaVisibility { get; private set; } = Visibility.Visible;

    private readonly IServiceProvider _services;

    private readonly Dictionary<string, Type> _navigationDictionary = new()
    {
        { "", typeof(MainWindow) },
    };

    private Type? _selectedType = default;


    public MainWindow()
    {
        InitializeComponent();
        _services = App.Current.Services;
    }

    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is MenuItem { Tag: string tag })
        {
            if (_navigationDictionary.TryGetValue(tag, out var navType))
            {
                // TODO: use types
                var o = _services.GetService(navType);

            }
            else
            {
                // tag not found
            }
        }
    }
}