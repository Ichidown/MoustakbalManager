using MoustakbalManager.Objects;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MoustakbalManager.Tools;
using System.Text;
using MoustakbalManager.Ressources;

namespace MoustakbalManager.Tools
{
	public class SqliteDataAccess
	{
		private String SqliteConnectionString;
		private String query;
		private String filterQuery;

		public SqliteDataAccess(String DataBaseLocation = "./LocalDb.db")
		{
			SetDataBaseLocation(DataBaseLocation);
		}

		public void SetDataBaseLocation(String DataBaseLocation)
		{
			SqliteConnectionString = "Data Source=" + DataBaseLocation + ";Version=3;";
		}

		// Member List
		public void UpdateMemberList(List<Member> members, int pageIndex, int maxLength, String F_Name, String F_LastName, String F_DateStart, String F_DateEnd, int F_Sex, int F_BWilaya, int F_BCommune, int F_CWilaya, int F_CCommune, int F_StdLvl, int F_Job, int F_WorkingArea, String F_JoinDateStart, String F_JoinDateEnd, String F_InsDateStart, String F_InsDateEnd)
		{

			query = "SELECT "+
				"ID," +
				"Name," +
				"LastName," +
				"Sex," +
				"BirthDate," +
				"B_adress," +
				"B_commune, (select value from commune where id = Members.B_commune) as B_commune_Value," +
				"B_wilaya, (select value from region where id = Members.B_wilaya) as B_wilaya_Value," +
				"StudyLevel, (select value from studdy_lvl where id = Members.StudyLevel) as StudyLevel_Value," +
				"Activity_area, (select name from ActivityAreas where id = Members.Activity_area) as Activity_area_Value," +
				"Fonction, (select name from jobs where id = Members.Fonction) as Fonction_Value," +
				"C_adress, " +
				"C_commune, (select value from commune where id = Members.C_commune) as C_commune_Value," +
				"C_wilaya, (select value from region where id = Members.C_wilaya) as C_wilaya_Value," +
				"PhoneNumber," +
				"EMail," +
				"Join_Date," +
				"Inscription_Date_Time" +
				" FROM Members";
			query += FilterQuery(F_Name, F_LastName, F_DateStart, F_DateEnd, F_Sex, F_BWilaya, F_BCommune, F_CWilaya, F_CCommune, F_StdLvl, F_Job, F_WorkingArea, F_JoinDateStart, F_JoinDateEnd, F_InsDateStart, F_InsDateEnd);
			query += " LIMIT " + (pageIndex - 1) * maxLength + "," + maxLength; // page length


			using (SQLiteConnection con = new SQLiteConnection(SqliteConnectionString))
			{
				using (SQLiteCommand cmd = new SQLiteCommand(query , con))
				{
					con.Open();
					using (SQLiteDataReader rdr = cmd.ExecuteReader())
					{
						members.Clear(); // emty list before using it
						
						while (rdr.Read())
						{
							members.Add(new Member(Int32.Parse(rdr["ID"].ToString()),
								(string)rdr["Name"],
								(string)rdr["LastName"],
								Convert.ToInt32(rdr["Sex"]) != 0,
								(string)rdr["B_adress"],
								new Wilaya(Int32.Parse(rdr["B_wilaya"].ToString()), (string)rdr["B_wilaya_Value"]),
								new Commune(Int32.Parse(rdr["B_commune"].ToString()), Int32.Parse(rdr["B_wilaya"].ToString()), (string)rdr["B_commune_Value"]),
								(string)rdr["BirthDate"],
								new StuddyLvl(Int32.Parse(rdr["studyLevel"].ToString()),(string)rdr["studyLevel_Value"]),
								new Job(Int32.Parse(rdr["Fonction"].ToString()), Int32.Parse(rdr["Activity_area"].ToString()), (string)rdr["Fonction_Value"]),
								new ActivityArea(Int32.Parse(rdr["Activity_area"].ToString()), (string)rdr["Activity_area_Value"]),
								(string)rdr["EMail"],
								(string)rdr["C_adress"],
								new Wilaya(Int32.Parse(rdr["C_wilaya"].ToString()), (string)rdr["C_wilaya_Value"]),
								new Commune(Int32.Parse(rdr["C_commune"].ToString()), Int32.Parse(rdr["C_wilaya"].ToString()), (string)rdr["C_commune_Value"]),
								(string)rdr["PhoneNumber"],
								(string)rdr["Join_Date"],
								(string)rdr["Inscription_Date_Time"]));
						}
					}
					con.Close();
					
				}
			}
		}

		// Create Member
		public int CreateMember(Member member)
		{
			using (SQLiteConnection con = new SQLiteConnection(SqliteConnectionString))
			{
				using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Members (Name,LastName,Sex," +
				"B_adress,B_wilaya,B_commune," +
				"BirthDate,StudyLevel,Fonction,Activity_area,EMail," +
				"C_adress,C_wilaya,C_commune,PhoneNumber,Join_Date,Inscription_Date_Time)" +
				"VALUES (@Name,@LastName,@Sex,@B_adress,@B_wilaya,@B_commune,@BirthDate,@StudyLevel,@Fonction,@Activity_area,@EMail,@C_adress,@C_wilaya,@C_commune,@PhoneNumber,@Join_Date,@Inscription_Date_Time)", con))
				{
					cmd.Parameters.Add(new SQLiteParameter("@Name", member.Name));
					cmd.Parameters.Add(new SQLiteParameter("@LastName", member.LastName));
					cmd.Parameters.Add(new SQLiteParameter("@Sex", member.Sex));
					cmd.Parameters.Add(new SQLiteParameter("@B_adress", member.BirthPlace.adress));
					cmd.Parameters.Add(new SQLiteParameter("@B_wilaya", member.BirthPlace.wilaya.ID));
					cmd.Parameters.Add(new SQLiteParameter("@B_commune", member.BirthPlace.commune.ID));
					cmd.Parameters.Add(new SQLiteParameter("@BirthDate", member.BirthDate));
					cmd.Parameters.Add(new SQLiteParameter("@StudyLevel", member.studyLevel.ID));
					cmd.Parameters.Add(new SQLiteParameter("@Fonction", member.Fonction.ID));
					cmd.Parameters.Add(new SQLiteParameter("@Activity_area", member.ActivityArea.ID));
					cmd.Parameters.Add(new SQLiteParameter("@EMail", member.EMail));
					cmd.Parameters.Add(new SQLiteParameter("@C_adress", member.CurrentAdress.adress));
					cmd.Parameters.Add(new SQLiteParameter("@C_wilaya", member.CurrentAdress.wilaya.ID));
					cmd.Parameters.Add(new SQLiteParameter("@C_commune", member.CurrentAdress.commune.ID));
					cmd.Parameters.Add(new SQLiteParameter("@PhoneNumber", member.PhoneNumber));
					cmd.Parameters.Add(new SQLiteParameter("@Join_Date", member.JoinDate));
					cmd.Parameters.Add(new SQLiteParameter("@Inscription_Date_Time", member.InscriptionDateTime));

					con.Open();
					// int responce = cmd.ExecuteNonQuery();
					cmd.ExecuteScalar();
					int responce = (int)con.LastInsertRowId;
					con.Close();
					
					return responce;
				}
			}
		}
		
		// Delete Member
		public int DeleteMember(int MemberId)
		{
			using (SQLiteConnection con = new SQLiteConnection(SqliteConnectionString))
			{
				SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Members WHERE ID="+MemberId, con);
				con.Open();
				int responce = cmd.ExecuteNonQuery();
				con.Close();

				return responce;
			}
		}

		// Update Member
		public int UpdateMember(int MemberId, Member member)
		{
			using (SQLiteConnection con = new SQLiteConnection(SqliteConnectionString))
			{

				using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Members SET Name=@Name , LastName=@LastName , Sex=@Sex," +
				"B_adress=@B_adress , B_wilaya=@B_wilaya , B_commune=@B_commune ," +
				"BirthDate=@BirthDate , StudyLevel=@StudyLevel , Fonction=@Fonction , Activity_area=@Activity_area , EMail=@EMail ," +
				"C_adress=@C_adress , C_wilaya=@C_wilaya , C_commune=@C_commune , PhoneNumber=@PhoneNumber , Join_Date=@Join_Date WHERE id= " + MemberId, con))
				{
					cmd.Parameters.Add(new SQLiteParameter("@Name", member.Name));
					cmd.Parameters.Add(new SQLiteParameter("@LastName", member.LastName));
					cmd.Parameters.Add(new SQLiteParameter("@Sex", member.Sex));
					cmd.Parameters.Add(new SQLiteParameter("@B_adress", member.BirthPlace.adress));
					cmd.Parameters.Add(new SQLiteParameter("@B_wilaya", member.BirthPlace.wilaya.ID));
					cmd.Parameters.Add(new SQLiteParameter("@B_commune", member.BirthPlace.commune.ID));
					cmd.Parameters.Add(new SQLiteParameter("@BirthDate", member.BirthDate));
					cmd.Parameters.Add(new SQLiteParameter("@StudyLevel", member.studyLevel.ID));
					cmd.Parameters.Add(new SQLiteParameter("@Fonction", member.Fonction.ID));
					cmd.Parameters.Add(new SQLiteParameter("@Activity_area", member.ActivityArea.ID));
					cmd.Parameters.Add(new SQLiteParameter("@EMail", member.EMail));
					cmd.Parameters.Add(new SQLiteParameter("@C_adress", member.CurrentAdress.adress));
					cmd.Parameters.Add(new SQLiteParameter("@C_wilaya", member.CurrentAdress.wilaya.ID));
					cmd.Parameters.Add(new SQLiteParameter("@C_commune", member.CurrentAdress.commune.ID));
					cmd.Parameters.Add(new SQLiteParameter("@PhoneNumber", member.PhoneNumber));
					cmd.Parameters.Add(new SQLiteParameter("@Join_Date", member.JoinDate));
					//cmd.Parameters.Add(new SQLiteParameter("@Inscription_Date_Time", member.InscriptionDateTime));

					con.Open();
					int responce = cmd.ExecuteNonQuery();
					con.Close();

					return responce;
				}
			}
		}

		// Get number of filtered records
		public int GetMembersCount(String F_Name, String F_LastName, String F_DateStart, String F_DateEnd, int F_Sex, int F_BWilaya, int F_BCommune, int F_CWilaya, int F_CCommune, int F_StdLvl, int F_Job, int F_WorkingArea, String F_JoinDateStart, String F_JoinDateEnd, String F_InsDateStart, String F_InsDateEnd)
		{
			query = "SELECT count(ID) FROM Members ";
			query += FilterQuery(F_Name, F_LastName, F_DateStart, F_DateEnd, F_Sex, F_BWilaya, F_BCommune, F_CWilaya, F_CCommune, F_StdLvl, F_Job, F_WorkingArea, F_JoinDateStart, F_JoinDateEnd, F_InsDateStart, F_InsDateEnd);

			int membersCount = 0;

			using (SQLiteConnection con = new SQLiteConnection(SqliteConnectionString))
			{
				using (SQLiteCommand cmd = new SQLiteCommand(query, con))
				{
					con.Open();
					membersCount = Convert.ToInt32(cmd.ExecuteScalar());
					con.Close();
					return membersCount;
				}
			}
		}

		// Filter assembly
		public String FilterQuery(String F_Name, String F_LastName, String F_DateStart, String F_DateEnd, int F_Sex, int F_BWilaya, int F_BCommune, int F_CWilaya, int F_CCommune, int F_StdLvl, int F_Job, int F_WorkingArea, String F_JoinDateStart, String F_JoinDateEnd, String F_InsDateStart, String F_InsDateEnd)
		{
			Boolean isMultipleConditions = false;
			filterQuery = " WHERE";

			// Name
			if (!string.IsNullOrWhiteSpace(F_Name))
			{
				filterQuery += " Name LIKE '%" + F_Name + "%'";
				isMultipleConditions = true;
			}

			// Last Name
			if (!string.IsNullOrWhiteSpace(F_LastName))
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " LastName LIKE '%" + F_LastName + "%'";
				isMultipleConditions = true;
			}

			// Birth Date
			if (!string.IsNullOrWhiteSpace(F_DateStart) && //in range
				!string.IsNullOrWhiteSpace(F_DateEnd))
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " BirthDate BETWEEN '" + F_DateStart + "' AND '" + F_DateEnd +"'";
				isMultipleConditions = true;
			}
			else if (!string.IsNullOrWhiteSpace(F_DateStart) && // greater than Date Start
						string.IsNullOrWhiteSpace(F_DateEnd))
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " BirthDate >= '" + F_DateStart + "'";
				isMultipleConditions = true;
			}
			else if (string.IsNullOrWhiteSpace(F_DateStart) && // less than Date End
					!string.IsNullOrWhiteSpace(F_DateEnd))
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " BirthDate <= '" + F_DateEnd + "'";
				isMultipleConditions = true;
			}


			// Joined Date
			if (!string.IsNullOrWhiteSpace(F_JoinDateStart) && //in range
				!string.IsNullOrWhiteSpace(F_JoinDateEnd))
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " Join_Date BETWEEN '" + F_JoinDateStart + "' AND '" + F_JoinDateEnd + "'";
				isMultipleConditions = true;
			}
			else if (!string.IsNullOrWhiteSpace(F_JoinDateStart) && // greater than Date Start
						string.IsNullOrWhiteSpace(F_JoinDateEnd))
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " Join_Date >= '" + F_JoinDateStart + "'";
				isMultipleConditions = true;
			}
			else if (string.IsNullOrWhiteSpace(F_JoinDateStart) && // less than Date End
					!string.IsNullOrWhiteSpace(F_JoinDateEnd))
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " Join_Date <= '" + F_JoinDateEnd + "'";
				isMultipleConditions = true;
			}

			// Register Date
			if (!string.IsNullOrWhiteSpace(F_InsDateStart) && //in range
				!string.IsNullOrWhiteSpace(F_InsDateEnd))
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " Inscription_Date_Time BETWEEN '" + F_InsDateStart + "' AND '" + F_InsDateEnd + "'";
				isMultipleConditions = true;
			}
			else if (!string.IsNullOrWhiteSpace(F_InsDateStart) && // greater than Date Start
						string.IsNullOrWhiteSpace(F_InsDateEnd))
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " Inscription_Date_Time >= '" + F_InsDateStart + "'";
				isMultipleConditions = true;
			}
			else if (string.IsNullOrWhiteSpace(F_InsDateStart) && // less than Date End
					!string.IsNullOrWhiteSpace(F_InsDateEnd))
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " Inscription_Date_Time <= '" + F_InsDateEnd + "'";
				isMultipleConditions = true;
			}

			// Sex
			if (F_Sex > 0)
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " Sex = '" + (F_Sex - 1) + "'";
				isMultipleConditions = true;
			}

			// Birth Wilaya
			if (F_BWilaya > -1)
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " B_wilaya = '" + F_BWilaya + "'";
				isMultipleConditions = true;
			}

			// Birth Commune
			if (F_BCommune > -1)
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " B_commune = '" + F_BCommune + "'";
				isMultipleConditions = true;
			}

			// Current Wilaya
			if (F_CWilaya > -1)
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " C_wilaya = '" + F_CWilaya + "'";
				isMultipleConditions = true;
			}

			// Current Commune
			if (F_CCommune > -1)
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " C_commune = '" + F_CCommune + "'";
				isMultipleConditions = true;
			}

			// Studdy Lvl
			if (F_StdLvl > -1)
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " StudyLevel = '" + F_StdLvl + "'";
				isMultipleConditions = true;
			}

			// Job
			if (F_Job > -1)
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " Fonction = '" + F_Job + "'";
				isMultipleConditions = true;
			}

			// Working Area
			if (F_WorkingArea > -1)
			{
				if (isMultipleConditions) filterQuery += " AND ";
				filterQuery += " Activity_area = '" + F_WorkingArea + "'";
				isMultipleConditions = true;
			}

			return filterQuery == " WHERE" ? "" : filterQuery;
		}

		// Wilaya List
		public List<Wilaya> GetWilayas()
		{
			List<Wilaya> wilaya = new List<Wilaya>();

			query = "SELECT * FROM region";

			using (SQLiteConnection con = new SQLiteConnection(SqliteConnectionString))
			{
				using (SQLiteCommand cmd = new SQLiteCommand(query, con))
				{
					con.Open();
					using (SQLiteDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							wilaya.Add(new Wilaya(Int32.Parse(rdr["ID"].ToString()),
								(string)rdr["value"]));
						}
					}
					con.Close();
				}
			}
			return wilaya;
		}

		// Commune List
		public List<Commune> GetCommunes(int idWilaya)
		{
			List<Commune> commune = new List<Commune>();

			query = "SELECT * FROM commune WHERE wilaya_id = " + idWilaya;

			using (SQLiteConnection con = new SQLiteConnection(SqliteConnectionString))
			{
				using (SQLiteCommand cmd = new SQLiteCommand(query, con))
				{
					con.Open();
					using (SQLiteDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							commune.Add(new Commune(
								Int32.Parse(rdr["ID"].ToString()),
								idWilaya,//Int32.Parse(rdr["wilaya_id"].ToString()),
								(string)rdr["value"]));
						}
					}
					con.Close();
				}
			}
			return commune;
		}

		// Activity Area List
		public List<ActivityArea> GetActivityAreas()
		{
			List<ActivityArea> activityAreas = new List<ActivityArea>();

			query = "SELECT * FROM ActivityAreas";

			using (SQLiteConnection con = new SQLiteConnection(SqliteConnectionString))
			{
				using (SQLiteCommand cmd = new SQLiteCommand(query, con))
				{
					con.Open();
					using (SQLiteDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							activityAreas.Add(new ActivityArea(Int32.Parse(rdr["ID"].ToString()),
								(string)rdr["name"]));
						}
					}
					con.Close();
				}
			}
			return activityAreas;
		}

		// Job List
		public List<Job> GetJobList(int idActivityArea)
		{
			List<Job> jobs = new List<Job>();

			query = "SELECT * FROM jobs WHERE id_Activity_Area = " + idActivityArea;

			using (SQLiteConnection con = new SQLiteConnection(SqliteConnectionString))
			{
				using (SQLiteCommand cmd = new SQLiteCommand(query, con))
				{
					con.Open();
					using (SQLiteDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							jobs.Add(new Job(
								Int32.Parse(rdr["ID"].ToString()),
								idActivityArea,//Int32.Parse(rdr["wilaya_id"].ToString()),
								(string)rdr["name"]));
						}
					}
					con.Close();
				}
			}
			return jobs;
		}

		// Studdy Lvl List
		public List<StuddyLvl> GetStuddyLvls()
		{
			List<StuddyLvl> studdyLvls = new List<StuddyLvl>();

			query = "SELECT * FROM studdy_lvl";

			using (SQLiteConnection con = new SQLiteConnection(SqliteConnectionString))
			{
				using (SQLiteCommand cmd = new SQLiteCommand(query, con))
				{
					con.Open();
					using (SQLiteDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							studdyLvls.Add(new StuddyLvl(Int32.Parse(rdr["ID"].ToString()),
								(string)rdr["value"]));
						}
					}
					con.Close();
				}
			}
			return studdyLvls;
		}

		// Member List
		public List<string[]> GetFullMemberList(String F_Name, String F_LastName, String F_DateStart, String F_DateEnd, int F_Sex, int F_BWilaya, int F_BCommune, int F_CWilaya, int F_CCommune, int F_StdLvl, int F_Job, int F_WorkingArea, String F_JoinDateStart, String F_JoinDateEnd, String F_InsDateStart, String F_InsDateEnd,
					bool NameCheckBox,
					bool LastNameCheckBox,
					bool SexCheckBox,
					bool BirthAdressCheckBox,
					bool BirthWilayaCheckBox,
					bool BirthCommuneCheckBox,
					bool BirthDateCheckBox,
					bool EmailCheckBox,
					bool PhoneCheckBox,
					bool CurrentAdressCheckBox,
					bool CurrentWilayaCheckBox,
					bool CurrentCommuneCheckBox,
					bool StdLvlCheckBox,
					bool WorkAreaCheckBox,
					bool JobCheckBox,
					bool JoinDateCheckBox,
					bool InscriptionDateCheckBox)
		{
			List<string[]> resultList = new List<string[]>();

			query = "SELECT " +
				(NameCheckBox?"Name,":"") +
				(LastNameCheckBox ? "LastName," : "") +
				(SexCheckBox ? "Sex," : "") +
				(BirthDateCheckBox ? "BirthDate," : "") +
				(BirthAdressCheckBox ? "B_adress," : "") +
				(BirthCommuneCheckBox ? "(select value from commune where id = Members.B_commune) as B_commune_Value," : "") +
				(BirthWilayaCheckBox ? "(select value from region where id = Members.B_wilaya) as B_wilaya_Value," : "") +
				(StdLvlCheckBox ? "(select value from studdy_lvl where id = Members.StudyLevel) as StudyLevel_Value," : "") +
				(WorkAreaCheckBox ? "(select name from ActivityAreas where id = Members.Activity_area) as Activity_area_Value," : "") +
				(JobCheckBox ? "(select name from jobs where id = Members.Fonction) as Fonction_Value," : "") +
				(CurrentAdressCheckBox ? "C_adress, " : "") +
				(CurrentCommuneCheckBox ? "(select value from commune where id = Members.C_commune) as C_commune_Value," : "") +
				(CurrentWilayaCheckBox ? "(select value from region where id = Members.C_wilaya) as C_wilaya_Value," : "") +
				(PhoneCheckBox ? "PhoneNumber," : "") +
				(EmailCheckBox ? "EMail," : "") +
				(JobCheckBox ? "Join_Date," : "") +
				(InscriptionDateCheckBox ? "Inscription_Date_Time," : "") +
				" FROM Members";

			//correct querry : remove last ','
			query = TextManipulator.ReplaceLastOccurrence(query,",","");

			query += FilterQuery(F_Name, F_LastName, F_DateStart, F_DateEnd, F_Sex, F_BWilaya, F_BCommune, F_CWilaya, F_CCommune, F_StdLvl, F_Job, F_WorkingArea, F_JoinDateStart, F_JoinDateEnd, F_InsDateStart, F_InsDateEnd);

			using (SQLiteConnection con = new SQLiteConnection(SqliteConnectionString))
			{
				using (SQLiteCommand cmd = new SQLiteCommand(query, con))
				{
					con.Open();
					using (SQLiteDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							string[] record = new String[rdr.FieldCount];
							for (int i = 0; i < rdr.FieldCount; i++)
								record[i] = rdr[i].ToString();
							resultList.Add(record);
						}

					}
					con.Close();
				}
			}

			return resultList;
		}


	
		public List<StatData> GetStatNumbers(int statChoise)
		{
			string query = "SELECT Count(Members.ID) as repetition ,";
			var results = new List<StatData>();

			switch (statChoise)
			{
				// par B_wilaya
				case 0: query += " region.value FROM Members,region WHERE Members.B_Wilaya = region.Id GROUP BY Members.B_wilaya"; break;

				// par B_commune
				case 1: query += " commune.value FROM Members,commune WHERE Members.B_commune = commune.Id GROUP BY Members.B_commune"; break;

				// par secteur de travail
				case 2: query += " ActivityAreas.name as value FROM Members,ActivityAreas WHERE Members.Activity_area = ActivityAreas.Id GROUP BY Members.Activity_area"; break;

				// par fonction
				case 3: query += " jobs.name as value FROM Members,jobs WHERE Members.Fonction = jobs.Id GROUP BY Members.Fonction"; break;

				// par sexe
				case 4: query += " CAST(CASE WHEN sex = 1 THEN 'Femmes' ELSE 'Hommes' END AS Text)as value FROM Members GROUP BY Members.Sex"; break;

				// par C_wilaya
				case 5: query += " region.value FROM Members,region WHERE Members.C_Wilaya = region.Id GROUP BY Members.C_wilaya"; break;

				// par C_commune
				case 6: query += " commune.value FROM Members,commune WHERE Members.C_commune = commune.Id GROUP BY Members.C_commune"; break;
			}

			
			using (SQLiteConnection con = new SQLiteConnection(SqliteConnectionString))
			{
				using (SQLiteCommand cmd = new SQLiteCommand(query, con))
				{
					con.Open();
					using (SQLiteDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							results.Add(new StatData(Int32.Parse(rdr["repetition"].ToString()), rdr["value"].ToString()));
						}
					}
					con.Close();
				}
			}
			return results;

		}

	}
}
