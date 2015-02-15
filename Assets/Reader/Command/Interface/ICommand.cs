using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ICommand 
{
	string Tag{get;}
	void Command(Dictionary<string, string> command);
}
