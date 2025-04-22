using UnityEngine;
using UnityEngine.Events;

public class ListHeader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SpriteType spriteType;
    public UnityEvent<SpriteType> onListHeaderClicked;

    public void setListCategory()
    {
        onListHeaderClicked.Invoke(spriteType);
    }
}
