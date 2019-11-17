using MaterialDesignThemes.Wpf;
using MoustakbalManager.Objects;
using MoustakbalManager.Pages.GridPage;
using MoustakbalManager.Tools;
using MoustakbalManager.Tools.DataConverters;
using MoustakbalManager.Tools.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MoustakbalManager.Pages.AdminPages
{
    public partial class MembersPage : Page
    {
        public MainGridPage mainGridPage;
        public List<Member> members = new List<Member>();
		public List<Wilaya> wilayas;
		public List<Commune> cCommunes;
		public List<Commune> bCommunes;
		public List<ActivityArea> activityAreas;
		public List<Job> jobList;
		public List<StuddyLvl> studdyLvls;
		public int membersCount;
		public int pagesNumber;
		public int focussedMemberId; // id of edit/delete member 
		public int[] paginationArray; // contain visible pages number
		private List<MaterialDataGridTextColumn> gridColumns = new List<MaterialDataGridTextColumn>();

		// filtering variables --------------------------------
		private int pageIndex = 1;
		private int F_MaxLength = 10; // number of result 
		public String F_Name, F_LastName, F_DateStart, F_DateEnd, F_JoinDateStart, F_JoinDateEnd, F_InsDateStart, F_InsDateEnd;
		public int F_Sex, F_BWilaya, F_BCommune, F_CWilaya, F_CCommune, F_StdLvl, F_Job, F_WorkingArea;

		// ----------------------------------------------------

		//private IdToWilaya idToWilayaConverter;


		// Constructor
		public MembersPage(MainGridPage mainGridPage)
		{
			InitializeComponent();
			// initialize value converters
			//IdToWilaya idToWilayaConverter = new IdToWilaya(this);

			DeleteDialogFrame.Content = new DeleteDialog(this);
			GenerateDialogFrame.Content = new GenerateDialog(this);

			// get tool data from DB
			this.studdyLvls = mainGridPage.adminPage.SqliteDA.GetStuddyLvls();
			this.wilayas = mainGridPage.adminPage.SqliteDA.GetWilayas();
			this.activityAreas = mainGridPage.adminPage.SqliteDA.GetActivityAreas();

			this.mainGridPage = mainGridPage;
			WilayaBirthComboBox.ItemsSource = CurrentWilayaComboBox.ItemsSource = this.wilayas.Select(o => o.Name).ToList();
			StdLvlComboBox.ItemsSource = this.studdyLvls.Select(o => o.Name).ToList();
			workingSectorComboBox.ItemsSource = this.activityAreas.Select(o => o.Name).ToList();

			this.gridColumns.Add(NameColumn);
			this.gridColumns.Add(LastNameColumn);
			this.gridColumns.Add(SexColumn);
			this.gridColumns.Add(BDateColumn);
			this.gridColumns.Add(BWilayaColumn);
			this.gridColumns.Add(BCommuneColumn);
			this.gridColumns.Add(StdLvlColumn);
			this.gridColumns.Add(FunctionColumn);
			this.gridColumns.Add(WorkAreaColumn);
			this.gridColumns.Add(CWilayaColumn);
			this.gridColumns.Add(CCommuneColumn);
			this.gridColumns.Add(JoinDateColumn);

			RefreshFilteringData();
			RefreshResultCount();
			RefreshPagination();
			RefreshGridData();
        }


        public void RefreshGridData()
        {
			mainGridPage.adminPage.SqliteDA.UpdateMemberList(members,pageIndex, F_MaxLength, F_Name, F_LastName, F_DateStart, F_DateEnd, F_Sex, F_BWilaya, F_BCommune, F_CWilaya, F_CCommune,F_StdLvl, F_Job, F_WorkingArea, F_JoinDateStart, F_JoinDateEnd, F_InsDateStart, F_InsDateEnd);// get data from DB
			MembersGrid.ItemsSource = this.members; // set grid data
			MembersGrid.Items.Refresh(); // refresh grid items
		}

		public void RefreshFilteringData()
		{
			F_MaxLength = Int32.Parse(((ComboBoxItem)LengthFilterBox.SelectedItem).Content.ToString());// save selected page content length
			F_Name = NameTextBox.Text;
			F_LastName = LastNameTextBox.Text;

			F_DateStart = StartBirthDatePicker.Text;
			F_DateEnd = EndBirthDatePicker.Text;
            

			F_Sex = SexComboBox.SelectedIndex;

			F_BWilaya = WilayaBirthComboBox.SelectedIndex > -1 ? wilayas[WilayaBirthComboBox.SelectedIndex].ID : -1;
			F_BCommune = CommuneBirthComboBox.SelectedIndex > -1 ? bCommunes[CommuneBirthComboBox.SelectedIndex].ID : -1;
			F_CWilaya = CurrentWilayaComboBox.SelectedIndex > -1 ? wilayas[CurrentWilayaComboBox.SelectedIndex].ID: -1;
			F_CCommune = CurrentCommuneComboBox.SelectedIndex > -1 ? cCommunes[CurrentCommuneComboBox.SelectedIndex].ID: -1;
			F_StdLvl = StdLvlComboBox.SelectedIndex > -1 ? studdyLvls[StdLvlComboBox.SelectedIndex].ID: -1;
			F_Job = FunctionComboBox.SelectedIndex > -1 ? jobList[FunctionComboBox.SelectedIndex].ID: -1;
			F_WorkingArea = workingSectorComboBox.SelectedIndex > -1 ? activityAreas[workingSectorComboBox.SelectedIndex].ID: -1;

			F_JoinDateStart = StartJoinDatePicker.Text;
			F_JoinDateEnd = EndJoinDatePicker.Text;
			F_InsDateStart = StartRegisterDatePicker.Text;
			F_InsDateEnd = EndRegisterDatePicker.Text;
		}

		public void RefreshPagination()
		{
			PageListBox.Items.Clear();

			pagesNumber = (int)Math.Ceiling((float)membersCount / (float)F_MaxLength);
			//Console.WriteLine((float)membersCount +" / "+ (float)F_MaxLength);
			paginationArray = Pagination.GetPaginationArray(pageIndex, pagesNumber, 3);

			// fill Pages ListBox
			for (int i = 0; i < paginationArray.Length; i++)
			{
				ListBoxItem newListBoxItem = new ListBoxItem();
				newListBoxItem.Content = paginationArray[i];
				PageListBox.Items.Add(newListBoxItem);
			}

			// reposition page index
			if (pagesNumber >= pageIndex) // if selected page is available
			{
				PageListBox.SelectedIndex = Pagination.GetPageIndexFromArray(pageIndex, paginationArray);
			}
			else // set index to last page
			{
				if(paginationArray.Length > 0)// not empty
				{
					pageIndex = paginationArray[paginationArray.Length - 1];
					PageListBox.SelectedIndex = paginationArray.Length - 1;
				}
			}
		}	
		
		public void RefreshResultCount()
		{
			membersCount = mainGridPage.adminPage.SqliteDA.GetMembersCount(F_Name, F_LastName, F_DateStart, F_DateEnd, F_Sex, F_BWilaya, F_BCommune, F_CWilaya, F_CCommune,F_StdLvl, F_Job, F_WorkingArea, F_JoinDateStart, F_JoinDateEnd, F_InsDateStart, F_InsDateEnd);

			ResultCountBadge.Badge = membersCount + " trouvée";
		}

		


		// WPF USER EVENTS
		public void CreateMemberBtnPressedEvent(object sender, RoutedEventArgs e)
		{
			mainGridPage.memberCreationPageIsVisible = true;
			this.mainGridPage.memberCreationPage.InitializeCreationPage();
			this.mainGridPage.GridTransitioner.SelectedIndex = 1; // transition to member creation page
		}

		public void EditMemberBtnPressedEvent(object sender, RoutedEventArgs e)
		{
			mainGridPage.memberCreationPageIsVisible = true;
			this.focussedMemberId = MemberManager.GetMemberById(Int32.Parse((sender as Button).Tag.ToString()), this.members); // save focussed member id
			this.mainGridPage.memberCreationPage.InitializeEditPage(this.members[this.focussedMemberId]); // initialise page : need current member
			this.mainGridPage.GridTransitioner.SelectedIndex = 1; // transition to member edit page
		}

		public void DeleteMemberBtnPressedEvent(object sender, RoutedEventArgs e)
		{
			this.focussedMemberId = MemberManager.GetMemberById(Int32.Parse((sender as Button).Tag.ToString()),this.members); // save focussed member id
		}

		public void FilterBtnClickedEvent(object sender, RoutedEventArgs e)
		{
			RefreshFilteringData();
			RefreshResultCount();
			RefreshPagination();
			RefreshGridData();
		}

		public void SelectedPageEvent(object sender, RoutedEventArgs e)
		{
			if (PageListBox.SelectedIndex != -1)// SelectedIndex == -1 when selecting the same page
			{
				pageIndex = paginationArray[PageListBox.SelectedIndex];
			}
			else // go back to the 1st page
			{
				pageIndex = 1;
			}

			RefreshGridData();
			RefreshPagination();
			
		}

		public void MemberDeleted()
		{
			if (mainGridPage.adminPage.SqliteDA.DeleteMember(this.members[this.focussedMemberId].ID) != 0)
			{
				RefreshResultCount();
				RefreshPagination();
				RefreshGridData();
			}
			else
			{
				Console.WriteLine("Error SQL Deleting");
			}
		}

		public void ClearFilterBtnClickedEvent(object sender, RoutedEventArgs e)
		{
			NameTextBox.Text = "";
			LastNameTextBox.Text = "";
			
			StartBirthDatePicker.Text = "";
			EndBirthDatePicker.Text = "";
			StartJoinDatePicker.Text = "";
			EndJoinDatePicker.Text = "";
			StartRegisterDatePicker.Text = "";
			EndRegisterDatePicker.Text = "";

			FunctionComboBox.SelectedValue = null;
			WilayaBirthComboBox.SelectedValue = null;
			CommuneBirthComboBox.SelectedValue = null;
			CurrentWilayaComboBox.SelectedValue = null;
			CurrentCommuneComboBox.SelectedValue = null;
			StdLvlComboBox.SelectedValue = null;
			workingSectorComboBox.SelectedValue = null;

			SexComboBox.SelectedIndex = 0;
		}

		public void columnHeader_Click(object sender, RoutedEventArgs e) // ORDER BY X
		{
			Console.WriteLine(((DataGridColumnHeader)sender).TabIndex);
		}

		

		public void CheckBoxEvent(object sender, RoutedEventArgs e)
		{
			try
			{
				gridColumns[Convert.ToInt32(((CheckBox)sender).Tag)].Visibility = Visibility.Visible;
			} catch (Exception) { } 
		}

		public void UnCheckBoxEvent(object sender, RoutedEventArgs e)
		{
			try
			{
				gridColumns[Convert.ToInt32(((CheckBox)sender).Tag)].Visibility = Visibility.Hidden;
			} catch (Exception) { }
		}

		public void BirthWilayaSelectedEvent(object sender, RoutedEventArgs e)
		{
			this.bCommunes = mainGridPage.adminPage.SqliteDA.GetCommunes(((ComboBox)sender).SelectedIndex+1);
			CommuneBirthComboBox.ItemsSource = this.bCommunes.Select(o => o.Name).ToList();
		}

		public void CurrentWilayaSelectedEvent(object sender, RoutedEventArgs e)
		{
			this.cCommunes = mainGridPage.adminPage.SqliteDA.GetCommunes(((ComboBox)sender).SelectedIndex+1);
			CurrentCommuneComboBox.ItemsSource = this.cCommunes.Select(o => o.Name).ToList();
		}

		public void workingSectorSelectedEvent(object sender, RoutedEventArgs e)
		{
			this.jobList = mainGridPage.adminPage.SqliteDA.GetJobList(((ComboBox)sender).SelectedIndex + 1);
			FunctionComboBox.ItemsSource = this.jobList.Select(o => o.Name).ToList();
		}

		public void GenerateExcelClick(object sender, RoutedEventArgs e)
		{
			GenerateDialog.IsOpen = true;
		}

		


	}
}
