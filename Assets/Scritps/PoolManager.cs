using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //public �޼���� 2��

    public static PoolManager pool;
    public GameObject projPrefab;

    private void Awake()
    {
        pool = this;
    }

    List<GameObject> poolList = new(); //��Ȱ��ȭ �� ��ü ����Ʈ

    public GameObject Pop()
    {
        if (poolList.Count <= 0) //���� ��ü�� ������?
            Push(Instantiate(projPrefab)); //���� ��ü�� �ϳ� ������ ����Ʈ�� �־���

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


    public void Push(GameObject proj, float delay) //Destroy�� ��ü�� Push �����ε�
    {
        StartCoroutine(PushCoroutine(proj, delay));
    }

    IEnumerator PushCoroutine(GameObject proj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Push(proj);
    }
}
