using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartBoxes_lvl11 : MonoBehaviour
{
    [SerializeField] Transform BoxSpawnPoint;
    bool CanDeleteBox;
    bool Deleting;
    GameObject box;
    float timerToDelete;
    [SerializeField] GameObject BoxPrefab;
    // Start is called before the first frame update
    private void Awake()
    {
        //BoxSpawnPoint = gameObject.GetComponentInChildren<Transform>();
        CanDeleteBox = false;
        timerToDelete = 0f;
        Deleting = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CanDeleteBox)
        {
            timerToDelete += Time.deltaTime;
            if(timerToDelete > 3f)
            {
                Deleting = true;
                CanDeleteBox = false;
                timerToDelete = 0;
                Instantiate(BoxPrefab, BoxSpawnPoint.position, box.transform.rotation);
                Destroy(box);
                Invoke("resetDeleting", 2f);
            }
        }
    }

    private void resetDeleting()
    {
        Deleting = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            if (!Deleting)
            {
                CanDeleteBox = true;
                box = other.gameObject; if (other.gameObject.CompareTag("Box"))
                {
                    CanDeleteBox = true;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            CanDeleteBox = false;
        }
    }
}
