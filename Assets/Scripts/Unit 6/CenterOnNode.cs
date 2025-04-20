using UnityEngine;

public class CenterOnNode : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public RectTransform targetNode;
    private RectTransform rect;
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    public void centerNodes()
    {
        rect.position = new Vector3(targetNode.position.x, rect.position.y, rect.position.z);
    }
}
