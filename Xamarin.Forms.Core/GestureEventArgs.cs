using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms
{
	public class GestureEventArgs : EventArgs
	{
		public TouchEvent TouchEvent { get; private set; }
	}
}
