using Assets.Scripts.ResourceHandlers;
using Assets.Scripts.Resources;
using RWReader.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.XR;

namespace Assets.Scripts
{
	public class InstanceVar
	{
		public MetaModel.MM_Variable Variable;
		public object Value;
	}

    class MetaModelInstance : MonoBehaviour
    {
	    private static List<string> TSGShaders = new List<string>()
	    {
		    "simpsons_chocolate",
		    "simpsons_vfx_rigid_textured",
		    "simpsons_rigid_normalmap",
		    "simpsons_rigid_multitone",
		    "simpsons_projtex",
		    "simpsons_rigid_dualtextured_uv",
		    "simpsons_skin_dualtextured_uv",
		    "simpsons_uv",
		    "simpsons_skin_flipbook",
		    "simpsons_flipbook",
		    "simpsons_sky",
		    "simpsons_skin_gloss",
		    "simpsons_rigid_gloss",
		    "simpsons_rigid_dualtextured",
		    "simpsons_skin_dualtextured",
		    "simpsons_rigid_textured",
		    "simpsons_skin_textured",
		    "simpsons_aa_col",
		    "simpsons_aa_row",
		    "simpsons_edgeAA",
		    "simpsons_aa",
		    "simpsons_edge",
		    "simpsons_rigid",
		    "simpsons_skin"
	    };

	    private static Dictionary<uint, string> HashToShader = new();

		public MetaModel MetaModel;
	    public InstanceVar[] Vars;

	    public void Assign(MetaModel metaModel)
	    {
		    MetaModel = metaModel;

		    Vars = new InstanceVar[metaModel.m_nVariables];
		    for (var i = 0; i < metaModel.m_nVariables; i++)
		    {
			    var iv = new InstanceVar();
			    iv.Variable = metaModel.Variables[i];

			    switch (iv.Variable.m_valueType)
			    {
					case MetaModel.MM_ValueType.BOOL:
						iv.Value = false;
						break;
					case MetaModel.MM_ValueType.UINT32:
						iv.Value = 0u;
						break;
					case MetaModel.MM_ValueType.FLOAT:
						iv.Value = 0.0f;
						break;
					default:
						throw new NotImplementedException();
			    }

			    Vars[i] = iv;
		    }

			CheckStates();
	    }

	    public void CheckStates()
	    {
		    if (MetaModel == null)
		    {
			    throw new NotImplementedException();
		    }

		    if (MetaModel.m_nStates == 0)
		    {
			    ClearState();
			    return;
		    }

			//Debug.Log($"Checking states for {this}", this);
			MetaModel.MM_State passingState = null;
		    for (var i = 0; i < MetaModel.m_nStates; i++)
		    {
			    var pass = true;

				//Debug.Log($"State {i+1}/{MetaModel.m_nStates}");
				var state = MetaModel.States[i];
			    for (var j = 0; j < state.m_nPredicates; j++)
			    {
				    //Debug.Log($"- Predicate {j + 1}/{state.m_nPredicates}");
					var predicate = state.m_pPredicateArr[j];
				    if (predicate.m_pVariable == null)
				    {
					    throw new NotImplementedException();
				    }

				    
				    var variable = predicate.m_pVariable;

				    var instanceVariable = Vars.First(x => x.Variable == variable);

				    switch (variable.m_valueType)
				    {
						case MetaModel.MM_ValueType.BOOL:
							pass = pass && Compare((bool)instanceVariable.Value, predicate.GetBool(), predicate.m_compareOp);
							break;
						case MetaModel.MM_ValueType.UINT32:
							pass = pass && Compare((uint)instanceVariable.Value, predicate.GetUInt(), predicate.m_compareOp);
							break;
						case MetaModel.MM_ValueType.FLOAT:
							pass = pass && Compare((float)instanceVariable.Value, predicate.GetFloat(), predicate.m_compareOp);
							break;
					}
			    }

			    if (pass)
			    {
				    passingState = state;
					break;
			    }
		    }

		    if (passingState == null)
		    {
			    throw new NotImplementedException("No states passed!");
		    }

			ChangeState(passingState);
	    }

	    public void ChangeState(MetaModel.MM_State newState)
	    {
		    //Debug.Log($"Changing to state {newState.m_pName}", this);

		    foreach (var part in newState.m_pPartArr)
		    {
			    if (part.m_classID == 835904732)
			    {
				    // ModelPart
				    if (part.m_pName != "ModelPart")
				    {
					    throw new NotImplementedException();
				    }

				    var attribute = part.m_pAttributeArr[0];

				    if (attribute.m_pName != "Model")
				    {
					    throw new NotImplementedException();
				    }

				    if (attribute.m_valueType != MetaModel.MM_ValueType.ASSET)
				    {
					    throw new NotImplementedException();
				    }

				    var asset = attribute.GetAsset();
				    var resource = ResourceHandlerManager.FindResourceById(asset.m_GUID);

				    if (resource is not EARS_MESH readFile)
				    {
					    throw new NotImplementedException();
				    }

				    if (HashToShader.Count == 0)
				    {
					    foreach (var item in TSGShaders)
					    {
						    var hash = new SDBMHash(item);
						    HashToShader.Add(hash.Value, item);
						    //Debug.Log($"{item} : {hash}");
					    }
					}

					var earsMeshes = readFile.SectionTree.GetChildren<EARSMesh>();
					var meshList = new List<Mesh>();

					foreach (var eaMesh in earsMeshes)
					{
						var skin = eaMesh.Parent.GetChildren<SkinPLG>().FirstOrDefault();
						var isSkinned = skin != null;

						foreach (var submesh in eaMesh.SubmeshInfos)
						{
							foreach (var split in submesh.MaterialSplits)
							{
								var mesh = new Mesh();

								var verts = submesh.Vertices.Select(x => x.Position).ToList();
								var normals = submesh.Vertices.Select(x => x.Normal).ToList();
								var tangents = submesh.Vertices.Select(x => x.Tangent).ToList();
								var colors = submesh.Vertices.Select(x => x.RGBA).ToList();

								var numTexCoords = submesh.Vertices[0].TexCoords.Length;

								mesh.SetVertices(verts);
								mesh.SetTriangles(split.Triangles, 0);

								for (var i = 0; i < numTexCoords; i++)
								{
									mesh.SetUVs(i, submesh.Vertices.Select(x => x.TexCoords[i]).ToList());
								}
								mesh.SetNormals(normals);
								mesh.SetTangents(tangents.Select(x => new Vector4(x.x, x.y, x.z, 1)).ToList());
								mesh.SetColors(colors);
								mesh.name = $"EAMesh{earsMeshes.IndexOf(eaMesh)}" +
											$"Submesh{Array.IndexOf(eaMesh.SubmeshInfos, submesh)}" +
											$"Material{Array.IndexOf(submesh.MaterialSplits, split)}" +
											$"Shader{HashToShader[submesh.ShaderHash]}";

								/*if (isSkinned)
								{
									mesh.name = "Skinned" + mesh.name;

									var binMesh = eaMesh.GetSibling<BinMeshPLG>().Single();
									var meshData = binMesh.Data[Array.IndexOf(submesh.MaterialSplits, split)];
									var triStrip = meshData.Indices;
									var faces = EARSMesh.StripToFaces(new EARSMesh.TriStrip() { Indices = triStrip });
									var triList = new List<int>();
									foreach (var item in faces)
									{
										triList.Add(item.one);
										triList.Add(item.two);
										triList.Add(item.three);
									}
									//mesh.SetTriangles(triList, 0);

									var boneWeights = new BoneWeight[verts.Count];

									for (var i = 0; i < verts.Count; i++)
									{
										var weight = new BoneWeight();

										var bones = skin.VertexBoneMapping[i];
										var weights = skin.WeightList[i];

										weight.boneIndex0 = bones[0];
										weight.boneIndex1 = bones[1];
										weight.boneIndex2 = bones[2];
										weight.boneIndex3 = bones[3];

										weight.weight0 = weights[0];
										weight.weight1 = weights[1];
										weight.weight2 = weights[2];
										weight.weight3 = weights[3];

										boneWeights[i] = weight;
									}

									mesh.boneWeights = boneWeights;

									mesh.bindposes = skin.SkinToBoneMatrix;
								}*/

								meshList.Add(mesh);
							}
						}
					}

					foreach (var mesh in meshList)
					{
						var meshObj = new GameObject(mesh.name);
						var mf = meshObj.AddComponent<MeshFilter>();
						mf.sharedMesh = mesh;

						meshObj.AddComponent<MeshRenderer>();
						meshObj.transform.parent = transform;
						meshObj.transform.localScale = new Vector3(1, 1, 1);
						meshObj.transform.localPosition = Vector3.zero;
					}
				}
		    }
		}

	    public bool Compare(bool a, bool b, MetaModel.CompareOp compareOp)
	    {
		    return Compare(a ? (byte)1 : (byte)0, b ? (byte)1 : (byte)0, compareOp);
	    }

		public bool Compare(uint a, uint b, MetaModel.CompareOp compareOp)
	    {
		    switch (compareOp)
		    {
			    case MetaModel.CompareOp.GT:
				    return a > b;
			    case MetaModel.CompareOp.LT:
				    return a < b;
			    case MetaModel.CompareOp.GE:
				    return a >= b;
			    case MetaModel.CompareOp.LE:
				    return a <= b;
			    case MetaModel.CompareOp.EQ:
				    return a == b;
			    case MetaModel.CompareOp.NEQ:
				    return a != b;
			    default:
				    throw new ArgumentOutOfRangeException(nameof(compareOp), compareOp, null);
		    }
	    }

	    public bool Compare(float a, float b, MetaModel.CompareOp compareOp)
	    {
		    switch (compareOp)
		    {
			    case MetaModel.CompareOp.GT:
				    return a > b;
			    case MetaModel.CompareOp.LT:
				    return a < b;
			    case MetaModel.CompareOp.GE:
				    return a >= b;
			    case MetaModel.CompareOp.LE:
				    return a <= b;
			    case MetaModel.CompareOp.EQ:
				    return a == b;
			    case MetaModel.CompareOp.NEQ:
				    return a != b;
			    default:
				    throw new ArgumentOutOfRangeException(nameof(compareOp), compareOp, null);
		    }
	    }

		public void ClearState()
	    {
		    throw new NotImplementedException();
	    }
    }
}
