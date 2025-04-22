using UnityEngine;
using UnityEngine.UI;

public class featureEditor : MonoBehaviour
{
    public void setImageColor(string color)
    {
        Image featureImage = gameObject.GetComponent<Image>();
        
        if (color == "negros" || color == "negro")
            featureImage.color = new Color32(45, 45, 45, 255);
        else if (color == "marrones" || color == "castaño")
            featureImage.color = new Color32(100, 75, 50, 255);
        else if (color == "azules")
            featureImage.color = new Color32(50, 140, 185, 255);
        else if (color == "verdes")
            featureImage.color = new Color32(60, 100, 60, 255);
        else if (color == "grises")
            featureImage.color = new Color32(110, 140, 160, 255);
        else if (color == "rubio")
            featureImage.color = new Color32(225, 215, 170, 255);
        else if (color == "pelirroja")
            featureImage.color = new Color32(205, 135, 45, 255);
    }
}
