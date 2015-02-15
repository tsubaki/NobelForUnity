using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

namespace Nobel
{
	[CustomEditor(typeof(TextAsset), true)]
	class ImageEditor : Editor
	{
		Texture2D _tex;
		
		public Texture2D tex{
			get{
				if( _tex == null ){
					_tex = new Texture2D (1, 1, TextureFormat.ARGB32, false);
					var textureAsset = (TextAsset)target;
					if(! tex.LoadImage (textureAsset.bytes)){
						DestroyImmediate(tex);
						_tex = null;
					}
				}
				return _tex;
			}
		}

		public override bool HasPreviewGUI ()
		{
			var path = AssetDatabase.GetAssetPath(target);
			return path.IndexOf(".png.bytes") != -1;
		}
		
		public override void OnPreviewGUI (Rect r, GUIStyle background)
		{
			DrawDefaultInspector();
			
			if (tex != null) {
				EditorGUI.DrawTextureTransparent (new Rect (r.x, r.y, r.width, r.height), tex);
			}else{
				EditorGUILayout.TextArea( ((TextAsset)target).text);
			}
		}
		
		public override Texture2D RenderStaticPreview (string assetPath, Object[] subAssets, int width, int height)
		{
			if( assetPath.IndexOf(".png.bytes") != -1)
				return tex;
			else
				return null;
		}
	} 
}