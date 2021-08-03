//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

// ReSharper disable once CheckNamespace

namespace SniffCore.Layouting
{
    /// <summary>
    ///     Defines where the <see cref="FrameResizer" /> in the <see cref="Resizer" /> is placed.
    /// </summary>
    public enum FrameResizerPositions
    {
        /// <summary>
        ///     The <see cref="FrameResizer" /> is placed on the left side.
        /// </summary>
        Left,

        /// <summary>
        ///     The <see cref="FrameResizer" /> is placed on the top.
        /// </summary>
        Top,

        /// <summary>
        ///     The <see cref="FrameResizer" /> is placed on the right side.
        /// </summary>
        Right,

        /// <summary>
        ///     The <see cref="FrameResizer" /> is placed on the bottom.
        /// </summary>
        Bottom
    }
}