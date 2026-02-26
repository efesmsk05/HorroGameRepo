using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path : MonoBehaviour
{
    public Transform[] paths;
    void Start()
    {
        Vector3[] points = new Vector3[paths.Length];
        for (int i = 0; i < paths.Length; i++)
        {
            points[i] = paths[i].position;
        }

        transform.DOPath(points, 5f, PathType.CatmullRom)
            .SetEase(Ease.Linear);
           // .SetLoops(-1, LoopType.Yoyo);

    }

}
