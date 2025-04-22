using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MoveBackground : MonoBehaviour
{
    public float speed;
    public List<RectTransform> patterns;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (RectTransform pattern in patterns)
        {
           
            pattern.localPosition += Vector3.down * (Time.deltaTime * speed);
            if (pattern.localPosition.y + 1130 < 0.001f)
                pattern.localPosition = new Vector3(pattern.localPosition.x, 1190, pattern.localPosition.z);
        }

        
    }
}
