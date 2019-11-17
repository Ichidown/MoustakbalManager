using System;

namespace MoustakbalManager.Objects
{
    public class Commune
    {
		public int ID { get; set; }

		public int ID_Wilaya { get; set; }

		public String Name { get; set; }


		public Commune()
		{
			this.ID = -1;
			this.ID_Wilaya = -1;
			this.Name = "";
		}
		public Commune(int ID, int ID_Wilaya , string Name)
		{
			this.ID = ID;
			this.ID_Wilaya = ID_Wilaya;
			this.Name = Name;
		}
	}
}
