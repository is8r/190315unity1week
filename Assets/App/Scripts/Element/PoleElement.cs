using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PoleElement : MonoBehaviour, IElement
{
	public GameObject pole;
	public ParticleSystem particle;
	public bool tutorials = false;

	bool already = false;

	private void Start()
	{
		transform.DOScale(0, 0);
	}

	public void Show(float delay = 0.0f)
	{
		transform.DOScale(1, 1f).SetEase(Ease.OutElastic).SetDelay(delay + 1.0f);
	}

	public void Hide(float time = 1.0f)
	{
		transform.DOScale(0, time).SetEase(Ease.InElastic);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Action();
		}
	}

	public void Refresh()
	{
		already = false;
	}

	void Action()
	{
		if (already) return;
		already = true;

		if (tutorials == false)
		{
			RemainPanel.Instance.Count(10);
		}

		SoundManager.Instance.Play(SoundManager.SEType.POLE);

		particle.Play();
		Hide();

		Check();
	}

	void Check()
	{
		int clear = 0;
		PoleElement[] poles = FindObjectsOfType(typeof(PoleElement)) as PoleElement[];
		foreach (PoleElement pole in poles)
		{
			if (pole.already)
			{
				clear++;
			}
		}
		//print(poles.Length.ToString() + "本中" + clear.ToString() + "本取得した！");

		if (poles.Length == clear)
		{
			GameManager.Instance.GameClear();
		}
	}
}
