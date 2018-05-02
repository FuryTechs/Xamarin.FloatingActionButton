using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FuryTechs.FloatingActionButton
{
  [ContentProperty(nameof(Contents))]
  public class ActionMenu : View
  {
    /// <summary>
    /// The default toggle button.
    /// </summary>
    public static readonly ActionButton DEFAULT_TOGGLE_BUTTON = new ActionButton()
    {
      ButtonColor = Color.Red,
      Color = Color.NavajoWhite,
      Content = new FontAwesomeIcon()
      {
        Icon = FAIcons.FAPlus
      }
    };


    /// <summary>
    /// Initializes a new instance of the <see cref="T:FuryTechs.FloatingActionButton.ActionMenu"/> class.
    /// </summary>
    public ActionMenu()
    {
      Contents = new ObservableCollection<ActionButton>();
      //BindingContext = new ActionMenuViewModel();
    }

    /// <summary>
    /// The contents property.
    /// </summary>
    public static readonly BindableProperty ContentsProperty =
      BindableProperty.Create(nameof(Contents),
                              typeof(ObservableCollection<ActionButton>),
                              typeof(ActionMenu),
                              propertyChanged: (m, oldColl, newColl) => (m as ActionMenu).ContentsChanged(oldColl as ObservableCollection<ActionButton>, newColl as ObservableCollection<ActionButton>)
                             );


    private void ContentsChanged(ObservableCollection<ActionButton> oldCollection, ObservableCollection<ActionButton> newCollection)
    {
      OnPropertyChanged(nameof(Contents));
      if (oldCollection != null)
        oldCollection.CollectionChanged -= CollectionChanged;

      if (newCollection != null)
      {
        newCollection.CollectionChanged += CollectionChanged;
        if (!Open)
        {
          foreach (var e in newCollection)
          {
            e.Hide?.Invoke();
          }
        }
      }
    }

    void CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      if (!Open)
      {
        foreach (var item in e.NewItems)
        {
          (item as ActionButton).Hide?.Invoke();
        }
      }
      OnPropertyChanged(nameof(Contents));
    }



    /// <summary>
    /// Gets or sets the contents.
    /// </summary>
    /// <value>The contents.</value>
    public ICollection<ActionButton> Contents
    {
      get
      {
        return (ICollection<ActionButton>)GetValue(ContentsProperty);
      }
      set
      {
        SetValue(ContentsProperty, value);
      }
    }

    /// <summary>
    /// The open property.
    /// </summary>
    public static readonly BindableProperty OpenProperty =
      BindableProperty.Create(nameof(Open),
                              typeof(bool),
                              typeof(ActionMenu),
                              false);

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:FuryTechs.FloatingActionButton.ActionMenu"/> is open.
    /// </summary>
    /// <value><c>true</c> if open; otherwise, <c>false</c>.</value>
    public bool Open
    {
      get
      {
        return (bool)GetValue(OpenProperty);
      }
      set
      {
        SetValue(OpenProperty, value);
      }
    }

    /// <summary>
    /// The toggle button property.
    /// </summary>
    public static readonly BindableProperty ToggleButtonProperty =
      BindableProperty.Create(nameof(ToggleButton),
                              typeof(ActionButton),
                              typeof(ActionMenu),
                              DEFAULT_TOGGLE_BUTTON,
                              propertyChanged: (mn, oldValue, newValue) => ((ActionMenu)mn).ToggleButtonChanged());

    /// <summary>
    /// Gets or sets the toggle button.
    /// </summary>
    /// <value>The toggle button.</value>
    public ActionButton ToggleButton
    {
      get
      {
        return (ActionButton)GetValue(ToggleButtonProperty);
      }
      set
      {
        SetValue(ToggleButtonProperty, value);
      }
    }

    protected override void OnSizeAllocated(double width, double height)
    {
      base.OnSizeAllocated(width, height);
    }

    /// <summary>
    /// Binds to toggle button click event
    /// </summary>
    void ToggleButtonChanged()
    {
      ToggleButton.Clicked += ToggleMenu;
    }

    /// <summary>
    /// Toggles the menu.
    /// </summary>
    /// <param name="arg1">Sender of event</param>
    /// <param name="arg2">Event arguments</param>
    void ToggleMenu(object arg1, EventArgs arg2)
    {
      if (!Open)
      {
        foreach (var c in Contents)
        {
          c.Show?.Invoke(0);
        }
        Open = true;
        ToggleButton.RotateTo(135, 500, Easing.SpringOut);
      }
      else
      {
        double Y = 0;
        for (int i = Contents.Count - 1; i >= 0; --i)
        {
          Y += Contents.ElementAt(i).Height;
          Contents.ElementAt(i).Hide?.Invoke(Y);
        }

        //foreach (var c in Contents)
        //{
        //  c.Hide?.Invoke();
        //}
        Open = false;
        ToggleButton.RotateTo(0, 500, Easing.SpringOut);
      }
    }
  }
}
