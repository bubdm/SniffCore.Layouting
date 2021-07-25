//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Linq;
using System.Windows.Media;

namespace SniffCore.Layouting
{
    internal static class PathCalculator
    {
        internal static double GetPathFigureLength(PathFigure pathFigure)
        {
            if (pathFigure == null)
                return 0;

            var isAlreadyFlattened = pathFigure.Segments.All(pathSegment => pathSegment is PolyLineSegment || pathSegment is LineSegment);

            var pathFigureFlattened = isAlreadyFlattened ? pathFigure : pathFigure.GetFlattenedPathFigure();
            double length = 0;
            var pt1 = pathFigureFlattened.StartPoint;

            foreach (var pathSegment in pathFigureFlattened.Segments)
                switch (pathSegment)
                {
                    case LineSegment segment:
                    {
                        var pt2 = segment.Point;
                        length += (pt2 - pt1).Length;
                        pt1 = pt2;
                        break;
                    }
                    case PolyLineSegment lineSegment:
                    {
                        var pointCollection = lineSegment.Points;
                        foreach (var pt2 in pointCollection)
                        {
                            length += (pt2 - pt1).Length;
                            pt1 = pt2;
                        }

                        break;
                    }
                }

            return length;
        }
    }
}