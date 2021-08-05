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
    ///     A UniformGrid with only one row or one column, depending on the orientation, which adds a spacing between the
    ///     items.
    /// </summary>
    /// <example>
    ///     <code lang="xaml">
    /// <![CDATA[
    /// <Window>
    ///     <DockPanel>
    ///         <controls:UniformPanel DockPanel.Dock="Bottom"
    ///                                HorizontalAlignment="Right"
    ///                                Orientation="Horizontal"
    ///                                Spacing="10">
    ///             <Button Content="Back" />
    ///             <Button Content="Next" />
    ///             <Button Content="Finish" />
    ///             <Button Content="Cancel" />
    ///         </controls:StackPanel>
    /// 
    ///         <Grid />
    ///     </DockPanel>
    /// </Window>
    /// ]]>
    /// </code>
    /// </example>
    public class UniformPanel : Panel
    {
        /// <summary>
        ///     Identifies the Spacing dependency property.
        /// </summary>
        public static readonly DependencyProperty SpacingProperty =
            DependencyProperty.Register("Spacing", typeof(double), typeof(UniformPanel), new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        ///     Identifies the Orientation dependency property.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(UniformPanel), new PropertyMetadata(Orientation.Horizontal));

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
            return Measure(availableSize, Children, Spacing, Orientation, HorizontalAlignment, VerticalAlignment);
        }

        /// <summary>
        ///     Arranges all the children in the given space by the parent control.
        /// </summary>
        /// <param name="finalSize">The available size give from the parent control.</param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            return Arrange(finalSize, Children, Spacing, Orientation);
        }

        internal static Size Measure(Size availableSize, UIElementCollection children, double spacing, Orientation orientation, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            var totalSpacing = spacing * (children.Count - 1);

            if (orientation == Orientation.Vertical)
            {
                var itemWidth = availableSize.Width;
                var maxItemHeight = (availableSize.Height - totalSpacing) / children.Count;
                var maxItemSize = new Size(itemWidth, maxItemHeight);
                var meassuredMaxItemHeight = 0d;
                var meassuredMaxItemWidth = 0d;
                foreach (UIElement child in children)
                {
                    child.Measure(maxItemSize);
                    meassuredMaxItemHeight = Math.Max(meassuredMaxItemHeight, child.DesiredSize.Height);
                    meassuredMaxItemWidth = Math.Max(meassuredMaxItemWidth, child.DesiredSize.Width);
                }

                if (horizontalAlignment != HorizontalAlignment.Stretch)
                    itemWidth = Math.Min(itemWidth, meassuredMaxItemWidth);

                if (double.IsInfinity(itemWidth))
                    itemWidth = meassuredMaxItemWidth;

                if (double.IsInfinity(maxItemHeight))
                    maxItemHeight = meassuredMaxItemHeight;

                if (verticalAlignment == VerticalAlignment.Stretch)
                    return new Size(itemWidth, maxItemHeight * children.Count + totalSpacing);
                return new Size(itemWidth, Math.Min(maxItemHeight, meassuredMaxItemHeight) * children.Count + totalSpacing);
            }
            else
            {
                var itemHeight = availableSize.Height;
                var maxItemWidth = (availableSize.Width - totalSpacing) / children.Count;
                var maxItemSize = new Size(maxItemWidth, itemHeight);
                var meassuredMaxItemHeight = 0d;
                var meassuredMaxItemWidth = 0d;
                foreach (UIElement child in children)
                {
                    child.Measure(maxItemSize);
                    meassuredMaxItemHeight = Math.Max(meassuredMaxItemHeight, child.DesiredSize.Height);
                    meassuredMaxItemWidth = Math.Max(meassuredMaxItemWidth, child.DesiredSize.Width);
                }

                if (verticalAlignment != VerticalAlignment.Stretch)
                    itemHeight = Math.Min(itemHeight, meassuredMaxItemHeight);

                if (double.IsInfinity(itemHeight))
                    itemHeight = meassuredMaxItemHeight;

                if (double.IsInfinity(maxItemWidth))
                    maxItemWidth = meassuredMaxItemWidth;

                if (horizontalAlignment == HorizontalAlignment.Stretch)
                    return new Size(maxItemWidth * children.Count + totalSpacing, itemHeight);
                return new Size(Math.Min(maxItemWidth, meassuredMaxItemWidth) * children.Count + totalSpacing, itemHeight);
            }
        }

        internal static Size Arrange(Size finalSize, UIElementCollection children, double spacing, Orientation orientation)
        {
            var spaces = spacing * (children.Count - 1);
            var itemHeight = finalSize.Height;
            var itemWidth = finalSize.Width;

            var left = 0d;
            var top = 0d;

            if (orientation == Orientation.Horizontal)
            {
                itemWidth = (finalSize.Width - spaces) / children.Count;
                foreach (UIElement child in children)
                {
                    child.Arrange(new Rect(new Point(left, top), new Size(itemWidth, itemHeight)));
                    left += itemWidth + spacing;
                }
            }
            else
            {
                itemHeight = (finalSize.Height - spaces) / children.Count;
                foreach (UIElement child in children)
                {
                    child.Arrange(new Rect(new Point(left, top), new Size(itemWidth, itemHeight)));
                    top += itemHeight + spacing;
                }
            }

            return finalSize;
        }
    }
}