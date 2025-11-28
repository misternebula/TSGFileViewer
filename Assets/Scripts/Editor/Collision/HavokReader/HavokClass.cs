using System.IO;

namespace Editor.Collision.HavokReader
{
	public abstract class HavokClass
	{
		public string Name;

		public abstract void Deserialize(BinaryReader reader);
	}
}
