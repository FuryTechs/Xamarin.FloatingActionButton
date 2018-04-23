using System;
using System.Collections.Generic;
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
      HorizontalOptions = LayoutOptions.Center,
      VerticalOptions = LayoutOptions.Center,
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
      Contents = new List<ActionButton>();
    }

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
    /// The open property.
    /// </summary>
    public static readonly BindableProperty OpenProperty =
                           BindableProperty.Create(nameof(Open),
                                                   typeof(bool),
                                                   typeof(ActionMenu),
                                                   false);

    /// <summary>
    /// Toggles the menu.
    /// </summary>
    /// <param name="arg1">Sender of event</param>
    /// <param name="arg2">Event arguments</param>
    void ToggleMenu(object arg1, EventArgs arg2)
    {
      ToggleButton.VerticalOptions = LayoutOptions.Center;
      ToggleButton.HorizontalOptions = LayoutOptions.Center;
      if (!Open)
      {
        foreach (var c in Contents)
        {
          c.Hide();
          c.Show();
        }
        Open = true;
        ToggleButton.RotateTo(315, 500, Easing.SpringOut);
      }
      else
      {
        foreach (var c in Contents)
        {
          c.Show();
          c.Hide();
        }
        Open = false;
        ToggleButton.RotateTo(0, 500, Easing.SpringOut);
      }
    }

    #region Toggle Button
    /// <summary>
    /// The toggle button property.
    /// </summary>
    public static readonly BindableProperty ToggleButtonProperty =
                           BindableProperty.Create(nameof(ToggleButton),
                                                   typeof(ActionButton),
                                                   typeof(ActionMenu),
                                                   DEFAULT_TOGGLE_BUTTON,
                                                   propertyChanged: HandleBindingPropertyChangedDelegate);

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
        value.Clicked += ToggleMenu;
        SetValue(ToggleButtonProperty, value);
      }
    }
    #endregion

    #region Contents
    /// <summary>
    /// The contents property.
    /// </summary>
    public static readonly BindableProperty ContentsProperty =
                           BindableProperty.Create(nameof(Contents),
                                                   typeof(ICollection<ActionButton>),
                                                   typeof(ActionMenu),
                                                   new List<ActionButton>(),
                                                   propertyChanged: HandleBindingPropertyChangedDelegate);

    /// <summary>
    /// Gets or sets the content of the Floating Action Button
    /// </summary>
    /// <value>The content of the button</value>
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
    #endregion

    /// <summary>
    /// Handles the binding property changed delegate.
    /// </summary>
    /// <param name="bindable">Bindable.</param>
    /// <param name="oldValue">Old value.</param>
    /// <param name="newValue">New value.</param>
    static void HandleBindingPropertyChangedDelegate(BindableObject bindable,
                                                     object oldValue,
                                                     object newValue)
    {
      if (bindable is ActionMenu ab)
      {
        if (newValue is ICollection<ActionButton> buttons)
        {
          ab.Contents = buttons;
        }
        if (newValue is ActionButton button)
        {
          ab.ToggleButton = button;
        }
      }
    }
  }

}
