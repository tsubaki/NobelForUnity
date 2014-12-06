using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// レイヤー情報
/// </summary>
[RequireComponent(typeof(RawImage))]
public class Layer : MonoBehaviour 
{
	/// <summary>
	/// rawImageのキャッシュ
	/// </summary>
	RawImage rawImage;

	/// <summary>
	///  テキストコマンド実行時に呼ばれるコールバック
	/// UITextManagerより呼ばれる
	/// </summary>
	void OnUpdateText ()
	{
		if( rawImage.texture == null ){
			rawImage.enabled = false;
		}else{
			rawImage.enabled = true;
			TextureresourceManager.Mark(rawImage.texture.name);
		}
	}

#region UNITY_DELEGATE
	void Awake () 
	{
		rawImage = GetComponent<RawImage>();
		gameObject.tag = "Layer";
	}
#endregion
}
