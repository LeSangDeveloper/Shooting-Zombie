using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    string horizontalAxis = "Horizontal";
    string VerticalAxis = "Vertical";

    string JumpAxis = "Jump";

    public float speed = 1f;

    public Vector3 velocity;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0f;
    public LayerMask groundMask;
    bool isGrounded;

    public float jumpHeight = 2f; 

    public CharacterController controller;
    public GameObject Weapon1;
    public GameObject Weapon2;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Weapon2.SetActive(false);
            Weapon1.SetActive(true);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Weapon1.SetActive(false);
            Weapon2.SetActive(true);
        }

        if (GameManager.Instance.gameState != GameState.Playing)
            return;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float moveX = Input.GetAxis(horizontalAxis);
        float moveZ = Input.GetAxis(VerticalAxis);
    
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime); 
    
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y  = Mathf.Sqrt(jumpHeight * -2f * gravity); 
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
