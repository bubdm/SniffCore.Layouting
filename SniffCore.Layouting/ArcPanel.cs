//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SniffCore.Layouting
{
    /// <summary>
    ///     Arranges child elements in a configurable arc form.
    /// </summary>
    /// <example>
    ///     <code lang="XAML">
    /// <![CDATA[
    /// <ItemsControl ItemsSource="{Binding Cards}">
    ///     <ItemsControl.ItemsPanel>
    ///         <ItemsPanelTemplate>
    ///             <controls:ArcPanel Width="210" Height="100" Bend="3" />
    ///         </ItemsPanelTemplate>
    ///     </ItemsControl.ItemsPanel>
    /// </ItemsControl>
    /// 
    /// <controls:ArcPanel Width="300" Height="150" Bend="2">
    ///     <Button Content="First" />
    ///     <Button Content="Second" />
    ///     <Button Content="Third" />
    /// </controls:ArcPanel>
    /// ]]>
    /// </code>
    /// </example>
    public class ArcPanel : Panel
    {
        /// <summary>
        ///     Identifies the RotateElements dependency property.
        /// </summary>
        public static readonly DependencyProperty RotateElementsProperty =
            DependencyProperty.Register("RotateElements", typeof(bool), typeof(ArcPanel), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsArrange));

        /// <summary>
        ///     Identifies the Bend dependency property.
        /// </summary>
        public static readonly DependencyProperty BendProperty =
            DependencyProperty.Register("Bend", typeof(double), typeof(ArcPanel), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsArrange));

        private readonly PathFigure _figure = new PathFigure();

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArcPanel" /> class.
        /// </summary>
        public ArcPanel()
        {
            _figure.StartPoint = new Point(0, 10);
            _figure.Segments.Add(new ArcSegment(new Point(200, 10), new Size(300, 150), 0, false, SweepDirection.Clockwise, false));
        }

        /// <summary>
        ///     Gets or sets a value that defines if the child elements has to be rotated by the arc line.
        /// </summary>
        [DefaultValue(true)]
        public bool RotateElements
        {
            get => (bool) GetValue(RotateElementsProperty);
            set => SetValue(RotateElementsProperty, value);
        }

        /// <summary>
        ///     Gets or sets a value that defines how strong the arc line should be bended.
        /// </summary>
        [DefaultValue(1.0)]
        public double Bend
        {
            get => (double) GetValue(BendProperty);
            set => SetValue(BendProperty, value);
        }

        /// <summary>
        ///     Lets each child calculating is needed size.
        /// </summary>
        /// <param name="availableSize">The available space by the parent control.</param>
        /// <returns>The calculated size needed for the control.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement child in InternalChildren)
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return base.MeasureOverride(availableSize);
        }

        /// <summary>
        ///     Positions each child in an arc line form depending on the Bend and amount of child controls.
        /// </summary>
        /// <param name="finalSize">The maximum possible space given by the parent control.</param>
        /// <returns>The calculated needed space in sum of all available child controls.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (InternalChildren.Count <= 0 || !(finalSize.Width > 0))
                return base.ArrangeOverride(finalSize);

            ResetArc(finalSize);

            var pathGeometry = new PathGeometry(new[] {_figure});
            var distance = 1 / (double) (InternalChildren.Count + 1);
            var position = distance;
            foreach (UIElement child in InternalChildren)
            {
                pathGeometry.GetPointAtFractionLength(position, out var point, out var tangent);

                var childSize = child.DesiredSize;
                point.X -= childSize.Width / 2;
                point.Y -= childSize.Height;
                child.Arrange(new Rect(point, childSize));

                if (RotateElements)
                {
                    var elementCenter = new Size(childSize.Width / 2, childSize.Height / 2);
                    var transforms = new TransformGroup();
                    transforms.Children.Add(new RotateTransform(Math.Atan2(tangent.Y, tangent.X) * 180 / Math.PI, elementCenter.Width, elementCenter.Height));
                    child.RenderTransform = transforms;
                }
                else
                {
                    child.RenderTransform = null;
                }

                position += distance;
            }

            return base.ArrangeOverride(finalSize);
        }

        private void ResetArc(Size finalSize)
        {
            var segment = (ArcSegment) _figure.Segments[0];
            _figure.StartPoint = new Point(0, finalSize.Height);
            segment.Point = new Point(finalSize.Width, finalSize.Height);
            segment.Size = new Size(finalSize.Width, finalSize.Height * Bend);
        }
    }
}