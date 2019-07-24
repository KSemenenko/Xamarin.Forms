using System;
using System.Collections.Generic;

namespace Xamarin.Forms
{
	public class TouchEventArgs : EventArgs
	{
		public long Id { get; }

		public bool IsInContact { get; }

		public IReadOnlyList<TouchPoint> TouchPoints { get; }

		public TouchState TouchState { get; }
	}
}