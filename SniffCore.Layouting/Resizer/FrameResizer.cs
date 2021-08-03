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
    ///     Represents a single line to drag in a specific direction. This is used in the <see cref="Resizer" />.
    /// </summary>
    public class FrameResizer : Thumb
    {
        /// <summary>
        ///     Identifies the <see cref="Direction" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register("Direction", typeof(FrameResizerDirections), typeof(FrameResizer), new UIPropertyMetadata(FrameResizerDirections.LeftRight));

        /// <summary>
        ///     Identifies the <see cref="Position" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(FrameResizerPositions), typeof(FrameResizer), new UIPropertyMetadata(FrameResizerPositions.Right));

        static FrameResizer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FrameResizer), new FrameworkPropertyMetadata(typeof(FrameResizer)));
        }

        /// <summary>
        ///     Gets or sets the direction where the frame resizer can be moved to.
        /// </summary>
        [DefaultValue(FrameResizerDirections.LeftRight)]
        public FrameResizerDirections Direction
        {
            get => (FrameResizerDirections) GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        /// <summary>
        ///     Gets or sets the position of the frame resizer inside the <see cref="Resizer" /> control.
        /// </summary>
        [DefaultValue(FrameResizerPositions.Right)]
        public FrameResizerPositions Position
        {
            get => (FrameResizerPositions) GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }
    }
}