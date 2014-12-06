using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// User interface text manager.
/// </summary>
[RequireComponent(typeof(TextController))]
public class ScenarioManager : MonoBehaviour 
{
	public string loadFileName;

	public static ScenarioManager Instance{ get; private set;}

	public GameObject canvas;

	static List<ICommand> commandList = new List<ICommand>(){
		new CommandChangeCharacterImage(), new CommandChangeBackground(),
		new CommandJampNextScenario(), new CommandComment(),
	};

	private string[] allTextLines = null;
	private TextController textController = null;

	private int currentLine = 0;

	/// <summary>
	/// Gets the current text.
	/// </summary>
	/// <value>The current text.</value>
	public string CurrentText
	{
		get{
			if( currentLine < allTextLines.Length ){ return allTextLines[currentLine]; }
			else{ return string.Empty; }
		}
	}

	/// <summary>
	/// Requests the next line.
	/// </summary>
	public void RequestNextLine()
	{
		string nextLine = CurrentText;
		nextLine = AnalyticsLine(nextLine);
		textController.SetNextLine(nextLine);
		currentLine ++;

		canvas.BroadcastMessage("OnUpdateText", SendMessageOptions.DontRequireReceiver);
	}

	/// <summary>
	/// 構文を解析する
	/// </summary>
	/// <returns>The line.</returns>
	/// <param name="line">Line.</param>
	private string AnalyticsLine(string line)
	{
		var lines = line.Split(new string[]{System.Environment.NewLine}, System.StringSplitOptions.RemoveEmptyEntries);
		var lineBuilder = new StringBuilder();

		foreach( var text in lines ){
			var tempText = text;
			foreach( var command in commandList ){
				if( command.Command(tempText) )
					tempText = string.Empty;
			}

			if(! string.IsNullOrEmpty( tempText )  ){
				lineBuilder.AppendLine(tempText.Replace(System.Environment.NewLine, ""));
			}
		}
		
		return lineBuilder.ToString();
	}

	/// <summary>
	/// Raises the complete display text event.
	/// </summary>
	/// <param name="line">Line.</param>
	private void OnCompleteDisplayText()
	{
		var lines = CurrentText.Split(new string[]{System.Environment.NewLine}, System.StringSplitOptions.RemoveEmptyEntries);
		foreach( var text in lines ){
			foreach( var command in commandList ){
				command.PreCommand(text);
			}
		}
	}

	private string SplitText(string line){
		var lines = line.Split(new string[]{System.Environment.NewLine}, System.StringSplitOptions.None);
		var lineBuilder = new StringBuilder();

		foreach( var text in lines )
		{
			if(! string.IsNullOrEmpty( text )  ){
				lineBuilder.AppendLine(text);
			}
		}

		return lineBuilder.ToString();
	}

	/// <summary>
	/// Updates the lines.
	/// </summary>
	/// <param name="filePath">File path.</param>
	public void UpdateLines()
	{
		var scenarioText = Resources.Load<TextAsset>("Scenario/" + loadFileName);

		if( scenarioText == null ){
			Debug.LogError("シナリオファイルが見つかりませんでした");
			Debug.LogError("ScenarioManagerを無効化します");
			enabled = false;
			throw new FileNotFoundException();
		}

		var allText = scenarioText.text;
		allTextLines = allText.Split(new string[]{"@br"}, System.StringSplitOptions.None);
		currentLine = 1;

		RequestNextLine();
	}

#region UNITY_DELEGATE

	void Awake()
	{
		Instance = this;
	}

	void Start () 
	{
		textController = GetComponent<TextController>();
		UpdateLines();
	}
	
	void Update () 
	{
		if( currentLine < allTextLines.Length && Input.GetMouseButtonDown(0))
		{
			if( textController.IsCompleteDisplayText ){
				RequestNextLine();
			}else{
				textController.ForceCompleteDisplayText();
			}
		}
	}
#endregion
}
