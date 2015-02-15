using System.Text.RegularExpressions;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nobel
{
	public class CommandManager : SingletonMonoBehaviourFast<CommandManager> 
	{
		private List<ICommand> m_commandList = new List<ICommand>();
		private List<IPreCommand> m_preCommandList = new List<IPreCommand>();
		
		public void PreloadCommand(string line)
		{
			foreach( var text in line.Split(System.Environment.NewLine.ToCharArray(), System.StringSplitOptions.None) )
			{
				var dic = CommandAnalytics(text);
				var tag = dic["tag"];
				var command = m_preCommandList.Find(item => item.Tag == tag);
				
				if( command == null )
					continue;
				
				try{
					command.PreCommand(dic);
				}catch(KeyNotFoundException)
				{
					Debug.LogError("コマンドに誤りがあります" + line);
				}
			}
		}
		
		public bool LoadCommand(string line)
		{
			var dic = CommandAnalytics(line);
			var tag = dic["tag"];
			
			var command = m_commandList.Find( item => item.Tag == tag);
			if( command == null )
				return false;
			
			try{
				command.Command(dic);
			}catch(KeyNotFoundException){
				Debug.LogError("コマンドに誤りがあります" + line);
				return false;
			}
			return true;
		}
		
		private Dictionary<string, string> CommandAnalytics(string line )
		{
			Dictionary<string, string> command = new Dictionary<string, string>();
			
			var tag = Regex.Match(line, "^@(\\S+)\\s");
			command.Add("tag", tag.Groups[1].ToString());
			
			Regex regex = new Regex("(\\S+)=(\\S+)");
			var matches = regex.Matches(line);
			foreach( Match match in matches )
			{
				command.Add(match.Groups[1].ToString(), match.Groups[2].ToString());
			}
			return command;
		}
		
		#region UNITY_CALLBACK
		
		new void Awake()
		{
			base.Awake();
			
			foreach( var command in RegisterCommands.commands.Where(item => item is IPreCommand).ToList() ){
				m_preCommandList.Add((IPreCommand)command);
			}

			foreach( var command in RegisterCommands.commands.Where(item => item is ICommand).ToList() ){
				m_commandList.Add((ICommand)command);
			}

		}
		
		#endregion
	}
}
