using System;
using System.IO;
using UnityEngine;

namespace AttributeHandlers
{
	public class CSystemCommands : MonoBehaviour, IAttributeHandler
	{
		public RwMatrixTag m_matrix;

		public void HandleAttributes(BinaryReader reader, SimGroup.AttrPacket attrPacket)
		{
			foreach (var attr in attrPacket.Attributes)
			{
				switch (attr.Index)
				{
					case 0:
						break;

					case 1:
					{
						reader.BaseStream.Position = attr.ReaderPosition + attr.Data.ToInt32BigEndian(); // negative offset
						m_matrix = new RwMatrixTag(reader);
						break;
					}

					case 2:
						break;

					case 3:
						break;

					case 4:
						break;

					default:
						throw new NotImplementedException();
				}
			}
		}

		private void OnDrawGizmos()
		{
			if (GetComponent<TriggerBase>() == null)
			{
				Gizmos.DrawWireSphere(transform.position, 0.5f);
			}
		}
	}
}
