using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;


public class CommandJampNextScenario : ICommand {

	#region ICommand implementation

	public bool Command (string line)
	{
		var match = Regex.Match(line, "^@next (?<fileName>.*)");
		if( match.Success )
		{
			var scenario = ScenarioManager.Instance;
			scenario.loadFileName = ( match.Groups["fileName"].ToString());
			scenario.UpdateLines();
			return true;
		}
		return false;
	}

	public void PreCommand (string line){}

	#endregion


}
