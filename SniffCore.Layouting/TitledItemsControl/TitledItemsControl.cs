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
    ///     Provides the possibility to automatically align titles and contents.
    /// </summary>
    /// <example>
    ///     <code lang="XAML">
    /// <![CDATA[
    /// <sniffCore:TitledItemsControl>
    ///     <sniffCore:TitledItem Text="Name">
    ///         <TextBox Text="{Binding Name}" />
    ///     </sniffCore:TitledItem>
    ///     <sniffCore:TitledItem Text="Family Name">
    ///         <TextBox Text="{Binding FamilyName}" />
    ///     </sniffCore:TitledItem>
    ///     <sniffCore:TitledItem Text="Age">
    ///         <TextBox Text="{Binding Age}" />
    ///     </sniffCore:TitledItem>
    /// </sniffCore:TitledItemsControl>
    /// ]]>
    /// </code>
    /// </example>
    public class TitledItemsControl : ItemsControl
    {
        /// <summary>
        ///     The DependencyProperty for the ViewModel VerticalTitleAlignments.
        /// </summary>
        public static readonly DependencyProperty VerticalTitleAlignmentsProperty =
            DependencyProperty.Register("VerticalTitleAlignments", typeof(VerticalAlignment), typeof(TitledItemsControl), new UIPropertyMetadata(VerticalAlignment.Center));

        /// <summary>
        ///     The DependencyProperty for the ViewModel HorizontalTitleAlignments.
        /// </summary>
        public static readonly DependencyProperty HorizontalTitleAlignmentsProperty =
            DependencyProperty.Register("HorizontalTitleAlignments", typeof(HorizontalAlignment), typeof(TitledItemsControl), new UIPropertyMetadata(HorizontalAlignment.Left));

        /// <summary>
        ///     The DependencyProperty for the ViewModel TitleMargins.
        /// </summary>
        public static readonly DependencyProperty TitleMarginsProperty =
            DependencyProperty.Register("TitleMargins", typeof(Thickness), typeof(TitledItemsControl), new UIPropertyMetadata(new Thickness(5, 0, 5, 0)));

        /// <summary>
        ///     The DependencyProperty for the ViewModel HorizontalContentAlignments.
        /// </summary>
        public static readonly DependencyProperty HorizontalContentAlignmentsProperty =
            DependencyProperty.Register("HorizontalContentAlignments", typeof(HorizontalAlignment), typeof(TitledItemsControl), new UIPropertyMetadata(HorizontalAlignment.Stretch));

        /// <summary>
        ///     The DependencyProperty for the ViewModel VerticalContentAlignments.
        /// </summary>
        public static readonly DependencyProperty VerticalContentAlignmentsProperty =
            DependencyProperty.Register("VerticalContentAlignments", typeof(VerticalAlignment), typeof(TitledItemsControl), new UIPropertyMetadata(VerticalAlignment.Center));

        /// <summary>
        ///     The DependencyProperty for the ViewModel ContentMargins.
        /// </summary>
        public static readonly DependencyProperty ContentMarginsProperty =
            DependencyProperty.Register("ContentMargins", typeof(Thickness), typeof(TitledItemsControl), new UIPropertyMetadata(new Thickness(0, 2, 0, 2)));

        static TitledItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TitledItemsControl), new FrameworkPropertyMetadata(typeof(TitledItemsControl)));
        }

        /// <summary>
        ///     Gets or sets the VerticalAlignment of all titles.
        /// </summary>
        /// <remarks>Default: HorizontalAlignment.Center</remarks>
        [DefaultValue(VerticalAlignment.Center)]
        public VerticalAlignment VerticalTitleAlignments
        {
            get => (VerticalAlignment) GetValue(VerticalTitleAlignmentsProperty);
            set => SetValue(VerticalTitleAlignmentsProperty, value);
        }

        /// <summary>
        ///     Gets or sets the HorizontalAlignment of all titles.
        /// </summary>
        /// <remarks>Default: HorizontalAlignment.Left</remarks>
        [DefaultValue(HorizontalAlignment.Left)]
        public HorizontalAlignment HorizontalTitleAlignments
        {
            get => (HorizontalAlignment) GetValue(HorizontalTitleAlignmentsProperty);
            set => SetValue(HorizontalTitleAlignmentsProperty, value);
        }

        /// <summary>
        ///     Gets or sets the margin for all titles.
        /// </summary>
        /// <remarks>Default: new Thickness(5, 0, 5, 0)</remarks>
        public Thickness TitleMargins
        {
            get => (Thickness) GetValue(TitleMarginsProperty);
            set => SetValue(TitleMarginsProperty, value);
        }

        /// <summary>
        ///     Gets or sets the HorizontalAlignment for all contents.
        /// </summary>
        /// <remarks>Default: HorizontalAlignment.Stretch</remarks>
        [DefaultValue(HorizontalAlignment.Stretch)]
        public HorizontalAlignment HorizontalContentAlignments
        {
            get => (HorizontalAlignment) GetValue(HorizontalContentAlignmentsProperty);
            set => SetValue(HorizontalContentAlignmentsProperty, value);
        }

        /// <summary>
        ///     Gets or sets the VerticalAlignment for all contents.
        /// </summary>
        /// <remarks>Default: VerticalAlignment.Center</remarks>
        [DefaultValue(VerticalAlignment.Center)]
        public VerticalAlignment VerticalContentAlignments
        {
            get => (VerticalAlignment) GetValue(VerticalContentAlignmentsProperty);
            set => SetValue(VerticalContentAlignmentsProperty, value);
        }

        /// <summary>
        ///     Gets or sets the margin of all contents.
        /// </summary>
        /// <remarks>Default: new Thickness(0, 2, 0, 2)</remarks>
        public Thickness ContentMargins
        {
            get => (Thickness) GetValue(ContentMarginsProperty);
            set => SetValue(ContentMarginsProperty, value);
        }

        /// <summary>
        ///     Returns a new child container.
        /// </summary>
        /// <returns>A new child container.</returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TitledItem();
        }

        /// <summary>
        ///     Checks if the item is already the right container.
        /// </summary>
        /// <param name="item">The item added to the collection.</param>
        /// <returns>True if the items is already the correct child container; otherwise false.</returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TitledItem;
        }
    }
}