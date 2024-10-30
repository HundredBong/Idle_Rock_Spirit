using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLuncher : MonoBehaviour
{
    private Coroutine fireCoroutine;

    private void Start()
    {
       fireCoroutine = StartCoroutine(FireCoroutine());
    }

    public IEnumerator FireCoroutine()
    {
        yield return null;
    }
}
