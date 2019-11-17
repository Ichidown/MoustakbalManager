using System;

namespace MoustakbalManager.Objects
{
    public class Wilaya
    {
		public int ID { get; set; }

		public String Name { get; set; }

		public Wilaya()
		{
			this.ID = -1;
			this.Name = "";
		}
		public Wilaya(int ID = 0, string Name = "")
		{
			this.ID = ID;
			this.Name = Name;
		}
	}
}
