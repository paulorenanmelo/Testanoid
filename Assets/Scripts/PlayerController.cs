using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Receive the input from the player and controls this gameobject's rigidbody
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _velocity;

    private Rigidbody2D rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rbody.velocity = Vector2.left * _velocity;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rbody.velocity = Vector2.right * _velocity;
        }
        else
        {
            rbody.velocity = Vector2.zero;
        }
    }
}