using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class NfuUtility {

	public static readonly string imagePath = "Assets/Reader/Resources/Image";
	public static readonly string scenarioPath = "Assets/Reader/Resources/Scenario";

	[MenuItem("File/Nfu/CreateSetupDirectry")]
	static void SetupDire()
	{
		Directory.CreateDirectory(imagePath);
		Directory.CreateDirectory(scenarioPath);
	}
}
