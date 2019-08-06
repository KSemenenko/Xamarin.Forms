using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.Core
{
	public class LongPressUpdatedEventArgs : TouchEventArgs
	{
		public GestureStatus StatusType { get; }
	}
}
