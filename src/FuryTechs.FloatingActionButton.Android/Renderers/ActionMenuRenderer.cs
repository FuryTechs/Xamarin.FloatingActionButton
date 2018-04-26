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

      CreateVisualElements();
    }

    void CreateVisualElements()
    {
      RemoveAllViews();
      var layout = new LinearLayout(Context);
      layout.RemoveAllViews();
      layout.Orientation = Orientation.Vertical;

      var lpar = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
      lpar.Gravity = Android.Views.GravityFlags.Bottom | Android.Views.GravityFlags.CenterHorizontal;
      layout.LayoutParameters = lpar;

      ChildrenLayout = new LinearLayout(Context)
      {
        Orientation = Orientation.Vertical,
        LayoutParameters = lpar
      };

      AttachActionButtons();

      layout.AddView(ChildrenLayout);

      if (Element.ToggleButton != null)
      {
        var toggleButtonRenderer = Platform.CreateRendererWithContext(Element.ToggleButton, Context).View;
        var layoutParams = new LinearLayout.LayoutParams(toggleButtonRenderer.Width, toggleButtonRenderer.Height);
        toggleButtonRenderer.LayoutParameters = layoutParams;
        layout.AddView(toggleButtonRenderer);
      }

      layout.Measure(AT_MOST, AT_MOST);
      Measure(AT_MOST, AT_MOST);
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

          var buttonRenderer = Platform.CreateRendererWithContext(internalButton, Context).View;
          if (buttonRenderer is ActionButtonRenderer fab)
          {
            fab.Index = Element.Contents.Count() - i;
          }
          var layoutParams = new LinearLayout.LayoutParams(buttonRenderer.Width, buttonRenderer.Height);
          layoutParams.Gravity = Android.Views.GravityFlags.CenterHorizontal | Android.Views.GravityFlags.Bottom;
          buttonRenderer.LayoutParameters = layoutParams;
          ChildrenLayout.AddView(buttonRenderer);
        }
      }
      ChildrenLayout.Measure(AT_MOST, AT_MOST);
      ChildrenLayout.Measure(AT_MOST, MeasureSpec.MakeMeasureSpec(ChildrenLayout.MeasuredHeight + 100, Android.Views.MeasureSpecMode.Exactly));
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
          Measure(MeasureSpec.MakeMeasureSpec(Control.MeasuredWidth, Android.Views.MeasureSpecMode.Exactly), MeasureSpec.MakeMeasureSpec(Control.MeasuredHeight + 100, Android.Views.MeasureSpecMode.Exactly));
          Element.HeightRequest = MeasuredHeight / Context.Resources.DisplayMetrics.Density;
          Element.WidthRequest = MeasuredWidth / Context.Resources.DisplayMetrics.Density;
        });
      }
    }
  }
}
