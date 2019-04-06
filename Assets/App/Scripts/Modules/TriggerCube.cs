using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCube : MonoBehaviour
{
	public List<FloatingElement> items = new List<FloatingElement>();
	public List<PoleElement> nexts = new List<PoleElement>();
	public bool flgEndTutorials = false;
	public bool flgRemainSwitch = false;
	bool already = false;

	public void Refresh()
	{
		already = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (GameManager.Instance.playing == false) return;
		if (already) return;

		if (other.CompareTag("Player"))
		{
			already = true;

			if (items.Count > 0)
			{
				for (int i = 0; i < items.Count; i++)
				{
					items[i].Show(i * 1.2f);
				}
			}
			if (nexts.Count > 0)
			{
				for (int i = 0; i < nexts.Count; i++)
				{
					nexts[i].Show(i * .2f);
				}
			}
			if (flgEndTutorials)
			{
				GameManager.Instance.HideTutorials();
			}
			if (flgRemainSwitch)
			{
				RemainPanel.Instance.Show();
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player") && items.Count > 0)
		{
			for (int i = 0; i < items.Count; i++)
			{
				items[i].Hide();
			}
		}
	}
}
