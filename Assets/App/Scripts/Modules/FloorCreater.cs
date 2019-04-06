using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCreater : MonoBehaviour
{
	//オリジナル
	public GameObject original;
	public GameObject original_foward;
	public GameObject original_back;

	//複製した要素
	List<GameObject> created = new List<GameObject>();

	//移動
	public void Create(Transform t, Vector3 pos)
	{
		Add(original_foward, t.position, t.rotation, .1f);
		Add(original_back, t.position + pos, t.rotation, .2f);
		Add(original, t.position + pos, t.rotation, .3f);
	}

	//回転
	public void Create(Transform t, Quaternion rot)
	{
		Add(original_foward, t.position, t.rotation, .1f);
		Add(original_back, t.position, t.rotation * rot, .2f);
		Add(original, t.position, t.rotation * rot, .3f);
	}

	//追加
	void Add(GameObject o, Vector3 pos, Quaternion rot, float delay)
	{
		GameObject clone = Instantiate(o, pos, rot);
		clone.SetActive(true);
		clone.GetComponent<IElement>().Show(delay);
		created.Add(clone);
	}

	//作成したものを全て削除
	public void Refresh()
	{
		for (int i = 0; i < created.Count; i++)
		{
			Destroy(created[i]);
		}
	}
}
