using System.IO;

namespace Editor.RWReader.Sections
{
	public class RightToRender : Section
	{
		public RightToRender()
		{
			Name = "Right To Render";
			Header.ClumpID = 0x0000001F;
			DataStorageType = SectionDataStorage.DataInSection;
			CanHaveChildren = false;
		}

		public int PluginID;
		public int ExtraData;

		public override void Deserialize(BinaryReader reader)
		{
			PluginID = reader.ReadInt32();
			ExtraData = reader.ReadInt32();
		}
	}
}