using System.IO;

namespace Editor.RWReader.Sections
{
	public class Struct : Section
	{
		public Struct()
		{
			Name = "Struct";
			Header.ClumpID = 0x00000001;
			DataStorageType = SectionDataStorage.NoData;
			CanHaveChildren = false;
		}

		public override void Deserialize(BinaryReader reader)
		{
			Raw = reader.ReadBytes(Header.Size);
		}
	}
}