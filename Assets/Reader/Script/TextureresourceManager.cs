using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// シナリオを解析してマネージャーを制御
/// </summary>
public class TextureresourceManager : MonoBehaviour
{
	static TextureresourceManager instance ;

	public int max = 5;
	List<Texture2D> textureList = new List<Texture2D>();

	/// <summary>
	/// Mark the specified textureName.
	/// </summary>
	/// <param name="textureName">Texture name.</param>
	public static void Mark(string textureName)
	{
		var tex = instance.textureList.Find( item => item.name == textureName );
		if( tex != null )
		{
			instance.textureList.Remove(tex);
			instance.textureList.Add(tex);
		}
	}

	/// <summary>
	/// Load the specified textureName.
	/// </summary>
	/// <param name="textureName">Texture name.</param>
	public static Texture Load(string textureName)
	{
		var tex = instance.textureList.Find( item => item.name == textureName );
		if( tex == null ){
			tex = instance.textureList[0];
 				var res = Resources.Load<TextAsset>("Image/" + textureName);
			tex.LoadImage(res.bytes);
			tex.name = textureName;
			Resources.UnloadAsset(res);
		}

		instance.textureList.Remove(tex);
		instance.textureList.Add(tex);

		return tex;
	}

#region UNITY_DELEGATE

	void Awake()
	{
		instance = this;
	}
	
	void OnEnable()
	{
		for(int i=0; i<instance.max; i++)
		{
			instance.textureList.Add(new Texture2D(1,1, TextureFormat.ARGB32, false));
		}
	}
	
	void OnDisable()
	{
		foreach( var tex in textureList )
		{
			Destroy (tex);
		}
		textureList.Clear();
		Resources.UnloadUnusedAssets();
	}
	
	void OnDestroy()
	{
		instance = null;
	}

#endregion
}