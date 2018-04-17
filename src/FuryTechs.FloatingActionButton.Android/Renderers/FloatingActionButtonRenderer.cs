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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ActionButton), typeof(FloatingActionButtonViewRenderer))]
namespace FuryTechs.FloatingActionButton.Droid.Renderers
{
  public class FloatingActionButtonViewRenderer : ViewRenderer<ActionButton, FrameLayout>
  {

    /// <summary>
    /// Used for registration with dependency service
    /// </summary>
    public async static void Init()
    {
      var temp = await Task.FromResult(DateTime.Now);
    }

    private const int MARGIN_DIPS = 16;
    private const int FAB_HEIGHT_NORMAL = 56;
    private const int FAB_HEIGHT_MINI = 40;
    private const int FAB_FRAME_HEIGHT_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_NORMAL;
    private const int FAB_FRAME_WIDTH_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_NORMAL;
    private const int FAB_MINI_FRAME_HEIGHT_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_MINI;
    private const int FAB_MINI_FRAME_WIDTH_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_MINI;
    private readonly Android.Support.Design.Widget.FloatingActionButton fab;
    /// <summary>
    /// Construtor
    /// </summary>
    public FloatingActionButtonViewRenderer(Context ctx) : base(ctx)
    {
      float d = Context.Resources.DisplayMetrics.Density;
      var margin = (int)(MARGIN_DIPS * d); // margin in pixels

      fab = new Android.Support.Design.Widget.FloatingActionButton(Context);
      var lp = new FrameLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
      lp.Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal;

      lp.LeftMargin = margin;
      lp.TopMargin = margin;
      lp.BottomMargin = margin;
      lp.RightMargin = margin;
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
        //UpdateContent ();
        this.Element.PropertyChanged += HandlePropertyChanged;
      }

      Element.Show = Show;
      Element.Hide = Hide;

      SetFabSize(Element.Size);
      fab.BackgroundTintList = ColorStateList.ValueOf(Element.ColorNormal.ToAndroid());
      fab.RippleColor = Element.ColorRipple.ToAndroid();
      fab.Click += Fab_Click;

      var frameLayout = new FrameLayout(Context);

      frameLayout.RemoveAllViews();
      frameLayout.AddView(fab);

      if (Element.Content != null)
      {
        var content = Platform.CreateRendererWithContext(Element.Content, Context).View;
        content.LayoutParameters = fab.LayoutParameters;
        content.Layout(fab.Left, fab.Top, fab.Right, fab.Bottom);
        content.SetX(fab.GetX());
        content.SetY(fab.GetY());
        content.SetZ(fab.GetZ() + 1000);
        frameLayout.AddView(content);
      }
      SetNativeControl(frameLayout);
    }

    /// <summary>
    /// Show
    /// </summary>
    public void Show() =>
        fab?.Show();


    /// <summary>
    /// Hide!
    /// </summary>
    public void Hide() =>
        fab?.Hide();


    void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "Content")
      {
        Tracker.UpdateLayout();
      }
      else if (e.PropertyName == ActionButton.ColorNormalProperty.PropertyName)
      {
        fab.BackgroundTintList = ColorStateList.ValueOf(Element.ColorNormal.ToAndroid());
      }
      //else if (e.PropertyName == ActionButton.ColorPressedProperty.PropertyName)
      //{
      //  fab.ColorPressed = Element.ColorPressed.ToAndroid();
      //}
      else if (e.PropertyName == ActionButton.ColorRippleProperty.PropertyName)
      {
        fab.RippleColor = Element.ColorRipple.ToAndroid();
      }
      //else if (e.PropertyName == ActionButton.ImageNameProperty.PropertyName)
      //{
      //  SetFabImage(Element.ImageName);
      //}
      else if (e.PropertyName == ActionButton.SizeProperty.PropertyName)
      {
        SetFabSize(Element.Size);
      }
      //else if (e.PropertyName == ActionButton.HasShadowProperty.PropertyName)
      //{
      //  fab.HasShadow = Element.HasShadow;
      //}
    }

    void SetFabImage(string imageName)
    {
      if (!string.IsNullOrWhiteSpace(imageName))
      {
        try
        {
          var drawableNameWithoutExtension = Path.GetFileNameWithoutExtension(imageName).ToLower();
          var resources = Context.Resources;
          var imageResourceName = resources.GetIdentifier(drawableNameWithoutExtension, "drawable", Context.PackageName);

          fab.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeResource(Context.Resources, imageResourceName));
        }
        catch (Exception ex)
        {
          throw new FileNotFoundException("There was no Android Drawable by that name.", ex);
        }
      }
    }

    void SetFabSize(Size size)
    {
      if (size == Size.Mini)
      {
        fab.Size = Android.Support.Design.Widget.FloatingActionButton.SizeMini;
        Element.WidthRequest = FAB_MINI_FRAME_WIDTH_WITH_PADDING;
        Element.HeightRequest = FAB_MINI_FRAME_HEIGHT_WITH_PADDING;
      }
      else if (size == Size.Normal)
      {
        fab.Size = Android.Support.Design.Widget.FloatingActionButton.SizeNormal;
        Element.WidthRequest = FAB_FRAME_WIDTH_WITH_PADDING;
        Element.HeightRequest = FAB_FRAME_HEIGHT_WITH_PADDING;
      }
      else
      {
        fab.Size = Android.Support.Design.Widget.FloatingActionButton.SizeAuto;
        //Element.WidthRequest = 0;
        //Element.HeightRequest = 0;
      }
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