using System;
using Android.Content;
using Android.Widget;
using FuryTechs.FloatingActionButton.ContentTypes;
using Xamarin.Forms.Platform.Android;

namespace FuryTechs.FloatingActionButton.Droid.Renderers.ContentTypes
{
  public class FontAweseomeIconRenderer : ViewRenderer<FontAwesomeIcon, FrameLayout>
  {
    protected FontAweseomeIconRenderer(Context context) : base(context)
    {
    }

		protected override void OnElementChanged(ElementChangedEventArgs<FontAwesomeIcon> e)
		{
      base.OnElementChanged(e);
      if(e.OldElement==null) {
        
      }
		}
	}
}
