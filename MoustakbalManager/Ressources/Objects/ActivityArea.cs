
using System;

namespace MoustakbalManager.Objects
{
    public class ActivityArea
    {
		public int ID { get; set; }

		public String Name { get; set; }

		public ActivityArea()
		{
			this.ID = -1;
			this.Name = "";
		}

		public ActivityArea(int ID = 0, string Name = "")
		{
			this.ID = ID;
			this.Name = Name;
		}
	}
}
