using MaterialDesignThemes.Wpf;
using MoustakbalManager.Objects;
using MoustakbalManager.Pages.AdminPages;
using MoustakbalManager.Pages.GridPage;
using MoustakbalManager.Pages.StatPage;
using MoustakbalManager.Ressources.Interfaces;
using MoustakbalManager.Tools;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MoustakbalManager.Pages
{
    public partial class AdminPage : Page
    {
        private MainWindow mainWindow;
		private MainGridPage mainGridPage;
		private MainStatPage mainStatPage;
		private List<SectionInterface> SectionList = new List<SectionInterface>();

		public SqliteDataAccess SqliteDA = new SqliteDataAccess("./LocalDb.db");
		CommandBinding NewCmd = new CommandBinding();


		public AdminPage(MainWindow window)
        {
			this.mainWindow = window;
            InitializeComponent();

			// instantiate sections
			this.mainGridPage = new MainGridPage(this);
			this.mainStatPage = new MainStatPage(this);

			// set frames content
			MainGridFrame.Content = this.mainGridPage;
			MainStatFrame.Content = this.mainStatPage;

			// save in a section list to keep track of all sections
			SectionList.Add(this.mainGridPage);
			SectionList.Add(this.mainStatPage);
		}

		// on section change
		public void pickedPageEvent(object sender, RoutedEventArgs e)
		{
			if (mainGridPage.memberCreationPageIsVisible) // hide Member creation page if shown
				mainGridPage.memberCreationPage.ExitEvent(null, null);

			MainTransitioner.SelectedIndex = ((ListBox)sender).SelectedIndex; // change section page
			SectionList[((ListBox)sender).SelectedIndex].UpdateData(); // triger on show Section event
		}

		/*private void OpenMenuButton_Click(object sender, RoutedEventArgs e)
		{
			OpenMenuButton.Visibility = Visibility.Collapsed;
			CloseMenuButton.Visibility = Visibility.Visible;
		}

		private void CloseMenuButton_Click(object sender, RoutedEventArgs e)
		{
			OpenMenuButton.Visibility = Visibility.Visible;
			CloseMenuButton.Visibility = Visibility.Collapsed;
		}*/
	}
}
