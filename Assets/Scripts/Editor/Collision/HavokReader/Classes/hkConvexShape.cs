using System.IO;

namespace Editor.Collision.HavokReader.Classes
{
	public class hkConvexShape : hkShape
	{
		public hkConvexShape()
		{
			Name = "hkConvexShape";
		}

		public float ConvexRadius;

		public override void Deserialize(BinaryReader reader)
		{
			base.Deserialize(reader);
			ConvexRadius = reader.ReadSingleBigEndian();
		}
	}
}
