//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

// ReSharper disable once CheckNamespace

namespace SniffCore.Layouting
{
    /// <summary>
    ///     Arranges child elements in a configurable ellipse form.
    /// </summary>
    /// <example>
    ///     <code lang="XAML">
    /// <![CDATA[
    /// <ItemsControl ItemsSource="{Binding Player}">
    ///     <ItemsControl.ItemsPanel>
    ///         <ItemsPanelTemplate>
    ///             <controls:EllipsePanel ElementStartPosition="Bottom"
    ///                                    EllipseRotateDirection="Clockwise"
    ///                                    ElementsRotateDirection="Outroversive"
    ///                                    RotateElements="True" />
    ///         </ItemsPanelTemplate>
    ///     </ItemsControl.ItemsPanel>
    /// </ItemsControl>
    /// 
    /// <controls:EllipsePanel>
    ///     <Button Content="One" />
    ///     <Button Content="Two" />
    ///     <Button Content="Three" />
    ///     <Button Content="Four" />
    /// </controls:EllipsePanel>
    /// ]]>
    /// </code>
    /// </example>
    public class EllipsePanel : Panel
    {
        /// <summary>
        ///     Identifies the RotateElements dependency property.
        /// </summary>
        public static readonly DependencyProperty RotateElementsProperty =
            DependencyProperty.Register("RotateElements", typeof(bool), typeof(EllipsePanel), new PropertyMetadata(ValueChanged));

        /// <summary>
        ///     Identifies the ElementsRotateDirection dependency property.
        /// </summary>
        public static readonly DependencyProperty ElementsRotateDirectionProperty =
            DependencyProperty.Register("ElementsRotateDirection", typeof(ElementsRotateDirection), typeof(EllipsePanel), new PropertyMetadata(ValueChanged));

        /// <summary>
        ///     Identifies the EllipseRotateDirection dependency property.
        /// </summary>
        public static readonly DependencyProperty EllipseRotateDirectionProperty =
            DependencyProperty.Register("EllipseRotateDirection", typeof(SweepDirection), typeof(EllipsePanel), new PropertyMetadata(ValueChanged));

        /// <summary>
        ///     Identifies the ElementStartPosition dependency property.
        /// </summary>
        public static readonly DependencyProperty ElementStartPositionProperty =
            DependencyProperty.Register("ElementStartPosition", typeof(ElementStartPosition), typeof(EllipsePanel), new PropertyMetadata(ValueChanged));

        private readonly EllipseGeometry _ellipse = new EllipseGeometry();

        /// <summary>
        ///     Gets or sets the indicator if the elements shall be rotated or not.
        /// </summary>
        public bool RotateElements
        {
            get => (bool) GetValue(RotateElementsProperty);
            set => SetValue(RotateElementsProperty, value);
        }

        /// <summary>
        ///     Gets or sets the value how to rotate the elements if <see cref="RotateElements" /> is true.
        /// </summary>
        public ElementsRotateDirection ElementsRotateDirection
        {
            get => (ElementsRotateDirection) GetValue(ElementsRotateDirectionProperty);
            set => SetValue(ElementsRotateDirectionProperty, value);
        }

        /// <summary>
        ///     Gets or sets the direction the elements shall be placed on the ellipse.
        /// </summary>
        public SweepDirection EllipseRotateDirection
        {
            get => (SweepDirection) GetValue(EllipseRotateDirectionProperty);
            set => SetValue(EllipseRotateDirectionProperty, value);
        }

        /// <summary>
        ///     Gets or sets the position where the first element shall start on the ellipse.
        /// </summary>
        public ElementStartPosition ElementStartPosition
        {
            get => (ElementStartPosition) GetValue(ElementStartPositionProperty);
            set => SetValue(ElementStartPositionProperty, value);
        }

        private static void ValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var control = (EllipsePanel) obj;
            control.InvalidateArrange();
        }

        /// <inheritdoc cref="FrameworkElement.MeasureOverride" />
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement child in InternalChildren)
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return base.MeasureOverride(availableSize);
        }

        /// <inheritdoc cref="FrameworkElement.ArrangeOverride" />
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (finalSize.Height <= 0 || finalSize.Width <= 0)
                throw new InvalidOperationException("The size have to be more then 0 for Width and Heigth");
            if (InternalChildren.Count > 0)
            {
                ResetEllipse(finalSize);

                var figure = _ellipse.GetOutlinedPathGeometry().Figures[0];
                var pathGeometry = new PathGeometry(new[] {figure});

                var points = new List<Point>();
                var tangents = new List<Point>();
                var distance = 1 / (double) InternalChildren.Count;
                var position = GetElementStartPositionValue();
                foreach (var _ in InternalChildren)
                {
                    pathGeometry.GetPointAtFractionLength(position, out var point, out var tangent);
                    points.Add(point);
                    tangents.Add(tangent);
                    position += distance;
                    if (position > 1)
                        position -= 1;
                }

                if (EllipseRotateDirection == SweepDirection.Counterclockwise)
                {
                    points.Reverse();
                    tangents.Reverse();
                    var point = points.Last();
                    points.Remove(point);
                    points.Insert(0, point);

                    var tangent = tangents.Last();
                    tangents.Remove(tangent);
                    tangents.Insert(0, tangent);
                }

                var pos = 0;
                foreach (UIElement child in InternalChildren)
                {
                    var childSize = child.DesiredSize;
                    var elementPos = points[pos];
                    elementPos.X -= childSize.Width / 2;
                    elementPos.Y -= childSize.Height / 2;
                    child.SnapsToDevicePixels = true;
                    child.Arrange(new Rect(elementPos, childSize));
                    if (RotateElements)
                    {
                        var elementCenter = new Size(childSize.Width / 2, childSize.Height / 2);
                        var transforms = new TransformGroup();
                        transforms.Children.Add(new RotateTransform(Math.Atan2(tangents[pos].Y, tangents[pos].X) * 180 / Math.PI + GetElementsRotateDirectionValue(), elementCenter.Width, elementCenter.Height));
                        child.RenderTransform = transforms;
                    }
                    else
                    {
                        child.RenderTransform = null;
                    }

                    ++pos;
                }
            }

            return base.ArrangeOverride(finalSize);
        }

        private double GetElementStartPositionValue()
        {
            return ElementStartPosition switch
            {
                ElementStartPosition.Left => 0.75,
                ElementStartPosition.Top => 0,
                ElementStartPosition.Right => 0.25,
                ElementStartPosition.Bottom => 0.5,
                _ => 0
            };
        }

        private double GetElementsRotateDirectionValue()
        {
            return ElementsRotateDirection switch
            {
                ElementsRotateDirection.Introversive => 0,
                ElementsRotateDirection.Outroversive => 180,
                ElementsRotateDirection.Clockwise => 90,
                ElementsRotateDirection.Counterclockwise => -90,
                _ => 0
            };
        }

        private void ResetEllipse(Size finalSize)
        {
            _ellipse.RadiusX = finalSize.Width / 2;
            _ellipse.RadiusY = finalSize.Height / 2;
            _ellipse.Center = new Point(finalSize.Width / 2, finalSize.Height / 2);
        }
    }
}