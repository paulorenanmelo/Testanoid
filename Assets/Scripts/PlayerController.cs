using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _velocity;

    void Update()
    {
        var rbody = GetComponent<Rigidbody2D>();

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