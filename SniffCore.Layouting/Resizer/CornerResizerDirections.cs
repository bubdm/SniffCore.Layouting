//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

// ReSharper disable once CheckNamespace

namespace SniffCore.Layouting
{
    /// <summary>
    ///     Represents the direction where the <see cref="CornerResizer" /> in the <see cref="Resizer" /> can be moved.
    /// </summary>
    public enum CornerResizerDirections
    {
        /// <summary>
        ///     The <see cref="CornerResizer" /> can be moved from the north west to south east and back.
        /// </summary>
        NWtoSE,

        /// <summary>
        ///     The <see cref="CornerResizer" /> can be moved from the north east to the south west and back.
        /// </summary>
        NEtoSW
    }
}