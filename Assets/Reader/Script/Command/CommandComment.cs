using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class CommandComment : ICommand {

#region ICommand implementation

	public bool Command (string line)
	{
		return Regex.IsMatch(line, "^//");
	}

	public void PreCommand (string line){}

#endregion
}
