﻿#pragma warning disable CS1701 // Assuming assembly reference matches identity

namespace Qoden.UI
{
    public static class LayoutBox_Unit
    {
        /// <summary>
        /// Set distance from <see cref="T:ILayoutBox.OuterBounds"/> left edge to view left edge.
        /// </summary>
        /// <param name="l">Distance in <see cref="T:ILayoutBox.Unit"/></param>
        public static void SetLeft(this ILayoutBox box, float l)
        {
            box.MarginLeft = box.Unit.ToFloatPixels(l);
        }
        /// <summary>
        /// Set distance from <see cref="T:ILayoutBox.OuterBounds"/> right edge to view right edge.
        /// </summary>
        /// <param name="r">Distance in <see cref="T:ILayoutBox.Unit"/></param>
        public static void SetRight(this ILayoutBox box, float r)
        {
            box.MarginRight = box.Unit.ToFloatPixels(r);
        }
        /// <summary>
        /// Set distance from <see cref="T:ILayoutBox.OuterBounds"/> top edge to view top edge.
        /// </summary>
        /// <param name="t">Distance in <see cref="T:ILayoutBox.Unit"/></param>
        public static void SetTop(this ILayoutBox box, float t)
        {
            box.MarginTop = box.Unit.ToFloatPixels(t);
        }
        /// <summary>
        /// Set distance from <see cref="T:ILayoutBox.OuterBounds"/> bottom edge to view bottom edge.
        /// </summary>
        /// <param name="b">Distance in <see cref="T:ILayoutBox.Unit"/></param>
        public static void SetBottom(this ILayoutBox box, float b)
        {
            box.MarginBottom = box.Unit.ToFloatPixels(b);
        }
        /// <summary>
        /// Set box width.
        /// </summary>
        /// <param name="w">Width in <see cref="T:ILayoutBox.Unit"/></param>
        public static void SetWidth(this ILayoutBox box, float w)
        {
            box.Width = box.Unit.ToFloatPixels(w);
        }
        /// <summary>
        /// Set box height.
        /// </summary>
        /// <param name="h">Height in <see cref="T:ILayoutBox.Unit"/></param>
        public static void SetHeight(this ILayoutBox box, float h)
        {
            box.Height = box.Unit.ToFloatPixels(h);
        }
        /// <summary>
        /// Set box center X position relative to <see cref="T:ILayoutBox.OuterBounds"/>.
        /// </summary>
        /// <param name="cx">Center x position in <see cref="T:ILayoutBox.Unit"/></param>
        public static void SetCenterX(this ILayoutBox box, float cx)
        {
            box.CenterX = box.Unit.ToFloatPixels(cx);
        }

        /// <summary>
        /// Set box center Y position relative to <see cref="T:ILayoutBox.OuterBounds"/>.
        /// </summary>
        /// <param name="cy">Center y position in <see cref="T:ILayoutBox.Unit"/></param>
        public static void SetCenterY(this ILayoutBox box, float cy)
        {
            box.CenterY = box.Unit.ToFloatPixels(cy);
        }
    }
}
