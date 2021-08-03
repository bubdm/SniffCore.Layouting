//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

// ReSharper disable once CheckNamespace

namespace SniffCore.Layouting
{
    /// <summary>
    ///     Brings the possibility to resize every UI control manually by hold and drag the corners or sides.
    /// </summary>
    /// <example>
    ///     <code lang="XAML">
    /// <![CDATA[
    /// <StackPanel Orientation="Horizontal" VerticalAlignment="Top">
    ///     <layouting:Resizer FrameSizes="0,0,4,4" Margin="0,0,20,0" VerticalAlignment="Top" CornerSize="12">
    ///         <Button Content="Button" Padding="12" />
    ///     </layouting:Resizer>
    ///     <layouting:Resizer RightWidth="4" Margin="0,0,20,0" VerticalAlignment="Top">
    ///         <Button Content="Button" Padding="12" />
    ///     </layouting:Resizer>
    ///     <layouting:Resizer BottomHeight="4" VerticalAlignment="Top">
    ///         <Button Content="Button" Padding="12" />
    ///     </layouting:Resizer>
    /// </StackPanel>
    /// ]]>
    /// </code>
    /// </example>
    [TemplatePart(Name = "PART_LeftThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_LeftTopThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_TopThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_RightTopThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_RightThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_RightBottomThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_BottomThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_LeftBottomThumb", Type = typeof(Thumb))]
    public class Resizer : ContentControl
    {
        /// <summary>
        ///     Identifies the <see cref="LeftWidth" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty LeftWidthProperty =
            DependencyProperty.Register("LeftWidth", typeof(double), typeof(Resizer), new UIPropertyMetadata(0.0, OnSizeChanged));

        /// <summary>
        ///     Identifies the <see cref="TopHeight" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TopHeightProperty =
            DependencyProperty.Register("TopHeight", typeof(double), typeof(Resizer), new UIPropertyMetadata(0.0, OnSizeChanged));

        /// <summary>
        ///     Identifies the <see cref="RightWidth" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty RightWidthProperty =
            DependencyProperty.Register("RightWidth", typeof(double), typeof(Resizer), new UIPropertyMetadata(0.0, OnSizeChanged));

        /// <summary>
        ///     Identifies the <see cref="BottomHeight" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty BottomHeightProperty =
            DependencyProperty.Register("BottomHeight", typeof(double), typeof(Resizer), new UIPropertyMetadata(0.0, OnSizeChanged));

        /// <summary>
        ///     Identifies the <see cref="FrameSizes" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty FrameSizesProperty =
            DependencyProperty.Register("FrameSizes", typeof(Thickness), typeof(Resizer), new UIPropertyMetadata(new Thickness(0), OnFrameSizesChanged));

        /// <summary>
        ///     Identifies the <see cref="CornerSize" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CornerSizeProperty =
            DependencyProperty.Register("CornerSize", typeof(double), typeof(Resizer), new UIPropertyMetadata(0.0, OnSizeChanged));

        static Resizer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Resizer), new FrameworkPropertyMetadata(typeof(Resizer)));
        }

        /// <summary>
        ///     Gets or sets the width of the left frame resizer.
        /// </summary>
        [DefaultValue(4.0)]
        public double LeftWidth
        {
            get => (double) GetValue(LeftWidthProperty);
            set => SetValue(LeftWidthProperty, value);
        }

        /// <summary>
        ///     Gets or sets the height of the top frame resizer.
        /// </summary>
        [DefaultValue(4.0)]
        public double TopHeight
        {
            get => (double) GetValue(TopHeightProperty);
            set => SetValue(TopHeightProperty, value);
        }

        /// <summary>
        ///     Gets or sets the width of the right frame resizer.
        /// </summary>
        [DefaultValue(4.0)]
        public double RightWidth
        {
            get => (double) GetValue(RightWidthProperty);
            set => SetValue(RightWidthProperty, value);
        }

        /// <summary>
        ///     Gets or sets the height of the bottom frame resizer.
        /// </summary>
        [DefaultValue(4.0)]
        public double BottomHeight
        {
            get => (double) GetValue(BottomHeightProperty);
            set => SetValue(BottomHeightProperty, value);
        }

        /// <summary>
        ///     Gets or sets all frame resizer widths and heights. Left,Top,Right,Bottom.
        /// </summary>
        [DefaultValue(8)]
        public Thickness FrameSizes
        {
            get => (Thickness) GetValue(FrameSizesProperty);
            set => SetValue(FrameSizesProperty, value);
        }

        /// <summary>
        ///     Gets or sets the width and height of all corner resizers.
        /// </summary>
        [DefaultValue(16.0)]
        public double CornerSize
        {
            get => (double) GetValue(CornerSizeProperty);
            set => SetValue(CornerSizeProperty, value);
        }

        /// <summary>
        ///     The template gets added to the control.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild("PART_LeftThumb") is Thumb leftThumb)
                leftThumb.DragDelta += LeftThumb_DragDelta;

            if (GetTemplateChild("PART_LeftTopThumb") is Thumb leftTopThumb)
                leftTopThumb.DragDelta += LeftTopThumb_DragDelta;

            if (GetTemplateChild("PART_TopThumb") is Thumb topThumb)
                topThumb.DragDelta += TopThumb_DragDelta;

            if (GetTemplateChild("PART_RightTopThumb") is Thumb rightTopThumb)
                rightTopThumb.DragDelta += RightTopThumb_DragDelta;

            if (GetTemplateChild("PART_RightThumb") is Thumb rightThumb)
                rightThumb.DragDelta += RightThumb_DragDelta;

            if (GetTemplateChild("PART_RightBottomThumb") is Thumb rightBottomThumb)
                rightBottomThumb.DragDelta += RightBottomThumb_DragDelta;

            if (GetTemplateChild("PART_BottomThumb") is Thumb bottomThumb)
                bottomThumb.DragDelta += BottomThumb_DragDelta;

            if (GetTemplateChild("PART_LeftBottomThumb") is Thumb leftBottomThumb)
                leftBottomThumb.DragDelta += LeftBottomThumb_DragDelta;

            RefreshCornerVisibilities();
        }

        private double GetFinalWidth(double additionalValue)
        {
            if (double.IsNaN(Width))
                Width = ActualWidth;
            var newWidth = Width + additionalValue;
            return PrepareWidthByRange(newWidth < 0 ? 0 : newWidth);
        }

        private double PrepareWidthByRange(double p)
        {
            if (double.IsNaN(MinWidth) || p >= MinWidth)
            {
                if (double.IsNaN(MaxWidth) || p <= MaxWidth)
                    return p;
                return MaxWidth;
            }

            return MinWidth;
        }

        private double GetFinalHeight(double additionalValue)
        {
            if (double.IsNaN(Height))
                Height = ActualHeight;
            var newHeight = Height + additionalValue;
            return PrepareHeightByRange(newHeight < 0 ? 0 : newHeight);
        }

        private double PrepareHeightByRange(double p)
        {
            if (double.IsNaN(MinHeight) || p >= MinHeight)
            {
                if (double.IsNaN(MaxHeight) || p <= MaxHeight)
                    return p;
                return MaxHeight;
            }

            return MinHeight;
        }

        private void LeftThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Width = GetFinalWidth(e.HorizontalChange * -1);
        }

        private void LeftTopThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Width = GetFinalWidth(e.HorizontalChange * -1);
            Height = GetFinalHeight(e.VerticalChange * -1);
        }

        private void TopThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Height = GetFinalHeight(e.VerticalChange * -1);
        }

        private void RightTopThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Width = GetFinalWidth(e.HorizontalChange);
            Height = GetFinalHeight(e.VerticalChange * -1);
        }

        private void RightThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Width = GetFinalWidth(e.HorizontalChange);
        }

        private void RightBottomThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Width = GetFinalWidth(e.HorizontalChange);
            Height = GetFinalHeight(e.VerticalChange);
        }

        private void BottomThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Height = GetFinalHeight(e.VerticalChange);
        }

        private void LeftBottomThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Width = GetFinalWidth(e.HorizontalChange * -1);
            Height = GetFinalHeight(e.VerticalChange);
        }

        private static void OnFrameSizesChanged(DependencyObject owner, DependencyPropertyChangedEventArgs e)
        {
            var control = (Resizer) owner;
            var thickness = (Thickness) e.NewValue;
            control.LeftWidth = thickness.Left;
            control.TopHeight = thickness.Top;
            control.RightWidth = thickness.Right;
            control.BottomHeight = thickness.Bottom;
        }

        private static void OnSizeChanged(DependencyObject owner, DependencyPropertyChangedEventArgs e)
        {
            var control = (Resizer) owner;
            control.RefreshCornerVisibilities();
        }

        private void RefreshCornerVisibilities()
        {
            if (LeftWidth > 0 && TopHeight > 0 && GetTemplateChild("PART_LeftTopThumb") is Thumb leftTopThumb)
                leftTopThumb.Visibility = Visibility.Visible;

            if (TopHeight > 0 && RightWidth > 0 && GetTemplateChild("PART_RightTopThumb") is Thumb rightTopThumb)
                rightTopThumb.Visibility = Visibility.Visible;

            if (RightWidth > 0 && BottomHeight > 0 && GetTemplateChild("PART_RightBottomThumb") is Thumb rightBottomThumb)
                rightBottomThumb.Visibility = Visibility.Visible;

            if (BottomHeight > 0 && LeftWidth > 0 && GetTemplateChild("PART_LeftBottomThumb") is Thumb leftBottomThumb)
                leftBottomThumb.Visibility = Visibility.Visible;
        }
    }
}