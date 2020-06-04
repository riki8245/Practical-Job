
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFadeOut : MonoBehaviour
{
    Color colorTransparent;
    private float durationFades;
    float timer;
    static int state = 1;
    private void Awake()
    {
        colorTransparent = new Color(0f, 0f, 0f, 0f);
        durationFades = 1f;
        timer = 0f;
        this.GetComponentInChildren<Image>().color = colorTransparent;

    }
    private void Start()
    {
        if (state == 1) StartCoroutine(FadeIn());
        else StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        state = 2;
        Color currentColor;
        while (timer < durationFades)
        {
            currentColor = Color.Lerp(colorTransparent, Color.black, timer);
            this.GetComponentInChildren<Image>().color = currentColor;
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(this);
        yield return null;
    }
    IEnumerator FadeOut()
    {
        state = 1;
        Color currentColor;
        while (timer < durationFades)
        {
            currentColor = Color.Lerp(Color.black, colorTransparent, timer);
            this.GetComponentInChildren<Image>().color = currentColor;
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(this);
        yield return null;
    }
}
