using MoustakbalManager.Pages.AdminPages;
using System.Windows;
using System.Windows.Controls;

namespace MoustakbalManager.Pages.GridPage
{

	public partial class DeleteDialog : UserControl
    {
		private MembersPage membersPage;

		public DeleteDialog(MembersPage membersPage)
        {
            InitializeComponent();

			this.membersPage = membersPage;
        }

		
		public void ConfirmDeleteBtnPressedEvent(object sender, RoutedEventArgs e)
		{
			this.membersPage.MemberDeleted();
		}
	}
}
