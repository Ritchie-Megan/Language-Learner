using UnityEngine;

public class CameraDragMovement : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 difference;
    private Vector3 resetCamera;

    public Vector2 clampMinPosition;
    public Vector2 clampMaxPosition;

    private bool drag = false;

    void Start() {}

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButton(2)) {
            difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (drag == false)
            {
                drag = true;
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else {
            drag = false;
        }

        if (drag) {
            Camera.main.transform.position = origin - difference;
            Camera.main.transform.position = new Vector3(
            Mathf.Clamp(Camera.main.transform.position.x, clampMinPosition.x, clampMaxPosition.x),
            Mathf.Clamp(Camera.main.transform.position.y, clampMinPosition.y, clampMaxPosition.y), transform.position.z);
        }

        if (Input.GetMouseButton(1))
            Camera.main.transform.position = resetCamera;
    }

    public void setCameraResetPosition(Vector3 resetPosition) {
        resetCamera = new Vector3(resetPosition.x, resetPosition.y, -10);
        Camera.main.transform.position = resetCamera;
    }
}
