using System.IO;
using UnityEditor;
using UnityEngine;

namespace AttributeHandlers
{
	public class TriggerBase : MonoBehaviour, IAttributeHandler
	{
		public string m_targetName;
		public string m_activate;
		public string m_deactivate;
		public int m_primitive;
		public string m_target;
		public float m_delay;
		public int m_count = -1;
		public string m_reverseTarget;
		// options?
		// touch record?
		public float m_wait;
		public float m_speedSquared;

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
							m_activate = reader.ReadNullTerminatedString();
							break;
						}

					case 2:
						{
							reader.BaseStream.Position = attr.ReaderPosition + attr.Data.ToInt32BigEndian(); // negative offset
							m_deactivate = reader.ReadNullTerminatedString();
							break;
						}

					case 3:
						{
							m_primitive = attr.Data.ToInt32BigEndian();
							break;
						}

					case 4:
						{
							reader.BaseStream.Position = attr.ReaderPosition + attr.Data.ToInt32BigEndian(); // negative offset
							m_target = reader.ReadNullTerminatedString();
							break;
						}

					case 5:
						{
							m_delay = attr.Data.ToSingleBigEndian();
							break;
						}

					case 6:
						{
							m_count = attr.Data.ToInt32BigEndian();
							break;
						}

					case 7:
						{
							reader.BaseStream.Position = attr.ReaderPosition + attr.Data.ToInt32BigEndian(); // negative offset
							m_reverseTarget = reader.ReadNullTerminatedString();
							break;
						}

					case 0xA:
						{
							m_wait = attr.Data.ToSingleBigEndian();
							break;
						}

					case 0xB:
						{
							m_speedSquared = attr.Data.ToSingleBigEndian();
							break;
						}
				}
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = new Color(0, 1, 0, 0.1f);

			Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
			Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
			Gizmos.DrawCube(Vector3.zero, Vector3.one);
			Gizmos.matrix = Matrix4x4.identity;

			Handles.Label(transform.position, m_target);
		}
	}
}
