using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittleCanvas : MonoBehaviour
{

    public ButtonsCanvasAnimator buttons;
    private void Awake()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 50, this.gameObject.transform.position.z);
    }
    void Start()
    {
        iTween.MoveTo(this.transform.gameObject, iTween.Hash("y", this.gameObject.transform.position.y - 50, "easeType", "easeOutBounce", "time", 2f, "oncomplete","activeButtons"));

    }
    private void activeButtons()
    {
        buttons.enabled = true;
    }
}
