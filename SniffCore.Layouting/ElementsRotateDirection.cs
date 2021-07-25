//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

namespace SniffCore.Layouting
{
    /// <summary>
    ///     Defines how the items should be rotated in the <see cref="EllipsePanel" />.
    /// </summary>
    public enum ElementsRotateDirection
    {
        /// <summary>
        ///     The top of the items are oriented to the ellipse panel center point.
        /// </summary>
        Introversive,

        /// <summary>
        ///     The bottom of the items are oriented to the ellipse panel center point.
        /// </summary>
        Outroversive,

        /// <summary>
        ///     The items are oriented with the ellipse form clockwise.
        /// </summary>
        Clockwise,

        /// <summary>
        ///     The items are oriented with the ellipse form counter clockwise.
        /// </summary>
        Counterclockwise
    }
}