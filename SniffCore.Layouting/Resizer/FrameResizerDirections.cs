//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

// ReSharper disable once CheckNamespace

namespace SniffCore.Layouting
{
    /// <summary>
    ///     Represents the direction where the <see cref="FrameResizer" /> in the <see cref="Resizer" /> can be moved.
    /// </summary>
    public enum FrameResizerDirections
    {
        /// <summary>
        ///     The <see cref="FrameResizer" /> can be moved from the top to the bottom and back.
        /// </summary>
        TopBottom,

        /// <summary>
        ///     The <see cref="FrameResizer" /> can be moved from the left to the right and back.
        /// </summary>
        LeftRight
    }
}