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
  public class ActionMenuRenderer : ViewRenderer<ActionMenu, LinearLayout>
  {
    public ActionMenuRenderer(Context context) : base(context)
    {
    }

    int AT_MOST = MeasureSpec.MakeMeasureSpec(LayoutParams.WrapContent, Android.Views.MeasureSpecMode.AtMost);

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

      var layout = new LinearLayout(Context);

      layout.RemoveAllViews();
      layout.Orientation = Orientation.Vertical;

      if (Element.Contents?.Count() > 0)
      {
        for (var i = 0; i < Element.Contents.Count(); ++i)
        {
          var internalButton = Element.Contents.ElementAt(i);

          var toggleButtonRenderer = Platform.CreateRendererWithContext(internalButton, Context).View;
          if (toggleButtonRenderer is FloatingActionButtonViewRenderer fab)
          {
            fab.Index = Element.Contents.Count() - i;
            if (!Element.Open)
            {
              fab.Hide();
            }
            else
            {
              fab.Show();
            }
          }
          var layoutParams = new LinearLayout.LayoutParams(toggleButtonRenderer.Width, toggleButtonRenderer.Height);
          layoutParams.Gravity = Android.Views.GravityFlags.CenterVertical | Android.Views.GravityFlags.CenterHorizontal;
          toggleButtonRenderer.LayoutParameters = layoutParams;
          layout.AddView(toggleButtonRenderer);
        }
      }
      if (Element.ToggleButton != null)
      {
        var toggleButtonRenderer = Platform.CreateRendererWithContext(Element.ToggleButton, Context).View;
        var layoutParams = new LinearLayout.LayoutParams(toggleButtonRenderer.Width, toggleButtonRenderer.Height);
        layoutParams.Gravity = Android.Views.GravityFlags.CenterVertical | Android.Views.GravityFlags.CenterHorizontal;
        toggleButtonRenderer.LayoutParameters = layoutParams;
        layout.AddView(toggleButtonRenderer);
      }
      layout.Measure(AT_MOST, AT_MOST);
      SetNativeControl(layout);
      Measure(AT_MOST, AT_MOST);
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
