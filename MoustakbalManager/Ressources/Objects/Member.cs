using System;

namespace MoustakbalManager.Objects
{
    public struct Member // CREATE MEMBER MANAGER
	{
		

        public int ID { get; set; }

		public String Name { get; set; }

		public String LastName { get; set; }

		public Boolean Sex { get; set; }

		public AdressLocation BirthPlace { get; set; }

		public String BirthDate { get; set; }

        public StuddyLvl studyLevel { get; set; }

		public Job Fonction { get; set; }

		public ActivityArea ActivityArea { get; set; }

		public String EMail { get; set; }

		public AdressLocation CurrentAdress { get; set; }

		public String PhoneNumber { get; set; }

		public String JoinDate { get; set; }

		public String InscriptionDateTime { get; set; }

		public Member(int ID=0)
		{
			this.ID = 0;
			this.Name = "";
			this.LastName = "";
			this.Sex = false;
			this.BirthPlace = new AdressLocation();
			this.BirthDate = "01-01-2000";
			this.studyLevel = new StuddyLvl();
			this.Fonction = new Job();
			this.ActivityArea = new ActivityArea();
			this.EMail = "EMail";
			this.CurrentAdress = new AdressLocation();
			this.PhoneNumber = "";
			this.JoinDate = "01-01-2000";
			this.InscriptionDateTime = "01-01-2000";
		}

		public Member(
			int ID,
			string Name, 
			string LastName, 
			bool Sex,
			string B_adress,
			Wilaya B_wilaya,
			Commune B_commune,
			string BirthDate,
			StuddyLvl studyLevel,
			Job Fonction,
			ActivityArea ActivityArea, 
			string EMail,
			string C_adress,
			Wilaya C_wilaya,
			Commune C_commune, 
			string PhoneNumber, 
			string JoinDate, 
			string InscriptionDate)
		{
			this.ID = ID;
			this.Name = Name;
			this.LastName = LastName;
			this.Sex = Sex;
			this.BirthPlace = new AdressLocation(B_adress, B_wilaya, B_commune);
			this.BirthDate = BirthDate;
			this.studyLevel = studyLevel;
			this.Fonction = Fonction;
			this.ActivityArea = ActivityArea;
			this.EMail = EMail;
			this.CurrentAdress = new AdressLocation(C_adress, C_wilaya, C_commune);
			this.PhoneNumber = PhoneNumber;
			this.JoinDate = JoinDate;
			this.InscriptionDateTime = InscriptionDate;
		}

		
	}
}
