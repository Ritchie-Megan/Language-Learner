using UnityEngine;

public class Unit1Manager : MonoBehaviour
{
    public ProfileGenerator profGenerator;
    public GameObject winScreen;
    public GameObject loseScreen;
    private string ageRange;
    private string personality;
    private string eyeColor;
    private string hairType;
    private string status;
    private string gender;
    private int count;

    void Start()
    {
        ageRange = "";
        personality = "";
        eyeColor = "";
        hairType = "";
        status = "";
        gender = "";
        count = 0;
    }

    public void checkSelections()
    {
        if (personality == profGenerator.personality.text)
            count++;
        if (eyeColor == profGenerator.eyeColor.text)
            count++;
        if (hairType == profGenerator.hairType.text)
            count++;
        if (status == profGenerator.status.text)
            count++;
        if ((gender == "male") == profGenerator.male)
            count++;
        // if (ageRange == profGenerator.ageRange.text)
        //     count++;

        if (count == 5)
            winScreen.SetActive(true);
        else
            loseScreen.SetActive(false);
    }

    public void setAgeRange(string _ageRange)
    {
        ageRange = _ageRange;
    }

    public void setPersonality(string _personality)
    {
        personality = _personality;
    }

    public void setEyeColor(string _eyeColor)
    {
        eyeColor = _eyeColor;
    }

    public void setHairType(string _hairType)
    {
        hairType = _hairType;
    }

    public void setStatus(string _status)
    {
        status = _status;
    }

    public void setGender(string _gender)
    {
        gender = _gender;
    }
}
