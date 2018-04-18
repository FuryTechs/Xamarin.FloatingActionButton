using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using FuryTechs.FloatingActionButton;
using FuryTechs.FloatingActionButton.Droid.Renderers;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(ActionMenu), typeof(ActionMenuRenderer))]
namespace FuryTechs.FloatingActionButton.Droid.Renderers
{
  public class ActionMenuRenderer : ViewRenderer<ActionMenu, FrameLayout>
  {
    public ActionMenuRenderer(Context context) : base(context)
    {
    }

    int atMost = MeasureSpec.MakeMeasureSpec(LayoutParams.WrapContent, Android.Views.MeasureSpecMode.AtMost);

    protected override void OnElementChanged(ElementChangedEventArgs<ActionMenu> e)
    {
      base.OnElementChanged(e);

      if (e.OldElement != null || this.Element == null)
        return;

      if (e.OldElement != null)
        e.OldElement.PropertyChanged -= HandlePropertyChanged;
      ;

      if (this.Element != null)
      {
        this.Element.PropertyChanged += HandlePropertyChanged;
      }

      var layout = new FrameLayout(Context);
      layout.RemoveAllViews();
      if (Element.ToggleButton != null)
      {
        var toggleButtonRenderer = Platform.CreateRendererWithContext(Element.ToggleButton, Context).View;

        var layoutParams = new FrameLayout.LayoutParams(toggleButtonRenderer.Width, toggleButtonRenderer.Height);
        layoutParams.Gravity = Android.Views.GravityFlags.CenterVertical | Android.Views.GravityFlags.CenterHorizontal;
        toggleButtonRenderer.LayoutParameters = layoutParams;
        layout.AddView(toggleButtonRenderer);
      }
      if (Element.Contents?.Count() > 0)
      {
        foreach (var internalButton in Element.Contents)
        {
          var toggleButtonRenderer = Platform.CreateRendererWithContext(internalButton, Context).View;
          var layoutParams = new FrameLayout.LayoutParams(toggleButtonRenderer.Width, toggleButtonRenderer.Height);
          layoutParams.Gravity = Android.Views.GravityFlags.CenterVertical | Android.Views.GravityFlags.CenterHorizontal;
          toggleButtonRenderer.LayoutParameters = layoutParams;
          layout.AddView(toggleButtonRenderer);
        }
      }
      layout.Measure(atMost, atMost);
      SetNativeControl(layout);
      Measure(atMost, atMost);
      Layout(0, 0, MeasuredWidth, MeasuredHeight);
    }

    void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "Content")
      {
        Tracker.UpdateLayout();
      }
    }

  }
}
