using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Occupation", menuName = "Scriptable Objects/Occupation")]
public class Occupation : ScriptableObject
{
    public string title;
    public List<string> associatedTraits;
    public int minAge;
    public int maxAge;
    public string description;
    
}
