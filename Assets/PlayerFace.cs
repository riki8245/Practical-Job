using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFace : MonoBehaviour
{
    public static int FaceState;
    // Start is called before the first frame update
    void Start()
    {
        FaceState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire2")) ChangeFaceState();
        //print(FaceState);
    }

    void ChangeFaceState()
    {
        if (FaceState == 2) FaceState = 0;
        else FaceState++;
    }
}
