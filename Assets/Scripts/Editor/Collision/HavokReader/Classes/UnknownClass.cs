using System.IO;

namespace Editor.Collision.HavokReader.Classes
{
	public class UnknownClass : HavokClass
	{
		public UnknownClass()
		{
			Name = "Unknown";
		}

		public override void Deserialize(BinaryReader reader)
		{

		}
	}
}
