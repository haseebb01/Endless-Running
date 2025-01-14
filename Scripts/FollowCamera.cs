using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;        // The player's transform
    public Vector3 offset;          // Offset between the camera and the player
    private float y;
    public float followSpeed = 10f; // Speed at which the camera follows
    //public float rotationSpeed = 5f; // Speed at which the camera rotates to align with the player

    private void Start()
    {
        offset = transform.position;

    }
    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player Transform is not assigned!");
            return;
        }

        Vector3 followPos = player.position + offset;
        RaycastHit hit;
        if (Physics.Raycast(player.position, Vector3.down, out hit, 2.5f))
            y = Mathf.Lerp(y, hit.point.y, Time.deltaTime * followSpeed);
        else y = Mathf.Lerp(y, player.position.y, Time.deltaTime * followSpeed);
        followPos.y = offset.y + y;
        transform.position = followPos;
        //// Smoothly rotate the camera to align with the player
        //Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
