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
    ///     Represents the shown tab in the <see cref="DynamicTabControl" />.
    /// </summary>
    public class DynamicTabItem : TabItem
    {
        /// <summary>
        ///     Identifies the <see cref="CloseButtonPosition" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CloseButtonPositionProperty =
            DependencyProperty.Register("CloseButtonPosition", typeof(Dock), typeof(DynamicTabItem), new UIPropertyMetadata(Dock.Right));

        /// <summary>
        ///     Identifies the <see cref="CloseButtonMargin" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CloseButtonMarginProperty =
            DependencyProperty.Register("CloseButtonMargin", typeof(Thickness), typeof(DynamicTabItem), new UIPropertyMetadata(new Thickness(5, 0, 0, 0)));

        /// <summary>
        ///     Identifies the <see cref="HorizontalCloseButtonAlignment" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty HorizontalCloseButtonAlignmentProperty =
            DependencyProperty.Register("HorizontalCloseButtonAlignment", typeof(HorizontalAlignment), typeof(DynamicTabItem), new UIPropertyMetadata(HorizontalAlignment.Center));

        /// <summary>
        ///     Identifies the <see cref="VerticalCloseButtonAlignment" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty VerticalCloseButtonAlignmentProperty =
            DependencyProperty.Register("VerticalCloseButtonAlignment", typeof(VerticalAlignment), typeof(DynamicTabItem), new UIPropertyMetadata(VerticalAlignment.Center));

        /// <summary>
        ///     Identifies the <see cref="CloseButtonHeight" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CloseButtonHeightProperty =
            DependencyProperty.Register("CloseButtonHeight", typeof(double), typeof(DynamicTabItem), new UIPropertyMetadata(14.0));

        /// <summary>
        ///     Identifies the <see cref="CloseButtonWidth" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CloseButtonWidthProperty =
            DependencyProperty.Register("CloseButtonWidth", typeof(double), typeof(DynamicTabItem), new UIPropertyMetadata(14.0));

        /// <summary>
        ///     Identifies the <see cref="ShowCloseButton" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ShowCloseButtonProperty =
            DependencyProperty.Register("ShowCloseButton", typeof(bool), typeof(DynamicTabItem), new UIPropertyMetadata(true));

        static DynamicTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DynamicTabItem), new FrameworkPropertyMetadata(typeof(DynamicTabItem)));
        }

        /// <summary>
        ///     Gets or sets a value which indicates where the close tab item button have to be placed in the header.
        /// </summary>
        [DefaultValue(Dock.Right)]
        public Dock CloseButtonPosition
        {
            get => (Dock) GetValue(CloseButtonPositionProperty);
            set => SetValue(CloseButtonPositionProperty, value);
        }

        /// <summary>
        ///     Gets or sets the margin of the close tab item button.
        /// </summary>
        public Thickness CloseButtonMargin
        {
            get => (Thickness) GetValue(CloseButtonMarginProperty);
            set => SetValue(CloseButtonMarginProperty, value);
        }

        /// <summary>
        ///     Gets or sets the horizontal alignment of the close tab item button.
        /// </summary>
        [DefaultValue(HorizontalAlignment.Center)]
        public HorizontalAlignment HorizontalCloseButtonAlignment
        {
            get => (HorizontalAlignment) GetValue(HorizontalCloseButtonAlignmentProperty);
            set => SetValue(HorizontalCloseButtonAlignmentProperty, value);
        }

        /// <summary>
        ///     Gets or sets the vertical alignment of the close tab item button.
        /// </summary>
        [DefaultValue(VerticalAlignment.Center)]
        public VerticalAlignment VerticalCloseButtonAlignment
        {
            get => (VerticalAlignment) GetValue(VerticalCloseButtonAlignmentProperty);
            set => SetValue(VerticalCloseButtonAlignmentProperty, value);
        }

        /// <summary>
        ///     Gets or sets the height of the close tab item button.
        /// </summary>
        [DefaultValue(14.0)]
        public double CloseButtonHeight
        {
            get => (double) GetValue(CloseButtonHeightProperty);
            set => SetValue(CloseButtonHeightProperty, value);
        }

        /// <summary>
        ///     Gets or sets the width of the close tab item button
        /// </summary>
        [DefaultValue(14.0)]
        public double CloseButtonWidth
        {
            get => (double) GetValue(CloseButtonWidthProperty);
            set => SetValue(CloseButtonWidthProperty, value);
        }

        /// <summary>
        ///     Gets or sets a value which indicates if the close tab item button is visible or not.
        /// </summary>
        [DefaultValue(true)]
        public bool ShowCloseButton
        {
            get => (bool) GetValue(ShowCloseButtonProperty);
            set => SetValue(ShowCloseButtonProperty, value);
        }
    }
}