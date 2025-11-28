using System;
using System.IO;

[Serializable]
public class Guid128
{
	public const uint INVALID_VALUE_0 = 0xBADBADBA;
	public const uint INVALID_VALUE_1 = 0xBEEFBEEF;
	public const uint INVALID_VALUE_2 = 0xEAC15A55;
	public const uint INVALID_VALUE_3 = 0x91100911;

	public uint[] Data { get; } = new uint[4];

	public Guid128()
	{
		Invalidate();
	}

	public Guid128(uint val0, uint val1, uint val2, uint val3)
	{
		Data[0] = val0;
		Data[1] = val1;
		Data[2] = val2;
		Data[3] = val3;
	}

	public Guid128(BinaryReader reader)
		: this(
			reader.ReadUInt32BigEndian(),
			reader.ReadUInt32BigEndian(),
			reader.ReadUInt32BigEndian(),
			reader.ReadUInt32BigEndian())
	{ }

	public void Clear()
	{
		Data[0] = 0;
		Data[1] = 0;
		Data[2] = 0;
		Data[3] = 0;
	}

	public void Invalidate()
	{
		Data[0] = INVALID_VALUE_0;
		Data[1] = INVALID_VALUE_1;
		Data[2] = INVALID_VALUE_2;
		Data[3] = INVALID_VALUE_3;
	}

	public bool IsClear()
	{
		return Data[0] == 0
		       && Data[1] == 0
		       && Data[2] == 0
		       && Data[3] == 0;
	}

	public bool IsValid()
	{
		return Data[0] != INVALID_VALUE_0
		       || Data[1] != INVALID_VALUE_1
		       || Data[2] != INVALID_VALUE_2
		       || Data[3] != INVALID_VALUE_3;
	}

	public uint this[int index] => Data[index];

	public override string ToString()
	{
		return $"{{{Data[0]:X8}-{Data[1]:x8}-{Data[2]:x8}-{Data[3]:x8}}}";
	}
}