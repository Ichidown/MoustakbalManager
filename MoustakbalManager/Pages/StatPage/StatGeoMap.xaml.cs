using MoustakbalManager.Ressources;
using MoustakbalManager.Ressources.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace MoustakbalManager.Pages.StatPage
{
	public partial class StatGeoMap : UserControl, StatChartInterface
	{
		public StatGeoMap()
		{
			InitializeComponent();

			var r = new Random();

			Values = new Dictionary<string, double>();

			Values["Batna"] = 200;
			Values["Alger"] = r.Next(0, 100);
			Values["Biskra"] = r.Next(0, 100);
			Values["Ouargla"] = r.Next(0, 100);

			Values["Djelfa"] = r.Next(0, 100);
			Values["Sétif"] = r.Next(0, 100);
			Values["Saïda"] = r.Next(0, 100);

			Values["M'Sila"] = r.Next(0, 100);
			Values["Mascara"] = r.Next(0, 100);
			Values["Naâma"] = r.Next(0, 100);

			Values["Relizane"] = r.Next(0, 100);
			Values["Tissemsilt"] = r.Next(0, 100);
			Values["El Taref"] = r.Next(0, 100);

			DataContext = this;


			GradientStopCollection collection = new GradientStopCollection();
			collection.Add(new GradientStop() { Color = Color.FromArgb(255, 161, 237, 177), Offset = 0 });
			collection.Add(new GradientStop() { Color = Color.FromArgb(255, 255, 0, 0), Offset = 0.9 });
			collection.Add(new GradientStop() { Color = Color.FromArgb(255, 76, 5, 18), Offset = 1 });

			GeoMap.GradientStopCollection = collection;
		}

		public Dictionary<string, double> Values { get; set; }

		public void UpdateData(List<StatData> statDatas)
		{
			
		}
	}
}
