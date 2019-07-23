using System;
using Xamarin.Forms.Core;

namespace Xamarin.Forms.Controls
{
	public class RotateGestureGalleryPage : ContentPage
	{
		public class RotateContainer : ContentView
		{
			public EventHandler SwipeLeft;
			public EventHandler SwipeRight;
			public EventHandler SwipeUp;
			public EventHandler SwipeDown;

			public RotateContainer()
			{
				GestureRecognizers.Add(GetRotate());
			}

			RotateGestureRecognizer GetRotate()
			{
				var rotate = new RotateGestureRecognizer();
				//swipe.Swiped += (sender, args) => SwipeLeft?.Invoke(this, new EventArgs());
				return rotate;
			}

			
		}

		public RotateGestureGalleryPage()
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

			var swipeme = new RotateContainer { Content = box };
			swipeme.SwipeLeft += (sender, args) => label.Text = "You swiped left.";
			swipeme.SwipeRight += (sender, args) => label.Text = "You swiped right.";
			swipeme.SwipeUp += (sender, args) => label.Text = "You swiped up.";
			swipeme.SwipeDown += (sender, args) => label.Text = "You swiped down.";

			Content = new StackLayout { Children = { label, swipeme }, Padding = new Thickness(20) };
		}
	}
}