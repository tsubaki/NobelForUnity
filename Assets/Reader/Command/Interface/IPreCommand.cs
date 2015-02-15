using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Nobel
{
	public interface IPreCommand 
	{
		string Tag{get;}
		void PreCommand(Dictionary<string, string> command);
	}
}

