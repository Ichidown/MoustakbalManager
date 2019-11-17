using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;


namespace MoustakbalManager
{
    public partial class LoginPage : Page//, INotifyPropertyChanged
	{
        private MainWindow mainWindow;
        

        private String UserName = "admin";
		private String Password = "pass";

		//public event PropertyChangedEventHandler PropertyChanged;

		// public String TextBoxUserName { get; set; }
		// private string _TextBoxUserName;

		/*public string TextBoxUserName
		{
			get { return _TextBoxUserName; }
			set
			{
				if (value != _TextBoxUserName)
				{
					_TextBoxUserName = value;
					OnPropertyChanged("TextBoxUserName");
				}
			}
		}*/

		//public String TextBoxPassword="Pass";

		public LoginPage(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
			UserBox.DataContext = this;


            
        }
        


        // load admin page content then navigate to it
        private void Navigate_To_Admin_Page(object sender, RoutedEventArgs e)
        {
			CardFlipper.IsFlipped = false;
		}

		private void Identify(object sender, RoutedEventArgs e)
		{
			if(UserBox.Text == UserName && PasswordBox.Password == Password)
			{
				this.mainWindow.LoadAdminPage();
				this.mainWindow.MainTransitioner.SelectedIndex = 1;
			} else
			{
				CardFlipper.IsFlipped = true;
			}
		}


		
		


	}
}
