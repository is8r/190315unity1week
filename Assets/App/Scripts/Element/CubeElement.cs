using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeElement : MonoBehaviour, IElement
{
    [SerializeField] bool isFirst = false;

    void Start()
    {
        if(isFirst == false)
        {
            transform.localScale = Vector3.zero;
        }
    }

    public void Show(float delay = 0.0f)
    {
        transform.DOScale(1, 1f).SetEase(Ease.OutBounce).SetDelay(delay);
    }

    public void Hide(float time = 1.0f)
    {
        transform.DOScale(0, time).SetEase(Ease.InElastic);
    }
}
