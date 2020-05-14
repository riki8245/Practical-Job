using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotateCassete : MonoBehaviour
{
    Button button;
    GameObject eventSystem;
    public static bool eventSystemBool { get; set; }
    public Material whenSelectedMat;
    public Material whenNotSelectedMat;
    public GameObject ifSelected;

    private MeshRenderer cassete;
    public Texture2D[] texture2Ds;
    public Transform onSelectedPos;
    private Vector3 onNotSelectedPos;
    static Quaternion actualRotation;
    static bool animCompleted;
    private void Awake()
    {
        if (!this.gameObject.name.Equals("OnlyRotation"))
        {
            this.button = this.gameObject.GetComponent<Button>();
            this.cassete = this.gameObject.GetComponentInChildren<MeshRenderer>();
            this.onNotSelectedPos = this.transform.position;
            Material[] materials = this.cassete.materials;
        }
        else eventSystemBool = false;
        animCompleted = true;
        this.eventSystem = GameObject.Find("EventSystem");



    }
    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name.Equals("OnlyRotation")) actualRotation = this.transform.rotation;
        if (eventSystemBool && this.eventSystem.GetComponent<EventSystem>().currentSelectedGameObject.Equals(this.gameObject))
        {
            if (this.transform.position != onSelectedPos.position)
            {
                if (animCompleted)
                {
                    iTween.MoveTo(this.gameObject,iTween.Hash("x", onSelectedPos.position.x, "y", onSelectedPos.position.y, "z", onSelectedPos.position.z, "time",.5f,"onStart","ChangeMaterial"));
                    iTween.ScaleTo(this.gameObject, new Vector3(-.3f, -.3f, .3f), .5f);
                    iTween.RotateTo(this.gameObject, new Vector3(133.4f, 90f, 0f), .5f);
                    animCompleted = false;
                }
            }
        }
        else
        {
            if (this.transform.position == this.onSelectedPos.position && !this.gameObject.name.Equals("OnlyRotation"))
            {
                iTween.MoveTo(this.gameObject, iTween.Hash("name", "backToOg","x", this.onNotSelectedPos.x,"y", this.onNotSelectedPos.y,"z", this.onNotSelectedPos.z,"time",.5f,"onComplete","AnimCompleted"));
                iTween.ScaleTo(this.gameObject, new Vector3(-.1f, -.1f, .1f), .5f);
                Material[] materials = this.cassete.materials;
                ifSelected.SetActive(false);
                materials[1] = whenNotSelectedMat;
                this.cassete.materials = materials;
            }
            else {
                transform.Rotate(30f * Time.deltaTime, 30f * Time.deltaTime, 30f * Time.deltaTime);
            }
        }
    }
    private void AnimCompleted()
    {
        animCompleted = true;
        this.transform.rotation = actualRotation;
    }
    private void ChangeMaterial()
    {
        if (!this.gameObject.name.Equals("Back")) this.whenSelectedMat.SetTexture("_BaseMap", texture2Ds[int.Parse(this.gameObject.name.Substring(5))]);
        else this.whenSelectedMat.SetTexture("_BaseMap", texture2Ds[texture2Ds.Length - 1]);
        Material[] materials = this.cassete.materials;
        ifSelected.SetActive(true);
        materials[1] = whenSelectedMat;
        this.cassete.materials = materials;
    }
}
