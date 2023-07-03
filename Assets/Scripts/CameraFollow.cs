using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public Vector3 offset; // Offset from the player's position
    public float followSpeed = 5f; // Adjust this value to control the camera's follow speed

    private void LateUpdate()
    {
        // Calculate the desired position for the camera
        Vector3 desiredPosition = target.position + Quaternion.Euler(0, 180, 0) * offset; // Reverse the offset direction

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Make the camera look at the player's position
        transform.LookAt(target.position);
    }
}
