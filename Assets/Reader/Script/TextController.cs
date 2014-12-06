using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Text controller.
/// </summary>
public class TextController : MonoBehaviour
{
	[SerializeField] Text _uiText = null;
	/// <summary>  文字列の表示時間 </summary>
	[Range(10f, 30f)] public float intervalForCharacterDisplay = 15;

	private string currentText = string.Empty;
	private float timeElapsed = 0;
	private float timeUntilDisplay = 0;
	private bool isSendFinishCallback = false;

	/// <summary>
	/// リクエストした文字列が全て表示完了していればtrue
	/// </summary>
	/// <value><c>true</c> if this instance is complete display text; otherwise, <c>false</c>.</value>
	public bool IsCompleteDisplayText 
	{
		get {
			return  Time.time > timeElapsed + timeUntilDisplay;
		}
	}

	/// <summary>
	/// 文字列の表示を強制する
	/// </summary>
	public void ForceCompleteDisplayText ()
	{
		timeUntilDisplay = 0;
	}

	/// <summary>
	/// 次のテキストを登録する
	/// </summary>
	/// <param name="text">表示するテキスト</param>
	public void SetNextLine (string text)
	{
		currentText = text;
		timeUntilDisplay = currentText.Length / intervalForCharacterDisplay;
		timeElapsed = Time.time;
		isSendFinishCallback = false;
	}

#region UNITY_DELEGATE

	void Start()
	{
		if( _uiText == null ){
			Debug.LogError("uiTextが設定されていません。Textを設定して下さい");
			Debug.LogError("TextControllerを無効化します");
			enabled = false;
		}
	}

	void Update ()
	{
		if (IsCompleteDisplayText) {
			_uiText.text = currentText;

			if (! isSendFinishCallback) {
				gameObject.SendMessage ("OnCompleteDisplayText", SendMessageOptions.DontRequireReceiver);
				isSendFinishCallback = true;
			}
		} else {
			float rate = (Time.time - timeElapsed) / timeUntilDisplay;
			int currentCharacterDisplayCount = Mathf.FloorToInt (rate * currentText.Length);
			_uiText.text = currentText.Substring (0, currentCharacterDisplayCount);
		}
	}
#endregion
}
