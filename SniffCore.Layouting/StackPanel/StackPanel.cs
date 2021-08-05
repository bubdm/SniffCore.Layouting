//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;
using System.Windows;
using System.Windows.Controls;

// ReSharper disable once CheckNamespace

namespace SniffCore.Layouting
{
    /// <summary>
    ///     A StackPanel which adds a spacing between the items.
    /// </summary>
    /// <example>
    ///     <code lang="xaml">
    /// <![CDATA[
    /// <Window>
    ///     <DockPanel>
    ///         <controls:StackPanel DockPanel.Dock="Bottom"
    ///                              HorizontalAlignment="Right"
    ///                              Orientation="Horizontal"
    ///                              Spacing="10">
    ///             <Button Content="Back" />
    ///             <Button Content="Next" />
    ///             <Button Content="Finish" />
    ///             <Button Content="Cancel" />
    ///         </controls:StackPanel>
    /// 
    ///         <controls:StackPanel Spacings="10">
    ///             <TextBox />
    ///             <TextBox />
    ///         </controls:StackPanel>
    ///     </DockPanel>
    /// </Window>
    /// ]]>
    /// </code>
    /// </example>
    public class StackPanel : Panel
    {
        /// <summary>
        ///     Identifies the Spacing dependency property.
        /// </summary>
        public static readonly DependencyProperty SpacingProperty =
            DependencyProperty.Register("Spacing", typeof(double), typeof(StackPanel), new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        ///     Identifies the Orientation dependency property.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(StackPanel), new PropertyMetadata(Orientation.Vertical));

        /// <summary>
        ///     Gets or sets the space between the items.
        /// </summary>
        public double Spacing
        {
            get => (double) GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        /// <summary>
        ///     Gets or sets the orientation of the panel.
        /// </summary>
        public Orientation Orientation
        {
            get => (Orientation) GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        /// <summary>
        ///     Calculates the size this panel would like to get from the parent control.
        /// </summary>
        /// <param name="availableSize">The available size from the parent control.</param>
        /// <returns>The size this panel would like to get from the parent control.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            var maxWidth = 0d;
            var maxHeight = 0d;
            var totalItemWidth = 0d;
            var totalItemHeight = 0d;
            foreach (UIElement child in Children)
            {
                var newAvailableSize = new Size(availableSize.Width, double.PositiveInfinity);
                if (Orientation == Orientation.Horizontal)
                    newAvailableSize = new Size(double.PositiveInfinity, availableSize.Height);

                child.Measure(newAvailableSize);
                maxWidth = Math.Max(maxWidth, child.DesiredSize.Width);
                maxHeight = Math.Max(maxHeight, child.DesiredSize.Height);

                totalItemWidth += child.DesiredSize.Width;
                totalItemHeight += child.DesiredSize.Height;
            }

            if (Orientation == Orientation.Horizontal)
            {
                var calculatedWidth = totalItemWidth + Spacing * (Children.Count - 1);
                var availableWidth = double.IsInfinity(availableSize.Width) ? calculatedWidth : availableSize.Width;
                if (HorizontalAlignment != HorizontalAlignment.Stretch)
                    availableWidth = Math.Min(availableWidth, calculatedWidth);
                return new Size(Math.Max(availableWidth, calculatedWidth), maxHeight);
            }

            var calculatedHeight = totalItemHeight + Spacing * (Children.Count - 1);
            var availableHeight = double.IsInfinity(availableSize.Height) ? calculatedHeight : availableSize.Height;
            if (VerticalAlignment != VerticalAlignment.Stretch)
                availableHeight = Math.Min(availableHeight, calculatedHeight);
            return new Size(maxWidth, Math.Max(availableHeight, calculatedHeight));
        }

        /// <summary>
        ///     Arranges all the children in the given space by the parent control.
        /// </summary>
        /// <param name="finalSize">The available size give from the parent control.</param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var left = 0d;
            var top = 0d;

            if (Orientation == Orientation.Horizontal)
                foreach (UIElement child in Children)
                {
                    var desiredSize = child.DesiredSize;
                    child.Arrange(new Rect(new Point(left, top), new Size(desiredSize.Width, finalSize.Height)));
                    left += desiredSize.Width + Spacing;
                }
            else
                foreach (UIElement child in Children)
                {
                    var desiredSize = child.DesiredSize;
                    child.Arrange(new Rect(new Point(left, top), new Size(finalSize.Width, desiredSize.Height)));
                    top += desiredSize.Height + Spacing;
                }

            return finalSize;
        }
    }
}