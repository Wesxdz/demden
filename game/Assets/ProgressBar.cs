using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public RawImage progress;

    private float barWidth;

    private void Start() {
        barWidth = GetComponent<RectTransform>().rect.width;
    }
    public void SetProgress(float percent)
    {
        var rt = progress.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(barWidth * percent, rt.sizeDelta.y); 
    }

}
