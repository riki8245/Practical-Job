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
    public GameObject LOD; //Borrar
    public Material[] materials; //Cambiar por shader
    public Material whenSelectedMat;
    private MeshRenderer cassete;
    public Texture2D[] texture2Ds;
    public Transform onSelectedPos;
    private Vector3 onNotSelectedPos;
    static Quaternion actualRotation;
    private void Awake()
    {
        this.button = this.gameObject.GetComponent<Button>();
        this.eventSystem = GameObject.Find("EventSystem");
        this.cassete = this.gameObject.GetComponentInChildren<MeshRenderer>();
        eventSystemBool = false;
        this.onNotSelectedPos = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (eventSystemBool && this.eventSystem.GetComponent<EventSystem>().currentSelectedGameObject.Equals(this.gameObject))
        {
            if (this.transform.position != onSelectedPos.position)
            {
                iTween.MoveTo(this.gameObject, onSelectedPos.position, 1f);
                iTween.ScaleTo(this.gameObject, new Vector3(.2f,-.2f,.2f), 1f);
                this.gameObject.transform.rotation = Quaternion.Euler(133.4f, 90f, 0f);

            }
            if (!this.gameObject.name.Equals("Back")) whenSelectedMat.SetTexture("_BaseMap", texture2Ds[int.Parse(gameObject.name.Substring(5))]);
            else whenSelectedMat.SetTexture("_BaseMap", texture2Ds[texture2Ds.Length - 1]);
            print(this.cassete.gameObject.name);
            this.cassete.materials[1] = whenSelectedMat;
        }
        else
        {
            if (this.transform.position == this.onSelectedPos.position)
            {
                iTween.MoveTo(this.gameObject, this.onNotSelectedPos, 1f);
                iTween.ScaleTo(this.gameObject, new Vector3(.1f, -.1f, .1f), 1f);
                this.transform.rotation = actualRotation;
            }
            else {
                transform.Rotate(20f * Time.deltaTime, 20f * Time.deltaTime, 20f * Time.deltaTime);
                actualRotation = this.transform.rotation;
            }
        }
    }
}
