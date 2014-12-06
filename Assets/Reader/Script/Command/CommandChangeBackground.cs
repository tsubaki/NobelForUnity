using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

/// <summary>
/// Cinnabd background change.
/// </summary>
public class CommandChangeBackground : ICommand 
{
	/// <summary>
	/// Command the specified line.
	/// </summary>
	/// <param name="line">Line.</param>
	public bool Command (string line)
	{
		var match = Regex.Match(line, "@bg (?<fileName>.*)");
		if( match.Success )
		{
			string objectName = "Backimage";
			var obj = Array.Find<GameObject>( GameObject.FindGameObjectsWithTag("Layer") ,item => item.name == objectName);

			var fileName = match.Groups["fileName"].ToString();
			obj.GetComponent<RawImage>().texture = TextureresourceManager.Load(fileName);

			return true;
		}
		return false;
	}

	/// <summary>
	/// Pres the command.
	/// </summary>
	/// <param name="line">Line.</param>
	public void PreCommand (string line)
	{
		var match = Regex.Match(line, "@bg (?<fileName>.*)");
		if( match.Success )
		{
			var fileName = match.Groups["fileName"].ToString();
			TextureresourceManager.Load(fileName);
		}
	}
}
