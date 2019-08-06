using System;
using Xamarin.Forms.Core;

namespace Xamarin.Forms.Controls
{
	public class LongPressGestureGalleryPage : ContentPage
	{
		public class PressContainer : ContentView
		{
			public EventHandler LongPress;

			public PressContainer()
			{
				GestureRecognizers.Add(GetLongPres());
			}

			LongPressGestureRecognizer GetLongPres()
			{
				try
				{
					var press = new LongPressGestureRecognizer();
					press.LongPressed += (sender, args) => LongPress?.Invoke(this, new EventArgs());
					return press;
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
				
			}

			
		}

		public LongPressGestureGalleryPage()
		{
			var box = new BoxView
			{
				BackgroundColor = Color.Gray,
				WidthRequest = 500,
				HeightRequest = 500,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center
			};

			var label = new Label { Text = "Use one finger to make long press in gray box." };

			var longPress = new PressContainer { Content = box };
			longPress.LongPress += (sender, args) => label.Text = "You make long press";

			Content = new StackLayout { Children = { label, longPress }, Padding = new Thickness(20) };
		}
	}
}