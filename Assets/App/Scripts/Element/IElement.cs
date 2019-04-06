using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElement
{
    void Show(float delay = 0.0f);
    void Hide(float time = 1.0f);
}
