using System.IO;

namespace Editor.Collision.HavokReader.Classes
{
	public class hkRigidBodyDeactivator : hkEntityDeactivator
	{
		public hkRigidBodyDeactivator()
		{
			Name = "hkRigidBodyDeactivator";
		}

		public override void Deserialize(BinaryReader reader)
		{
			base.Deserialize(reader);
		}
	}
}
