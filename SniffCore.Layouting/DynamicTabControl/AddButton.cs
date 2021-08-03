//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

// ReSharper disable once CheckNamespace

namespace SniffCore.Layouting
{
    /// <summary>
    ///     Represents the add new tab Button shown in the <see cref="DynamicTabControl" />.
    /// </summary>
    public class AddButton : Button
    {
        /// <summary>
        ///     Identifies the <see cref="StrokeThickness" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(AddButton), new UIPropertyMetadata(1.5));

        static AddButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AddButton), new FrameworkPropertyMetadata(typeof(AddButton)));
        }

        /// <summary>
        ///     Gets or sets the stroke thickness of the plus icon shown in the template.
        /// </summary>
        [DefaultValue(1.5)]
        public double StrokeThickness
        {
            get => (double) GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }
    }
}