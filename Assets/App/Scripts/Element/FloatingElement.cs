using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatingElement : MonoBehaviour, IElement
{
	void Start()
	{
		GetComponent<MeshRenderer>().material.DOFade(0, 0);
	}

	public void Show(float delay = 0.0f)
	{
		GetComponent<MeshRenderer>().material.DOKill();
		GetComponent<MeshRenderer>().material.DOFade(1, 1.0f).SetDelay(delay);
	}

	public void Hide(float time = 1.0f)
	{
		GetComponent<MeshRenderer>().material.DOKill();
		GetComponent<MeshRenderer>().material.DOFade(0, 1.0f);
	}
}
