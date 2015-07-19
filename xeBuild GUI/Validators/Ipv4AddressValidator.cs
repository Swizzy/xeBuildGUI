//
// Ipv4AddressValidator.cs
// xeBuild GUI
//
// Created by Swizzy 19/07/2015
// Copyright (c) 2015 Swizzy. All rights reserved.

using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Windows.Controls;

namespace xeBuild_GUI.Validators {

    internal class IpAddressValidator : ValidationRule {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            var address = value as string;
            IPAddress addr;
            return address != null && IPAddress.TryParse(address, out addr) &&
                   addr.AddressFamily == AddressFamily.InterNetwork
                       ? ValidationResult.ValidResult
                       : new ValidationResult(
                             false,
                             string.Format("Invalid IPv4 Address{0}Expected format:{0}###.###.###.###{0}Example:{0}192.168.0.100", Environment.NewLine));
        }
    }

}