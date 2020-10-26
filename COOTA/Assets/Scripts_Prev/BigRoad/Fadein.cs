using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadein : MonoBehaviour
{
    public Image Panel;
    public float time;
    float F_time = 5f;
    private void Awake()
    {
        Fade();
    }
    public void Fade()
    {
        
        StartCoroutine(FadeFlow());
    }
    IEnumerator FadeFlow()
    {
        Color alpha = Panel.color;
        Panel.gameObject.SetActive(true);
        
        while (alpha.r > 0f && alpha.g>0f && alpha.b > 0f)
        {
            time -= Time.deltaTime / F_time;
            alpha.r = Mathf.Lerp(0, 1, time);
            alpha.g = Mathf.Lerp(0, 1, time);
            alpha.b = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
        yield return null;
        }
    }
     
}
