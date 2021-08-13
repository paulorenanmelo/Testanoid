using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TopScore : MonoBehaviour
{
    private TextMeshProUGUI Label;
    
    private void Awake()
    {
        Label = GetComponent<TextMeshProUGUI>();
        if (Label == null) return;

        Label.text += GamePlay.Instance.Score.ToString();
    }
}