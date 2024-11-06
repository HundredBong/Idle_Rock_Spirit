using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //public 메서드는 2개

    public static PoolManager pool;
    public GameObject projPrefab;

    private void Awake()
    {
        pool = this;
    }

    List<GameObject> poolList = new(); //비활성화 된 객체 리스트

    public GameObject Pop()
    {
        if (poolList.Count <= 0) //꺼낼 객체가 없으면?
            Push(Instantiate(projPrefab)); //새로 객체를 하나 생성후 리스트에 넣어줌

        GameObject proj = poolList[0];

        poolList.Remove(proj);

        proj.gameObject.SetActive(true);
        proj.transform.SetParent(null);

        return proj;
    }

    public void Push(GameObject proj)
    {
        poolList.Add(proj);
        proj.gameObject.SetActive(false);
        proj.transform.SetParent(transform, false);
    }


    public void Push(GameObject proj, float delay) //Destroy를 대체할 Push 오버로드
    {
        StartCoroutine(PushCoroutine(proj, delay));
    }

    IEnumerator PushCoroutine(GameObject proj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Push(proj);
    }
}
