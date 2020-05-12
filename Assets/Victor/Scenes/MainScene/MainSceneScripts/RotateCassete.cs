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
    private Material whenSelectedMat;
    private MeshRenderer cassete;
    public Texture2D[] texture2Ds;
    public Transform onSelectedPos;
    private Vector3 onNotSelectedPos;
    static Quaternion actualRotation;
    static bool animCompleted = true;
    private Material baseMat;
    private void Awake()
    {
        if (!this.gameObject.name.Equals("OnlyRotation"))
        {
            this.button = this.gameObject.GetComponent<Button>();
            this.cassete = this.gameObject.GetComponentInChildren<MeshRenderer>();
            this.onNotSelectedPos = this.transform.position;
            this.baseMat = this.cassete.materials[1];
            this.whenSelectedMat = this.baseMat;
            this.baseMat.SetTexture("_BaseMap", texture2Ds[0]);
        }
        else eventSystemBool = false;
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
                    iTween.MoveTo(this.gameObject, onSelectedPos.position, 1f);
                    iTween.ScaleTo(this.gameObject, new Vector3(-.2f, -.2f, .2f), 1f);
                    this.gameObject.transform.rotation = Quaternion.Euler(133.4f, 90f, 0f);
                    animCompleted = false;
                }
            }
            if (!this.gameObject.name.Equals("Back")) this.whenSelectedMat.SetTexture("_BaseMap", texture2Ds[int.Parse(this.gameObject.name.Substring(5))]);
            else this.whenSelectedMat.SetTexture("_BaseMap", texture2Ds[texture2Ds.Length - 1]);
            Material[] materials = this.cassete.materials;
            materials[1] = whenSelectedMat;
            this.cassete.materials = materials;
        }
        else
        {
            if (this.transform.position == this.onSelectedPos.position && !this.gameObject.name.Equals("OnlyRotation"))
            {
                iTween.MoveTo(this.gameObject, iTween.Hash("name", "backToOg","x", this.onNotSelectedPos.x,"y", this.onNotSelectedPos.y,"z", this.onNotSelectedPos.z,"time",.5f,"onComplete","AnimCompleted"));
                iTween.ScaleTo(this.gameObject, new Vector3(-.1f, -.1f, .1f), .5f);
                Material[] materials = this.cassete.materials;
                materials[1] = baseMat;
                print(this.baseMat);
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
}
