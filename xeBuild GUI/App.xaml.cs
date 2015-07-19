//
// App.xaml.cs
// xeBuild GUI
//
// Created by Swizzy 18/07/2015
// Copyright (c) 2015 Swizzy. All rights reserved.

using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace xeBuild_GUI {

    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App {
        private static readonly Icon Icon =
            Icon.ExtractAssociatedIcon(
                                       Path.Combine(
                                                    Path.GetDirectoryName(Assembly.GetAssembly(typeof(App)).Location),
                                                    Path.GetFileName(Assembly.GetAssembly(typeof(App)).Location)));

        internal static readonly ImageSource WpfIcon = Imaging.CreateBitmapSourceFromHIcon(
                                                                                           Icon.Handle, Int32Rect.Empty,
                                                                                           BitmapSizeOptions
                                                                                               .FromEmptyOptions());

        internal static Version Version = Assembly.GetAssembly(typeof(App)).GetName().Version;
        internal static AppSettings Settings = new AppSettings();
        internal static MainWindow AppWindow;

        private void AppStart(object sender, StartupEventArgs e) {
            AppWindow = new MainWindow(e.Args);
            AppWindow.Show();
        }

    }

}