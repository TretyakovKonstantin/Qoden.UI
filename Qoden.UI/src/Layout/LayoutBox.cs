using System;
using System.Drawing;
#pragma warning disable CS1701 // Assuming assembly reference matches identity

namespace Qoden.UI
{
    /// <summary>
    /// Contains layout parameters relative to provided rectangle.
    /// </summary>
    public interface ILayoutBox
    {
        /// <summary>
        /// Measurement unit for relative offsets
        /// </summary>
        IUnit Unit { get; }

        void SetLeft(Pixel l);

        void SetLeft(float l);

        void SetRight(Pixel r);

        void SetRight(float r);

        void SetTop(Pixel t);

        void SetTop(float t);

        void SetBottom(Pixel b);

        void SetBottom(float b);

        void SetWidth(Pixel w);

        void SetWidth(float w);

        void SetHeight(Pixel h);

        void SetHeight(float h);

        void SetCenterX(Pixel cx);

        void SetCenterX(float cx);

        void SetCenterY(Pixel cy);

        void SetCenterY(float cy);

        /// <summary>
        /// Caculated layout width in pixels
        /// </summary>
        float LayoutWidth { get; }

        /// <summary>
        /// Calculated layout height in pixels
        /// </summary>
        float LayoutHeight { get; }

        /// <summary>
        /// Calculated layout left position in view coordinates in pixels
        /// </summary>
        float LayoutLeft { get; }

        /// <summary>
        /// Calculated layout right position in view coordinates in pixels
        /// </summary>
        float LayoutRight { get; }

        /// <summary>
        /// Caculated layout top position in view coordinates in pixels
        /// </summary>
        float LayoutTop { get; }

        /// <summary>
        /// Calculated layout bottom position in view coordinates in pixels
        /// </summary>
        float LayoutBottom { get; }

        /// <summary>
        /// Caclulated layout center in view coordinates in pixels
        /// </summary>
        PointF LayoutCenter { get; }

        /// <summary>
        /// Caclualted layout size in pixels
        /// </summary>
        SizeF LayoutSize { get; }

        /// <summary>
        /// Calculated layout bounds in view coordinate system in pixels
        /// </summary>
        RectangleF LayoutBounds { get; }

        /// <summary>
        /// Outer bounds in view coordinate system in pixels
        /// </summary>
        RectangleF Bounds { get; }
    }

    public class LayoutBox : ILayoutBox
    {
        RectangleF bounds;
        IUnit unit = IdentityUnit.Identity;

        float left, right, top, bottom, width, height, centerX, centerY;
        const float NOT_SET = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Qoden.UI.LayoutBox"/> class.
        /// </summary>
        /// <param name="outerBounds">Layout bounds in view coordinate system in pixels</param>
        public LayoutBox(RectangleF outerBounds) : this(outerBounds, IdentityUnit.Identity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Qoden.UI.LayoutBox"/> class.
        /// </summary>
        /// <param name="outerBounds">Layout bounds in view coordinate system in pixels</param>
        /// <param name="unit">Unit to be used for relative offsets</param>
        public LayoutBox(RectangleF outerBounds, IUnit unit)
        {
            this.bounds = outerBounds;
            left = right = top = bottom = width = height = centerX = centerY = NOT_SET;
            this.unit = unit ?? IdentityUnit.Identity;
        }

        /// <summary>
        /// Measurement unit for relative offsets
        /// </summary>
        public IUnit Unit => unit;

        static bool IsSet(float val)
        {
            return Math.Abs(val - NOT_SET) > float.Epsilon;
        }

        public void SetLeft(Pixel l)
        {
            if (IsSet(centerX) && IsSet(width))
                centerX = NOT_SET;
            if (IsSet(right) && IsSet(width))
                width = NOT_SET;
            left = l.Value;
        }

        public void SetLeft(float l)
        {
            SetLeft(unit.ToPixels(l));
        }

        public void SetRight(Pixel r)
        {
            if (IsSet(centerX) && IsSet(width))
                centerX = NOT_SET;
            if (IsSet(left) && IsSet(width))
                width = NOT_SET;
            right = r.Value;
        }

        public void SetRight(float r)
        {
            SetRight(unit.ToPixels(r));
        }

        public void SetTop(Pixel t)
        {
            if (IsSet(centerY) && IsSet(height))
                centerY = NOT_SET;
            if (IsSet(bottom) && IsSet(height))
                height = NOT_SET;
            top = t.Value;
        }

        public void SetTop(float t)
        {
            SetTop(unit.ToPixels(t));
        }

        public void SetBottom(Pixel b)
        {
            if (IsSet(centerY) && IsSet(height))
                centerY = NOT_SET;
            if (IsSet(top) && IsSet(height))
                height = NOT_SET;
            bottom = b.Value;
        }

        public void SetBottom(float b)
        {
            SetBottom(unit.ToPixels(b));
        }

        public void SetWidth(Pixel w)
        {
            if (IsSet(left) && IsSet(right))
            {
                left = NOT_SET;
            }
            if (IsSet(centerX) && IsSet(left))
                centerX = NOT_SET;
            if (IsSet(centerX) && IsSet(right))
                centerX = NOT_SET;
            width = w.Value;
        }

        public void SetWidth(float w)
        {
            SetWidth(unit.ToPixels(w));
        }

        public void SetHeight(Pixel h)
        {
            if (IsSet(top) && IsSet(bottom))
            {
                top = NOT_SET;
            }
            if (IsSet(centerY) && IsSet(top))
                centerY = NOT_SET;
            if (IsSet(centerY) && IsSet(bottom))
                centerY = NOT_SET;
            height = h.Value;
        }

        public void SetHeight(float h)
        {
            SetHeight(unit.ToPixels(h));
        }

        public void SetCenterX(Pixel cx)
        {
            if (IsSet(left) && IsSet(right))
            {
                width = right - left;
                left = right = NOT_SET;
            }
            if (IsSet(left) && IsSet(width))
                width = NOT_SET;
            if (IsSet(right) && IsSet(width))
                width = NOT_SET;
            centerX = cx.Value;
        }

        public void SetCenterX(float cx)
        {
            SetCenterX(unit.ToPixels(cx));
        }

        public void SetCenterY(Pixel cy)
        {
            if (IsSet(top) && IsSet(bottom))
            {
                height = bounds.Height - bottom - top;
                top = bottom = NOT_SET;
            }
            if (IsSet(top) && IsSet(height))
                height = NOT_SET;
            if (IsSet(bottom) && IsSet(height))
                height = NOT_SET;

            centerY = cy.Value;
        }

        public void SetCenterY(float cy)
        {
            SetCenterY(unit.ToPixels(cy));
        }

        public float LayoutWidth
        {
            get
            {
                if (IsSet(width))
                    return width;
                if (IsSet(left) && IsSet(right))
                    return bounds.Width - right - left;
                if (IsSet(centerX) && IsSet(left))
                    return (centerX - left) * 2;
                if (IsSet(centerX) && IsSet(right))
                    return (centerX - right) * 2;
                return 0;
            }
        }

        public float LayoutHeight
        {
            get
            {
                if (IsSet(height))
                    return height;
                if (IsSet(top) && IsSet(bottom))
                    return bounds.Height - bottom - top;
                if (IsSet(centerY) && IsSet(top))
                    return (centerY - top) * 2;
                if (IsSet(centerY) && IsSet(bottom))
                    return (centerY - bottom) * 2;
                return 0;
            }
        }

        public float LayoutLeft
        {
            get
            {
                if (IsSet(left))
                    return bounds.Left + left;
                if (IsSet(centerX) && IsSet(width))
                    return bounds.Left + centerX - width / 2;
                if (IsSet(right) && IsSet(width))
                    return bounds.Right - right - width;
                return 0;
            }
        }

        public float LayoutRight
        {
            get
            {
                if (IsSet(right))
                    return bounds.Right - right;
                if (IsSet(centerX) && IsSet(width))
                    return bounds.Left + centerX + width / 2;
                if (IsSet(left) && IsSet(width))
                    return bounds.Left + left + width;
                return 0;
            }
        }

        public float LayoutTop
        {
            get
            {
                if (IsSet(top))
                    return bounds.Top + top;
                if (IsSet(centerY) && IsSet(height))
                    return bounds.Top + centerY - height / 2;
                if (IsSet(bottom) && IsSet(height))
                    return bounds.Bottom - bottom - height;
                return 0;
            }
        }

        public float LayoutBottom
        {
            get
            {
                if (IsSet(bottom))
                    return bounds.Bottom - bottom;
                if (IsSet(centerY) && IsSet(height))
                    return bounds.Top + centerY + height / 2;
                if (IsSet(top) && IsSet(height))
                {
                    return bounds.Top + top + height;
                }
                return NOT_SET;
            }
        }

        public PointF LayoutCenter
        {
            get { return new PointF(LayoutLeft + LayoutWidth / 2, LayoutTop + LayoutHeight / 2); }
        }

        public SizeF LayoutSize
        {
            get { return new SizeF(LayoutWidth, LayoutHeight); }
        }

        public RectangleF LayoutBounds
        {
            get
            {
                return new RectangleF(LayoutLeft, LayoutTop, LayoutWidth, LayoutHeight);
            }
        }

        public RectangleF Bounds => bounds;
    }

    public static class LayoutBoxCenter
    {
        public static T CenterHorizontally<T>(this T box, Pixel dx) where T : ILayoutBox
        {
            box.SetCenterX(Pixel.Val(box.Bounds.Left + box.Bounds.Width / 2 + dx.Value));
            return box;
        }

        public static T CenterHorizontally<T>(this T box, float dx = 0) where T : ILayoutBox
        {
            return box.CenterHorizontally(box.Unit.ToPixels(dx));
        }

        public static T CenterVertically<T>(this T box, Pixel dx) where T : ILayoutBox
        {
            box.SetCenterY(Pixel.Val(box.Bounds.Top + box.Bounds.Height / 2 + dx.Value));
            return box;
        }

        public static T CenterVertically<T>(this T box, float dx = 0) where T : ILayoutBox
        {
            return box.CenterVertically(box.Unit.ToPixels(dx));
        }
    }

    public static class LayoutBoxRectangle
    {
        public static T Width<T>(this T box, float w) where T : ILayoutBox
        {
            box.SetWidth(w);
            return box;
        }

        public static T Width<T>(this T box, Pixel w) where T : ILayoutBox
        {
            box.SetWidth(w);
            return box;
        }

        public static T Height<T>(this T box, float h) where T : ILayoutBox
        {
            box.SetHeight(h);
            return box;
        }

        public static T Height<T>(this T box, Pixel h) where T : ILayoutBox
        {
            box.SetHeight(h);
            return box;
        }

        public static T Left<T>(this T box, float l) where T : ILayoutBox
        {
            box.SetLeft(l);
            return box;
        }

        public static T Left<T>(this T box, Pixel l) where T : ILayoutBox
        {
            box.SetLeft(l);
            return box;
        }

        public static T Right<T>(this T box, float r) where T : ILayoutBox
        {
            box.SetRight(r);
            return box;
        }

        public static T Right<T>(this T box, Pixel r) where T : ILayoutBox
        {
            box.SetRight(r);
            return box;
        }

        public static T Top<T>(this T box, float t) where T : ILayoutBox
        {
            box.SetTop(t);
            return box;
        }

        public static T Top<T>(this T box, Pixel t) where T : ILayoutBox
        {
            box.SetTop(t);
            return box;
        }

        public static T Bottom<T>(this T box, float b) where T : ILayoutBox
        {
            box.SetBottom(b);
            return box;
        }

        public static T Bottom<T>(this T box, Pixel b) where T : ILayoutBox
        {
            box.SetBottom(b);
            return box;
        }
    }

    public static class LayoutBoxRelative
    {
        /// <summary>
        /// Place this box before provided reference box. Reference is in view coordinate system.
        /// </summary>
        public static T Before<T>(this T box, RectangleF reference, Pixel dx) where T : ILayoutBox
        {
            var referenceOffset = box.Bounds.Right - reference.Left;
            box.SetRight(Pixel.Val(referenceOffset + dx.Value));
            return box;
        }

        /// <summary>
        /// Place this box before provided reference box. Reference is in view coordinate system.
        /// </summary>
        public static T Before<T>(this T box, RectangleF reference, float dx = 0) where T : ILayoutBox
        {
            return box.Before(reference, box.Unit.ToPixels(dx));
        }

        /// <summary>
        /// Place this box after provided reference box. Reference is in view coordinate system.
        /// </summary>
        public static T After<T>(this T box, RectangleF reference, Pixel dx) where T : ILayoutBox
        {
            var referenceOffset = reference.Right - box.Bounds.Left;
            box.SetLeft(Pixel.Val(referenceOffset + dx.Value));
            return box;
        }
        /// <summary>
        /// Place this box after provided reference box. Reference is in view coordinate system.
        /// </summary>
        public static T After<T>(this T box, RectangleF reference, float dx = 0) where T : ILayoutBox
        {
            return box.After(reference, box.Unit.ToPixels(dx));
        }

        /// <summary>
        /// Place this box below provided reference box. Reference is in view coordinate system.
        /// </summary>
        public static T Below<T>(this T box, RectangleF reference, Pixel dx) where T : ILayoutBox
        {
            var referenceOffset = reference.Bottom - box.Bounds.Top;
            box.SetTop(Pixel.Val(referenceOffset + dx.Value));
            return box;
        }

        /// <summary>
        /// Place this box below provided reference box. Reference is in view coordinate system.
        /// </summary>
        public static T Below<T>(this T box, RectangleF reference, float dx = 0) where T : ILayoutBox
        {
            return box.Below(reference, box.Unit.ToPixels(dx));
        }
        /// <summary>
        /// Place this box above provided reference box. Reference is in view coordinate system.
        /// </summary>
        public static T Above<T>(this T box, RectangleF reference, Pixel dx) where T : ILayoutBox
        {
            var referenceOffset = box.Bounds.Bottom - reference.Top;
            box.SetBottom(Pixel.Val(referenceOffset + dx.Value));
            return box;
        }
        /// <summary>
        /// Place this box above provided reference box. Reference is in view coordinate system.
        /// </summary>
        public static T Above<T>(this T box, RectangleF reference, float dx = 0) where T : ILayoutBox
        {
            return box.Above(reference, box.Unit.ToPixels(dx));
        }
    }

    public static class LayoutBoxMinMax
    {
        public static T MinWidth<T>(this T box, float mw) where T : ILayoutBox
        {
            return box.MinWidth(box.Unit.ToPixels(mw));
        }

        public static T MinHeight<T>(this T box, float mh) where T : ILayoutBox
        {
            return box.MinHeight(box.Unit.ToPixels(mh));
        }

        public static T MinWidth<T>(this T box, Pixel mw) where T : ILayoutBox
        {
            if (box.LayoutWidth < mw.Value)
            {
                box.SetWidth(mw);
            }
            return box;
        }

        public static T MinHeight<T>(this T box, Pixel mh) where T : ILayoutBox
        {
            if (box.LayoutHeight < mh.Value)
            {
                box.SetHeight(mh);
            }

            return box;
        }

        public static T MaxWidth<T>(this T box, float mw) where T : ILayoutBox
        {
            return box.MaxWidth(box.Unit.ToPixels(mw));
        }

        public static T MaxHeight<T>(this T box, float mh) where T : ILayoutBox
        {
            return box.MaxHeight(box.Unit.ToPixels(mh));
        }

        public static T MaxWidth<T>(this T box, Pixel mw) where T : ILayoutBox
        {
            if (box.LayoutWidth > mw.Value)
            {
                box.SetWidth(mw);
            }
            return box;
        }

        public static T MaxHeight<T>(this T box, Pixel mh) where T : ILayoutBox
        {
            if (box.LayoutHeight > mh.Value)
            {
                box.SetHeight(mh);
            }

            return box;
        }
    }
}

