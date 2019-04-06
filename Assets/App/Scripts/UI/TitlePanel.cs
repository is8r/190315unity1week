using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitlePanel : SingletonMonoBehaviour<TitlePanel>
{
	public GameObject panel;

	private void Awake()
	{
		panel.SetActive(false);
		canvasGroup.DOFade(0, 0);
	}

	public void Show()
	{
		panel.SetActive(true);
		canvasGroup.interactable = true;
		canvasGroup.DOFade(1, .5f);
	}

	public void Hide()
	{
		canvasGroup.interactable = false;
		canvasGroup.DOFade(0, .5f).OnComplete(() =>
		{
			panel.SetActive(false);
		});
	}

	public void Play()
	{
		Hide();

		DOVirtual.DelayedCall(.5f, () => GameManager.Instance.GameStart());
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
	}
	#endregion
}
