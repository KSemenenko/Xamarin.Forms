using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.Android
{
	internal class LongPressGestureHandler
	{
		public LongPressGestureHandler(Func<View> getView, Func<IList<GestureElement>> getChildElements)
		{
			GetView = getView;
			GetChildElements = getChildElements;
		}

		Func<IList<GestureElement>> GetChildElements { get; }
		Func<View> GetView { get; }


		public bool OnLongTap(int count, Point point)
		{
			View view = GetView();

			if (view == null)
				return false;

			var captured = false;

			var children = view.GetChildElements(point);

			if (children != null)
				foreach (var recognizer in children.GetChildGesturesFor<LongPressGestureRecognizer>(recognizer => recognizer.NumberOfTapsRequired == count))
				{
					recognizer.SendLongPress();
					captured = true;
				}

			if (captured)
				return captured;

			IEnumerable<LongPressGestureRecognizer> gestureRecognizers = LongPressGestureRecognizers(count);
			foreach (var gestureRecognizer in gestureRecognizers)
			{
				gestureRecognizer.SendLongPress();
				captured = true;
			}

			return captured;
		}


		public bool HasAnyGestures()
		{
			var view = GetView();
			return view != null && view.GestureRecognizers.OfType<LongPressGestureRecognizer>().Any()
			       || GetChildElements().GetChildGesturesFor<LongPressGestureRecognizer>().Any();
		}

		public IEnumerable<LongPressGestureRecognizer> LongPressGestureRecognizers(int count)
		{
			View view = GetView();
			if (view == null)
				return Enumerable.Empty<LongPressGestureRecognizer>();

			return view.GestureRecognizers.GetGesturesFor<LongPressGestureRecognizer>(recognizer => recognizer.NumberOfTapsRequired == count);
		}
	}
}