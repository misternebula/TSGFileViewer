using System;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.ResourceHandlers;
using Assets.Scripts.Resources;
using RWReader.Sections;
using UnityEngine;
using Material = UnityEngine.Material;
using Texture = RWReader.Sections.Texture;

namespace Assets.Scripts
{
    public class MaterialManager : MonoBehaviour
    {
		public static MaterialManager Instance { get; private set; }

	    private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");

	    public Shader Rigid;
	    public Shader RigidTextured;
	    public Shader RigidDualTextured;
	    public Shader RigidDualTexturedUV;
	    public Shader RigidGloss;
	    public Shader RigidMultitone;
	    public Shader RigidNormalMap;

	    public Shader Skin;
	    public Shader SkinTextured;
		public Shader SkinDualTextured;
		public Shader SkinDualTexturedUV;
		public Shader SkinGloss;
		public Shader SkinFlipbook;

		public Shader AA;
		public Shader AACol;
		public Shader AARow;

		public Shader Edge;
		public Shader EdgeAA;

		public Shader Flipbook;
		public Shader ProjTex;
		public Shader Chocolate;
		public Shader Sky;
		public Shader UV;
		public Shader VFXRigidTextured;

		private void Awake()
		{
			Instance = this;
		}

		public Shader GetShader(uint shaderID) =>
			shaderID switch
			{
				103326086 => Rigid,
				2566618050 => RigidTextured,
				3052503966 => RigidDualTextured,
				1913811202 => RigidDualTexturedUV,
				3408801425 => RigidGloss,
				3052993522 => RigidMultitone,
				2345721244 => RigidNormalMap,

				1551361698 => Skin,
				2084877734 => SkinTextured,
				3487979906 => SkinDualTextured,
				2866759198 => SkinDualTexturedUV,
				3120040365 => SkinGloss,
				2596055507 => SkinFlipbook,

				4214712379 => Flipbook,

				4204451511 => Chocolate,

				617769830 => UV,

				_ => throw new NotImplementedException($"{shaderID} not found")
			};

		public Material CreateMaterialInstance(uint shaderID, params Texture2D[] textures)
		{
			var shader = GetShader(shaderID);

			var material = new Material(shader);

			//if (shaderID == 103326086)
			//{
			if (textures != null && textures.Length >= 1)
			{

				/*if (textures.Length == 1)
				{*/
					material.SetTexture(BaseMap, textures[0]);
				/*}
				else
				{
					material.SetTexture(BaseMap, textures[1]);
				}*/

				
			}
				
			//}

			return material;
		}

		public Texture2D[] GetTextures(RWReader.Sections.Material material)
		{
			var returnTextures = new List<Texture2D>();

			var textureSections = material.GetChildren<Texture>();
			foreach (var texture in textureSections)
			{
				var texName = texture.GetTextureName();
				var textureNative = GetTextureNative(texName);

				returnTextures.Add(textureNative?.Texture);
			}

			return returnTextures.ToArray();
		}

		public TextureNative GetTextureNative(string textureName)
		{
			var textureDicts = ResourceHandlerManager.GetResources<rwID_TEXDICTIONARY>();

			foreach (var texDict in textureDicts)
			{
				foreach (var texture in texDict.SectionTree.GetChildren<TextureNative>())
				{
					if (texture.TextureName == textureName)
					{
						return texture;
					}
				}
			}

			Debug.LogWarning($"Couldn't find a texture with name {textureName}");
			return null;
		}
    }
}
