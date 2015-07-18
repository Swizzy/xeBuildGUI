//
// MainWindow.xaml.cs
// xeBuild GUI
//
// Created by Swizzy 18/07/2015
// Copyright (c) 2015 Swizzy. All rights reserved.

using System;
using System.Linq;
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
        }

        private void ModeChanged(object sender, RoutedEventArgs routedEventArgs) {
            var itm = sender as MenuItem;
            if(itm == null)
                return;
            App.Settings.Mode = (AppSettings.UserModes)itm.Header;
            SetMode();
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e) {
            CpukeyBox.Text = "Width: " + Width + " Height: " + Height;
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
            //TODO: make mode changes
        }

        private void MainWindow_OnLocationChanged(object sender, EventArgs e) {
            PatchesPopup.HorizontalOffset += 1;
            PatchesPopup.HorizontalOffset -= 1;
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
    }

}