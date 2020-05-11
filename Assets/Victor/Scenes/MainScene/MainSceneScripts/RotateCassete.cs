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
    private void Awake()
    {
        this.button = this.gameObject.GetComponent<Button>();
        this.eventSystem = GameObject.Find("EventSystem");
        eventSystemBool = false;        
    }
    // Update is called once per frame
    void Update()
    {
        if (eventSystemBool && this.eventSystem.GetComponent<EventSystem>().currentSelectedGameObject.Equals(this.gameObject))
            this.LOD.GetComponent<MeshRenderer>().material = materials[1];
        else this.LOD.GetComponent<MeshRenderer>().material = materials[0];
        transform.Rotate(20f * Time.deltaTime, 20f * Time.deltaTime, 20f * Time.deltaTime);
    }
}
