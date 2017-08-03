using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Com.Github.Aakira.Expandablelayout;

namespace ExpandableLayoutQs
{
    [Activity(Label = "ExampleReadMoreActivity", MainLauncher =true, Theme = "@style/AppTheme")]
    public class ExampleReadMoreActivity : AppCompatActivity
    {

        public static void startActivity(Context context)
        {
            context.StartActivity(new Intent(context, typeof(ExampleReadMoreActivity)));

        }

        private Button mExpandButton;
        private ExpandableRelativeLayout mExpandLayout;
        private TextView mOverlayText;
        private ViewTreeObserver.IOnGlobalLayoutListener mGlobalLayoutListener;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_example_read_more);

            SupportActionBar.Title = "more jump from Naxam";

            mExpandButton = (Button)FindViewById(Resource.Id.expandButton);
            mExpandLayout = (ExpandableRelativeLayout)FindViewById(Resource.Id.expandableLayout);
            mOverlayText = (TextView)FindViewById(Resource.Id.overlayText);
            mExpandButton.Click += (s, e) =>
            {
                mExpandLayout.Expand();
                mExpandButton.Visibility=ViewStates.Gone;
                mOverlayText.Visibility = ViewStates.Gone;
            };
            mGlobalLayoutListener = new myViewTree(mExpandLayout, mOverlayText);

            mOverlayText.ViewTreeObserver.AddOnGlobalLayoutListener(mGlobalLayoutListener);
        }

        private class myViewTree : Java.Lang.Object, ViewTreeObserver.IOnGlobalLayoutListener
        {
            private ExpandableRelativeLayout mExpandLayout;
            private TextView mOverlayText;
            public myViewTree(ExpandableRelativeLayout mExpandLayout, TextView mOverlayText)
            {
                this.mExpandLayout = mExpandLayout;
                this.mOverlayText = mOverlayText;
            }


            public void OnGlobalLayout()
            {
                mExpandLayout.Move(mOverlayText.Height, 0, null);

                if (Build.VERSION.SdkInt < Build.VERSION_CODES.JellyBean)
                {
                    mOverlayText.ViewTreeObserver.RemoveGlobalOnLayoutListener(this);
                }
                else
                {
                    mOverlayText.ViewTreeObserver.RemoveOnGlobalLayoutListener(this);
                }

            }
        }
    }
}