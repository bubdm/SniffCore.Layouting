//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SniffCore.Layouting
{
    /// <summary>
    ///     Represents a single line in the <see cref="TitledItemsControl" />.
    /// </summary>
    public class TitledItem : ContentControl
    {
        /// <summary>
        ///     The DependencyProperty for the Title VerticalTitleAlignments.
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(object), typeof(TitledItem), new UIPropertyMetadata(null));

        /// <summary>
        ///     The DependencyProperty for the VerticalTitleAlignment VerticalTitleAlignments.
        /// </summary>
        public static readonly DependencyProperty VerticalTitleAlignmentProperty =
            DependencyProperty.Register("VerticalTitleAlignment", typeof(VerticalAlignment), typeof(TitledItem), new UIPropertyMetadata(VerticalAlignment.Center));

        /// <summary>
        ///     The DependencyProperty for the HorizontalTitleAlignment VerticalTitleAlignments.
        /// </summary>
        public static readonly DependencyProperty HorizontalTitleAlignmentProperty =
            DependencyProperty.Register("HorizontalTitleAlignment", typeof(HorizontalAlignment), typeof(TitledItem), new UIPropertyMetadata(HorizontalAlignment.Left));

        /// <summary>
        ///     The DependencyProperty for the TitleMargin VerticalTitleAlignments.
        /// </summary>
        public static readonly DependencyProperty TitleMarginProperty =
            DependencyProperty.Register("TitleMargin", typeof(Thickness), typeof(TitledItem), new UIPropertyMetadata(new Thickness(5, 0, 5, 0)));

        /// <summary>
        ///     The DependencyProperty for the ContentMargin VerticalTitleAlignments.
        /// </summary>
        public static readonly DependencyProperty ContentMarginProperty =
            DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(TitledItem), new UIPropertyMetadata(new Thickness(0, 2, 0, 2)));

        static TitledItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TitledItem), new FrameworkPropertyMetadata(typeof(TitledItem)));
        }

        /// <summary>
        ///     Gets or sets the title of the row.
        /// </summary>
        /// <remarks>Default: null</remarks>
        [DefaultValue(null)]
        public object Title
        {
            get => GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        ///     Gets or sets the VerticalAlignment of the title.
        /// </summary>
        /// <remarks>Default: VerticalAlignment.Center</remarks>
        [DefaultValue(VerticalAlignment.Center)]
        public VerticalAlignment VerticalTitleAlignment
        {
            get => (VerticalAlignment) GetValue(VerticalTitleAlignmentProperty);
            set => SetValue(VerticalTitleAlignmentProperty, value);
        }

        /// <summary>
        ///     Gets or sets the HorizontalAlignment of the title.
        /// </summary>
        /// <remarks>Default: HorizontalAlignment.Left</remarks>
        [DefaultValue(HorizontalAlignment.Left)]
        public HorizontalAlignment HorizontalTitleAlignment
        {
            get => (HorizontalAlignment) GetValue(HorizontalTitleAlignmentProperty);
            set => SetValue(HorizontalTitleAlignmentProperty, value);
        }

        /// <summary>
        ///     Gets or sets the margin of the title.
        /// </summary>
        /// <remarks>Default: new Thickness(5, 0, 5, 0)</remarks>
        public Thickness TitleMargin
        {
            get => (Thickness) GetValue(TitleMarginProperty);
            set => SetValue(TitleMarginProperty, value);
        }

        /// <summary>
        ///     Gets or sets the margin of the content.
        /// </summary>
        /// <remarks>Default: new Thickness(0, 2, 0, 2)</remarks>
        public Thickness ContentMargin
        {
            get => (Thickness) GetValue(ContentMarginProperty);
            set => SetValue(ContentMarginProperty, value);
        }
    }
}