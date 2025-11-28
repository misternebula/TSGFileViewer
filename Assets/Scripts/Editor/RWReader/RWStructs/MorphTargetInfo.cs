using System.Numerics;

namespace Editor.RWReader.RWStructs
{
	public struct MorphTargetInfo
	{
		public Sphere BoundingSphere;
		public bool HasVertices;
		public bool HasNormals;
		public Vector3[] Vertices;
		public Vector3[] Normals;
	}
}