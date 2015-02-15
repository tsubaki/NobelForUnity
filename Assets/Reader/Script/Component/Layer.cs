using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Nobel
{
	[RequireComponent(typeof(RawImage))]
	public class Layer : MonoBehaviour 
	{
		/// <summary>
		/// rawImageのキャッシュ
		/// </summary>
		RawImage m_rawImage;
		
		/// <summary>
		///  テキストコマンド実行時に呼ばれるコールバック
		/// UITextManagerより呼ばれる
		/// </summary>
		public void UpdateTexture (Texture texture)
		{
			if( texture == null ){
				m_rawImage.enabled = false;
				m_rawImage.texture = null;
			}else{
				m_rawImage.texture = texture;
				m_rawImage.enabled = true;
				m_rawImage.SetNativeSize();
				TextureresourceManager.Mark(texture.name);
			}
		}

		/// <summary>
		/// ScenarioManagerのRequestNextLineよりBroadcastMessage経由で呼ばれるコールバック。
		/// 新しいテキストを呼ぶ直前に呼ばれる
		/// </summary>
		public void OnRequestNextLine()
		{
			if( m_rawImage.texture != null )
				TextureresourceManager.Mark(m_rawImage.texture .name);
		}
		
		#region UNITY_DELEGATE
		
		void Awake () 
		{
			m_rawImage = GetComponent<RawImage>();
			gameObject.tag = "Layer";
			
			m_rawImage.enabled = false;
		}
		
		#endregion
		
	}
}