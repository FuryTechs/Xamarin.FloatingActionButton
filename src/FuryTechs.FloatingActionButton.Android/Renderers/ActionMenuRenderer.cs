using System;
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
    int exactly = MeasureSpec.MakeMeasureSpec(LayoutParams.WrapContent, Android.Views.MeasureSpecMode.AtMost);

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
        //UpdateContent ();
        this.Element.PropertyChanged += HandlePropertyChanged;
      }

      var layout = new FrameLayout(Context);
      layout.RemoveAllViews();
      if (Element.ToggleButton != null)
      {
        var toggleButtonRenderer = Platform.CreateRendererWithContext(Element.ToggleButton, Context);
        toggleButtonRenderer.View.Measure(atMost, atMost);
        var layoutParams = new FrameLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
        layoutParams.Gravity = Android.Views.GravityFlags.CenterVertical | Android.Views.GravityFlags.CenterHorizontal;
        toggleButtonRenderer.View.LayoutParameters = layoutParams;
        layout.AddView(toggleButtonRenderer.View);
      }

      layout.Measure(atMost, atMost);
      SetNativeControl(layout);
      Measure(atMost, atMost);
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
