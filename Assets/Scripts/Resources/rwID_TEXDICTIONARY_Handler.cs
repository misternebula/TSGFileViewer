using Assets.Scripts.Resources;
using RWReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts.ResourceHandlers
{
	public class rwID_TEXDICTIONARY : Resource
	{
		public Section SectionTree;
	}

	public class rwID_TEXDICTIONARY_Handler : ResourceHandler
	{
		public Dictionary<Guid128, rwID_TEXDICTIONARY> TextureDictionaries = new();
		public List<Guid128> DebugList = new();

		public override void HandleBytes(byte[] data, Guid128 guid)
		{
			var section = new RWStreamReader().Read(data);

			var texDict = new rwID_TEXDICTIONARY();
			texDict.GUID = guid;
			texDict.SectionTree = section;

			TextureDictionaries.Add(guid, texDict);
			DebugList.Add(guid);
		}

		public override IEnumerable<Resource> GetResources()
		{
			return TextureDictionaries.Values.ToList();
		}
	}
}
