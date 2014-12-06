using UnityEngine;
using System.Collections;

/// <summary>
/// I command.
/// </summary>
public interface ICommand 
{
	/// <summary>
	/// コマンドを解析し処理を実行する
	/// </summary>
	/// <param name="line">コマンド（一行）</param>
	bool Command(string line);

	/// <summary>
	/// テキスト表示が完了時に呼ばれる
	/// </summary>
	/// <param name="line">コマンド（一行）</param>
	void PreCommand(string line);
}
