using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using FuryTechs.FloatingActionButton;
using FuryTechs.FloatingActionButton.Droid.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(FloatingActionButton<>), typeof(FTFloatingActionButton))]
namespace FuryTechs.FloatingActionButton.Droid.Renderers
{
  public class FTFloatingActionButton : Android.Support.Design.Widget.FloatingActionButton
  {
    public FTFloatingActionButton(Context context) : base(context)
    {
    }

    public FTFloatingActionButton(Context context, IAttributeSet attrs) : base(context, attrs)
    {
    }

    public FTFloatingActionButton(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
    {
    }

    protected FTFloatingActionButton(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
    {
    }
  }
}