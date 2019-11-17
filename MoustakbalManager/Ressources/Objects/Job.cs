using System;

namespace MoustakbalManager.Objects
{
    public class Job
    {
		public int ID { get; set; }

		public int ID_Activity_Area { get; set; }

		public String Name { get; set; }

		public Job()
		{
			this.ID = -1;
			this.ID_Activity_Area = -1;
			this.Name = "";
		}

		public Job(int ID = 0, int ID_Activity_Area = 0, string Name = "")
		{
			this.ID = ID;
			this.ID_Activity_Area = ID_Activity_Area;
			this.Name = Name;
		}
	}
}
