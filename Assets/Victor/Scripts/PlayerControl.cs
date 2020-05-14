using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField]private Vector3 p_input;
    [SerializeField] private float p_Speed;
    [SerializeField] private bool pushingOrPulling;
    [SerializeField] private float p_timeMovingObject;
    [SerializeField] private int axisToUseWhileBox;
    [SerializeField] private float p_fallVelocity;
    private const int p_gravity = 45;
    public bool inFrontBox;
    public GameObject box;
    [SerializeField] float forceToPushBox;
    public Collider[] colliders = new Collider[2];
    public bool canMoveBox;
    private void Awake()
    {
        this.characterController = this.GetComponent<CharacterController>();
        pushingOrPulling = false;
        inFrontBox = false;
        canMoveBox = true;
        box = null;
        forceToPushBox = 0f;
        int playerLayer = LayerMask.NameToLayer("Player");
        int slopeLayer = LayerMask.NameToLayer("SlopeLimitCollider");
    }
    private void Update()
    {
        float p_horizontalMove = Input.GetAxis("Horizontal");
        float p_verticalMove = Input.GetAxis("Vertical");
        MovePlayer(p_horizontalMove , p_verticalMove);
        inFrontBox = Physics.Raycast(transform.position, transform.forward,50f,LayerMask.GetMask("Player"));
        Physics.IgnoreCollision(colliders[0], colliders[1], !pushingOrPulling);
    }
   
    private void MovePlayer(float p_horizontalMove, float p_verticalMove)
    {
        p_input = new Vector3(p_horizontalMove, 0f, p_verticalMove);
        p_input = p_input.normalized;
        p_input = Camera.main.transform.TransformDirection(p_input);
        p_input.y = 0.0f;

        p_Speed = Input.GetButton("L3") ? 6.5f : 4.5f;
        ManageInputs();
        if (p_input != Vector3.zero && !pushingOrPulling)
        {
            characterController.transform.LookAt(characterController.transform.position + p_input);
            p_input *= p_Speed;
        }

        else if (pushingOrPulling)
        {
            if (axisToUseWhileBox == 1)
                p_input.z = 0f;
            else if (axisToUseWhileBox == 2)
                p_input.x = 0f;
            p_input *= Mathf.Clamp(p_Speed, .1f, p_Speed);
        }
        SetGravity();
        characterController.Move(p_input * Time.deltaTime);
    }
    void ManageInputs()
    {
        if (box != null)
        {
            if (canMoveBox)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    String playerPosRelativeBox = box.GetComponent<BoxControl>().playerSide;
                    axisToUseWhileBox = (playerPosRelativeBox.Equals("inLeft") || playerPosRelativeBox.Equals("inRight") ? 1 : playerPosRelativeBox.Equals("inFront") || playerPosRelativeBox.Equals("inBack") ? 2 : 0);
                    box.transform.parent = this.transform;
                    pushingOrPulling = true;
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                box.transform.parent = null;
                pushingOrPulling = false;
            }
            if (!pushingOrPulling)
            {
                if (Input.GetButtonDown("Fire3"))
                    forceToPushBox = 0f;
                if (Input.GetButton("Fire3") && !pushingOrPulling)
                    forceToPushBox += Time.deltaTime;
                if (Input.GetButtonUp("Fire3"))
                    box.GetComponent<BoxControl>().PushBox(forceToPushBox);
            }
        }
        else
        {
            pushingOrPulling = false;
            axisToUseWhileBox = 0;
        }
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
  
}
