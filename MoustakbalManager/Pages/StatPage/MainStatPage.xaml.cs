using MoustakbalManager.Ressources;
using MoustakbalManager.Ressources.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MoustakbalManager.Pages.StatPage
{
    public partial class MainStatPage : Page, SectionInterface
	{
		public AdminPage adminPage;
		private StatColumn basicColumn;
		private StatPie statPie;
		private StatLine statLine;
		private StatGeoMap statGeoMap;
		public List<StatData> statsData;
		private int currentStatDataTypeIndex=0;

		public MainStatPage(AdminPage adminPage)
		{
			InitializeComponent();
			this.adminPage = adminPage;

			basicColumn = new StatColumn();
			statPie = new StatPie();
			statLine = new StatLine();
			statGeoMap = new StatGeoMap();
			

			MainStatFrame.Content = basicColumn;
		}


		private void ColumnBtnClick(object sender, RoutedEventArgs e)
		{
			basicColumn.UpdateData(statsData);
			MainStatFrame.Content = basicColumn;
		}

		private void PieBtnClick(object sender, RoutedEventArgs e)
		{
			statPie.UpdateData(statsData);
			MainStatFrame.Content = statPie;
		}

		private void LineBtnClick(object sender, RoutedEventArgs e)
		{
			statLine.UpdateData(statsData);
			MainStatFrame.Content = statLine;
		}

		private void MapBtnClick(object sender, RoutedEventArgs e)
		{
			statGeoMap.UpdateData(statsData);
			MainStatFrame.Content = statGeoMap;
		}


		private void StatCategorySelection(object sender, RoutedEventArgs e)
		{
			currentStatDataTypeIndex = ((ComboBox)sender).SelectedIndex;
			UpdateData();
		}

		public void UpdateData()// trigered when this section is viewed / when data type changed
		{
			if (this.adminPage != null)
			{
				statsData = this.adminPage.SqliteDA.GetStatNumbers(currentStatDataTypeIndex);
				((StatChartInterface)MainStatFrame.Content).UpdateData(statsData);
			}
		}

	}
}
