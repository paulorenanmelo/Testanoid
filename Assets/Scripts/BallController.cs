using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR // draw debug rays for the angle to find out minimum of dot product we need to accept the kick, from which I decided 0.98f is good
[ExecuteInEditMode]
#endif
public class BallController : MonoBehaviour
{
    public float Speed = 1f;
    private Rigidbody2D rb;
    [Header("Editor testing")]
    public float angle = 0;
    public Vector2 horizontal = Vector2.right;
    public Vector2 direction = Vector2.right;
    public float dot;

    public void Kick()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        Vector2 vel = Random.insideUnitCircle.normalized * Speed;
        float dot = Mathf.Abs(Vector2.Dot(vel, Vector2.right));
        if (dot >= 0.98f) // if the angle is too horizontal (ball will bounce forever)
        {
#if DEBUG
            Debug.Log("Random direction for ball kick too horizontal. Picking perpendicular.");
#endif
            vel = Vector2.Perpendicular(vel); // change it to the perpendicular vector to that
        }

        rb.velocity = vel;
    }

#if UNITY_EDITOR // draw debug rays for the angle to find out minimum of dot product we need to accept the kick, from which I decided 0.98f is good
    private void OnGUI()
    {
        direction = new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle)).normalized;
        dot = Vector2.Dot(direction, horizontal);
        Debug.DrawRay(Vector2.zero, horizontal, Color.green);
        Debug.DrawRay(Vector2.zero, direction, Color.red);
    }
#endif

    private void Update()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        rb.velocity = rb.velocity.normalized * Speed;
    }

    public void Reset()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector2.zero;
        transform.position = Vector3.zero;
    }
}