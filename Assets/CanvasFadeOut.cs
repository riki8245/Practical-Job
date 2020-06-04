
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFadeOut : MonoBehaviour
{
    Color colorTransparent = new Color(0f, 0f, 0f, 0f);
    private void Awake()
    {
        this.GetComponentInChildren<Image>().color = colorTransparent;
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        StartCoroutine(Fades());
    }

    IEnumerator Fades()
    {
        float timer = 0;

        Color currentColor;
        while (timer < 1f)
        {
            currentColor = Color.Lerp(colorTransparent, Color.black, timer);
            this.GetComponentInChildren<Image>().color = currentColor;
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0f;
        while (timer < .5f)
        {
           
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0f;
        while (timer < 1f)
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
