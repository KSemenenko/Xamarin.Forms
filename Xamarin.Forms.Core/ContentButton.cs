using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms
{
	public class ContentButton : Frame, IButtonController, IButtonElement
	{
		/// <summary>Backing store for the Command bindable property.</summary>
		/// <remarks />
		public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ContentButton),
			null, BindingMode.OneWay, null, (bo, o, n) => ((ContentButton)bo).OnCommandChanged());

		public static readonly BindableProperty ToggledCommandProperty =
			BindableProperty.Create(nameof(ToggledCommand), typeof(ICommand), typeof(ContentButton));

		public static readonly BindableProperty UntoggledCommandProperty =
			BindableProperty.Create(nameof(UntoggledCommand), typeof(ICommand), typeof(ContentButton));

		public static readonly BindableProperty PressAnimationProperty =
			BindableProperty.Create(nameof(PressAnimation), typeof(Animation), typeof(ContentButton));

		/// <summary>Backing store for the CommandParameter bindable property.</summary>
		/// <remarks />
		public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object),
			typeof(ContentButton), null, BindingMode.OneWay, null,
			(bindable, oldvalue, newvalue) => ((ContentButton)bindable).CommandCanExecuteChanged(bindable, EventArgs.Empty));

		public static readonly BindableProperty NormalViewProperty = BindableProperty.Create(nameof(NormalView), typeof(View), typeof(ContentButton),
			propertyChanged: (bindable, value, newValue) =>
			{
				((ContentButton)bindable).SetView(newValue, true);
			});

		public static readonly BindableProperty ToggleViewProperty = BindableProperty.Create(nameof(ToggleView), typeof(View), typeof(ContentButton),
			propertyChanged: (bindable, value, newValue) =>
			{
				((ContentButton)bindable).SetView(newValue, false);
			});

		public static readonly BindableProperty IsRadioButtonModeProperty =
			BindableProperty.Create(nameof(IsRadioButtonMode), typeof(bool), typeof(ContentButton), false);

		public static readonly BindableProperty IsToggleProperty = BindableProperty.Create(nameof(IsToggle), typeof(bool), typeof(ContentButton),
			false, BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
			{
				var button = (ContentButton)bindable;
				var isToggle = (bool)newValue;

				if ((bool)oldValue == isToggle)
				{
					return;
				}

				button.OnToggled(isToggle);

				if (!button.IsToggleModeEnabled)
				{
					return;
				}

				if (isToggle)
				{
					Layout layout = GetGroupLayout(button);
					if (layout != null)
					{
						foreach (ContentButton otherButton in GetButtons(layout).Where(w => w != button))
						{
							otherButton?.SetToggle(false);
						}
					}
				}

				button.UpdateToggleView();
			});

		public static readonly BindableProperty IsToggleModeEnabledProperty =
			BindableProperty.Create(nameof(IsToggleModeEnabled), typeof(bool), typeof(ContentButton), false);

		bool _buttonIsPresed;

		public ContentButton()
		{
			Padding = 0;
			HasShadow = false;
			CornerRadius = 0;
			BorderColor = Color.Transparent;
			BackgroundColor = Color.Transparent;
			IsClippedToBounds = true;

			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += TapGestureRecognizer_OnTapped;
			GestureRecognizers.Add(tapGestureRecognizer);
		}

		public event EventHandler Tapped;
		public event EventHandler<ToggledEventArgs> Toggled;

		public ICommand Command
		{
			get => (ICommand)GetValue(CommandProperty);
			set => SetValue(CommandProperty, value);
		}

		public ICommand ToggledCommand
		{
			get => (ICommand)GetValue(ToggledCommandProperty);
			set => SetValue(ToggledCommandProperty, value);
		}

		public ICommand UntoggledCommand
		{
			get => (ICommand)GetValue(UntoggledCommandProperty);
			set => SetValue(UntoggledCommandProperty, value);
		}

		public object CommandParameter
		{
			get => GetValue(CommandParameterProperty);
			set => SetValue(CommandParameterProperty, value);
		}

		public Animation PressAnimation
		{
			get => (Animation)GetValue(PressAnimationProperty);
			set => SetValue(PressAnimationProperty, value);
		}

		public View ToggleView
		{
			get => (View)GetValue(ToggleViewProperty);
			set => SetValue(ToggleViewProperty, value);
		}

		public View NormalView
		{
			get => (View)GetValue(NormalViewProperty);
			set => SetValue(NormalViewProperty, value);
		}

		public bool IsToggleModeEnabled
		{
			get => (bool)GetValue(IsToggleModeEnabledProperty);
			set => SetValue(IsToggleModeEnabledProperty, value);
		}

		public bool IsToggle
		{
			get => (bool)GetValue(IsToggleProperty);
			set => SetValue(IsToggleProperty, value);
		}

		public bool IsToggleButtonClicked { get; set; }

		public bool IsRadioButtonMode
		{
			get => (bool)GetValue(IsRadioButtonModeProperty);
			set => SetValue(IsRadioButtonModeProperty, value);
		}

		bool IsEnabledCore { get; set; }

		public bool IsPressed
		{
			get => _buttonIsPresed;
		}

		bool IButtonElement.IsEnabledCore { set => IsEnabledCore = value; }

		public void SendClicked()
		{
			if (IsToggleModeEnabled)
			{
				if (IsRadioButtonMode)
				{
					IsToggle = true;
				}
				else if (IsToggleButtonClicked)
				{
					IsToggle = !IsToggle;
					IsToggleButtonClicked = false;
				}

				if (IsToggle && ToggledCommand?.CanExecute(CommandParameter) == true)
				{
					ToggledCommand.Execute(CommandParameter);

					if (Command?.CanExecute(CommandParameter) ?? false)
					{
						Command.Execute(CommandParameter);
					}
				}
				else if (!IsToggle && UntoggledCommand?.CanExecute(CommandParameter) == true)
				{
					UntoggledCommand.Execute(CommandParameter);
				}
			}
			else
			{
				if (Command?.CanExecute(CommandParameter) ?? false)
				{
					Command.Execute(CommandParameter);
				}
			}

			Tapped?.Invoke(this, EventArgs.Empty);
		}

		public void SendPressed()
		{
		}

		public void SendReleased()
		{
		}

		public void SetToggle(bool toggle)
		{
			IsToggle = toggle;
		}

		protected void UpdateToggleView()
		{
			try
			{
				if (Device.IsInvokeRequired)
				{
					Device.BeginInvokeOnMainThread(UpdateToggleViewInternal);
				}
				else
				{
					UpdateToggleViewInternal();
				}
			}
			catch (NullReferenceException)
			{
				
			}
		}

		void UpdateToggleViewInternal()
		{
			if (IsToggle)
			{
				if (NormalView != null && ToggleView != null)
				{
					ToggleView.IsVisible = true;
					NormalView.IsVisible = false;
				}
			}
			else
			{
				if (NormalView != null && ToggleView != null)
				{
					ToggleView.IsVisible = false;
					NormalView.IsVisible = true;
				}
			}
		}

		static IEnumerable<ContentButton> GetButtons(Layout layout)
		{
			foreach (Element item in layout.Children)
			{
				if (item is ContentButton cb)
				{
					yield return cb;
				}
				else if (item is ContentView cv && cv.Content is ContentButton ccb)
				{
					yield return ccb;
				}
			}
		}

		void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			//TODO: fix animation
			//PressAnimation.
			//_ = this.Animate(new PressAnimation());

			if (_buttonIsPresed)
			{
				return;
			}

			try
			{
				_buttonIsPresed = true;
				IsToggleButtonClicked = true;
				SendClicked();
				Tapped?.Invoke(this, EventArgs.Empty);
			}
			finally
			{
				_buttonIsPresed = false;
			}
		}

		protected override void OnPropertyChanging(string propertyName = null)
		{
			base.OnPropertyChanging(propertyName);

			if (propertyName == Button.CommandProperty.PropertyName)
			{
				ICommand command = Command;
				if (command != null)
				{
					command.CanExecuteChanged -= CommandCanExecuteChanged;
				}
			}
		}

		void CommandCanExecuteChanged(object sender, EventArgs eventArgs)
		{
			ICommand command = Command;
			if (command == null)
			{
				return;
			}

			IsEnabledCore = command.CanExecute(CommandParameter);
		}

		void OnCommandChanged()
		{
			if (Command != null)
			{
				Command.CanExecuteChanged += CommandCanExecuteChanged;
				CommandCanExecuteChanged(this, EventArgs.Empty);
			}
			else
			{
				IsEnabledCore = true;
			}
		}

		static Layout GetGroupLayout(ContentButton button)
		{
			if (button.Parent != null && button.Parent is Layout layout)
			{
				if (layout is ContentView view) // we will skip ContentView
				{
					if (view.Parent != null && view.Parent is Layout viewLayout)
					{
						return viewLayout;
					}
				}

				return layout;
			}

			return null;
		}

		void OnToggled(bool isToggled)
		{
			Toggled?.Invoke(this, new ToggledEventArgs(isToggled));
		}

		Grid GetContent()
		{
			if (Content == null)
			{
				Content = new Grid {RowSpacing = 0, ColumnSpacing = 0};
			}

			return (Grid)Content;
		}

		void SetView(object obj, bool normal)
		{
			if(obj is View view)
			{
				var grid = GetContent();
				view.IsVisible = normal;
				grid.Children.Add(view);
			}
		}

		public void PropagateUpClicked()
		{
		}

		public void PropagateUpPressed()
		{
		}

		public void PropagateUpReleased()
		{
		}

		public void SetIsPressed(bool isPressed)
		{
		}

		public void OnCommandCanExecuteChanged(object sender, EventArgs e)
		{
		}
	}
}