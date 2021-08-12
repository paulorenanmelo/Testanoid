using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GamePlay.Instance.Lives--;
        GamePlay.Instance.Goal();
    }
}