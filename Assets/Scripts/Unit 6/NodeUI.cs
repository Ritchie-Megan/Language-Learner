using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class NodeUI : MonoBehaviour
{
    public TMP_Text nameText;
    
    public void setName(string personName) {
        nameText.text = personName;
    }
}
