using System.IO;

namespace Editor.Collision.HavokReader.Classes
{
	public class hkReferencedObject : HavokClass
	{
		public hkReferencedObject()
		{
			Name = "hkReferencedObject";
		}

		public ushort MemSizeAndFlags;
		public short ReferenceCount;

		public override void Deserialize(BinaryReader reader)
		{
			MemSizeAndFlags = reader.ReadUInt16BigEndian();
			ReferenceCount = reader.ReadInt16BigEndian();
		}
	}
}
