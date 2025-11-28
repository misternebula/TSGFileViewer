using System.IO;
using UnityEngine;

namespace AttributeHandlers
{
	public class LoadMusicProject : MonoBehaviour, IAttributeHandler
	{
		public string m_targetName;
		public Guid128 m_musicProjectGuid;

		public void HandleAttributes(BinaryReader reader, SimGroup.AttrPacket attrPacket)
		{
			foreach (var attr in attrPacket.Attributes)
			{
				switch (attr.Index)
				{
					case 0:
					{
						reader.BaseStream.Position = attr.ReaderPosition + attr.Data.ToInt32BigEndian(); // negative offset
						m_targetName = reader.ReadNullTerminatedString();
						break;
					}

					case 1:
					{
						reader.BaseStream.Position = attr.ReaderPosition + attr.Data.ToInt32BigEndian(); // negative offset
						m_musicProjectGuid = new Guid128(reader);
						break;
					}
				}
			}
		}
	}
}
