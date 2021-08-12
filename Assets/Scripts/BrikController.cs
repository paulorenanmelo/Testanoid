using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrikController : MonoBehaviour
{    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        var game = GamePlay.Instance;
        game.Score++;
    }
}