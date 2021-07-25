//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

namespace SniffCore.Layouting
{
    /// <summary>
    ///     Defines where the <see cref="CornerResizer" /> in the <see cref="Resizer" /> is placed.
    /// </summary>
    public enum CornerResizerPositions
    {
        /// <summary>
        ///     The <see cref="CornerResizer" /> is placed on the top left corner.
        /// </summary>
        NW,

        /// <summary>
        ///     The <see cref="CornerResizer" /> is placed on the top right corner.
        /// </summary>
        NE,

        /// <summary>
        ///     The <see cref="CornerResizer" /> is placed on the bottom right corner.
        /// </summary>
        SE,

        /// <summary>
        ///     The <see cref="CornerResizer" /> is placed on the bottom left corner.
        /// </summary>
        SW
    }
}