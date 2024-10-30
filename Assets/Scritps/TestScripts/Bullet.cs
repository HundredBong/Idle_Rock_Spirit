using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Bullet : MonoBehaviour
//{
//    Rigidbody2D rb;
//    public float bulletSpeed;
//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        rb.velocity = transform.position * bulletSpeed;
//    }

//    // Update is called once per frame
//    void FixedUpdate()
//    {
//        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
//        transform.eulerAngles = new Vector3(0,0,angle);
//    }
//}
public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float bulletSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       // rb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
