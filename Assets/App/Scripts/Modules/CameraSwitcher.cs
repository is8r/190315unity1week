using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
	public enum VCamType
	{
		TITLE,
		GAME,
		END
	}

	public GameObject titleCam;
	public GameObject gameCam;
	public GameObject endCam;

	private void Awake()
	{
		titleCam.SetActive(true);
		gameCam.SetActive(true);
		endCam.SetActive(true);
	}

	public void ChangeCamera(VCamType type)
	{
		titleCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
		gameCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
		endCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;

		switch (type)
		{
			case VCamType.TITLE:
				titleCam.GetComponent<CinemachineVirtualCamera>().Priority = 10;
				break;
			case VCamType.GAME:
				gameCam.GetComponent<CinemachineVirtualCamera>().Priority = 10;
				break;
			case VCamType.END:
				endCam.GetComponent<CinemachineVirtualCamera>().Priority = 10;
				break;
			default:
				break;
		}
	}
}
