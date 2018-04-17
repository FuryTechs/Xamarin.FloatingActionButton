using System;
using System.Windows.Input;
using FuryTechs.FloatingActionButton.Abstraction;
using Xamarin.Forms;

namespace FuryTechs.FloatingActionButton
{
  [ContentProperty("Content")]
  public class ActionButton : View
  {
    /// <summary>
    /// Gets the color normal property
    /// </summary>
    public static readonly BindableProperty ColorNormalProperty =
        BindableProperty.Create(nameof(ColorNormal),
            typeof(Color),
            typeof(ActionButton),
            Color.Black);

    /// <summary>
    /// Gets or sets the color of the button
    /// </summary>
    public Color ColorNormal
    {
      get { return (Color)GetValue(ColorNormalProperty); }
      set { SetValue(ColorNormalProperty, value); }
    }

    /// <summary>
    /// Gets the color pressed property
    /// </summary>
    public static readonly BindableProperty ColorPressedProperty =
        BindableProperty.Create(nameof(ColorPressed),
            typeof(Color),
            typeof(ActionButton),
            Color.White);


    /// <summary>
    /// Gets or sets the color pressed property
    /// </summary>
    public Color ColorPressed
    {
      get { return (Color)GetValue(ColorPressedProperty); }
      set { SetValue(ColorPressedProperty, value); }
    }

    /// <summary>
    /// Gets the ripple color property
    /// </summary>
    public static readonly BindableProperty ColorRippleProperty =
        BindableProperty.Create(nameof(ColorRipple),
            typeof(Color),
            typeof(ActionButton),
            Color.White);

    /// <summary>
    /// Gets or sets the ripple color of the floating action button
    /// </summary>
    public Color ColorRipple
    {
      get { return (Color)GetValue(ColorRippleProperty); }
      set { SetValue(ColorRippleProperty, value); }
    }

    /// <summary>
    /// Gets the has shadow property
    /// </summary>
    public static readonly BindableProperty HasShadowProperty =
        BindableProperty.Create(nameof(HasShadow),
            typeof(bool),
            typeof(ActionButton),
            true);

    /// <summary>
    /// Gets or sets the has shadow property
    /// </summary>
    public bool HasShadow
    {
      get { return (bool)GetValue(HasShadowProperty); }
      set { SetValue(HasShadowProperty, value); }
    }

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
      get { return (ICommand)GetValue(CommandProperty); }
      set { SetValue(CommandProperty, value); }
    }

    /// <summary>
    /// Gets or sets the command parameter
    /// </summary>
    public object CommandParameter
    {
      get { return GetValue(CommandParameterProperty); }
      set { SetValue(CommandParameterProperty, value); }
    }

    /// <summary>
    /// Show Hide Delegate
    /// </summary>
    public delegate void ShowHideDelegate();

    /// <summary>
    /// Attach to list view delegate
    /// </summary>
    /// <param name="listView"></param>
    public delegate void AttachToListViewDelegate(ListView listView);

    /// <summary>
    /// Show the control
    /// </summary>
    public ShowHideDelegate Show { get; set; }

    /// <summary>
    /// Hide the control
    /// </summary>
    public ShowHideDelegate Hide { get; set; }



    /// <summary>
    /// Gets or sets the size of the button
    /// </summary>
    public static readonly BindableProperty SizeProperty =
                           BindableProperty.Create(nameof(Size),
                                                   typeof(Size),
                                                   typeof(ActionButton),
                                                   default(Size));

    /// <summary>
    /// Gets or sets the size of the button
    /// </summary>
    /// <value>The size of the button.</value>
    public Size Size
    {
      get { return (Size)GetValue(SizeProperty); }
      set
      {
        SetValue(SizeProperty, value);
      }
    }

    /// <summary>
    /// Action to call when clicked
    /// </summary>
    public Action<object, EventArgs> Clicked { get; set; }

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
    public static readonly BindableProperty ContentProperty =
                           BindableProperty.Create(nameof(Content),
                                                   typeof(ActionButtonContent),
                                                   typeof(ActionButton),
                                                   null, propertyChanged: HandleBindingPropertyChangedDelegate);

    /// <summary>
    /// Gets or sets the content of the Floating Action Button
    /// </summary>
    /// <value>The content of the button</value>
    public ActionButtonContent Content { get; set; }

    static void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
    {
      if(bindable is ActionButton ab && newValue is ActionButtonContent content) {
        ab.Content = content;
      }
    }


  }

  public enum Size
  {
    Mini,
    Normal,
    Auto
  }
}