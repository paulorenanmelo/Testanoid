using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private static Stats _instance;
    public static Stats Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Stats>();
                if (_instance == null)
                {
#if DEBUG
                    Debug.LogWarning("Couldn't find Stats object. Generating object.");
#endif
                    var go = new GameObject("Stats");
                    _instance = go.AddComponent<Stats>();
                    DontDestroyOnLoad(_instance);
                }
            }

            return _instance;
        }
    }

    public uint Score = 0;
    public uint TotalScore = 0;
    public uint Lives = 3;
    public uint Briks = 4;
}
