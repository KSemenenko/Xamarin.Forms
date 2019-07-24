using System;
using Xamarin.Forms.Core;

namespace Xamarin.Forms.Controls
{
	public class LongPressGestureGalleryPage : ContentPage
	{
		public class PressContainer : ContentView
		{
			public EventHandler SwipeLeft;
			public EventHandler SwipeRight;
			public EventHandler SwipeUp;
			public EventHandler SwipeDown;

			public PressContainer()
			{
				GestureRecognizers.Add(GetPres());
			}

			LongPressGestureRecognizer GetPres()
			{
				var press = new LongPressGestureRecognizer();
				//swipe.Swiped += (sender, args) => SwipeLeft?.Invoke(this, new EventArgs());
				return press;
			}

			
		}

		public LongPressGestureGalleryPage()
		{
			var box = new Image
			{
				BackgroundColor = Color.Gray,
				WidthRequest = 500,
				HeightRequest = 500,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center
			};

			var label = new Label { Text = "Use one finger and swipe inside the gray box." };

			var swipeme = new PressContainer { Content = box };
			swipeme.SwipeLeft += (sender, args) => label.Text = "You swiped left.";
			swipeme.SwipeRight += (sender, args) => label.Text = "You swiped right.";
			swipeme.SwipeUp += (sender, args) => label.Text = "You swiped up.";
			swipeme.SwipeDown += (sender, args) => label.Text = "You swiped down.";

			Content = new StackLayout { Children = { label, swipeme }, Padding = new Thickness(20) };
		}
	}
}