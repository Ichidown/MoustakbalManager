using MoustakbalManager.Pages.AdminPages;
using System;
using System.Globalization;
using System.Windows.Data;

namespace MoustakbalManager.Tools.DataConverters
{
	public class IdToWilaya : IValueConverter
	{
		private MembersPage membersPage;

		public IdToWilaya(MembersPage membersPage)
		{
			this.membersPage = membersPage;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return membersPage.wilayas[membersPage.wilayas.FindIndex(Wilaya => Wilaya.ID == (int)value)].Name;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return membersPage.wilayas[membersPage.wilayas.FindIndex(Wilaya => Wilaya.Name == (string)value)].ID;
		}
	}
}
