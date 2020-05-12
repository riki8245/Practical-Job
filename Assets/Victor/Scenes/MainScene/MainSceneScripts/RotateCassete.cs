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
    private static Material baseMat;
    private void Awake()
    {
        if (!this.gameObject.name.Equals("OnlyRotation"))
        {
            this.button = this.gameObject.GetComponent<Button>();
            this.eventSystem = GameObject.Find("EventSystem");
            this.cassete = this.gameObject.GetComponentInChildren<MeshRenderer>();
            eventSystemBool = false;
            this.onNotSelectedPos = this.transform.position;
            baseMat = cassete.materials[1];
            whenSelectedMat = baseMat;
        }
        


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
            else whenSelectedMat.SetTexture("_BaseMap", texture2Ds[texture2Ds.Length - 2]);
            Material[] materials = this.cassete.materials;
            materials[1] = whenSelectedMat;
            this.cassete.materials = materials;
        }
        else
        {
            if (this.transform.position == this.onSelectedPos.position)
            {
                iTween.MoveTo(this.gameObject, iTween.Hash("name", "backToOg","x", this.onNotSelectedPos.x,"y", this.onNotSelectedPos.y,"z", this.onNotSelectedPos.z,"time",.5f,"onComplete","AnimCompleted"));
                iTween.ScaleTo(this.gameObject, new Vector3(-.1f, -.1f, .1f), .5f);
                this.transform.rotation = actualRotation;
                Material[] materials = this.cassete.materials;
                materials[1] = baseMat;
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
    }
}
