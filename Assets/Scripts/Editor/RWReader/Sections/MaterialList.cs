using System.IO;

namespace Editor.RWReader.Sections
{
	public class MaterialList : Section
	{
		public MaterialList()
		{
			Name = "Material List";
			Header.ClumpID = 0x00000008;
			DataStorageType = SectionDataStorage.DataInStruct;
			CanHaveChildren = true;
		}

		public int MaterialCount;
		public int[] MaterialIndexes;

		public override void Deserialize(BinaryReader reader)
		{
			MaterialCount = reader.ReadInt32();

			MaterialIndexes = new int[MaterialCount];

			for (var i = 0; i < MaterialCount; i++)
			{
				MaterialIndexes[i] = reader.ReadInt32();
			}
		}
	}
}