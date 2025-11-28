using System.IO;

internal interface IAttributeHandler
{
	public void HandleAttributes(BinaryReader reader, SimGroup.AttrPacket attrPacket);
}