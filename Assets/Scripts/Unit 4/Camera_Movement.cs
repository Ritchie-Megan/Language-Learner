using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    public Transform player;
    public float smoothspeed = .5f;
    public Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 target = player.TransformPoint(new Vector3(0, 5, -10));
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothspeed);
        }
    }
}
