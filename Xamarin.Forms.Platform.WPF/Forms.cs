﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.WPF;

namespace Xamarin.Forms
{
	public static class Forms
	{
		public static bool IsInitialized { get; private set; }

		public static IFormsInit Create(IEnumerable<Assembly> rendererAssemblies = null)
		{
			return new FormsInit(() => Init(rendererAssemblies));
		}

		static void Init(IEnumerable<Assembly> rendererAssemblies = null)
		{
			if (IsInitialized)
				return;

			string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

			System.Windows.Application.Current.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary
			{
				Source = new Uri(string.Format("/{0};component/WPFResources.xaml", assemblyName), UriKind.Relative)
			});

			var accentColor = (SolidColorBrush)System.Windows.Application.Current.Resources["AccentColor"];
			Color.SetAccent(Color.FromRgba(accentColor.Color.R, accentColor.Color.G, accentColor.Color.B, accentColor.Color.A));

			Log.Listeners.Add(new DelegateLogListener((c, m) => Console.WriteLine("[{0}] {1}", m, c)));
			Registrar.ExtraAssemblies = rendererAssemblies?.ToArray();

			Device.SetTargetIdiom(TargetIdiom.Desktop);
			Device.PlatformServices = new WPFPlatformServices();
			Device.Info = new WPFDeviceInfo();
			ExpressionSearch.Default = new WPFExpressionSearch();

			Registrar.RegisterAll(new[] { typeof(ExportRendererAttribute), typeof(ExportCellAttribute), typeof(ExportImageSourceHandlerAttribute) });
			
			Ticker.SetDefault(new WPFTicker());
			Device.SetIdiom(TargetIdiom.Desktop);

			IsInitialized = true;
		}
	}
}
