using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Receive the input from the player and controls this gameobject's rigidbody
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _velocity = 1f;
    [SerializeField] private float _maxSpeed = 6f;

    private Rigidbody2D rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (rbody.velocity.x < _maxSpeed)
                rbody.AddForce(Vector2.left * _velocity);
            //rbody.velocity = Vector2.left * _velocity;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (rbody.velocity.x < _maxSpeed)
                rbody.AddForce(Vector2.right * _velocity);
            //rbody.velocity = Vector2.right * _velocity;
        }
        else
        {
            //rbody.velocity = Vector2.zero;
        }
    }

    public void Reset()
    {
        var pos1 = transform.position;
        pos1.x = 0f;
        transform.position = pos1;
    }
}