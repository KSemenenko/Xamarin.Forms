﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.Controls.GalleryPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TouchGesturesGalleryPage : ContentPage
	{
		int _count;
		ObservableCollection<string> _logs = new ObservableCollection<string>();

		ObservableCollection<string> _testData = new ObservableCollection<string>();
		public TouchGesturesGalleryPage()
		{
			InitializeComponent();
			LogListView.ItemsSource = _logs;

			for (int i = 0; i < 10; i++)
			{
				_testData.Add($"{i}{i}{i}{i}{i}");
			}

			//TouchCollectionView.ItemsSource = _testData;
			TouchListView.ItemsSource = _testData;
		}

		void TouchGestureRecognizer_OnTouchUpdated(object sender, TouchEventArgs e)
		{
			var logItem = $"N:{++_count},{(sender as View)?.GetType().Name ?? "?"} Touch:{e.TouchPoints.Count}, InView:{e.TouchPoints.All(a => a.IsInOriginalView)}, Event:{e.TouchState}; ";

			if (_logs.Count > 0)
			{
				var first = _logs.First();
				if (first.Contains(TouchState.Move.ToString()) && e.TouchState == TouchState.Move)
				{
					_logs.Remove(first);
				}
			}

			_logs.Insert(0, logItem);

			LogListView.ScrollTo(logItem,ScrollToPosition.MakeVisible,true);
		}

		void BoxViewTest_Click(object sender, EventArgs e)
		{
			BoxViewGrid.IsVisible = true;
			ListViewGrid.IsVisible = false;
			CollectionViewGrid.IsVisible = false;
			ScrollViewGrid.IsVisible = false;
		}

		void ListViewTest_Click(object sender, EventArgs e)
		{
			BoxViewGrid.IsVisible = false;
			ListViewGrid.IsVisible = true;
			CollectionViewGrid.IsVisible = false;
			ScrollViewGrid.IsVisible = false;
		}

		void CollectionViewTest_Click(object sender, EventArgs e)
		{
			BoxViewGrid.IsVisible = false;
			ListViewGrid.IsVisible = false;
			CollectionViewGrid.IsVisible = true;
			ScrollViewGrid.IsVisible = false;
		}


		void ScrollViewTest_Click(object sender, EventArgs e)
		{
			BoxViewGrid.IsVisible = false;
			ListViewGrid.IsVisible = false;
			CollectionViewGrid.IsVisible = false;
			ScrollViewGrid.IsVisible = true;
		}

		void LongPressGestureRecognizer_OnLongPressed(object sender, EventArgs e)
		{
			DisplayAlert(null, "LongPressed", "OK");
		}
	}
}