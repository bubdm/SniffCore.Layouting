//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

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
    ///         <controls:ItemsPanel DockPanel.Dock="Bottom"
    ///                              IsUniform="True"
    ///                              HorizontalAlignment="Right"
    ///                              Orientation="Horizontal"
    ///                              Spacing="10">
    ///             <Button Content="Back" />
    ///             <Button Content="Next" />
    ///             <Button Content="Finish" />
    ///             <Button Content="Cancel" />
    ///         </controls:ItemsPanel>
    /// 
    ///         <Grid />
    ///     </DockPanel>
    /// </Window>
    ///
    /// <Window>
    ///     <DockPanel>
    ///         <controls:ItemsPanel DockPanel.Dock="Bottom"
    ///                              IsUniform="False"
    ///                              HorizontalAlignment="Right"
    ///                              Orientation="Horizontal"
    ///                              Spacing="10">
    ///             <Button Content="Back" />
    ///             <Button Content="Next" />
    ///             <Button Content="Finish" />
    ///             <Button Content="Cancel" />
    ///         </controls:ItemsPanel>
    /// 
    ///         <controls:ItemsPanel Spacings="10">
    ///             <TextBox />
    ///             <TextBox />
    ///         </controls:ItemsPanel>
    ///     </DockPanel>
    /// </Window>
    /// ]]>
    /// </code>
    /// </example>
    public class ItemsPanel : Panel
    {
        /// <summary>
        ///     Identifies the Spacing dependency property.
        /// </summary>
        public static readonly DependencyProperty SpacingProperty =
            DependencyProperty.Register("Spacing", typeof(double), typeof(ItemsPanel), new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        ///     Identifies the Orientation dependency property.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ItemsPanel), new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        ///     Identifies the IsUniform dependency property.
        /// </summary>
        public static readonly DependencyProperty IsUniformProperty =
            DependencyProperty.Register("IsUniform", typeof(bool), typeof(ItemsPanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure));

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
        ///     Gets or sets the indicator if the ItemsPanel shall behave like a <see cref="UniformPanel" /> or a
        ///     <see cref="StackPanel" />.
        /// </summary>
        public bool IsUniform
        {
            get => (bool) GetValue(IsUniformProperty);
            set => SetValue(IsUniformProperty, value);
        }

        /// <summary>
        ///     Calculates the size this panel would like to get from the parent control.
        /// </summary>
        /// <param name="availableSize">The available size from the parent control.</param>
        /// <returns>The size this panel would like to get from the parent control.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            if (IsUniform)
                return UniformPanel.Measure(availableSize, Children, Spacing, Orientation, HorizontalAlignment, VerticalAlignment);
            return StackPanel.Measure(availableSize, Children, Spacing, Orientation, HorizontalAlignment, VerticalAlignment);
        }

        /// <summary>
        ///     Arranges all the children in the given space by the parent control.
        /// </summary>
        /// <param name="finalSize">The available size give from the parent control.</param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (IsUniform)
                return UniformPanel.Arrange(finalSize, Children, Spacing, Orientation);
            return StackPanel.Arrange(finalSize, Children, Spacing, Orientation);
        }
    }
}