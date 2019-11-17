using MoustakbalManager.Objects;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using MoustakbalManager.Pages.GridPage;

namespace MoustakbalManager.Pages.AdminPages
{
	public partial class MemberCreationPage : Page
	{
		private MainGridPage mainGridPage;
		public Member resultingMember = new Member(); // contain member data when confirming editing/creation
		private Boolean isEdit = false;

		public List<Commune> cCommunes;
		public List<Commune> bCommunes;
		public List<Job> jobList;

		public MemberCreationPage(MainGridPage mainGridPage)
		{
			this.mainGridPage = mainGridPage;
			InitializeComponent();

			BirthWilayaComboBox.ItemsSource = CurrentWilayaComboBox.ItemsSource = mainGridPage.membersPage.wilayas.Select(o => o.Name).ToList();
			StudyLevelComboBox.ItemsSource = mainGridPage.membersPage.studdyLvls.Select(o => o.Name).ToList();
			workingSectorComboBox.ItemsSource = mainGridPage.membersPage.activityAreas.Select(o => o.Name).ToList();
		}

		public void InitializeCreationPage()
		{
			// fill form values 
			NameTextBox.Text = "";
			LastNameTextBox.Text = "";
			BirthAdressTextBox.Text = "";
			BirthWilayaComboBox.SelectedValue = null;
			BirthCommuneComboBox.SelectedValue = null;
			BirthDatePicker.SelectedDate = null;
			JoinedDatePicker.SelectedDate = null;
			EMailTextBox.Text = "";
			PhoneTextBox.Text = "";
			CurrentAdressTextBox.Text = "";
			CurrentWilayaComboBox.SelectedValue = null;
			CurrentCommuneComboBox.SelectedValue = null;
			StudyLevelComboBox.SelectedValue = null;
			workingSectorComboBox.SelectedValue = null;
			FunctionComboBox.SelectedValue = null;
			SexToggleButton.IsChecked = false;

			// other
			this.isEdit = false;
		}

		public void InitializeEditPage(Member selectedMember)
		{
			// fill form values 
			SexToggleButton.IsChecked = selectedMember.Sex;//selectedMember.Sex == 'F';

			NameTextBox.Text = selectedMember.Name;
			LastNameTextBox.Text = selectedMember.LastName;
			BirthAdressTextBox.Text = selectedMember.BirthPlace.adress;
			BirthDatePicker.Text = selectedMember.BirthDate;
			JoinedDatePicker.Text = selectedMember.JoinDate;
			EMailTextBox.Text = selectedMember.EMail;
			PhoneTextBox.Text = selectedMember.PhoneNumber;
			CurrentAdressTextBox.Text = selectedMember.CurrentAdress.adress;

			// get index of wilaya in wilayaList
			BirthWilayaComboBox.SelectedIndex = mainGridPage.membersPage.wilayas.FindIndex(Wilaya => Wilaya.ID == selectedMember.BirthPlace.wilaya.ID);
			BirthCommuneComboBox.SelectedIndex = bCommunes.FindIndex(Commune => Commune.ID == selectedMember.BirthPlace.commune.ID);
			CurrentWilayaComboBox.SelectedIndex = mainGridPage.membersPage.wilayas.FindIndex(Wilaya => Wilaya.ID == selectedMember.CurrentAdress.wilaya.ID);
			CurrentCommuneComboBox.SelectedIndex = cCommunes.FindIndex(Commune => Commune.ID == selectedMember.CurrentAdress.commune.ID);
			StudyLevelComboBox.SelectedIndex = mainGridPage.membersPage.studdyLvls.FindIndex(StuddyLvl => StuddyLvl.ID == selectedMember.studyLevel.ID);
			workingSectorComboBox.SelectedIndex = mainGridPage.membersPage.activityAreas.FindIndex(ActivityArea => ActivityArea.ID == selectedMember.ActivityArea.ID);
			FunctionComboBox.SelectedIndex = jobList.FindIndex(Job => Job.ID == selectedMember.Fonction.ID);

			// other
			this.isEdit = true;
		}


		public Boolean ConfirmMember()
		{
			// fill resultingMember data
			this.resultingMember.Name = NameTextBox.Text;
			this.resultingMember.LastName = LastNameTextBox.Text;
			this.resultingMember.Sex = (Boolean)SexToggleButton.IsChecked; //(Boolean)SexToggleButton.IsChecked? 'F' : 'M';


			this.resultingMember.BirthPlace.adress = BirthAdressTextBox.Text;
			this.resultingMember.BirthPlace.wilaya.ID = mainGridPage.membersPage.wilayas[BirthWilayaComboBox.SelectedIndex].ID;
			this.resultingMember.BirthPlace.commune.ID = bCommunes[BirthCommuneComboBox.SelectedIndex].ID;
			this.resultingMember.BirthDate = BirthDatePicker.Text;
			this.resultingMember.JoinDate = JoinedDatePicker.Text;

			this.resultingMember.EMail = EMailTextBox.Text;
			this.resultingMember.PhoneNumber = PhoneTextBox.Text;
			this.resultingMember.CurrentAdress.adress = CurrentAdressTextBox.Text;
			this.resultingMember.CurrentAdress.wilaya.ID = mainGridPage.membersPage.wilayas[CurrentWilayaComboBox.SelectedIndex].ID;
			this.resultingMember.CurrentAdress.commune.ID = cCommunes[CurrentCommuneComboBox.SelectedIndex].ID;
			this.resultingMember.studyLevel.ID = mainGridPage.membersPage.studdyLvls[StudyLevelComboBox.SelectedIndex].ID;
			this.resultingMember.ActivityArea.ID = mainGridPage.membersPage.activityAreas[workingSectorComboBox.SelectedIndex].ID;
			this.resultingMember.Fonction.ID = jobList[FunctionComboBox.SelectedIndex].ID;

			// save changes
			if (this.isEdit)
				return EditMember();
			else
				return CreateMember();
		}

		public Boolean CreateMember()
		{
			if (mainGridPage.adminPage.SqliteDA.CreateMember(this.resultingMember)  != 0)
			{
				return true;
			}
			else
			{
				Console.WriteLine("Error SQL Create");
				return false;
			}
		}

		public Boolean EditMember()
		{
			if (mainGridPage.adminPage.SqliteDA.UpdateMember(this.mainGridPage.membersPage.members[this.mainGridPage.membersPage.focussedMemberId].ID, resultingMember) != 0)
			{
				return true;
			}
			else
			{
				Console.WriteLine("Error SQL UPDATE");
				return false;
			}
		}





		// WPF USER EVENTS
		public void ConfirmEvent(object sender, RoutedEventArgs e) // VALIDATION !!!!
		{
			if (BirthWilayaComboBox.SelectedValue == null ||
				BirthCommuneComboBox.SelectedValue == null ||
				CurrentWilayaComboBox.SelectedValue == null ||
				CurrentCommuneComboBox.SelectedValue == null ||
				StudyLevelComboBox.SelectedValue == null ||
				BirthDatePicker.SelectedDate == null)
			{
				Console.WriteLine("Empty values !!!");
			}
			else
			{
				if (ConfirmMember())
				{
					this.mainGridPage.membersPage.RefreshResultCount();
					this.mainGridPage.membersPage.RefreshPagination(); // update page number
					this.mainGridPage.membersPage.RefreshGridData(); // update GRID data

					//this.mainGridPage.GridTransitioner.SelectedIndex = 0; // switch back to member list page
					ExitEvent(null, null);
				}
			}
		}

		public void ExitEvent(object sender, RoutedEventArgs e)
		{
			mainGridPage.memberCreationPageIsVisible = true;

			// to hide validation errors
			NameTextBox.Text = "_";
			LastNameTextBox.Text = "_";
			BirthAdressTextBox.Text = "_";
			EMailTextBox.Text = "adress@provider.com";
			CurrentAdressTextBox.Text = "_";
			PhoneTextBox.Text = "000000000";

			BirthWilayaComboBox.SelectedIndex = 0;
			BirthCommuneComboBox.SelectedIndex = 0;
			StudyLevelComboBox.SelectedIndex = 0;
			CurrentWilayaComboBox.SelectedIndex = 0;
			CurrentCommuneComboBox.SelectedIndex = 0;
			workingSectorComboBox.SelectedIndex = 0;
			FunctionComboBox.SelectedIndex = 0;

			BirthDatePicker.SelectedDate = JoinedDatePicker.SelectedDate = DateTime.Now;

			this.mainGridPage.GridTransitioner.SelectedIndex = 0; // switch back to member list page
		}


		private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		public void BirthWilayaSelectedEvent(object sender, RoutedEventArgs e)
		{
			this.bCommunes = mainGridPage.adminPage.SqliteDA.GetCommunes(((ComboBox)sender).SelectedIndex + 1);
			BirthCommuneComboBox.ItemsSource = this.bCommunes.Select(o => o.Name).ToList();

		}

		public void CurrentWilayaSelectedEvent(object sender, RoutedEventArgs e)
		{
			this.cCommunes = mainGridPage.adminPage.SqliteDA.GetCommunes(((ComboBox)sender).SelectedIndex + 1);
			CurrentCommuneComboBox.ItemsSource = this.cCommunes.Select(o => o.Name).ToList();
		}

		public void workingSectorSelectedEvent(object sender, RoutedEventArgs e)
		{
			this.jobList = mainGridPage.adminPage.SqliteDA.GetJobList(((ComboBox)sender).SelectedIndex + 1);
			FunctionComboBox.ItemsSource = this.jobList.Select(o => o.Name).ToList();
		}
	}
}
