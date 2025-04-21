using UnityEngine;

public class MoveWord : MonoBehaviour
{

    private ClickWrong manager;

    private float speed;

    public float baseSpeed = 1f;

    private bool correct;
    
    private DissapearOnClick dissapearOnClick;
    
    
    private RectTransform rectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        dissapearOnClick = GetComponent<DissapearOnClick>();
        correct = dissapearOnClick.isCorrect;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager == null) return;

        transform.position += Vector3.right * speed * Time.deltaTime;

        // Check if it's off the right side of the screen
        if (Camera.main.WorldToScreenPoint(transform.position).x >= Screen.width + 10f)
        {
            if (dissapearOnClick != null && !dissapearOnClick.isCorrect)
            {
                manager.WordMissed();
            }

            Destroy(gameObject);
        }
    }
    
    public void SetSpeedMultiplier(float speedMultiplier)
    {
        // Assuming you have a speed variable in this class
        // speed = baseSpeed * speedMultiplier;
        speed = baseSpeed * speedMultiplier; // Example: base speed of 1
        
    }
    
    public void SetGameManager(ClickWrong gameManager)
    {
        // Assuming you have a reference to the game manager in this class
        // this.gameManager = gameManager;
        manager = gameManager;
    }
}
