using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    public int mouseSensitivity; // Sensitivity of the mouse movement

    public Transform playerCamera; // Reference to the player's camera transform

    float xRotation, yRotation; // Variables to store rotation values
    private float mouseX, mouseY; // Variables to store mouse X & Y-axis movement


    void Start()
    {
        // Locks the cursor to the center of the screen and hides it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Scaling mouse movement by deltaTime and sensitivity
        mouseX *= Time.deltaTime * mouseSensitivity;
        mouseY *= Time.deltaTime * mouseSensitivity;

        // Adjusting yRotation based on mouseX movement
        yRotation += mouseX;

        // Adjusting xRotation based on mouseY movement, and clamping it within a range
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -35f, 40f);

        // Applying rotation to the player object (for left and right rotation)
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // Applying rotation to the player's camera (Both vertical and horizontal rotation)
        playerCamera.rotation = Quaternion.Euler(xRotation, yRotation, 0);



    }

    // This method is invoked when the player moves the mouse
    private void OnLook(InputValue input)
    {
        // Getting mouse input values
        mouseX = input.Get<Vector2>().x;
        mouseY = input.Get<Vector2>().y;
    }
}