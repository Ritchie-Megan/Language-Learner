using UnityEngine;

public class AcceptButton : MonoBehaviour
{
    public GameObject draggablePrefab;
    public Transform activityContentPanel;

    public void checkAccept() {
        Debug.Log("Accept Button Pressed");
        //TODO: create logic that checks to see if you should accept the invitation or not
        //if on a yes invitation:
        /*
        if () {
            //makes a new draggable object upon correct accept invitation and displays name
            //TODO: make sure that all activities are generated with a one hour limit, so we don't need multiple gameObjects
            GameObject newActivityDragable = Instantiate(activityContentPanel, schedulePanel);
            newActivityDragable.GetComponent<TextMeshProUGUI>.text = acivity;
        }
        */
    }
}
