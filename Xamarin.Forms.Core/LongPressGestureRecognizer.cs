using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms.Core;

namespace Xamarin.Forms
{
	public sealed class LongPressGestureRecognizer : GestureRecognizer
	{
		public static readonly BindableProperty IsLongPressingProperty = BindableProperty.Create(nameof(IsLongPressing), typeof(bool), typeof(TapGestureRecognizer));
		public static readonly BindableProperty NumberOfTouchesRequiredProperty = BindableProperty.Create(nameof(NumberOfTouchesRequired), typeof(int), typeof(TapGestureRecognizer), 1);
		public static readonly BindableProperty NumberOfTapsRequiredProperty = BindableProperty.Create(nameof(NumberOfTapsRequired), typeof(int), typeof(TapGestureRecognizer), 1);
		public static readonly BindableProperty MinimumPressDurationProperty = BindableProperty.Create(nameof(MinimumPressDuration), typeof(double), typeof(TapGestureRecognizer), (double)0.0d);
		public static readonly BindableProperty AllowableMovementProperty = BindableProperty.Create(nameof(AllowableMovement), typeof(uint), typeof(TapGestureRecognizer), (uint)0);

		public static readonly BindableProperty StartedCommandProperty = BindableProperty.Create(nameof(StartedCommand), typeof(ICommand), typeof(TapGestureRecognizer));
		public static readonly BindableProperty CancelledCommandProperty = BindableProperty.Create(nameof(CancelledCommand), typeof(ICommand), typeof(TapGestureRecognizer));
		public static readonly BindableProperty FinishedCommandProperty = BindableProperty.Create(nameof(FinishedCommand), typeof(ICommand), typeof(TapGestureRecognizer));

		public static readonly BindableProperty StartedCommandParameterProperty = BindableProperty.Create(nameof(StartedCommandParameter), typeof(object), typeof(TapGestureRecognizer));
		public static readonly BindableProperty FinishedCommandParameterProperty = BindableProperty.Create(nameof(FinishedCommandParameter), typeof(object), typeof(TapGestureRecognizer));
		public static readonly BindableProperty CancelledCommandParameterProperty = BindableProperty.Create(nameof(CancelledCommandParameter), typeof(object), typeof(TapGestureRecognizer));

		public bool IsLongPressing
		{
			get { return (bool)GetValue(IsLongPressingProperty); }
			set { SetValue(IsLongPressingProperty, value); }
		}

		public int NumberOfTouchesRequired
		{
			get { return (int)GetValue(NumberOfTouchesRequiredProperty); }
			set { SetValue(NumberOfTouchesRequiredProperty, value); }
		}

		public int NumberOfTapsRequired
		{
			get { return (int)GetValue(NumberOfTapsRequiredProperty); }
			set { SetValue(NumberOfTapsRequiredProperty, value); }
		}

		public double MinimumPressDuration
		{
			get { return (double)GetValue(MinimumPressDurationProperty); }
			set { SetValue(MinimumPressDurationProperty, value); }
		}

		public uint AllowableMovement
		{
			get { return (uint)GetValue(AllowableMovementProperty); }
			set { SetValue(AllowableMovementProperty, value); }
		}

		public ICommand StartedCommand
		{
			get { return (ICommand)GetValue(StartedCommandProperty); }
			set { SetValue(StartedCommandProperty, value); }
		}

		public ICommand CancelledCommand
		{
			get { return (ICommand)GetValue(CancelledCommandProperty); }
			set { SetValue(CancelledCommandProperty, value); }
		}

		public ICommand FinishedCommand
		{
			get { return (ICommand)GetValue(FinishedCommandProperty); }
			set { SetValue(FinishedCommandProperty, value); }
		}


		public object StartedCommandParameter
		{
			get { return (object)GetValue(StartedCommandParameterProperty); }
			set { SetValue(StartedCommandParameterProperty, value); }
		}

		public object FinishedCommandParameter
		{
			get { return (object)GetValue(FinishedCommandParameterProperty); }
			set { SetValue(FinishedCommandParameterProperty, value); }
		}

		public object CancelledCommandParameter
		{
			get { return (object)GetValue(CancelledCommandParameterProperty); }
			set { SetValue(CancelledCommandParameterProperty, value); }
		}

		public event EventHandler LongPressed;
		//public event EventHandler<LongPressUpdatedEventArgs> LongPressUpdated;

		public void SendPress(View sender)
		{

		}

		public void SendRelease()
		{

		}

		public void SendLongPress()
		{
			LongPressed?.Invoke(this,new EventArgs());
		}

	}
}
