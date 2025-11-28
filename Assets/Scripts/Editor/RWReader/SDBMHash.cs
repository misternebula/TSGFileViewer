namespace Editor.RWReader
{
	public class SDBMHash
	{
		public string OriginalString;
		public uint Value;

		public SDBMHash(string input)
		{
			OriginalString = input;

			input = input.ToLower();

			Value = 0;
			foreach (var c in input)
			{
				Value = (65599 * Value) + c;
			}
		}
	}
}