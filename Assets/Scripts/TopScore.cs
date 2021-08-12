using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopScore : MonoBehaviour
{
    public Text Label;
    
    private void Awake()
    {
        var n = GamePlay.Instance.Score;
        Label.text += n.ToString();
    }
}