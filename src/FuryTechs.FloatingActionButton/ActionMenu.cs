using System;
using System.Collections.Generic;
using FuryTechs.FloatingActionButton.ContentTypes;
using Xamarin.Forms;

namespace FuryTechs.FloatingActionButton
{
  [ContentProperty(nameof(Contents))]
  public class ActionMenu : View
  {
    public static readonly ActionButton DEFAULT_TOGGLE_BUTTON = new ActionButton()
    {
      Content = new FontAwesomeIcon()
      {
        Icon = FAIcons.FAPlus
      }
    };

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
      if (!Open)
      {
        Open = true;
        ToggleButton.RotateTo(315, 500, Easing.SpringOut);
      }
      else
      {
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
                                                   typeof(IEnumerable<ActionButton>),
                                                   typeof(ActionMenu),
                                                   null,
                                                   propertyChanged: HandleBindingPropertyChangedDelegate);

    /// <summary>
    /// Gets or sets the content of the Floating Action Button
    /// </summary>
    /// <value>The content of the button</value>
    public IEnumerable<ActionButton> Contents
    {
      get
      {
        return (IEnumerable<ActionButton>)GetValue(ContentsProperty);
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
        if (newValue is IEnumerable<ActionButton> buttons)
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
