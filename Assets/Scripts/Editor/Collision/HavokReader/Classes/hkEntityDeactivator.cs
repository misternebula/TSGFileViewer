using System.IO;

namespace Editor.Collision.HavokReader.Classes
{
	public class hkEntityDeactivator : hkReferencedObject
	{
		public hkEntityDeactivator()
		{
			Name = "hkEntityDeactivator";
		}

		public override void Deserialize(BinaryReader reader)
		{
			base.Deserialize(reader);
		}
	}
}
