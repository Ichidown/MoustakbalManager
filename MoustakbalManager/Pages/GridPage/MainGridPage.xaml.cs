using MoustakbalManager.Pages.AdminPages;
using MoustakbalManager.Ressources.Interfaces;
using System;
using System.Windows.Controls;

namespace MoustakbalManager.Pages.GridPage
{
	public partial class MainGridPage : Page, SectionInterface
	{
		public MembersPage membersPage;
		public MemberCreationPage memberCreationPage;
		public AdminPage adminPage;

		public Boolean memberCreationPageIsVisible = false;

		public MainGridPage(AdminPage adminPage)
        {
            InitializeComponent();
			this.adminPage = adminPage;
			this.membersPage = new MembersPage(this);
			this.memberCreationPage = new MemberCreationPage(this);

			MembersFrame.Content = this.membersPage;
			MemberCreationFrame.Content = this.memberCreationPage;
		}

		public void UpdateData()// trigered when this section is viewed
		{
			
		}
	}
}
