using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using FuryTechs.FloatingActionButton;
using FuryTechs.FloatingActionButton.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

[assembly: Xamarin.Forms.ExportRenderer(typeof(ActionMenu), typeof(ActionMenuRenderer))]
namespace FuryTechs.FloatingActionButton.Droid.Renderers
{
  public class ActionMenuRenderer : ViewRenderer<ActionMenu, LinearLayout>
  {
    public ActionMenuRenderer(Context context) : base(context)
    {
    }


    protected LinearLayout ChildrenLayout
    {
      get;
      set;
    }

    int AT_MOST = MeasureSpec.MakeMeasureSpec(LayoutParams.WrapContent, Android.Views.MeasureSpecMode.AtMost);

    protected override void OnElementChanged(ElementChangedEventArgs<ActionMenu> e)
    {
      base.OnElementChanged(e);

      if (e.OldElement != null || this.Element == null)
        return;

      if (e.OldElement != null)
        e.OldElement.PropertyChanged -= HandlePropertyChanged;


      if (this.Element != null)
      {
        this.Element.PropertyChanged += HandlePropertyChanged;
      }

      LayoutMode = Android.Views.ViewLayoutMode.OpticalBounds;

      ReCreateVisualElement();
    }

    void ReCreateVisualElement()
    {
      RemoveAllViews();
      var layout = new LinearLayout(Context);

      layout.RemoveAllViews();
      layout.Orientation = Orientation.Vertical;

      ChildrenLayout = new LinearLayout(Context)
      {
        Orientation = Orientation.Vertical
      };
      AttachActionButtons();
      layout.AddView(ChildrenLayout);

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
    }

    void AttachActionButtons()
    {
      ChildrenLayout.RemoveAllViews();
      if (Element.Contents?.Count() > 0)
      {
        for (var i = 0; i < Element.Contents.Count(); ++i)
        {
          var internalButton = Element.Contents.ElementAt(i);

          var toggleButtonRenderer = Platform.CreateRendererWithContext(internalButton, Context).View;
          if (toggleButtonRenderer is ActionButtonRenderer fab)
          {
            fab.Index = Element.Contents.Count() - i;
          }
          var layoutParams = new LinearLayout.LayoutParams(toggleButtonRenderer.Width, toggleButtonRenderer.Height);
          layoutParams.Gravity = Android.Views.GravityFlags.CenterVertical | Android.Views.GravityFlags.CenterHorizontal;
          toggleButtonRenderer.LayoutParameters = layoutParams;
          ChildrenLayout.AddView(toggleButtonRenderer);
        }
      }
      ChildrenLayout.Measure(AT_MOST, AT_MOST);
      ChildrenLayout.Layout(0, 0, ChildrenLayout.MeasuredWidth, ChildrenLayout.MeasuredHeight);
    }

    void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "Content")
      {
        Tracker.UpdateLayout();
      }
      if (e.PropertyName == ActionMenu.ContentsProperty.PropertyName)
      {
        Device.BeginInvokeOnMainThread(() =>
        {
          AttachActionButtons();
          Control.Measure(AT_MOST, AT_MOST);
          Measure(MeasureSpec.MakeMeasureSpec(Control.MeasuredWidth, Android.Views.MeasureSpecMode.Exactly), MeasureSpec.MakeMeasureSpec(Control.MeasuredHeight, Android.Views.MeasureSpecMode.Exactly));
          Element.HeightRequest = MeasuredHeight / Context.Resources.DisplayMetrics.Density;
          Element.WidthRequest = MeasuredWidth / Context.Resources.DisplayMetrics.Density;
          Tracker?.UpdateLayout();
        });
    }
  }
}
}
