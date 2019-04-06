using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	public List<GameObject> firstSteps;
	public Player player;
	public Transform debugPosition;

	[HideInInspector] public bool playing = false;

	private void Awake()
	{
		OnTitle();

		//debug
		//GameStart();
		//player.gameObject.transform.SetPositionAndRotation(debugPosition.position, debugPosition.rotation);
	}

	void OnTitle()
	{
		cameraSwitcher.ChangeCamera(CameraSwitcher.VCamType.TITLE);
		TitlePanel.Instance.Show();
	}

	public void GameStart()
	{
		SoundManager.Instance.Play(SoundManager.SEType.TITLE);

		playing = true;
		ShowTutorials();
		cameraSwitcher.ChangeCamera(CameraSwitcher.VCamType.GAME);
		ScorePanel.Instance.Show();
	}

	public void GameOver()
	{
		SoundManager.Instance.Play(SoundManager.SEType.OVER);

		playing = false;
		ScorePanel.Instance.Hide();
		RemainPanel.Instance.Hide();
		cameraSwitcher.ChangeCamera(CameraSwitcher.VCamType.END);
		ClearPanel.Instance.Show();
	}

	public void GameClear()
	{
		SoundManager.Instance.Play(SoundManager.SEType.CLEAR);

		playing = false;
		ScorePanel.Instance.Hide();
		RemainPanel.Instance.Hide();
		cameraSwitcher.ChangeCamera(CameraSwitcher.VCamType.END);
		ClearPanel.Instance.Clear();
		ClearPanel.Instance.Show();
	}

	public void Restart()
	{
		Refresh();
		GameStart();
	}

	public void Refresh()
	{
		DOTween.KillAll();
		ScorePanel.Instance.Refresh();
		RemainPanel.Instance.Refresh();
		RefreshFlags();

		playing = false;
		player.Refresh();
	}

	//--------------------------------------------------

	void RefreshFlags()
	{
		TriggerCube[] triggerCubes = FindObjectsOfType(typeof(TriggerCube)) as TriggerCube[];
		foreach (TriggerCube triggerCube in triggerCubes)
		{
			triggerCube.Refresh();
		}
		PoleElement[] poles = FindObjectsOfType(typeof(PoleElement)) as PoleElement[];
		foreach (PoleElement pole in poles)
		{
			pole.Refresh();
		}
	}

	void ShowTutorials()
	{
		for (int i = 0; i < firstSteps.Count; i++)
		{
			firstSteps[i].GetComponent<IElement>().Show(0.2f * i);
		}
	}

	public void HideTutorials()
	{
		for (int i = 0; i < firstSteps.Count; i++)
		{
			firstSteps[i].GetComponent<IElement>().Hide(0.2f * i);
		}
	}


	#region properties
	private CameraSwitcher m_CameraSwitcher;
	private CameraSwitcher cameraSwitcher => m_CameraSwitcher ?? (m_CameraSwitcher = Camera.main.GetComponent<CameraSwitcher>());
	#endregion

}
