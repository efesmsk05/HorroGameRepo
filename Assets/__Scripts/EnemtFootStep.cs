using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemtFootStep : MonoBehaviour
{
    public Transform[] paths;
    [SerializeField] GameObject footStepPrefab;
    [SerializeField] float pathTime = 5f;

    Vector3[] points;
    bool moveStart = false;
    Transform sonPath;
    private Coroutine footStepCoroutine;
    void Start()
    {
        Vector3[] points = new Vector3[paths.Length];
        
        for (int i = 0; i < paths.Length; i++)
        {
            points[i] = paths[i].position;
        }

        sonPath = paths[paths.Length - 1];
        transform.DOPath(points, pathTime, PathType.Linear)
            .SetEase(Ease.Linear);

        if(footStepCoroutine == null)
        {
            footStepCoroutine = StartCoroutine(SpawnFootStep());
        }
    }


    private void Update()
    {
        if(transform.position.x == sonPath.localPosition.x)// son konumdaysa
        {
            StopCoroutine(footStepCoroutine);
            print("bitti");
        }

    }



    IEnumerator SpawnFootStep()
    {

        while (true)
        {
            GameObject footStep = Instantiate(footStepPrefab, new Vector3(transform.position.x , transform.position.y -.5f , transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(2f);
            Destroy(footStep ,2f);
        }

    }

}



