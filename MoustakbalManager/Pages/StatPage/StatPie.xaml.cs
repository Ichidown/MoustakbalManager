using LiveCharts;
using LiveCharts.Wpf;
using MoustakbalManager.Ressources;
using MoustakbalManager.Ressources.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace MoustakbalManager.Pages.StatPage
{
	public partial class StatPie : UserControl , StatChartInterface
	{
		public SeriesCollection SeriesCollection { get; set; }
		public Func<ChartPoint, string> PointLabel { get; set; }

		public StatPie()
		{
			InitializeComponent();
			DataContext = this;
			SeriesCollection = new SeriesCollection();
			PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);			
		}

		

		public void UpdateData(List<StatData> statDatas)
		{
			SeriesCollection.Clear();

			foreach(var statData in statDatas)
			{
				SeriesCollection.Add(new PieSeries
				{
					Title = statData.StringData,
					Values = new ChartValues<int> { statData.DataValue },
					DataLabels = true,
					LabelPoint = PointLabel
				});
			}
		}

		private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
		{
			//var selectedSeries = (PieSeries)chartpoint.SeriesView;
			//selectedSeries.PushOut =selectedSeries.PushOut>0? 0:10;
		}
	}
}
