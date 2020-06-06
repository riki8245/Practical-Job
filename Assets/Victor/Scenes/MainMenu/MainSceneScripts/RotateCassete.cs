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
    public Material locked;
    Material unlocked;
    bool levelLock;

    private void Awake()
    {
        animating = false;
        eventSystemBool = false;
        eventSystem = GameObject.Find("EventSystem");
        eventSystem.GetComponent<EventSystem>().enabled = true;
        if (!this.gameObject.name.Equals("OnlyRotation"))this.unlocked = this.GetComponentInChildren<MeshRenderer>().materials[1];
    
    }
    private void Start()
    {
        bool aux = !this.gameObject.name.Equals("Back") && !this.gameObject.name.Equals("OnlyRotation");
        levelLock = aux && int.Parse(this.gameObject.name.Substring(5)) > GameManager.instance.currentLevel;
        if (levelLock)
        {
            Material[] mats = this.gameObject.GetComponentInChildren<MeshRenderer>().materials;
            mats[1] = this.locked;
            this.gameObject.GetComponentInChildren<MeshRenderer>().materials = mats;
        }

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
        if (eventSystemBool && !levelLock)
        {
            if (AudioController.AudioInstance) AudioController.AudioInstance.soundSelectButton(true);
            eventSystem.GetComponent<EventSystem>().enabled = false;
            iTween.MoveTo(this.gameObject, iTween.Hash("x", onSelectedPos.position.x, "y", onSelectedPos.position.y, "z", onSelectedPos.position.z, "time", .5f, "onComplete", "LoadLevel"));
            iTween.ScaleTo(this.gameObject, new Vector3(-.3f, -.3f, .3f), .5f);
            iTween.RotateTo(this.gameObject, new Vector3(133.4f, 90f, 0f), .5f);
        }
        else if (levelLock) if (AudioController.AudioInstance) AudioController.AudioInstance.soundInvalidSelection(true);
    }
    private void LoadLevel() {
        new WaitForSeconds(1f);
        uIControl.SelectLevel(this.gameObject);
    }
    
}
