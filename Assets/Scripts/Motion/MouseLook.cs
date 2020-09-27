using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    float mouseSensitivity = 100f;
    string mouseHorizontalAxis = "Mouse X";
    string mouseVerticalAxis = "Mouse Y";

    float xRotation = 0f;

    [SerializeField]
    Transform playerBody;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.gameState != GameState.Playing)
            return;

        float mouseX = Input.GetAxis(mouseHorizontalAxis) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseVerticalAxis) * mouseSensitivity * Time.deltaTime;   
   
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
