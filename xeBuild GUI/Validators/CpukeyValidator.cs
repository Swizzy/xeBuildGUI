//
// CpukeyValidator.cs
// xeBuild GUI
//
// Created by Swizzy 19/07/2015
// Copyright (c) 2015 Swizzy. All rights reserved.

using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace xeBuild_GUI.Validators {

    internal class CpukeyValidator : ValidationRule {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            var key = value as string;
            if(string.IsNullOrWhiteSpace(key))
                return new ValidationResult(false, "You need to supply a CPUKey...");
            if(key.Length != 32)
                return new ValidationResult(false, "The CPUKey must be 32 characters long");
            return !Regex.IsMatch(key, "[a-fA-F0-9]{32}")
                       ? new ValidationResult(false, "The CPUKey can only contain hex characters (a-f, A-F or 0-9)")
                       : ValidationResult.ValidResult;
            //TODO: Implement further checks
        }
    }

}