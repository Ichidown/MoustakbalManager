using LiveCharts;
using LiveCharts.Wpf;
using MoustakbalManager.Ressources;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using MoustakbalManager.Ressources.Interfaces;

namespace MoustakbalManager.Pages.StatPage
{
	public partial class StatColumn : UserControl, StatChartInterface
	{
		public SeriesCollection SeriesCollection { get; set; }
		public Func<double, string> Formatter { get; set; }

		private ChartValues<double> StatValues = new ChartValues<double> { };

		public StatColumn()
		{
			InitializeComponent();
			DataContext = this;
			
			SeriesCollection = new SeriesCollection();
			SeriesCollection.Add(new ColumnSeries
			{
				Title = "",
				Values = new ChartValues<int> {}
			});

			Formatter = value => value.ToString("N");
		}

		public void UpdateData(List<StatData> statDatas)
		{
			SeriesCollection[0].Values = new ChartValues<int>(statDatas.Select(x => x.DataValue).ToArray());

			ColumnChart.AxisX[0].Labels = statDatas.Select(x => x.StringData).ToArray();
		}
	}
}
