using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FuryTechs.FloatingActionButton.Abstraction;
using Xamarin.Forms;

namespace FuryTechs.FloatingActionButton
{
  [ContentProperty(nameof(Content))]
  public class ActionButton : View
  {
    #region Bindable properties
    /// <summary>
    /// The color property.
    /// </summary>
    public static readonly BindableProperty ColorProperty =
      BindableProperty.Create(nameof(Color),
                              typeof(Color),
                              typeof(ActionButton),
                              Color.Transparent);

    /// <summary>
    /// Gets the ripple color property
    /// </summary>
    public static readonly BindableProperty ColorRippleProperty =
        BindableProperty.Create(nameof(ColorRipple),
                                typeof(Color),
                                typeof(ActionButton),
                                Color.White
                               );


    /// <summary>
    /// Gets the command property
    /// </summary>
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command),
        typeof(ICommand),
        typeof(ActionButton),
        null,
        propertyChanged: (bo, o, n) => ((ActionButton)bo).OnCommandChanged());


    /// <summary>
    /// Gets the command 
    /// </summary>
    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter),
            typeof(object),
            typeof(ActionButton),
            null,
            propertyChanged: (bindable, oldvalue, newvalue) => ((ActionButton)bindable).CommandCanExecuteChanged(bindable, EventArgs.Empty));

    /// <summary>
    /// Gets or sets the size of the button
    /// </summary>
    public static readonly BindableProperty SizeProperty =
                           BindableProperty.Create(nameof(Size),
                                                   typeof(Abstraction.Size),
                                                   typeof(ActionButton),
                                                   default(Abstraction.Size));

    /// <summary>
    /// Gets or sets the content of the Floating Action Button
    /// </summary>
    public static readonly BindableProperty ContentProperty =
                           BindableProperty.Create(nameof(Content),
                                                   typeof(ActionButtonContent),
                                                   typeof(ActionButton),
                                                   null);

    /// <summary>
    /// The clicked property.
    /// </summary>
    public static readonly BindableProperty ClickedProperty =
                           BindableProperty.Create(nameof(Clicked),
                                                   typeof(Action<object, EventArgs>),
                                                   typeof(ActionButton));

    /// <summary>
    /// The animation duration property.
    /// </summary>
    public static readonly BindableProperty AnimationDurationProperty =
      BindableProperty.Create(nameof(AnimationDuration),
                              typeof(int),
                              typeof(ActionButton),
                              (int)300);
    #endregion

    /// <summary>
    /// Gets or sets the duration of the animation.
    /// </summary>
    /// <value>The duration of the animation.</value>
    public int AnimationDuration
    {
      get
      {
        return (int)GetValue(AnimationDurationProperty);
      }
      set
      {
        SetValue(AnimationDurationProperty, value);
      }
    }

    /// <summary>
    /// The button color property.
    /// </summary>
    public static readonly BindableProperty ButtonColorProperty =
                           BindableProperty.Create(nameof(ButtonColor),
                                                   typeof(Color),
                                                   typeof(ActionButton),
                                                   Color.NavajoWhite);

    /// <summary>
    /// Gets or sets the color of the button.
    /// </summary>
    /// <value>The color of the button.</value>
    public Color ButtonColor
    {
      get
      {
        return (Color)GetValue(ButtonColorProperty);
      }

      set
      {
        SetValue(ButtonColorProperty, value);
      }
    }

    /// <summary>
    /// Executes if enabled or not based on can execute changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventArgs"></param>
    void CommandCanExecuteChanged(object sender, EventArgs eventArgs)
    {
      ICommand cmd = Command;
      if (cmd != null)
        IsEnabled = cmd.CanExecute(CommandParameter);
    }

    /// <summary>
    /// Gets or sets the command to execute when clicked
    /// </summary>
    public ICommand Command
    {
      get
      {
        return (ICommand)GetValue(CommandProperty);
      }
      set
      {
        SetValue(CommandProperty, value);
      }
    }

    /// <summary>
    /// Gets or sets the command parameter
    /// </summary>
    public object CommandParameter
    {
      get
      {
        return GetValue(CommandParameterProperty);
      }
      set
      {
        SetValue(CommandParameterProperty, value);
      }
    }

    /// <summary>
    /// Gets or sets the color.
    /// </summary>
    /// <value>The color.</value>
    public Color Color
    {
      get
      {
        return (Color)GetValue(ColorProperty);
      }
      set
      {
        SetValue(ColorProperty, value);
        if (Content?.Color == Color.Transparent)
        {
          Content.Color = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets the ripple color of the floating action button
    /// </summary>
    public Color ColorRipple
    {
      get
      {
        return (Color)GetValue(ColorRippleProperty);
      }
      set
      {
        SetValue(ColorRippleProperty, value);
      }
    }

    /// <summary>
    /// The tooltip property.
    /// </summary>
    public static readonly BindableProperty TooltipProperty =
                           BindableProperty.Create(nameof(Tooltip),
                                                   typeof(string),
                                                   typeof(ActionButton),
                                                   string.Empty);

    /// <summary>
    /// Gets or sets the tooltip.
    /// </summary>
    /// <value>The tooltip.</value>
    public string Tooltip
    {
      get
      {
        return (string)GetValue(TooltipProperty);
      }

      set
      {
        SetValue(TooltipProperty, value);
      }
    }

    /// <summary>
    /// Show Hide Delegate
    /// </summary>
    public delegate void ShowHideDelegate(double? moveTo = null);

    /// <summary>
    /// Show the control
    /// </summary>
    public ShowHideDelegate Show
    {
      get; set;
    }

    /// <summary>
    /// Hide the control
    /// </summary>
    public ShowHideDelegate Hide
    {
      get; set;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:FuryTechs.FloatingActionButton.ActionButton"/> is hidden.
    /// </summary>
    /// <value><c>true</c> if is hidden; otherwise, <c>false</c>.</value>
    public bool IsHidden
    {
      get; set;
    }

    /// <summary>
    /// Gets or sets the size of the button
    /// </summary>
    /// <value>The size of the button.</value>
    public Abstraction.Size Size
    {
      get
      {
        return (Abstraction.Size)GetValue(SizeProperty);
      }
      set
      {
        SetValue(SizeProperty, value);
      }
    }

    /// <summary>
    /// Action to call when clicked
    /// </summary>
    public Action<object, EventArgs> Clicked
    {
      get
      {
        return (Action<object, EventArgs>)GetValue(ClickedProperty);
      }
      set
      {
        SetValue(ClickedProperty, value);
      }
    }


    void OnCommandChanged()
    {
      if (Command != null)
      {
        Command.CanExecuteChanged += CommandCanExecuteChanged;
        CommandCanExecuteChanged(this, EventArgs.Empty);
      }
      else
        IsEnabled = true;
    }


    /// <summary>
    /// Gets or sets the content of the Floating Action Button
    /// </summary>
    /// <value>The content of the button</value>
    public ActionButtonContent Content
    {
      get
      {
        return (ActionButtonContent)GetValue(ContentProperty);
      }
      set
      {
        SetValue(ContentProperty, value);
      }
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      base.OnPropertyChanged(propertyName);
      switch (propertyName)
      {
        case nameof(Color):
          {
            if (Content != null && Content.Color == Color.Transparent && Color != Color.Transparent)
            {
              Content.Color = Color;
            }

          }
          break;
      }
    }

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
      if (bindable is ActionButton ab)
      {
        if (newValue is ActionButtonContent content)
        {
          ab.Content = content;
        }
      }
    }
  }
}