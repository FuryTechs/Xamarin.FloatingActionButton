using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using FuryTechs.FloatingActionButton.ContentTypes;
using FuryTechs.FloatingActionButton.Droid.Renderers.ContentTypes;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(ButtonLabel), typeof(ActionButtonLabelRenderer))]
namespace FuryTechs.FloatingActionButton.Droid.Renderers.ContentTypes
{
  public class ActionButtonLabelRenderer : ViewRenderer<ButtonLabel, TextView>
  {
    public ActionButtonLabelRenderer(Context ctx) : base(ctx)
    {
    }

    protected override void OnElementChanged(ElementChangedEventArgs<ButtonLabel> e)
    {
      base.OnElementChanged(e);

      if (e.OldElement != null || this.Element == null)
        return;

      SetNativeControl(new TextView(Context)
      {
        Text = Element.Text,
        Gravity = GravityFlags.CenterHorizontal | GravityFlags.CenterVertical,
        TextSize = (float)Element.Font.FontSize,
        Typeface = Element.Font.ToTypeface(),
        TextAlignment = TextAlignment.Center,
        ForegroundTintList = ColorStateList.ValueOf(Element.Color.ToAndroid()),
      });
    }
  }
}
