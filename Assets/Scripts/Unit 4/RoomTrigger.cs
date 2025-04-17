using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public string roomName; // e.g., "Library"
    private bool playerInside;

    public MiniGameManager gameManager;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.Return))
        {
            gameManager.CheckRoom(roomName);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInside = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInside = false;
    }
}