using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittleCanvas : MonoBehaviour
{
    public ButtonsCanvasAnimator buttons;
    private void Awake()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 20, this.gameObject.transform.position.z);
    }
    void Start()
    {
        iTween.MoveTo(this.transform.gameObject, iTween.Hash("y", this.gameObject.transform.position.y - 20, "easeType", "easeOutBounce", "time", 2f, "oncomplete","activeButtons"));

    }
    private void activeButtons()
    {
        buttons.enabled = true;
        iTween.ScaleTo(this.transform.gameObject, iTween.Hash("x", this.gameObject.transform.localScale.x + .02f, "y", this.gameObject.transform.localScale.y +.02f, "z", this.gameObject.transform.localScale.z + .02f, "easeType", "linear", "time", 3f, "looptype", "pingPong"));

    }

}
