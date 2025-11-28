using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Editor.RWReader.RWStructs;

namespace Assets.Scripts.Editor
{
	public static class Extensions
	{
		public static Sphere ReadRwSphere(this BinaryReader reader)
			=> new(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
	}
}
