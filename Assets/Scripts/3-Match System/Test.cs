using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class Test : MonoBehaviour
{
    void Start()
    {
        transform.DOMove(Vector2.up * 100, 2);
    }

}
