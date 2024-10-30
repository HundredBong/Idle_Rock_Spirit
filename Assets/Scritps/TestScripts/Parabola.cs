using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    public float deg;
    public float turretSpeed;

    public GameObject turret;
    public GameObject bullet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    deg = deg + Time.deltaTime * turretSpeed;
        //    float rad = deg * Mathf.Deg2Rad;
        //    turret.transform.position = new Vector2(Mathf.Cos(rad),Mathf.Sin(rad));
        //    turret.transform.eulerAngles = new Vector3(0, 0, deg);
        //}

        //else if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    deg = deg - Time.deltaTime * turretSpeed;
        //    float rad = deg * Mathf.Deg2Rad;
        //    turret.transform.localPosition = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        //    turret.transform.eulerAngles = new Vector3(0, 0, deg);
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject go = Instantiate(bullet,transform.position,transform.rotation);
            go.transform.position = turret.transform.position;
            go.transform.rotation = turret.transform.rotation;  
        }


    }
}
