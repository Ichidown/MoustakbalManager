
namespace MoustakbalManager.Ressources
{
	public struct StatData
	{
		public StatData(int doubleValue, string strValue)
		{
			DataValue = doubleValue;
			StringData = strValue;
		}

		public int DataValue { get; private set; }
		public string StringData { get; private set; }
	}
}
