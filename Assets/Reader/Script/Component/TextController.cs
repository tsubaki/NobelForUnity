using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Nobel
{
	public class TextController : MonoBehaviour 
	{
		[SerializeField][Range(0.001f, 0.3f), TooltipAttribute("１文字の表示時間")]
		public float IntervalForCharacterDisplay = 0.05f;

		[SerializeField, TooltipAttribute("ノベルゲームの文字表示に使うText")]
		private Text m_uiText;

		private string m_currentText = string.Empty;
		private float m_timeUntilDisplay = 0;
		private float m_timeElapsed = 1;
		private int m_lastUpdateCharacter = -1;
		

		public bool IsCompleteDisplayText 
		{
			get { return  Time.realtimeSinceStartup > m_timeElapsed + m_timeUntilDisplay; }
		}
		
		public void ForceCompleteDisplayText ()
		{
			m_timeUntilDisplay = 0;
		}
		
		public void SetNextLine(string text)
		{
			m_currentText = text;
			m_timeUntilDisplay = m_currentText.Length * IntervalForCharacterDisplay;
			m_timeElapsed = Time.realtimeSinceStartup;
			m_lastUpdateCharacter = -1;
		}
		
		#region UNITY_CALLBACK	
		
		void Update () 
		{
			int displayCharacterCount = (int)(Mathf.Clamp01((Time.realtimeSinceStartup - m_timeElapsed) / m_timeUntilDisplay) * m_currentText.Length);
			if( displayCharacterCount != m_lastUpdateCharacter ){
				m_uiText.text = m_currentText.Substring(0, displayCharacterCount);
				m_lastUpdateCharacter = displayCharacterCount;
			}
		}
		
		#endregion
	}
}