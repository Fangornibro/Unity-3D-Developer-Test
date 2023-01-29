using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rb.MovePosition(new Vector3(rb.position.x, rb.position.y, rb.position.z + Time.deltaTime * speed));
    }
}
