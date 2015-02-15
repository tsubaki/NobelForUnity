using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Nobel
{
	public class RegisterCommands {
		
		public static readonly System.Object[] commands = new System.Object[]
		{
			new CommandUpdateImage(),
			new CommandJampNextScenario(),
		};
	}
}
