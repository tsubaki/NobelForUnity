using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

namespace Nobel{

	public class ComvertAssets : AssetPostprocessor {
		
		void OnPreprocessTexture()
		{
			TextureImporter textureImporter = assetImporter as TextureImporter;
			if(! textureImporter.assetPath.Contains(NobelUtility.imagePath) )
				return;
			
			string newPath = textureImporter.assetPath + ".bytes";
			if( File.Exists(newPath) )
				File.Delete(newPath);
			
			File.Copy(textureImporter.assetPath, newPath);
		}
		
		static void OnPostprocessAllAssets (
			string[] importedAssets,
			string[] deletedAssets,
			string[] movedAssets,
			string[] movedFromAssetPaths) {
			
			foreach( var import in importedAssets )
			{
				if( Path.GetExtension(import).Equals(".png") && import.Contains(NobelUtility.imagePath) )
				{
					AssetDatabase.DeleteAsset(import);
				}
			}
			
			//		var loadtextures = GameObject.FindObjectsOfType<LoadTexture>();
			//		
			//		foreach( var loadtexture in loadtextures )
			//		{
			//			loadtexture.UpdateTexture();
			//		}
			
			AssetDatabase.Refresh();
		}
		
	} 
}

