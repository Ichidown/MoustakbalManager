using MoustakbalManager.Pages;
using System.Windows;
using System.Windows.Controls;



namespace MoustakbalManager
{
    public partial class MainWindow : Window
    {
        // public Page CurrentPage;
        private Page loginPage, adminPage;


        public MainWindow()
        {
            InitializeComponent();
            // initialize login page
			this.loginPage = new LoginPage(this);
            MainFrame.Content = this.loginPage;

			//this.adminPage = new AdminPage(this);
			//MainFrame.Content = this.adminPage;
		}

		public void LoadAdminPage()
        {
            this.adminPage = new AdminPage(this);
            AdminFrame.Content = this.adminPage;
        }

        

       
    }
    

}
