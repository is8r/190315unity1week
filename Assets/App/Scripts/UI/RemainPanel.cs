using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RemainPanel : SingletonMonoBehaviour<RemainPanel>
{
	int remain;
	bool ready = false;

	void Awake()
	{
		Refresh();
		Hide(0);
	}

	public void Refresh()
	{
		remain = -1;
		ready = false;
	}

	public int Count(int plus = 1, float delay = 0)
	{
		if (ready == false) return -1;

		remain += plus;
		text.CountUp(remain);

		DOVirtual.DelayedCall(delay, () =>
		{
			if (remain < 1)
			{
				GameManager.Instance.GameOver();
			}
		});

		return remain;
	}

	public void Show()
	{
		ready = true;
		Count(11);
		rectTransform.DOAnchorPosY(20, 1f);
	}

	public void Hide(float time = 0.5f)
	{
		rectTransform.DOAnchorPosY(-60, time);
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
