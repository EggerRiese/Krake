using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AView = Android.Views.View;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Krake.CustomRenderer;
using Krake.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TransparentNavigation), typeof(TransparentNavbarRenderer))]
namespace Krake.Droid.Renderers
{
    public class TransparentNavbarRenderer : NavigationPageRenderer
    {
        public TransparentNavbarRenderer(Context context) : base(context)
        {

        }

        IPageController PageController => Element as IPageController;
        TransparentNavigation TransparentNavigation => Element as TransparentNavigation;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            TransparentNavigation.IgnoreLayoutChange = true;
            base.OnLayout(changed, l, t, r, b);
            TransparentNavigation.IgnoreLayoutChange = false;

            int containerHeight = b - t;

            PageController.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));

            for (int i = 0; i < ChildCount; i++)
            {
                AView child = GetChildAt(i);

                if(child is Android.Support.V7.Widget.Toolbar)
                {
                    continue;
                }

                child.Layout(0, 0, r, b);
            }
        }
    }
}