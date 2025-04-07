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
    //CURRENTLY ONLY WORKING ONE
    /*
    public void createNodeOnCanvas(Node person, int x, int y, string relationship) {
        //avoiding multiples
        if (person == null || nodeTree.ContainsKey(person)) {
            return;
        }
        //create the instance
        GameObject newHolderObj = Instantiate(nodePrefab, treeBase);
        newHolderObj.name = person.personName;
        //newHolderObj.GetComponentInChildren<TMP_Text>().text = person.personName;
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
    */
    
    //ATTEMPT like 6
    //correct levels, incorrect spacing
    public void createNodeOnCanvas(Node person, int x, int y, string relationship) {
        //avoiding multiples
        if (person == null || nodeTree.ContainsKey(person)) {
            return;
        }

        //create the instance
        GameObject newHolderObj = Instantiate(nodePrefab, treeBase);
        newHolderObj.name = person.personName;
        //newHolderObj.GetComponentInChildren<TMP_Text>().text = person.personName;
        //make the panel item slot
        RectTransform rectTransform = newHolderObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(x,y);
        if (newHolderObj.GetComponent<ItemSlot>() == null) {
            newHolderObj.AddComponent<ItemSlot>();
        }
        
        //put into location
        newHolderObj.transform.localPosition = new Vector2(x, y);
        //store node to be able to reference
        nodeTree[person] = newHolderObj;
        //store placement
        nodePositionsBank.Add(new Vector2(x, y));

        //add spouse to the right
        if (person.spouse != null) {
            createNodeOnCanvas(person.spouse, x + 100, y, "spouse");
        }
        //add ex-spouses to the left
        if (person.childrenOtherParent.Count > 0) {
            for (int i = 0; i < person.childrenOtherParent.Count; i++) {
                createNodeOnCanvas(person.childrenOtherParent[i], x - (i * 100), y, "ex-spouse");
            }
        }


        //children position determined (minus to y axis for children)
        if (person.children.Count > 0) {
            int childrenNumber = person.children.Count;
            int childrenSpouseAndExNumber = 0;
            
            Debug.Log("Children of " + person.personName);
            foreach (Node child in person.children) {
                //checks for if the children have a spouse, adds them to the number to calculate space
                if (child.spouse != null) {
                    childrenSpouseAndExNumber++;
                }
                //checks for if the children have an exspouse that they share children with(they will be on the same level as siblings)
                if (child.childrenOtherParent.Count > 0) {
                    for (int i = 0; i < child.childrenOtherParent.Count; i++) {
                        childrenSpouseAndExNumber++;
                    }
                }
                Debug.Log("--" + child.personName);
            }
            int center = ((childrenNumber + childrenSpouseAndExNumber) * 100)/2;
            //round up to the nearest 100 if the dividing in half doesn't already do that
            if (center % 100 != 0) {
                center = center + (100 - center);
            }
            //
            //if there is a spouse to the parent, load all children below, but the center is inbetween them
            /*
            if (person.spouse != null) {
                for (int i = 0; i < childrenNumber; i++) {
                    //if child sibling has a spouse, leave space for them
                    if (i > 0 && person.children[i - 1].spouse != null) {
                        // x + 100 = end of parent plane. "- center" places first child leftmost needed. "((i)*100)" will shift child 100 to the right each new child, 100 means that it leaves space for the spouse
                        createNodeOnCanvas(person.children[i], (x + 100) - center + ((i)*100) + 100, y - 60, "child");
                    }
                    else {
                        // x + 100 = end of parent plane. "- center" places first child leftmost needed. "((i-1)*100)" will shift child 100 to the right each new child
                        createNodeOnCanvas(person.children[i], (x + 100) - center + ((i)*100), y - 60, "child"); 
                    }
                    //if child 
                    
                }
            }
            //else if there is no spouse, the ceneter will be right below it
            else {
                for (int i = 0; i < childrenNumber; i++) {
                    if (i > 0 && person.children[i - 1].spouse != null) {
                        // x + 100 = end of parent plane. "- center" places first child leftmost needed. "((i)*100)" will shift child 100 to the right each new child, 100 means that it leaves space for the spouse
                        createNodeOnCanvas(person.children[i], (x + 100) - center + ((i-1)*100) + 100, y - 60, "child");
                    }
                    else {
                        createNodeOnCanvas(person.children[i], (x + 100) - center + ((i-1)*100), y - 60, "child");
                    }
                    
                }
            }
            */

            //siblings need to be palced to the right past the number of children of previous siblings and half of thier children
            
            List<Node> alreadyVisitedChildren = new List<Node>();
            int currentChildIndex = -1;
            //if the person has an ex, we need to put the person and thier ex's children first
            if (person.childrenOtherParent.Count > 0) {
                foreach (Node otherParent in person.childrenOtherParent) {
                    List<Node> childrenOfOtherParent = new List<Node>();
                    foreach(Node child in otherParent.children){
                        if (child.parentOne == person || child.parentTwo == person) {
                            childrenOfOtherParent.Add(child);
                            alreadyVisitedChildren.Add(child);
                        }
                    }
                    //now that we have all the common children, we create new nodes of the children
                    for (int i = 0; i < childrenOfOtherParent.Count; i++) {
                        //if child sibling has a spouse/ex, leave space for them
                        if (i > 0) {
                            //get previous number of people on the same level in order to place this next person correctly
                            int currentNumberOfSpouseEx = 0;
                            if (childrenOfOtherParent[i-1].spouse != null) {
                                currentNumberOfSpouseEx++;
                            }
                            currentNumberOfSpouseEx += childrenOfOtherParent[i-1].childrenOtherParent.Count;
                            currentChildIndex++;
                            // x + 100 = end of parent plane. "- center" places first child leftmost needed. "((currentChildIndex)*100)" will shift child 100 to the right each new child, 100 means that it leaves space for the spouse
                            createNodeOnCanvas(childrenOfOtherParent[i], (x + 100) - center + ((currentChildIndex)*100) + (currentNumberOfSpouseEx * 100), y - 60, "child");
                        }
                        else {
                            currentChildIndex++;
                            // x + 100 = end of parent plane. "- center" places first child leftmost needed. "((currentChildIndex-1)*100)" will shift child 100 to the right each new child
                            createNodeOnCanvas(childrenOfOtherParent[i], (x + 100) - center + ((currentChildIndex)*100), y - 60, "child"); 
                        }
                        
                    }
                }
            }
            //then we put the rest of the children
            if(person.children.Count - alreadyVisitedChildren.Count > 0) {
                List<Node> restOfChildren = new List<Node>();
                foreach (Node child in person.children) {
                    if (!alreadyVisitedChildren.Contains(child)) {
                        restOfChildren.Add(child);
                    }
                }

                if (alreadyVisitedChildren.Count > 0) {
                    //restOfChildren needs to look at last of alreadyVisitedChildren
                    int lastInAlreadyVisited = alreadyVisitedChildren.Count - 1; // -1 so we don't get out of index error
                    int currentNumberOfSpouseAndEx = 0;
                    if (alreadyVisitedChildren[lastInAlreadyVisited].spouse != null) {
                        currentNumberOfSpouseAndEx++;
                    }
                    currentNumberOfSpouseAndEx += alreadyVisitedChildren[lastInAlreadyVisited].childrenOtherParent.Count;
                    currentChildIndex++;
                    // x + 100 = end of parent plane. "- center" places first child leftmost needed. "((currentChildIndex)*100)" will shift child 100 to the right each new child, 100 means that it leaves space for the spouse
                    createNodeOnCanvas(restOfChildren[0], (x + 100) - center + ((currentChildIndex)*100) + (currentNumberOfSpouseAndEx * 100), y - 60, "child");
                }
                else {
                    //if alreadyVisitedChildren is empty, it just makes this node the first one
                    currentChildIndex++;
                    createNodeOnCanvas(restOfChildren[0], (x + 100) - center + ((currentChildIndex)*100), y - 60, "child");
                }
                
                //now the rest in the restOfChildren list
                for (int i = 1; i < restOfChildren.Count; i++) {
                    int currentNumberOfSpouseEx = 0;
                    if (restOfChildren[i - 1].spouse != null) {
                        currentNumberOfSpouseEx++;
                    }
                    currentNumberOfSpouseEx += restOfChildren[i - 1].childrenOtherParent.Count;
                    currentChildIndex++;
                    // x + 100 = end of parent plane. "- center" places first child leftmost needed. "((currentChildIndex)*100)" will shift child 100 to the right each new child, 100 means that it leaves space for the spouse
                    createNodeOnCanvas(restOfChildren[i], (x + 100) - center + ((currentChildIndex)*100) + (currentNumberOfSpouseEx * 100), y - 60, "child");
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
    }
    
    //ATTEMPT like 5
    //first person starts at 0,0
    /*
    public void createNodeOnCanvas(Node person, int x, int y, string relationship) {
        //avoiding multiples
        if (person == null || nodeTree.ContainsKey(person)) {
            return;
        }
        //create the instance
        GameObject newHolderObj = Instantiate(nodePrefab, treeBase);
        newHolderObj.name = person.personName;
        //newHolderObj.GetComponentInChildren<TMP_Text>().text = person.personName;
        //make the panel item slot
        RectTransform rectTransform = newHolderObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(x,y);
        if (newHolderObj.GetComponent<ItemSlot>() == null) {
            newHolderObj.AddComponent<ItemSlot>();
        }
        //store node to be able to reference
        nodeTree[person] = newHolderObj;
        

        Vector2 parentOnePos;
        Vector2 parentTwoPos;
        Vector2 spousePos;
        List<Vector2> childrenPos = new List<Vector2>();

        //parents first
        if (person.parentOne != null) {
            createNodeOnCanvas(person.parentOne, x, y + 60, "parentOne");
        }
        if (person.parentTwo != null) {
            createNodeOnCanvas(person.parentTwo, x, y + 60, "parentTwo");
        }

        //children(include spouse on same level)
        if (person.children.Count > 0) {
            foreach (Node child in person.children) {
                //we will fan out the children later
                createNodeOnCanvas(child, x, y - 60, "child");
                if (child.spouse != null) {
                    createNodeOnCanvas(child.spouse, x, y - 60, "spouse");
                }
            }
        }

        //put into location
        newHolderObj.transform.localPosition = new Vector2(x, y);
        //store placement
        nodePositionsBank.Add(new Vector2(x, y));

        /*
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
        */

        //moves object to random place(testing purposes)
        //float x = Random.Range(-500f, 500f);
        //float y = Random.Range(-200f, 200f);
        //newHolderObj.transform.localPosition = new Vector2(x, y);
    //}
    

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
