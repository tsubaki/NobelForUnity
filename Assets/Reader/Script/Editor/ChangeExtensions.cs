using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class ChangeExtensions : AssetPostprocessor
{
	static void OnPostprocessAllAssets (
		string[] importedAssets,
		string[] deletedAssets,
		string[] movedAssets,
		string[] movedFromAssetPaths) 
	{
		foreach( var assetPath in importedAssets )
		{
			if( Path.GetDirectoryName(assetPath).Equals(NfuUtility.imagePath) && Path.GetExtension(assetPath) == ".png")
			{
				var fileName = Path.ChangeExtension(assetPath, ".bytes");
				File.Copy(assetPath, fileName, true);
				AssetDatabase.DeleteAsset(assetPath);
			}
		}
	}
}
