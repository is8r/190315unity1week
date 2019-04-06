using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
	enum DirectionType
	{
		FORWARD,
		BACK,
		RIGHT,
		LEFT,
		FALL
	}

	bool isWait = false;
	Tween tween;

	void Update()
	{
		if (isWait) return;
		if (GameManager.Instance.playing == false) return;

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			Move(DirectionType.FORWARD);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Move(DirectionType.RIGHT);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			Move(DirectionType.BACK);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Move(DirectionType.LEFT);
		}
		else if (Input.GetKeyDown(KeyCode.Return))
		{
			Move(DirectionType.FALL);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Flag"))
		{
			animator.SetTrigger("Joy");
			Wait(2f);
		}
	}

	//移動
	void Move(DirectionType type)
	{
		switch (type)
		{
			case DirectionType.FORWARD:
				OnFrorward(type);
				break;
			case DirectionType.BACK:
				break;
			case DirectionType.RIGHT:
				OnTurn(type);
				break;
			case DirectionType.LEFT:
				OnTurn(type);
				break;
			case DirectionType.FALL:
				OnSide(type);
				break;
			default:
				break;
		}
	}

	//(移動)前進
	void OnFrorward(DirectionType type)
	{
		Vector3 force = Vector3.zero;
		switch (type)
		{
			case DirectionType.FORWARD:
				force = transform.forward;
				break;
			default:
				break;
		}

		Vector3 now = transform.position;
		transform.DOMove(now + force, .5f).SetDelay(.4f);
		Wait(.9f);

		animator.SetFloat("Speed", 1.0f);

		floorCreater.Create(transform, force);

		SoundManager.Instance.Play(SoundManager.SEType.DASH);

		ScorePanel.Instance.AddScore(1);

		RemainPanel.Instance.Count(-1, 1.4f);
	}

	//(移動)横の壁に移動
	void OnSide(DirectionType type)
	{
		Quaternion q = Quaternion.identity;
		switch (type)
		{
			case DirectionType.RIGHT:
				q = Quaternion.Euler(0, 0, -90);
				break;
			case DirectionType.LEFT:
				q = Quaternion.Euler(0, 0, 90);
				break;
			case DirectionType.FALL:
				q = Quaternion.Euler(90, 0, 0);
				break;
			default:
				break;
		}
		transform.DOLocalRotateQuaternion(transform.rotation * q, 1f).SetDelay(1f);
		Wait(1f + 1f);

		animator.SetTrigger("Jump");

		floorCreater.Create(transform, q);

		SoundManager.Instance.Play(SoundManager.SEType.JUMP, 1.0f);

		ScorePanel.Instance.AddScore(1);

		RemainPanel.Instance.Count(-1, 2.5f);
	}

	//(移動)横を向く
	void OnTurn(DirectionType type)
	{
		Quaternion q = Quaternion.identity;
		switch (type)
		{
			case DirectionType.RIGHT:
				q = Quaternion.Euler(0, 90, 0);
				break;
			case DirectionType.LEFT:
				q = Quaternion.Euler(0, -90, 0);
				break;
			default:
				break;
		}
		transform.DOLocalRotateQuaternion(transform.rotation * q, 1f);
		Wait(1f);
	}

	//待機
	void Wait(float time)
	{
		isWait = true;

		tween.Kill();
		tween = DOVirtual.DelayedCall(time, () =>
		{
			isWait = false;
			animator.SetFloat("Speed", 0f);
		});
	}

	public void Refresh()
	{
		transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
		floorCreater.Refresh();
	}

	#region properties
	private Animator m_Animator;
	private Animator animator => m_Animator ?? (m_Animator = gameObject.GetComponentInChildren<Animator>());
	private FloorCreater m_FloorCreater;
	private FloorCreater floorCreater => m_FloorCreater ?? (m_FloorCreater = gameObject.GetComponent<FloorCreater>());
	#endregion
}
