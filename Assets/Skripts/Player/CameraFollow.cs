using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float followSpeed = 5f;
    public int adjustmentValue = 5;

    [SerializeField]
    private float minXPosition = 0f; // Set this to the starting X position of the camera

    private void Update()
    {
        if (playerTransform != null)
        {
            // Get the current position of the camera
            Vector3 currentPosition = transform.position;

            // Set the X position of the camera to match the player's X position
            currentPosition.x = Mathf.Max(playerTransform.position.x + adjustmentValue, minXPosition);

            // Update the camera's position smoothly
            transform.position = Vector3.Lerp(transform.position, currentPosition, followSpeed * Time.deltaTime);
        }
    }
}


