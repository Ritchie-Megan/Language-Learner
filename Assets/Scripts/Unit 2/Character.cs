using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public string characterName;
    public Sprite portrait;
    public bool isMale;
    public HairLength shortHair;
    public HairColor hairColor;
    public bool hasGlasses;
    public bool hasHat;
    public bool hasFacialHair;
    public bool inDress;
    public bool inTShirt;
    public bool inLongSleeve;
    public bool hasTie;
    public bool hasPonyTail;
    
    public enum HairLength {Bald, VeryShort, Short, Long, Unknown}

    public enum HairColor {Brown, Blonde, Ginger, Blue, Black, White, Unknown}
}
