using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterFeature", menuName = "Scriptable Objects/CharacterFeature")]
public class CharacterFeature : ScriptableObject
{
    public Sprite fullSprite;
    public List<Sprite> subSprites;
    public Sprite croppedSprite;

    void Reset()
    {
        //yPos = 0;
        //scale = 1;
    }
    
}
