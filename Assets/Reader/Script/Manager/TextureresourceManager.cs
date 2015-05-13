using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Nobel
{
	/// <summary>
	/// シナリオを解析してマネージャーを制御
	/// </summary>
	public class TextureresourceManager : SingletonMonoBehaviourFast<TextureresourceManager>
	{
		[Range(3, 15),
		 TooltipAttribute("事前ロードも含めた画像の最大同時利用数. レイヤー数x2が安全圏")]
		public int MaxTextureCount = 5;
		private List<Texture2D> m_textureList = new List<Texture2D>();
		
		/// <summary>
		/// Mark the specified textureName.
		/// </summary>
		/// <param name="textureName">Texture name.</param>
		public static void Mark(string textureName)
		{
			var tex = Instance.m_textureList.Find( item => item.name == textureName );
			if( tex != null )
			{
				Instance.m_textureList.Remove(tex);
				Instance.m_textureList.Add(tex);
			}
		}
		
		/// <summary>
		/// Load the specified textureName.
		/// </summary>
		/// <param name="textureName">Texture name.</param>
		public static Texture Load(string textureName)
		{
			var tex = Instance.m_textureList.Find( item => item.name == textureName );
			if( tex == null ){
				tex = Instance.m_textureList[0];
				var res = Resources.Load<TextAsset>("Image/" + textureName);
				tex.LoadImage(res.bytes);
				tex.name = textureName;
				Resources.UnloadAsset(res);
			}
			
			Instance.m_textureList.Remove(tex);
			Instance.m_textureList.Add(tex);
			
			return tex;
		}
		
		#region UNITY_DELEGATE
		
		void OnEnable()
		{
			for(int i=0; i<Instance.MaxTextureCount; i++)
			{
				Instance.m_textureList.Add(new Texture2D(1,1, TextureFormat.ARGB32, false));
			}
		}
		
		void OnDisable()
		{
			foreach( var tex in m_textureList )
			{
				Destroy (tex);
			}
			m_textureList.Clear();
			Resources.UnloadUnusedAssets();
		}
		
		#endregion
	}
}
