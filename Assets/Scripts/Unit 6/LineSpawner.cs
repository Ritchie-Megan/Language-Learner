using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using FamilyTree;

public class LineSpawner : MonoBehaviour
{
    public Canvas lineCanvas;
    public GameObject grandparentsGen;
    public GameObject parentsGen;
    public GameObject rootGen;


    public void generateLines()
    {
        Dictionary<string, List<GameObject>> gparentsBranch = new Dictionary<string, List<GameObject>>();
        // Generating grandparent lines
        foreach (Transform gparentGroup in grandparentsGen.transform)
        {
            foreach (Transform spouseGroups in gparentGroup)
            {
                foreach (Transform spouseGroup in spouseGroups)
                {
                    Node personNode = spouseGroup.GetChild(0).GetChild(0).gameObject.GetComponent<ItemSlotNode>().node;
                    List<string> spouseGroupNames = getSpouseGroupNames(personNode);
                    
                    createLine(spouseGroup, new Vector2(80, 2), Vector3.zero);
                    foreach(string spouseGroupName in spouseGroupNames)
                    {
                        List<GameObject> kidbranch = new List<GameObject>();
                        kidbranch.Add(createLine(spouseGroup, new Vector2(2, 80), new Vector3(0, 0.378f, 0)));
                        gparentsBranch.Add(spouseGroupName, kidbranch);
                    }
                }
            }
        }

        Dictionary<string, List<GameObject>> parentsBranch = new Dictionary<string, List<GameObject>>();

        foreach (Transform parentGroup in parentsGen.transform)
        {
            foreach (Transform parentSiblings in parentGroup)
            {
                foreach (Transform familyUnit in parentSiblings)
                {
                    foreach(Transform spouseGroups in familyUnit)
                    {
                        foreach (Transform spouseGroup in spouseGroups)
                        {
                            createLine(spouseGroup, new Vector2(80, 2), Vector3.zero);

                            if (parentSiblings.gameObject.tag == "Center")
                            {
                                Node personNode = spouseGroup.GetChild(0).gameObject.GetComponent<ItemSlotNode>().node;
                                List<string> spouseGroupNames = getSpouseGroupNames(personNode);
                                
                                foreach(string spouseGroupName in spouseGroupNames)
                                {
                                    if(!parentsBranch.ContainsKey(spouseGroupName))
                                    {
                                        List<GameObject> kidbranch = new List<GameObject>();
                                        kidbranch.Add(createLine(spouseGroup, new Vector2(2, 80), new Vector3(0, 0.378f, 0)));
                                        parentsBranch.Add(spouseGroupName, kidbranch);
                                    }
                                }
                            }
                            foreach (Transform person in spouseGroup)
                            {
                                if (person.gameObject.tag == "HasParent")
                                {
                                    Node personNode = person.gameObject.GetComponent<ItemSlotNode>().node;
                                    GameObject parentLine = createLine(person, new Vector2(2, 80), new Vector3(0, -1.1f, 0));
                                    if (personNode.parentTwo != null)
                                    {
                                        string parentNames = getCombinedNames(personNode.parentOne, personNode.parentTwo);
                                        gparentsBranch[parentNames].Add(parentLine);
                                    }
                                    else
                                    {
                                        gparentsBranch[personNode.parentOne.personName].Add(parentLine);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        Dictionary<string, List<GameObject>> siblingsBranch = new Dictionary<string, List<GameObject>>();

        foreach (Transform siblingsGroup in rootGen.transform)
        {
            foreach (Transform familyUnit in siblingsGroup)
            {
                foreach (Transform nodeGroup in familyUnit)
                {
                    if (nodeGroup.GetSiblingIndex() == 0)
                    {
                        foreach (Transform spouseGroup in nodeGroup)
                        {
                            foreach (Transform person in spouseGroup)
                            {
                                Node personNode = person.gameObject.GetComponent<ItemSlotNode>().node;
                                if (person.gameObject.tag == "HasParent")
                                {
                                    GameObject parentLine = createLine(person, new Vector2(2, 80), new Vector3(0, -1.1f, 0));
                                    if (personNode.parentTwo != null)
                                    {
                                        string parentNames = getCombinedNames(personNode.parentOne, personNode.parentTwo);
                                        parentsBranch[parentNames].Add(parentLine);
                                    }
                                    else
                                    {
                                        parentsBranch[personNode.parentOne.personName].Add(parentLine);
                                    }
                                }
                                
                                createLine(spouseGroup, new Vector2(80, 2), Vector3.zero);

                                personNode = person.gameObject.GetComponent<ItemSlotNode>().node;
                                List<string> spouseGroupNames = getSpouseGroupNames(personNode);
                                
                                foreach(string spouseGroupName in spouseGroupNames)
                                {
                                    if(!siblingsBranch.ContainsKey(spouseGroupName))
                                    {
                                        List<GameObject> kidbranch = new List<GameObject>();
                                        kidbranch.Add(createLine(spouseGroup, new Vector2(2, 80), new Vector3(0, 0.378f, 0)));
                                        siblingsBranch.Add(spouseGroupName, kidbranch);
                                    }
                                }
                            }
                            
                        }
                    }
                    if (nodeGroup.GetSiblingIndex() == 1)
                    {
                        foreach (Transform spouseGroup in nodeGroup)
                        {
                            createLine(spouseGroup, new Vector2(80, 2), Vector3.zero);
                            
                            foreach (Transform person in spouseGroup)
                            {
                                if (person.gameObject.tag == "HasParent")
                                {
                                    Node personNode = person.gameObject.GetComponent<ItemSlotNode>().node;
                                    GameObject parentLine = createLine(person, new Vector2(2, 80), new Vector3(0, -1.1f, 0));
                                    if (personNode.parentTwo != null)
                                    {
                                        string parentNames = getCombinedNames(personNode.parentOne, personNode.parentTwo);
                                        siblingsBranch[parentNames].Add(parentLine);
                                    }
                                    else
                                    {
                                        siblingsBranch[personNode.parentOne.personName].Add(parentLine);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }



        createBranchLine(gparentsBranch);
        createBranchLine(parentsBranch);
        createBranchLine(siblingsBranch);
        lineCanvas.transform.localPosition += Vector3.down*0.35f;
    }

    private GameObject createLine(Transform target, Vector2 sizeDelta, Vector3 offset)
    {
        GameObject horiLine = new GameObject();
        Image lineImage = horiLine.AddComponent<Image>();
        RectTransform rect = horiLine.GetComponent<RectTransform>();
        horiLine.transform.SetParent(lineCanvas.transform);

        rect.sizeDelta = sizeDelta;
        rect.position = target.position-offset;
        rect.localScale = new Vector3(1,1,1);

        return horiLine;
    }

    private void createBranchLine(Dictionary<string, List<GameObject>> branch)
    {
        foreach (KeyValuePair<string, List<GameObject>> entry in branch)
        {
            float minX = 99999;
            float maxX = -99999;
            float minY = 99999;
            float maxY = -99999;

            foreach (GameObject line in entry.Value)
            {
                minX = Mathf.Min(minX, line.transform.position.x);
                maxX = Mathf.Max(maxX, line.transform.position.x);
                minY = Mathf.Min(minY, line.transform.position.y);
                maxY = Mathf.Max(maxY, line.transform.position.y);
            }
            
            float scale = 1f / lineCanvas.transform.localScale.x;

            GameObject branchLine = createLine(lineCanvas.transform, new Vector2((scale*(maxX-minX)+2f), 2), Vector3.zero);
            branchLine.transform.position = new Vector3((maxX+minX)/2f, (maxY+minY)/2f, 0);
        }
    }

    private List<string> getSpouseGroupNames(Node node)
    {
        List<string> spouseGroupNames = new List<string>();

        foreach (Node child in node.children)
        {
            if (child.parentTwo == null)
            {
                string spouseGroupName = node.personName;
                if (!spouseGroupNames.Contains(spouseGroupName))
                    spouseGroupNames.Add(spouseGroupName);
            }
            else if ((child.parentOne == node || child.parentTwo == node) && node.spouse != null && (child.parentOne == node.spouse || child.parentTwo == node.spouse))
            {
                string spouseGroupName = getCombinedNames(node, node.spouse);

                if (!spouseGroupNames.Contains(spouseGroupName))
                    spouseGroupNames.Add(spouseGroupName);
            }
            else if ((child.parentOne == node || child.parentTwo == node) && node.exSpouse != null && (child.parentOne == node.exSpouse || child.parentTwo == node.exSpouse))
            {
                string spouseGroupName = getCombinedNames(node, node.exSpouse);

                if (!spouseGroupNames.Contains(spouseGroupName))
                    spouseGroupNames.Add(spouseGroupName);
            }
        }
        
        // string printMessage = "Returned spouse groups of size " + spouseGroupNames.Count + ": ";
        // foreach(string name in spouseGroupNames)
        // {
        //     printMessage += name + ", ";
        // }
        //
        // Debug.Log(printMessage);
        
        return spouseGroupNames;
    }

    private string getCombinedNames(Node parentOne, Node parentTwo)
    {
        string combinedNames;
        int nameOrder = string.Compare(parentOne.personName, parentTwo.personName, true); //ordering parentnames alphabetically
                
        if (nameOrder <= 0)
            combinedNames = parentOne.personName + parentTwo.personName;
        else
            combinedNames = parentTwo.personName + parentOne.personName;
        
        return combinedNames;
    }
}
