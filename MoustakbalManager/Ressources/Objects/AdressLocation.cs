using System;

namespace MoustakbalManager.Objects
{
    public class AdressLocation // CREATE ADRESS LOCATION MANAGER
    {
		public String adress { get; set; }

		public Wilaya wilaya { get; set; }

		public Commune commune { get; set; }

		public AdressLocation()
		{
			this.adress = "";
			this.wilaya = new Wilaya();
			this.commune = new Commune();
		}

			public AdressLocation(string adress, Wilaya wilaya, Commune commune)
		{
			this.adress = adress;
			this.wilaya = wilaya;
			this.commune = commune;
		}

		/*public void CopyAdressLocation(AdressLocation Source)
		{
			adress = Source.adress;
			wilaya.ID = Source.wilaya.ID;
			wilaya.Name = Source.wilaya.Name;
			commune.ID = Source.commune.ID;
			commune.ID_Wilaya = Source.commune.ID_Wilaya;
			commune.Name = Source.commune.Name;
		}*/
	}
}
