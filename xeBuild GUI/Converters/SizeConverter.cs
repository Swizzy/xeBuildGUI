//
// SizeConverter.cs
// xeBuild GUI
//
// Created by Swizzy 18/07/2015
// Copyright (c) 2015 Swizzy. All rights reserved.

using System;
using System.Globalization;
using System.Windows.Data;

namespace xeBuild_GUI.Converters {

    [ValueConversion(typeof(object), typeof(double))] public class SizeConverter : BaseConverter, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return System.Convert.ToDouble(value) + System.Convert.ToDouble(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

}