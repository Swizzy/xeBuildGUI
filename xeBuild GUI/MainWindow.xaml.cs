//
// MainWindow.xaml.cs
// xeBuild GUI
//
// Created by Swizzy 18/07/2015
// Copyright (c) 2015 Swizzy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace xeBuild_GUI {

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow(string[] args) {
            InitializeComponent();
            Title = string.Format(Title, App.Version.Major, App.Version.Minor, App.Version.Build);
            Icon = App.WpfIcon;
            foreach(var itm in from object mode in Enum.GetValues(typeof(AppSettings.UserModes)) select new MenuItem {
                                                                                                                         Header
                                                                                                                             =
                                                                                                                             mode,
                                                                                                                         IsCheckable
                                                                                                                             =
                                                                                                                             true
                                                                                                                     }) {
                itm.Checked += ModeChanged;
                ModesMenu.Items.Add(itm);
            }
            SetMode();
            SetDefaultIp();
            ProcessArgs(args);
        }

        private void ProcessArgs(ICollection<string> args) {
            if(args.Count > 0)
                MessageBox.Show("ERROR: Not implemented yet...");
        }

        public string Cpukey { get { return App.Settings.Cpukey; } set { App.Settings.Cpukey = value; } }
        public string IpAddress { get; set; }

        public void SetDefaultIp() {
            foreach(var ipsplit in
                from t in Dns.GetHostAddresses(Dns.GetHostName()) where t.AddressFamily == AddressFamily.InterNetwork
                select t.ToString().Split(".".ToCharArray())) {
                IpAddress = ipsplit[0] + "." + ipsplit[1] + "." + ipsplit[2] + ".";
                return;
            }
        }

        private void ModeChanged(object sender, RoutedEventArgs routedEventArgs) {
            var itm = sender as MenuItem;
            if(itm == null)
                return;
            App.Settings.Mode = (AppSettings.UserModes)itm.Header;
            SetMode();
        }

        private void SetMode() {
            foreach(var menuItem in
                ModesMenu.Items.OfType<MenuItem>().Where(menuItem => !Equals(menuItem.Header, App.Settings.Mode)))
                menuItem.IsChecked = false;
            foreach(var menuItem in
                ModesMenu.Items.OfType<MenuItem>().Where(menuItem => Equals(menuItem.Header, App.Settings.Mode)))
                menuItem.IsChecked = true;
            AdvancedSettingsTab.Visibility = App.Settings.Mode == AppSettings.UserModes.Advanced
                                                 ? Visibility.Visible : Visibility.Collapsed;
            ModeText.Content = App.Settings.Mode;
            if(App.Settings.Mode != AppSettings.UserModes.Advanced) {
                if(AdvancedSettingsTab.IsSelected)
                    ConsoleInfoTab.IsSelected = true;
            }
            //TODO: make mode changes
        }

        private void UpdatePatchesSize() {
            PatchesPopup.HorizontalOffset += 1;
            PatchesPopup.HorizontalOffset -= 1;
            var p = PatchesButton.TranslatePoint(new Point(0, 0), this);
            PatchesBorder.MaxHeight = ActualHeight - p.Y - PatchesButton.ActualHeight - 40;
        }

        private void PatchesButton_OnClick(object sender, RoutedEventArgs e) { OpenPatches(); }

        private void OpenPatches() {
            if(PatchesPopup.IsOpen)
                PatchesPopup.IsOpen = false;
            PatchesPopup.IsOpen = true;
            OptionalPatchesBox.Children.Clear();
            foreach(var cbox in from OptionalPatch patch in App.Settings.GetAvailablePatches() select new CheckBox {
                                                                                                                       Content
                                                                                                                           =
                                                                                                                           patch
                                                                                                                   }) {
                var patch = cbox.Content as OptionalPatch;
                if(patch == null)
                    continue;
                if(!string.IsNullOrWhiteSpace(patch.Description))
                    cbox.ToolTip = patch.Description;
                var binding = new Binding("IsUsed") {
                                                        Source = patch,
                                                        Mode = BindingMode.TwoWay,
                                                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                                                    };
                BindingOperations.SetBinding(cbox, ToggleButton.IsCheckedProperty, binding);
                OptionalPatchesBox.Children.Add(cbox);
            }
        }

        private void PatchesButton_OnMouseEnter(object sender, MouseEventArgs e) { OpenPatches(); }

        internal void UpdateValidation() {
            UpdateValidation(CpukeyBox);
            UpdateValidation(IpBox);
        }

        private static void UpdateValidation(FrameworkElement tbox) {
            if(tbox == null)
                return;
            var expr = tbox.GetBindingExpression(TextBox.TextProperty);
            if(expr != null)
                expr.UpdateSource();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
            UpdateValidation();
            UpdatePatchesSize();
        }
    }

}