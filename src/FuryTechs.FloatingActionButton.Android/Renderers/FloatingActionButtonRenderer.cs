using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

      //SetFabImage(Element.ImageName);
      SetFabSize(Element.Size);

      fab.SetBackgroundColor(Element.ColorPressed.ToAndroid());
      fab.RippleColor = Element.ColorRipple.ToAndroid();
      
      fab.Click += Fab_Click;

      var frame = new FrameLayout(Context);
      frame.RemoveAllViews();
      frame.AddView(fab);

      SetNativeControl(frame);
    }

    /// <summary>
    /// Show
    /// </summary>
    /// <param name="animate"></param>
    public void Show() =>
        fab?.Show();


    /// <summary>
    /// Hide!
    /// </summary>
    /// <param name="animate"></param>
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
        fab.SetBackgroundColor(Element.ColorNormal.ToAndroid());
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

    void SetFabSize(int size)
    {
      //if (size == FloatingActionButtonSize.Mini)
      //{
        fab.Size = size;
        //Element.WidthRequest = FAB_MINI_FRAME_WIDTH_WITH_PADDING;
        //Element.HeightRequest = FAB_MINI_FRAME_HEIGHT_WITH_PADDING;
      //}
      //else
      //{
      //  fab.Size = size;
        Element.WidthRequest = FAB_FRAME_WIDTH_WITH_PADDING;
        Element.HeightRequest = FAB_FRAME_HEIGHT_WITH_PADDING;
      //}
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