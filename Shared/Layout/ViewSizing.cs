﻿using System;
using System.Drawing;

namespace Qoden.UI
{
#if __IOS__
    using PlatformView = UIKit.UIView;
    using CoreGraphics;
    using UIKit;
#endif
#if __ANDROID__
    using ViewGroup = Android.Views.ViewGroup;
    using PlatformView = Android.Views.View;
    using static Android.Views.View;
    using Android.Views;
#endif

    public static class ViewSizing
    {
        public static SizeF PreferredSize(this PlatformView v, SizeF size)
        {
#if __IOS__
            if (v is UILabel)
            {
                var rect = ((UILabel)v).TextRectForBounds(new CGRect(0, 0, size.Width, size.Height), 0);
                return new SizeF((float)rect.Width, (float)rect.Height);
            }
            return (SizeF)v.SizeThatFits((CGSize)size);
#elif __ANDROID__
            var ws = MeasureSpec.MakeMeasureSpec((int)Math.Round(size.Width), MeasureSpecMode.AtMost);
            var hs = MeasureSpec.MakeMeasureSpec((int)Math.Round(size.Height), MeasureSpecMode.AtMost);
            v.Measure(ws, hs);
            return new SizeF(v.MeasuredWidth, v.MeasuredHeight);
#else
            return v.Frame.Size;
#endif
        }
    }
}
