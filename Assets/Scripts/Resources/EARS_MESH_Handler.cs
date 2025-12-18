using Assets.Scripts.Resources;
using RWReader;
using RWReader.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts.ResourceHandlers
{
	public class EARS_MESH : Resource
	{
		public Section SectionTree;
	}

	public class EARS_MESH_Handler : ResourceHandler
	{
		public Dictionary<Guid128, EARS_MESH> Meshes = new();
		public List<Guid128> DebugList = new();

		public override void HandleBytes(byte[] data, Guid128 guid)
		{
			var section = new RWStreamReader().Read(data);

			var mesh = new EARS_MESH();
			mesh.GUID = guid;
			mesh.SectionTree = section;

			Meshes.Add(guid, mesh);
			DebugList.Add(guid);
		}

		public override IEnumerable<Resource> GetResources()
		{
			return Meshes.Values;
		}
	}
}