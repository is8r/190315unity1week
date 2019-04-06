using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ClearPanel : SingletonMonoBehaviour<ClearPanel>
{
	public GameObject panel;
	public Text score;

	public GameObject overElement;
	public GameObject clearElement;

	private void Awake()
	{
		panel.SetActive(false);
		overElement.SetActive(true);
		clearElement.SetActive(false);
		canvasGroup.DOFade(0, 0);
	}

	public void Show()
	{
		panel.SetActive(true);
		canvasGroup.interactable = true;
		canvasGroup.DOFade(1, .5f);

		score.CountUp(ScorePanel.Instance.score);
	}

	public void Hide()
	{
		canvasGroup.interactable = false;
		canvasGroup.DOFade(0, .5f).OnComplete(() =>
		{
			panel.SetActive(false);
		});
	}

	public void Clear()
	{
		overElement.SetActive(false);
		clearElement.SetActive(true);
	}

	public void Restart()
	{
		Hide();

		DOVirtual.DelayedCall(.5f, () =>
		{
			overElement.SetActive(true);
			clearElement.SetActive(false);
			GameManager.Instance.Restart();
		});
	}

	//--------------------------------------------------

	#region properties
	private CanvasGroup m_CanvasGroup;
	private CanvasGroup canvasGroup => m_CanvasGroup ?? (m_CanvasGroup = gameObject.GetComponent<CanvasGroup>());
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
		if (GUI.Button(new Rect(10, 50, 150, 30), "Hide"))
		{
			Hide();
		}
		if (GUI.Button(new Rect(10, 90, 150, 30), "Clear"))
		{
			Clear();
		}
	}
	#endregion
}
