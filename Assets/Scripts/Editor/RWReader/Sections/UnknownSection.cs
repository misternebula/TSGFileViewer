using System.IO;

namespace Editor.RWReader.Sections
{
	public class UnknownSection : Section
	{
		public UnknownSection()
		{
			Name = "Unknown";
			CanHaveChildren = false;
		}

		public override void Deserialize(BinaryReader reader)
		{

		}
	}
}