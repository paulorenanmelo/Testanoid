using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This changes the ortographic camera size by a variable factor, when the resolution becomes wider or narrower than the reference given
/// </summary>
#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
[RequireComponent(typeof(Camera))]
public class CameraSizeController : MonoBehaviour
{
    [Tooltip("x=9,y=16 for portrait, x=16,y=9 for landscape")]
    [SerializeField] private Vector2Int _refAspectRatio = Vector2Int.one;
    [Tooltip("Canvas to read the aspect ratio from (optional). This is more flexible than using screen resolution.")]
    [SerializeField] private Canvas _refCanvas;

    [SerializeField] float camOrthoSizeRef = 5f;
    [SerializeField] float scaleFactor = 1f;
    [SerializeField] bool scaleNarrower;
    [SerializeField] bool scaleWider;
    [SerializeField] bool executeInEditMode = false;

    float currentAspectRatio;
    float refAspectRatio;
    Camera cam;

    private void OnEnable()
    {
        cam = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        Scale();
    }
#if UNITY_EDITOR
    private void OnGUI()
    {
        if (!Application.isPlaying)
            Scale();
    }
#endif
    private void Scale()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying && !executeInEditMode) return;
#endif
        if (_refCanvas == null)
            currentAspectRatio = Round((float)Screen.width / Screen.height);
        else
            currentAspectRatio = Round((float)_refCanvas.pixelRect.width / _refCanvas.pixelRect.height);

        refAspectRatio = Round((float)_refAspectRatio.x / _refAspectRatio.y);
        if (ScaleNarrower || ScaleWider)
            cam.orthographicSize = camOrthoSizeRef + (scaleFactor * (refAspectRatio - currentAspectRatio));
        else
            cam.orthographicSize = camOrthoSizeRef;
    }

    /// <summary>
    /// Use this to prevent floating point precision fluctuations, so keep it to 4 decimal cases of precision
    /// </summary>
    private float Round(float f)
    {
        return Mathf.Round(f * 10000) / 10000;
    }

    private bool ScaleNarrower
    {
        get
        {
            return scaleNarrower && currentAspectRatio < refAspectRatio;
        }
    }
    private bool ScaleWider
    {
        get
        {
            return scaleWider && currentAspectRatio > refAspectRatio;
        }
    }
}
