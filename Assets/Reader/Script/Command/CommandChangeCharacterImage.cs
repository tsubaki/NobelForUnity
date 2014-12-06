using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

/// <summary>
/// キャラクター画像を差し替える
/// （img{レイヤー番号} {画像名}）
/// </summary>
public class CommandChangeCharacterImage : ICommand 
{
	/// <summary>
	/// 画像を登録する
	/// </summary>
	/// <param name="line">コマンド</param>
	public void PreCommand (string line)
	{
		var match = Regex.Match(line, "^@img\\d (?<fileName>.*)");
		if( match.Success )
		{
			var fileName = match.Groups["fileName"].ToString();
			TextureresourceManager.Load(fileName);
		}
	}

	/// <summary>
	/// 指定レイヤーの画像を事前ロードする
	/// </summary>
	/// <param name="line">コマンド</param>
	public bool Command (string line)
	{
		var match = Regex.Match(line, "@img(?<layerNo>\\d) (?<fileName>.*)");
		if( match.Success )
		{
			var objectName = string.Format("Layer{0}",  match.Groups["layerNo"]);
			var fileName = match.Groups["fileName"].ToString();

			var obj = Array.Find<GameObject>( GameObject.FindGameObjectsWithTag("Layer") ,item => item.name == objectName);
			var rawImage = obj.GetComponent<RawImage>();
			rawImage.texture = TextureresourceManager.Load(fileName);

			rawImage.SetNativeSize();

			return true;
		}else{
			return false;
		}
	}
}
