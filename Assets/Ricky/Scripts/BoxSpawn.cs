﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject BoxPrefab;
    private GameObject box;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Jump"))
        {
            box = Instantiate(BoxPrefab, this.transform);
            box.transform.parent = null;
        }
    }
}
