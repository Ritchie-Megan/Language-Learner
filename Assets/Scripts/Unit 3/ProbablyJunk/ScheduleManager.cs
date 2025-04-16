using UnityEngine;

public class ScheduleManager : MonoBehaviour
{
    public GameObject schedulePanel;
    public GameObject slotPrefab;
    public int slotCount = 91;  
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < slotCount; i++) {
            //Slot slot = Instantiate(slotPrefab, schedulePanel.transform).GetComponent<Slot>;
            Instantiate(slotPrefab, schedulePanel.transform);
        }
    }
}
