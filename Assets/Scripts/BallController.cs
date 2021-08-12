using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float Speed = 1f;
    
    public void Kick()
    {
        var rb = GetComponent < Rigidbody2D>();
        rb.velocity = Random.insideUnitCircle * Speed;
    }

    private void Update()
    {
        var rb = GetComponent < Rigidbody2D>();
        rb.velocity = rb.velocity.normalized * Speed;
    }
}