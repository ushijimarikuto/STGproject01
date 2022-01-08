using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ShotBehavior : MonoBehaviour
{
    private Rigidbody rb;
    private float speed;

    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    speed = 5.0f;
    //}

    //void Update()
    //{
    //    rb.AddRelativeForce(Vector3.forward * speed);
    //}

    void FixedUpdate()
    {
        Destroy(gameObject, 2.0f);
    }
}
