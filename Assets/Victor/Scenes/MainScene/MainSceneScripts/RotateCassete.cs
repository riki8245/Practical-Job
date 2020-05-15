using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotateCassete : MonoBehaviour
{
    GameObject eventSystem;
    public static bool eventSystemBool { get; set; }
    public GameObject ifSelected;
    public Transform onSelectedPos;
    public UIControl uIControl;
    static Quaternion actualRotation;
    private bool animating;

    private void Awake()
    {
        animating = false;
        eventSystemBool = false;
        eventSystem = GameObject.Find("EventSystem");
        eventSystem.GetComponent<EventSystem>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        try
        {
            if (this.gameObject.name.Equals("OnlyRotation")) actualRotation = this.transform.rotation;
            if (eventSystemBool && eventSystem.GetComponent<EventSystem>().currentSelectedGameObject.Equals(this.gameObject))
            {
                ifSelected.SetActive(true);
                if (!this.transform.rotation.Equals(Quaternion.Euler(133.4f, 90f, 0f)) && !animating)
                {
                    this.animating = true;
                    iTween.RotateTo(this.gameObject, new Vector3(133.4f, 90f, 0f), .5f);
                }
            }
            else
            {
                ifSelected.SetActive(false);
                if (this.animating) this.animating = false;
                if (!this.transform.rotation.Equals(actualRotation)) this.transform.rotation = actualRotation;
                transform.Rotate(-30f * Time.deltaTime, -30f * Time.deltaTime, -30f * Time.deltaTime);
            }
        }
        catch (System.NullReferenceException){
        }
        
        
    }
    public void selectLevel()
    {
        if (eventSystemBool)
        {
            eventSystem.GetComponent<EventSystem>().enabled = false;
            iTween.MoveTo(this.gameObject, iTween.Hash("x", onSelectedPos.position.x, "y", onSelectedPos.position.y, "z", onSelectedPos.position.z, "time", .5f, "onComplete", "LoadLevel"));
            iTween.ScaleTo(this.gameObject, new Vector3(-.3f, -.3f, .3f), .5f);
            iTween.RotateTo(this.gameObject, new Vector3(133.4f, 90f, 0f), .5f);
        }
    }
    private void LoadLevel() {
        new WaitForSeconds(1f);
        uIControl.SelectLevel(this.gameObject);
    }
    
}
