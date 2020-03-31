using UnityEngine;

public class BoxController : MonoBehaviour
{
    CharacterController b_Controller;
    Vector3 b_moveDirection;
    PlayerController playerController;
    CameraAdjust cameraAdjust;

    private float b_horizontalMove;
    private const float b_Speed = 3.5f;
    private float b_verticalMove;
    private float b_fallVelocity;
    private const float b_gravity = 10f;

    private void Awake()
    {
        b_Controller = this.GetComponent<CharacterController>();
    }

    public void RotateBox(float slopeAngle, Vector3 slopeNormalV)
    {
        if (slopeNormalV.x != 0)
        {
            if (slopeNormalV.x < 0)
                iTween.RotateTo(this.gameObject, new Vector3(0f, 0f, slopeAngle), 1f);
            else
                iTween.RotateTo(this.gameObject, new Vector3(0f, 0f, -slopeAngle), 1f);
        }
        else if (slopeNormalV.z != 0)
        {
            if (slopeNormalV.z < 0)
                iTween.RotateTo(this.gameObject, new Vector3(-slopeAngle, 0f, 0f), 1f);
            else
                iTween.RotateTo(this.gameObject, new Vector3(slopeAngle, 0f, 0f), 1f);
        }
        else
            iTween.RotateTo(this.gameObject, slopeNormalV, 1.5f);
    }

    private void Update()
    {
        b_horizontalMove = Input.GetAxis("Horizontal");
        b_verticalMove = Input.GetAxis("Vertical");
        MoveBox();
        SetGravity();
        b_Controller.Move(b_moveDirection * Time.deltaTime);
    }

    private void SetGravity()
    {
        if (b_Controller.isGrounded)
            b_fallVelocity = -b_gravity * Time.deltaTime;

        else
            b_fallVelocity -= b_gravity * Time.deltaTime;

        b_moveDirection.y = b_fallVelocity;

    }

    private void MoveBox()
    {
        if (playerController != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (Mathf.Abs(playerController.transform.position.x - transform.position.x) >= .5f &&
                    Mathf.Abs(playerController.transform.position.z - transform.position.z) <= .5f)
                    playerController.axisToUseWhileBox = 1;
                else
                    playerController.axisToUseWhileBox = 2;
            }
            if (Input.GetButton("Fire1"))
            {
                playerController.p_PushingOrPulling = true;
                if (playerController.axisToUseWhileBox == 1)
                    b_moveDirection = Vector3.right * (b_horizontalMove > 0f ? 1 : b_horizontalMove < 0f ? -1 : 0) * b_Speed;
                else if (playerController.axisToUseWhileBox == 2)
                    b_moveDirection = Vector3.forward * (b_verticalMove > 0f ? 1 : b_verticalMove < 0f ? -1 : 0) * b_Speed;


                
            }
            if(Input.GetButtonUp("Fire1"))
            {
                playerController.p_PushingOrPulling = false;
                playerController.axisToUseWhileBox = 0;
            }
        }
        else
            b_moveDirection = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.p_PushingOrPulling = false;
            playerController = null;
        }
    }
    private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle(); //create a new variable
        guiStyle.fontSize = 30;
        GUI.Label(new Rect(10, 130, 500, 100), "B_Grounded: " + b_Controller.isGrounded, guiStyle);
    }
}
