//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SniffCore.Layouting
{
    /// <summary>
    ///     Enhances the <see cref="WrapPanel" /> by the feature that all items will have the same size.
    /// </summary>
    /// <example>
    ///     <code lang="XAML">
    /// <![CDATA[
    /// <ItemsControl ItemsSource="{Binding Images}">
    ///     <ItemsControl.ItemsPanel>
    ///         <ItemsPanelTemplate>
    ///             <controls:UniformWrapPanel />
    ///         </ItemsPanelTemplate>
    ///     </ItemsControl.ItemsPanel>
    /// </ItemsControl>
    /// 
    /// <controls:UniformWrapPanel Orientation="Horizontal" MinItemWidth="100">
    ///     <Button Content="One" />
    ///     <Button Content="Two" />
    ///     <Button Content="Three" />
    /// </controls:UniformWrapPanel>
    /// ]]>
    /// </code>
    /// </example>
    public class UniformWrapPanel : WrapPanel
    {
        /// <summary>
        ///     Identifies the <see cref="IsAutoUniform" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsAutoUniformProperty =
            DependencyProperty.Register("IsAutoUniform", typeof(bool), typeof(UniformWrapPanel), new UIPropertyMetadata(true, IsAutoUniformChanged));

        /// <summary>
        ///     Identifies the <see cref="MinItemWidth" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MinItemWidthProperty =
            DependencyProperty.Register("MinItemWidth", typeof(double), typeof(UniformWrapPanel), new UIPropertyMetadata(0.0));

        /// <summary>
        ///     Identifies the <see cref="MinItemHeight" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MinItemHeightProperty =
            DependencyProperty.Register("MinItemHeight", typeof(double), typeof(UniformWrapPanel), new UIPropertyMetadata(0.0));

        /// <summary>
        ///     Gets or sets a value that defines if the common height or width will be taken by the biggest child element.
        /// </summary>
        [DefaultValue(true)]
        public bool IsAutoUniform
        {
            get => (bool) GetValue(IsAutoUniformProperty);
            set => SetValue(IsAutoUniformProperty, value);
        }

        /// <summary>
        ///     Gets or sets the minimum width of the items.
        /// </summary>
        [DefaultValue(0.0)]
        public double MinItemWidth
        {
            get => (double) GetValue(MinItemWidthProperty);
            set => SetValue(MinItemWidthProperty, value);
        }

        /// <summary>
        ///     Gets or sets the minimum height of the items.
        /// </summary>
        [DefaultValue(0.0)]
        public double MinItemHeight
        {
            get => (double) GetValue(MinItemHeightProperty);
            set => SetValue(MinItemHeightProperty, value);
        }

        private static void IsAutoUniformChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is UniformWrapPanel panel)
                panel.InvalidateVisual();
        }

        /// <summary>
        ///     Lets each child calculating is needed size.
        /// </summary>
        /// <param name="availableSize">The available space by the parent control.</param>
        /// <returns>The calculated size needed for the control.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            if (Children.Count > 0 && IsAutoUniform)
                foreach (UIElement el in Children)
                {
                    el.Measure(availableSize);
                    if (Orientation == Orientation.Vertical)
                        ItemHeight = MeasureItem(ItemHeight, el.DesiredSize.Height, MinItemHeight);
                    else
                        ItemWidth = MeasureItem(ItemWidth, el.DesiredSize.Width, MinItemWidth);
                }

            return base.MeasureOverride(availableSize);
        }

        private double MeasureItem(double finalItemSize, double currentItemSize, double minimumSize)
        {
            if (double.IsNaN(finalItemSize))
                finalItemSize = 0;

            if (double.IsInfinity(currentItemSize) || double.IsNaN(currentItemSize))
                return finalItemSize;

            finalItemSize = Math.Max(finalItemSize, currentItemSize);
            if (finalItemSize < minimumSize)
                finalItemSize = minimumSize;
            return finalItemSize;
        }
    }
}