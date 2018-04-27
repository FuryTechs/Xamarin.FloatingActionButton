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
      //layout.SetBackgroundColor(Color.Fuchsia.ToAndroid());

      layout.LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
      {
        Gravity = Android.Views.GravityFlags.Bottom | Android.Views.GravityFlags.End,
        Weight = 1
      };

      ChildrenLayout = new LinearLayout(Context)
      {
        Orientation = Orientation.Vertical,
        LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
        {
          Weight = 1,
          Gravity = Android.Views.GravityFlags.Bottom | Android.Views.GravityFlags.CenterHorizontal
        }
      };

      AttachActionButtons();

      layout.AddView(ChildrenLayout);

      if (Element.ToggleButton != null)
      {
        var toggleButtonRenderer = Platform.CreateRendererWithContext(Element.ToggleButton, Context).View;
        toggleButtonRenderer.LayoutParameters = new LinearLayout.LayoutParams(toggleButtonRenderer.Width, toggleButtonRenderer.Height)
        {
          Gravity = Android.Views.GravityFlags.Bottom,
          Weight = 0
        };
        layout.AddView(toggleButtonRenderer);
      }

      SetNativeControl(layout);
      SetLayout();
    }

    void SetLayout()
    {
      Control.Measure(AT_MOST, AT_MOST);
      Control.Measure(AT_MOST,
                      MeasureSpec.MakeMeasureSpec(Control.MeasuredHeight + 200, Android.Views.MeasureSpecMode.Exactly));
      Control.Layout(0, 0, Control.MeasuredWidth, Control.MeasuredHeight);
     
      Measure(MeasureSpec.MakeMeasureSpec(Control.MeasuredWidth, Android.Views.MeasureSpecMode.Exactly),
                      MeasureSpec.MakeMeasureSpec(Control.MeasuredHeight, Android.Views.MeasureSpecMode.Exactly));

      Layout(0, 0, MeasuredWidth, MeasuredHeight);
      Element.HeightRequest = MeasuredHeight / Context.Resources.DisplayMetrics.Density;
      Element.WidthRequest = MeasuredWidth / Context.Resources.DisplayMetrics.Density;
    }

    void AttachActionButtons()
    {
      ChildrenLayout.RemoveAllViews();
      ChildrenLayout.AddView(new LinearLayout(Context)
      {
        LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
        {
          Weight = 1
        }
      });

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

          buttonRenderer.LayoutParameters = new LinearLayout.LayoutParams(buttonRenderer.Width, buttonRenderer.Height)
          {
            Gravity = Android.Views.GravityFlags.Bottom | Android.Views.GravityFlags.CenterHorizontal,
            Weight = 0
          };
          buttonRenderer.Measure(MeasureSpec.MakeMeasureSpec(buttonRenderer.Width, Android.Views.MeasureSpecMode.Exactly),
                                 MeasureSpec.MakeMeasureSpec(buttonRenderer.Height, Android.Views.MeasureSpecMode.Exactly));
          ChildrenLayout.AddView(buttonRenderer);
        }
      }
      ChildrenLayout.Measure(AT_MOST, AT_MOST);
      ChildrenLayout.Measure(AT_MOST, MeasureSpec.MakeMeasureSpec(ChildrenLayout.MeasuredHeight + (200), Android.Views.MeasureSpecMode.Exactly));
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
          SetLayout();
        });
      }
    }
  }
}
