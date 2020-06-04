
using UnityEngine;
using UnityEngine.UI;

public class CanvasFadeOut : MonoBehaviour
{
    Color color = new Color();
    float duration;
    float timer;
    private void Awake()
    {
        color = Color.black;
        duration = 3f;
        timer = 0f;
    }
    void Update()
    {
        color = Color.Lerp(Color.black, new Color(0f, 0f, 0f, 0f), timer);
        this.GetComponentInChildren<Image>().color = color;
        if (color.Equals(new Color(0, 0, 0, 0))) Destroy(this);
        else timer += Time.deltaTime / duration;
    }
}
