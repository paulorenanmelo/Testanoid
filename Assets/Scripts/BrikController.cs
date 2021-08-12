using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrikController : MonoBehaviour
{
    int scoreValue = 1;
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        GamePlay.Instance.Scored(scoreValue);
    }
}