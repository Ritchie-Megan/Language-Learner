using UnityEngine;

[CreateAssetMenu(fileName = "CharacterFeature", menuName = "Scriptable Objects/CharacterFeature")]
public class CharacterFeature : ScriptableObject
{
    public Sprite featureSprite;
    public Sprite spriteCrop;
    public float yPos;
    public float scale;

    void Reset()
    {
        yPos = 0;
        scale = 1;
    }
    
}
