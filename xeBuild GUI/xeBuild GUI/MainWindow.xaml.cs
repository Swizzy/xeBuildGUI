// 
// MainWindow.xaml.cs
// xeBuild GUI
// 
// Created by Swizzy 18/07/2015
// Copyright (c) 2015 Swizzy. All rights reserved.

namespace xeBuild_GUI {

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow(string[] args) {
            InitializeComponent();
            Title = string.Format(Title, App.Version.Major, App.Version.Minor, App.Version.Build);
            Icon = App.WpfIcon;
        }
    }

}