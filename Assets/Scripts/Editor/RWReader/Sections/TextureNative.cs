using System;
using System.IO;

namespace Editor.RWReader.Sections
{
	public class TextureNative : Section
	{
		public TextureNative()
		{
			Name = "Texture Native";
			Header.ClumpID = 0x00000015;
			DataStorageType = SectionDataStorage.DataInStruct;
			CanHaveChildren = true;
		}

		public int DeviceID;
		public int FilterFlags;
		public string TextureName;
		public string AlphaName;
		public int AlphaFlags;

		public override void Deserialize(BinaryReader reader)
		{
			DeviceID = reader.ReadInt32();
			FilterFlags = reader.ReadInt32();
			TextureName = reader.ReadString256();
			AlphaName = reader.ReadString256();
			AlphaFlags = reader.ReadInt32();

			Console.WriteLine($"TextureName:{TextureName}, SDBM:{new SDBMHash(TextureName).Value}");
		}
	}
}