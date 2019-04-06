/*

hotwto:
		SoundManager.Instance.Play(SoundManager.SEType.POLE);

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
	public enum SEType
	{
		DASH,
		POLE,
		JUMP,
		TITLE,
		OVER,
		CLEAR
	}

	public List<ClipInfo> clips;
	[System.Serializable]
	public class ClipInfo
	{
		[SerializeField] public SEType type;
		[SerializeField] public AudioClip audio;
	}

	Tween tween;

	public void Play(SEType type)
	{
		audioSource.Stop();
		audioSource.PlayOneShot(GetClip(type));
	}

	public void Play(SEType type, float deley)
	{
		tween.Kill();
		tween = DOVirtual.DelayedCall(deley, () => Play(type));
	}

	AudioClip GetClip(SEType type)
	{
		return clips.Find(c => c.type == type).audio;
	}

	#region properties
	private AudioSource m_AudioSource;
	private AudioSource audioSource => m_AudioSource ?? (m_AudioSource = gameObject.GetComponent<AudioSource>());
	#endregion

	#region debug
	[Header("Debug Setting")]
	public bool isDebug = true;

	void OnGUI()
	{
		if (!isDebug) return;

		if (GUI.Button(new Rect(10, 10, 150, 30), "Play"))
		{
			Play(SEType.CLEAR, 1.0f);
		}
	}
	#endregion
}
