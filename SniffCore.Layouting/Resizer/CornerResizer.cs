//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;

// ReSharper disable once CheckNamespace

namespace SniffCore.Layouting
{
    /// <summary>
    ///     Represents a element in the corners of the <see cref="Resizer" /> to hold and drag in a specific direction.
    /// </summary>
    public class CornerResizer : Thumb
    {
        /// <summary>
        ///     Identifies the <see cref="Direction" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register("Direction", typeof(CornerResizerDirections), typeof(CornerResizer), new UIPropertyMetadata(CornerResizerDirections.NEtoSW));

        /// <summary>
        ///     Identifies the <see cref="Position" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(CornerResizerPositions), typeof(CornerResizer), new UIPropertyMetadata(CornerResizerPositions.NW));

        static CornerResizer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerResizer), new FrameworkPropertyMetadata(typeof(CornerResizer)));
        }

        /// <summary>
        ///     Gets or sets the direction where the corner resizer can be moved.
        /// </summary>
        [DefaultValue(CornerResizerDirections.NEtoSW)]
        public CornerResizerDirections Direction
        {
            get => (CornerResizerDirections) GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        /// <summary>
        ///     Gets or sets the position of the corner resizer inside the <see cref="Resizer" /> control.
        /// </summary>
        [DefaultValue(CornerResizerPositions.NW)]
        public CornerResizerPositions Position
        {
            get => (CornerResizerPositions) GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }
    }
}