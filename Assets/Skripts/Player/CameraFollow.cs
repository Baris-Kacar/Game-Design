using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float followSpeed = 5f;

    private void Update()
    {
        if (playerTransform != null)
        {
            // Get the current position of the camera
            Vector3 currentPosition = transform.position;

            // Set the X position of the camera to match the player's X position
            currentPosition.x = playerTransform.position.x + 7;

            // Update the camera's position smoothly
            transform.position = Vector3.Lerp(transform.position, currentPosition, followSpeed * Time.deltaTime);
        }
    }
}
