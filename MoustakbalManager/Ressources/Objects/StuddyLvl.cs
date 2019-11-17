using System;

namespace MoustakbalManager.Objects
{
    public class StuddyLvl
    {
		public int ID { get; set; }

		public String Name { get; set; }

		public StuddyLvl()
		{
			this.ID = -1;
			this.Name = "";
		}

		public StuddyLvl(int ID = 0, string Name = "")
		{
			this.ID = ID;
			this.Name = Name;
		}
	}
}
