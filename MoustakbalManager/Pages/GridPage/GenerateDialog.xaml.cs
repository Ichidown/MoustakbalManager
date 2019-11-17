using MoustakbalManager.Pages.AdminPages;
using System;
using System.Windows.Controls;

//using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;
using Microsoft.Win32;
using OfficeOpenXml;

namespace MoustakbalManager.Pages.GridPage
{
    public partial class GenerateDialog : UserControl
    {
		private MembersPage membersPage;
		public int checkedBoxes = 0;

		public GenerateDialog(MembersPage membersPage)
        {
            InitializeComponent();
			this.membersPage = membersPage;
        }

		private void BoxCheckedEvent(object sender, System.Windows.RoutedEventArgs e)
		{
			checkedBoxes += 1;
		}

		private void BoxUnCheckedEvent(object sender, System.Windows.RoutedEventArgs e)
		{
			checkedBoxes -= 1;
		}

		private void BoxCheckedAllEvent(object sender, System.Windows.RoutedEventArgs e)
		{
			NameCheckBox.IsChecked = true;
			LastNameCheckBox.IsChecked = true;
			SexCheckBox.IsChecked = true;
			BirthAdressCheckBox.IsChecked = true;
			BirthWilayaCheckBox.IsChecked = true;
			BirthCommuneCheckBox.IsChecked = true;
			BirthDateCheckBox.IsChecked = true;
			EmailCheckBox.IsChecked = true;
			PhoneCheckBox.IsChecked = true;
			CurrentAdressCheckBox.IsChecked = true;
			CurrentWilayaCheckBox.IsChecked = true;
			CurrentCommuneCheckBox.IsChecked = true;
			StdLvlCheckBox.IsChecked = true;
			WorkAreaCheckBox.IsChecked = true;
			JobCheckBox.IsChecked = true;
			JoinDateCheckBox.IsChecked = true;
			InscriptionDateCheckBox.IsChecked = true;

			Console.WriteLine(checkedBoxes);
		}

		private void BoxUnCheckedAllEvent(object sender, System.Windows.RoutedEventArgs e)
		{
			NameCheckBox.IsChecked = false;
			LastNameCheckBox.IsChecked = false;
			SexCheckBox.IsChecked = false;
			BirthAdressCheckBox.IsChecked = false;
			BirthWilayaCheckBox.IsChecked = false;
			BirthCommuneCheckBox.IsChecked = false;
			BirthDateCheckBox.IsChecked = false;
			EmailCheckBox.IsChecked = false;
			PhoneCheckBox.IsChecked = false;
			CurrentAdressCheckBox.IsChecked = false;
			CurrentWilayaCheckBox.IsChecked = false;
			CurrentCommuneCheckBox.IsChecked = false;
			StdLvlCheckBox.IsChecked = false;
			WorkAreaCheckBox.IsChecked = false;
			JobCheckBox.IsChecked = false;
			JoinDateCheckBox.IsChecked = false;
			InscriptionDateCheckBox.IsChecked = false;

			Console.WriteLine(checkedBoxes);
		}

		private void GenerateBtnClick(object sender, System.Windows.RoutedEventArgs e)
		{
			// if nothing is checked : do nothing
			if(checkedBoxes > 0)
			using (ExcelPackage excel = new ExcelPackage())
			{
				string pageName = "Page1";
				excel.Workbook.Worksheets.Add(pageName); // new page
				var excelWorksheet = excel.Workbook.Worksheets[pageName]; // select page
				List<string[]> excelFileContent = new List<string[]>();
				// Determine the header range
				string headerRange = "A1:Z1";

				if ((bool)IncludeHeadersCheckBox.IsChecked) // set headers if allowed
				{
					string[] excelFileHeader = new string[checkedBoxes];
					int headerIndex = 0;

					if ((bool)NameCheckBox.IsChecked) { excelFileHeader[headerIndex] = "Nom"; headerIndex++; };
					if ((bool)LastNameCheckBox.IsChecked){ excelFileHeader[headerIndex] = "Prenom"; headerIndex++; };
					if ((bool)SexCheckBox.IsChecked){ excelFileHeader[headerIndex] = "Sexe"; headerIndex++; };
					if ((bool)BirthDateCheckBox.IsChecked) { excelFileHeader[headerIndex] = "Date de naissance"; headerIndex++; }

					if ((bool)BirthAdressCheckBox.IsChecked){ excelFileHeader[headerIndex] = "Adresse de naissance"; headerIndex++; }
					if ((bool)BirthCommuneCheckBox.IsChecked) { excelFileHeader[headerIndex] = "Commune de naissance"; headerIndex++; }
					if ((bool)BirthWilayaCheckBox.IsChecked){ excelFileHeader[headerIndex] = "Wilaya de naissance"; headerIndex++; }
					
					if ((bool)StdLvlCheckBox.IsChecked) { excelFileHeader[headerIndex] = "Niveau d'etudes"; headerIndex++; }
					if ((bool)WorkAreaCheckBox.IsChecked) { excelFileHeader[headerIndex] = "Secteur de travail"; headerIndex++; }
					if ((bool)JobCheckBox.IsChecked) { excelFileHeader[headerIndex] = "Fonction"; headerIndex++; }

					if ((bool)CurrentAdressCheckBox.IsChecked) { excelFileHeader[headerIndex] = "Adresse"; headerIndex++; }
					if ((bool)CurrentCommuneCheckBox.IsChecked) { excelFileHeader[headerIndex] = "Commune"; headerIndex++; }
					if ((bool)CurrentWilayaCheckBox.IsChecked) { excelFileHeader[headerIndex] = "Wilaya"; headerIndex++; }

					if ((bool)PhoneCheckBox.IsChecked) { excelFileHeader[headerIndex] = "Telephone"; headerIndex++; }
					if ((bool)EmailCheckBox.IsChecked){ excelFileHeader[headerIndex] = "Email"; headerIndex++; }
					
					if ((bool)JoinDateCheckBox.IsChecked){ excelFileHeader[headerIndex] = "Date de recrutement"; headerIndex++; }
					if ((bool)InscriptionDateCheckBox.IsChecked) { excelFileHeader[headerIndex] = "Date d'inscription"; }
						excelFileContent.Add(excelFileHeader);
					
					// styling header
					excelWorksheet.Cells[headerRange].Style.Font.Bold = true;
					excelWorksheet.Cells[headerRange].Style.Font.Size = 14;
					excelWorksheet.Cells[headerRange].Style.Font.Color.SetColor(System.Drawing.Color.Green);
				}


					// Get all filtered records from DB
					excelFileContent.AddRange(
				membersPage.mainGridPage.adminPage.SqliteDA.GetFullMemberList(
					membersPage.F_Name,
					membersPage.F_LastName,
					membersPage.F_DateStart,
					membersPage.F_DateEnd,
					membersPage.F_Sex,
					membersPage.F_BWilaya,
					membersPage.F_BCommune,
					membersPage.F_CWilaya,
					membersPage.F_CCommune,
					membersPage.F_StdLvl,
					membersPage.F_Job,
					membersPage.F_WorkingArea,
					membersPage.F_JoinDateStart,
					membersPage.F_JoinDateEnd,
					membersPage.F_InsDateStart,
					membersPage.F_InsDateEnd,

					(bool)NameCheckBox.IsChecked,
					(bool)LastNameCheckBox.IsChecked,
					(bool)SexCheckBox.IsChecked,
					(bool)BirthAdressCheckBox.IsChecked,
					(bool)BirthWilayaCheckBox.IsChecked,
					(bool)BirthCommuneCheckBox.IsChecked,
					(bool)BirthDateCheckBox.IsChecked,
					(bool)EmailCheckBox.IsChecked,
					(bool)PhoneCheckBox.IsChecked,
					(bool)CurrentAdressCheckBox.IsChecked,
					(bool)CurrentWilayaCheckBox.IsChecked,
					(bool)CurrentCommuneCheckBox.IsChecked,
					(bool)StdLvlCheckBox.IsChecked,
					(bool)WorkAreaCheckBox.IsChecked,
					(bool)JobCheckBox.IsChecked,
					(bool)JoinDateCheckBox.IsChecked,
					(bool)InscriptionDateCheckBox.IsChecked
				));

				// load data
				excelWorksheet.Cells[headerRange].LoadFromArrays(excelFileContent);

				// initialize file saver dialog
				SaveFileDialog dlg = new SaveFileDialog();
				dlg.FileName = "Document"; // Default file name
				dlg.DefaultExt = ".xlsx"; // Default file extension
				dlg.Filter = "Fichier Excel (.xlsx)|*.xlsx"; // Filter files by extension

				// Show save file dialog box
				Nullable<bool> result = dlg.ShowDialog();

				// Process save file dialog box results
				if (result == true)
				{
					FileInfo excelFile = new FileInfo(@dlg.FileName);
					excel.SaveAs(excelFile);
					// put popUp exit command here
				}

			}
		}



	}
}