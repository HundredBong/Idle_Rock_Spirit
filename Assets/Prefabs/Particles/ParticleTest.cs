using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    public ParticleSystem pa;

    void Start()
    {
        ParticleSystem p = Instantiate(pa,transform.position,transform.localRotation);
        p.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
