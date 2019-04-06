using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScorePanel : SingletonMonoBehaviour<ScorePanel>
{
	public int score
	{
		get;
		private set;
	}

	void Awake()
	{
		score = 0;
		Hide(0);
	}

	public int AddScore(int plus = 1)
	{
		score += plus;
		text.CountUp(score);
		return score;
	}

	public void Refresh()
	{
		score = 0;
		text.CountUp(0);
	}

	public void Show()
	{
		rectTransform.DOAnchorPosY(-5, 1f);
	}

	public void Hide(float time = 0.5f)
	{
		rectTransform.DOAnchorPosY(60, time);
	}

	#region properties
	private Text m_Text;
	private Text text => m_Text ?? (m_Text = gameObject.GetComponent<Text>());
	private RectTransform m_RectTransform;
	private RectTransform rectTransform => m_RectTransform ?? (m_RectTransform = transform as RectTransform);
	#endregion

	#region debug
	[Header("Debug Setting")]
	public bool isDebug = true;

	void OnGUI()
	{
		if (!isDebug) return;

		if (GUI.Button(new Rect(10, 10, 150, 30), "Show"))
		{
			Show();
		}

		if (GUI.Button(new Rect(10, 40, 150, 30), "Hide"))
		{
			Hide();
		}
	}
	#endregion
}
