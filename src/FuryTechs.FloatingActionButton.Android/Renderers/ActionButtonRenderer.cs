using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using FuryTechs.FloatingActionButton;
using FuryTechs.FloatingActionButton.Droid.Renderers;
using FuryTechs.FloatingActionButton.Droid.Renderers.ContentTypes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ActionButton), typeof(ActionButtonRenderer))]
namespace FuryTechs.FloatingActionButton.Droid.Renderers
{
  public class ActionButtonRenderer : ViewRenderer<ActionButton, FrameLayout>
  {

    /// <summary>
    /// Used for registration with dependency service
    /// </summary>
    public async static void Init()
    {
      var temp = await Task.FromResult(DateTime.Now);
    }

    private const int MARGIN_DIPS = 8;
    private const int FAB_HEIGHT_NORMAL = 56;
    private const int FAB_HEIGHT_MINI = 40;
    private const int FAB_FRAME_HEIGHT_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_NORMAL;
    private const int FAB_FRAME_HEIGHT_WITH_PADDING_MINI = (MARGIN_DIPS * 2) + FAB_HEIGHT_MINI;
    private readonly int MARGIN;


    private readonly int AT_MOST = MeasureSpec.MakeMeasureSpec(LayoutParams.WrapContent, MeasureSpecMode.AtMost);
    private readonly Android.Support.Design.Widget.FloatingActionButton fab;

    /// <summary>
    /// Gets or sets the index.
    /// </summary>
    /// <value>The index.</value>
    public int Index
    {
      get;
      set;
    }

    /// <summary>
    /// Construtor
    /// </summary>
    public ActionButtonRenderer(Context ctx) : base(ctx)
    {
      float d = Context.Resources.DisplayMetrics.Density;
      MARGIN = (int)(MARGIN_DIPS * d); // margin in pixels

      fab = new Android.Support.Design.Widget.FloatingActionButton(Context);
      fab.Measure(AT_MOST, AT_MOST);
      var lp = new FrameLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
      lp.Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal;
      lp.SetMargins(MARGIN, MARGIN, MARGIN, MARGIN);
      fab.LayoutParameters = lp;
    }

    /// <summary>
    /// Element Changed
    /// </summary>
    /// <param name="e"></param>
    protected override void OnElementChanged(ElementChangedEventArgs<ActionButton> e)
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

      Element.Show = Show;
      Element.Hide = Hide;
      Element.Margin = MARGIN;

      if (Element.Color != Color.Transparent)
      {
        Element.Content.Color = Element.Color;
      }

      SetFabSize(Element.Size);

      fab.BackgroundTintList = ColorStateList.ValueOf(Element.ButtonColor.ToAndroid());
      fab.RippleColor = Element.ColorRipple.ToAndroid();
      fab.Click += Fab_Click;

      var frameLayout = new FrameLayout(Context);
      frameLayout.RemoveAllViews();
      frameLayout.AddView(fab);
      //frameLayout.SetBackgroundColor(Element.ButtonColor.MultiplyAlpha(.5).ToAndroid());

      if (Element.Content != null)
      {
        var content = Platform.CreateRendererWithContext(Element.Content, Context).View;

        if (content is IConvertable bitmapContent)
        {
          bitmapContent.SetSize(Element.Size);
          var bmp = bitmapContent.ToBitmap((int)Element.Width);
          var drawable = new Android.Graphics.Drawables.BitmapDrawable(Resources, bmp);
          fab.SetImageDrawable(drawable);
        }
      }
      SetNativeControl(frameLayout);
      Layout(0, 0, fab.MeasuredWidth + MARGIN * 2, fab.MeasuredHeight + MARGIN * 2);
    }

    /// <summary>
    /// Gets or sets the show easing.
    /// </summary>
    /// <value>The show easing.</value>
    public Easing ShowEasing
    {
      get;
      set;
    } = Easing.SpringOut;

    /// <summary>
    /// Gets or sets the hide easing.
    /// </summary>
    /// <value>The hide easing.</value>
    public Easing HideEasing
    {
      get;
      set;
    } = Easing.CubicInOut;

    /// <summary>
    /// Show
    /// </summary>
    public void Show(bool animate = true)
    {
      fab?.Show();
      if (animate)
      {
        Element.TranslateTo(0, 0, (uint)Math.Max(0, Element.AnimationDuration), ShowEasing);
        Element.ScaleTo(1, (uint)Math.Max(0, Element.AnimationDuration), ShowEasing);
      }
      else
      {
        Element.TranslationY = 0;
        Element.Scale = 1;
      }
      Element.IsHidden = false;
    }


    /// <summary>
    /// Hide!
    /// </summary>
    public void Hide(bool animate = true)
    {
      fab.Hide();
      if (animate)
      {
        Element.TranslateTo(0, (Height / Context.Resources.DisplayMetrics.Density) * Index, (uint)Math.Max(0, Element.AnimationDuration), HideEasing);
        Element.ScaleTo(0, (uint)Math.Max(0, Element.AnimationDuration), HideEasing);
      }
      else
      {
        Element.TranslationY = (Height / Context.Resources.DisplayMetrics.Density) * Index;
        Element.Scale = 0;
      }
      Element.IsHidden = true;
    }


    void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "Content")
      {
        Tracker.UpdateLayout();
      }
      else if (e.PropertyName == ActionButton.ButtonColorProperty.PropertyName)
      {
        fab.BackgroundTintList = ColorStateList.ValueOf(Element.BackgroundColor.ToAndroid());
      }
      else if (e.PropertyName == ActionButton.ColorProperty.PropertyName)
      {
        Element.Content.Color = Element.Color;
      }
      else if (e.PropertyName == ActionButton.ColorRippleProperty.PropertyName)
      {
        fab.RippleColor = Element.ColorRipple.ToAndroid();
      }
      else if (e.PropertyName == ActionButton.SizeProperty.PropertyName)
      {
        SetFabSize(Element.Size);
      }
    }

    void SetFabSize(Abstraction.Size size)
    {
      if (size == Abstraction.Size.Mini)
      {
        fab.Size = Android.Support.Design.Widget.FloatingActionButton.SizeMini;
        Element.WidthRequest = FAB_FRAME_HEIGHT_WITH_PADDING_MINI;
        Element.HeightRequest = FAB_FRAME_HEIGHT_WITH_PADDING_MINI;
      }
      else
      {
        fab.Size = Android.Support.Design.Widget.FloatingActionButton.SizeNormal;
        Element.WidthRequest = FAB_FRAME_HEIGHT_WITH_PADDING;
        Element.HeightRequest = FAB_FRAME_HEIGHT_WITH_PADDING;
      }
      fab.Measure(AT_MOST, AT_MOST);

      Element.Margin = MARGIN;
      Element.Layout(new Rectangle(0, 0, Element.WidthRequest, Element.HeightRequest));
    }

    void Fab_Click(object sender, EventArgs e)
    {
      Element?.Clicked?.Invoke(sender, e);

      if (Element?.Command?.CanExecute(Element?.CommandParameter) ?? false)
      {
        Element.Command.Execute(Element?.CommandParameter);
      }
    }
  }
}