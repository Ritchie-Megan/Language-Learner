using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro; 
using FamilyTreeBackend;


public class NodeSpawner : MonoBehaviour
{
    //public GameObject prefab;
    public TMP_Text nameText;
    public GameObject nodePrefab;
    public Transform treeBase;

    public Dictionary<Node, GameObject> nodeTree = new Dictionary<Node, GameObject>();
    public List<Vector2> nodePositionsBank = new List<Vector2>();


    // Update is called once per frame
    void Start()
    {
        HashSet<Node> nodes = new HashSet<Node>();
        generateNodes(nodes);
    }


    

    public void generateNodes(HashSet<FamilyTreeBackend.Node> nodes) {
        foreach (FamilyTreeBackend.Node person in nodes) {
            createNodeOnCanvas(person, 0, 0, "start");
        }
    }

    
    //TODO: if it goes off the screen
    public void createNodeOnCanvas(Node person, int x, int y, string relationship) {
        //avoiding multiples
        if (person == null || nodeTree.ContainsKey(person)) {
            return;
        }
        //create the instance
        GameObject newHolderObj = Instantiate(nodePrefab, treeBase);
        newHolderObj.GetComponentInChildren<TMP_Text>().text = person.personName;
        //make the panel item slot
        RectTransform rectTransform = newHolderObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(x,y);
        if (newHolderObj.GetComponent<ItemSlot>() == null) {
            newHolderObj.AddComponent<ItemSlot>();
        }
        

        //check to make sure there isn't overlap
        Vector2 nodePos = new Vector2(x,y);
        for (int i = 0; i < nodePositionsBank.Count; i++) {
            if (nodePositionsBank[i] == nodePos) {
                if (relationship == "spouse") {
                    shiftRight(x, y);
                }
                else if (relationship == "child") {
                    shiftRight(x, y);
                }
                else if (relationship == "parentOne") {
                    shiftLeft(x, y);
                }
                else if (relationship == "parentTwo") {
                    shiftRight(x, y);
                }
            }
                        
        }
        //make sure it doesn't go off?? did I do this with the scroll bar?

        //put into location
        newHolderObj.transform.localPosition = new Vector2(x, y);
        //store node to be able to reference
        nodeTree[person] = newHolderObj;
        //store placement
        nodePositionsBank.Add(new Vector2(x, y));

        //spouse determied (plus to x axis)
        if (person.spouse != null) {
            createNodeOnCanvas(person.spouse, x + 100, y, "spouse");
        }
        //children determined (minus to y axis for children)
        if (person.children.Count > 0) {
            int childrenNumber = person.children.Count;
            int center = (childrenNumber * 100)/2;
            if (center % 100 == 0) {
                center = center + (100 - center);
            }
            //if spouse, inbetween those two
            if (person.spouse != null) {
                for (int i = 0; i < childrenNumber; i++) {
                    // x + 100 = end of parent plane. "- center" places first child leftmost needed. "((i-1)*100)" will shift child 100 to the right each new child
                    createNodeOnCanvas(person.children[i], (x + 100) - center + ((i-1)*100), y - 60, "child");
                }
            }
            else {
                for (int i = 0; i < childrenNumber; i++) {
                    createNodeOnCanvas(person.children[i], (x + 100) - center + ((i-1)*100), y - 60, "child");
                }
            }
            
        }
        //parents determined (add to y axis)
        if (person.parentOne != null && person.parentTwo != null) {
            createNodeOnCanvas(person.parentOne, x - 100, y + 60, "parentOne");
            createNodeOnCanvas(person.parentTwo, x + 100, y + 60, "parentTwo");
        }
        else if (person.parentOne != null) {
            createNodeOnCanvas(person.parentOne, x, y + 60, "parentOne");
        }
        else if (person.parentTwo != null) {
            createNodeOnCanvas(person.parentTwo, x, y + 60, "parentTwo");
        }
        //siblings determined?


        //moves object to random place(testing purposes)
        //float x = Random.Range(-500f, 500f);
        //float y = Random.Range(-200f, 200f);
        //newHolderObj.transform.localPosition = new Vector2(x, y);
    }
    

    /*
    //start from the absolute top and go down for this one
    public void createNodeOnCanvas() {

    }
    */

    public void shiftRight(int posx, int posy) {
        foreach(FamilyTreeBackend.Node person in new List<FamilyTreeBackend.Node>(nodeTree.Keys)) {
            //https://discussions.unity.com/t/change-position-ui-image-in-canvas/722293/4
            GameObject nodeObj = nodeTree[person];
            RectTransform moveNode = nodeObj.GetComponent<RectTransform>();
            Vector2 position = moveNode.anchoredPosition;
            float getposx = position.x;
            float getposy = position.y;
            if (nodePositionsBank.Contains(new Vector2(getposx, getposy))) {
                nodePositionsBank.Remove(new Vector2(getposx, getposy));
            }

            if (getposx >= posx) {
                moveNode.anchoredPosition = new Vector2(getposx + 100, getposy);
                nodePositionsBank.Add(new Vector2(getposx + 100, getposy));
            }

        }
    }

    public void shiftLeft(int posx, int posy) {
        foreach(FamilyTreeBackend.Node person in new List<FamilyTreeBackend.Node>(nodeTree.Keys)) {
            GameObject nodeObj = nodeTree[person];
            RectTransform moveNode = nodeObj.GetComponent<RectTransform>();
            Vector2 position = moveNode.anchoredPosition;
            float getposx = position.x;
            float getposy = position.y;

            if (nodePositionsBank.Contains(new Vector2(getposx, getposy))) {
                nodePositionsBank.Remove(new Vector2(getposx, getposy));
            }

            if (getposx <= posx) {
                moveNode.anchoredPosition = new Vector2(getposx - 100, getposy);
                nodePositionsBank.Add(new Vector2(getposx - 100, getposy));
            }

        }
    }
    
    public void shiftUp(int posx, int posy) {
        foreach(FamilyTreeBackend.Node person in new List<FamilyTreeBackend.Node>(nodeTree.Keys)) {
            GameObject nodeObj = nodeTree[person];
            RectTransform moveNode = nodeObj.GetComponent<RectTransform>();
            Vector2 position = moveNode.anchoredPosition;
            float getposx = position.x;
            float getposy = position.y;

            if (nodePositionsBank.Contains(new Vector2(getposx, getposy))) {
                nodePositionsBank.Remove(new Vector2(getposx, getposy));
            }

            if (getposy >= posy) {
                moveNode.anchoredPosition = new Vector2(getposx, getposy + 60);
                nodePositionsBank.Add(new Vector2(getposx, getposy + 60));
            }

        }
    }
    
    public void shiftDown(int posx, int posy) {
        foreach(FamilyTreeBackend.Node person in new List<FamilyTreeBackend.Node>(nodeTree.Keys)) {
            GameObject nodeObj = nodeTree[person];
            RectTransform moveNode = nodeObj.GetComponent<RectTransform>();
            Vector2 position = moveNode.anchoredPosition;
            float getposx = position.x;
            float getposy = position.y;

            if (nodePositionsBank.Contains(new Vector2(getposx, getposy))) {
                nodePositionsBank.Remove(new Vector2(getposx, getposy));
            }

            if (getposy <= posy) {
                moveNode.anchoredPosition = new Vector2(getposx, getposy - 60);
                nodePositionsBank.Add(new Vector2(getposx, getposy - 60));
            }

        }
    }
    
    public void setName(string personName) {
        nameText.text = personName;
    }
    
    
}
