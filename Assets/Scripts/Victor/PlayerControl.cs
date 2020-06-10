using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 p_input;
    public Material neutral, scary, vulnerable, emission, smokeTrail;
    [ColorUsage(true, true)]
    public Color blue, red, green;

    private float p_Speed;
    private int axisToUseWhileBox;
    private float p_fallVelocity;
    private const int p_gravity = 45;
    private float forceToPushBox;
    private bool pushingOut;
    private string playerPosRelativeBox;
    private int playerLayer;
    private int slopeLayer;

    public int FaceState;
    public bool inFrontBox;
    public bool canMoveBox;
    public bool grabbingBox;

    public GameObject box;
    private bool enableUpdate;
    private Material[] mats;
    public bool collisionWhileGrabbing;

    public GameObject p_Head;

    float resetMovement;
    public bool onSlope;

    private void Awake()
    {
        this.characterController = this.GetComponent<CharacterController>();
        grabbingBox = false;
        inFrontBox = false;
        canMoveBox = true;
        pushingOut = false;
        onSlope = false;
        box = null;
        enableUpdate = false;
        forceToPushBox = 0f;
        playerPosRelativeBox = "";
        playerLayer = LayerMask.NameToLayer("Player");
        slopeLayer = LayerMask.NameToLayer("SlopeLimitCollider");
        mats = p_Head.GetComponent<SkinnedMeshRenderer>().materials;
        mats[1] = neutral;
        emission.SetColor("_EmissionColor", green);
        FaceState = 0;
        smokeTrail.SetColor("_EmissionColor", green);
        resetMovement = 0;
        Invoke("EnableUpdate", 1f);
    }
    void EnableUpdate()
    {
        enableUpdate = true;
    }
    private void Update()
    {
        float p_horizontalMove = Input.GetAxis("Horizontal");
        float p_verticalMove = Input.GetAxis("Vertical");
        if (enableUpdate) MovePlayer(p_horizontalMove, p_verticalMove);
        else MovePlayer(0f, 0f);
        inFrontBox = Physics.Raycast(transform.position, transform.forward, 50f, LayerMask.GetMask("Boxes"));
        //Debug.DrawRay(transform.position, transform.forward * 100, Color.green);
        Physics.IgnoreLayerCollision(playerLayer, slopeLayer, !grabbingBox);
        if (enableUpdate) ChangeFaceTexture();
        contToResetMove();
    }


    private void ChangeFaceTexture()
    {
        if (Input.GetButtonUp("Fire2"))
        {
            switch (mats[1].name.ToString())
            {
                case "Face_neutral":    mats[1] = vulnerable; emission.SetColor("_EmissionColor", blue);  smokeTrail.SetColor("_EmissionColor", blue);  FaceState = 1; break;
                case "Face_vulnerable": mats[1] = scary;      emission.SetColor("_EmissionColor", red);   smokeTrail.SetColor("_EmissionColor", red);   FaceState = 2; break;
                case "Face_scary":      mats[1] = neutral;    emission.SetColor("_EmissionColor", green); smokeTrail.SetColor("_EmissionColor", green); FaceState = 0; break;
                default: break;
            }
            p_Head.GetComponent<SkinnedMeshRenderer>().materials = mats;
        }
    }


    private void MovePlayer(float p_horizontalMove, float p_verticalMove)
    {
        p_input = new Vector3(p_horizontalMove, 0f, p_verticalMove);
        p_input = p_input.normalized;
        p_input = Camera.main.transform.TransformDirection(p_input);
        p_input.y = 0.0f;

        p_Speed = Input.GetButton("L3") ? 6.5f : 4.5f;
        if (enableUpdate) ManageInputs();
        if (p_input != Vector3.zero && !grabbingBox && !pushingOut && resetMovement <= 0)
        {
            characterController.transform.LookAt(characterController.transform.position + p_input);
            p_input *= p_Speed;
        }

        else if (grabbingBox)
        {
            if (axisToUseWhileBox == 1)
                p_input.z = 0f;
            else if (axisToUseWhileBox == 2)
                p_input.x = 0f;
            p_input *= Mathf.Clamp(p_Speed, .1f, p_Speed);
            if (collisionWhileGrabbing /*Physics.Raycast(movingBoxRayPosition.position, this.transform.forward,out hit, .2f,LayerMask.GetMask("Floor")) || Physics.Raycast(movingBoxRayPosition.position, this.transform.forward, out hit, .2f, LayerMask.GetMask("Walls"))*/)
            {
                if ((playerPosRelativeBox.Equals("inRight") && p_input.x < 0f) || (playerPosRelativeBox.Equals("inBack") && p_input.z > 0f)) p_input = Vector3.zero;
                else if ((playerPosRelativeBox.Equals("inLeft") && p_input.x > 0f) || (playerPosRelativeBox.Equals("inFront") && p_input.z < 0f)) p_input = Vector3.zero;
            }
        }

        SetGravity();
        if (!pushingOut && resetMovement <= 0)
            characterController.Move(p_input * Time.deltaTime);
    }
    void ManageInputs()
    {
        if (box != null)
        {
            if (inFrontBox && !box.GetComponent<BoxControl>().playerSide.Equals("")) //Dentro del trigger + delante de la caja
            {
                canMoveBox = true;

                playerPosRelativeBox = box.GetComponent<BoxControl>().playerSide;
                if (canMoveBox && !onSlope)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        axisToUseWhileBox = playerPosRelativeBox.Equals("inLeft") || playerPosRelativeBox.Equals("inFront") ? 1 : playerPosRelativeBox.Equals("inBack") || playerPosRelativeBox.Equals("inRight") ? 2 : 0;
                        LookAtBox();
                        box.transform.parent = this.transform;
                        grabbingBox = true;
                    }

                }
                if (Input.GetButtonUp("Fire1"))
                {
                    box.transform.parent = null;
                    grabbingBox = false;
                    playerPosRelativeBox = "";
                }
                if (!grabbingBox)
                {
                    if (Input.GetButtonDown("Fire3"))
                    {
                        if (AudioController.AudioInstance) AudioController.AudioInstance.soundChargeStart(true);
                        LookAtBox();
                        pushingOut = true;
                        forceToPushBox = 0f;
                    }
                    else if (Input.GetButton("Fire3"))
                    {
                        if (AudioController.AudioInstance) AudioController.AudioInstance.emitter[2].Stop();
                        forceToPushBox += Time.deltaTime;
                    }
                    else if (Input.GetButtonUp("Fire3"))
                    {
                        if (AudioController.AudioInstance) AudioController.AudioInstance.soundChargeStart(false);
                        resetMovement = 0.7f;
                        box.GetComponent<BoxControl>().PushBox(forceToPushBox);
                        pushingOut = false;
                    }
                }
            }
            box.GetComponent<BoxControl>().showOutline = inFrontBox && (!grabbingBox && !pushingOut) && box.GetComponent<Rigidbody>().velocity == Vector3.zero;
        }
        else
        {
            grabbingBox = false;
            canMoveBox = false;
            pushingOut = false;
            axisToUseWhileBox = 0;
            playerPosRelativeBox = "";
        }
    }

    private void contToResetMove()
    {
        if (resetMovement < 0) resetMovement = 0;
        else resetMovement -= Time.deltaTime;
    }
    private void SetGravity()
    {
        if (characterController.isGrounded)
        {
            p_fallVelocity = -p_gravity * Time.deltaTime;
            p_input.y = p_fallVelocity;
        }

        else
        {
            p_fallVelocity -= p_gravity * Time.deltaTime;
            p_input.y = p_fallVelocity;
        }
    }
    private void LookAtBox()
    {
        switch (playerPosRelativeBox)
        {
            case "inLeft":
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, 90f, this.transform.rotation.z);
                axisToUseWhileBox = 1;
                break;
            case "inRight":
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, -90f, this.transform.rotation.z);
                axisToUseWhileBox = 1;
                break;
            case "inFront":
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, 180f, this.transform.rotation.z);
                axisToUseWhileBox = 2;
                break;
            case "inBack":
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, 0f, this.transform.rotation.z);
                axisToUseWhileBox = 2;
                break;
            default:
                axisToUseWhileBox = 0;
                break;
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Enemy"))
            p_input = Vector3.zero;
    }
}
