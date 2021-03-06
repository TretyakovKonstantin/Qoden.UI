﻿using System;
using Android.App;
using Android.Graphics;
using Example.Model;
using Qoden.UI;

namespace Example
{
    [Application]
    public class ExampleApplication : Application
    {
        public ExampleApplication(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            var dbFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            ExampleApp.Init(dbFolder).ContinueWith(x => {});

            var icons = Typeface.CreateFromAsset(Assets, "fonts/proveo_ios.ttf");
            TypefaceCollection.Add("proveo_ios", FontStyle.Normal, icons);
        }
    }
}
