using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro; 
using FamilyTree;


public class NodeSpawner : MonoBehaviour
{
    //public GameObject spouseGroupPrefab;
    public GameObject nodePrefab;
    public GameObject blackoutScreen;
    public GameObject tree;
    public int nodeSpacing;
    public int groupSpacing;
    public UnityVector3Event onLoad;

    private Transform grandparentsGen;
    private Transform parentsGen;
    private Transform rootGen;
    private List<RectTransform> nodeTransforms;
    private Dictionary<string, List<Node>> childDict;
    //
    private List<GameObject> checkForWin = new List<GameObject>();
    
    [HideInInspector]
    public Vector3 medianTreePos;


    public List<GameObject> generateNodes(Node root) {
        if (nodeTransforms != null) {
            nodeTransforms.Clear();
            childDict.Clear();
        }
        else {
            nodeTransforms = new List<RectTransform>();
            childDict = new Dictionary<string, List<Node>>();
        }

        grandparentsGen = tree.transform.GetChild(0);
        parentsGen = tree.transform.GetChild(1);
        rootGen = tree.transform.GetChild(2);

        if (root != null)
            createHalfFamilyNodes(root);
        if (root.spouse != null && root.spouse.needsParent) {
            createHalfFamilyNodes(root.spouse);
            Debug.Log("Spouse Family Built");
        }
        else if (root.exSpouse != null && root.exSpouse.needsParent) {
            createHalfFamilyNodes(root.exSpouse);
            Debug.Log("Ex-Spouse Family Built");
        }

        StartCoroutine(RefreshLayout(tree));

        return checkForWin;
    
    }
    public void createHalfFamilyNodes(Node person) {
        // determining which half of the tree to create
        string familySide;
        if (person.isLeftSpouse)
            familySide = "LeftSide";
        else
            familySide = "RightSide";

        // GENERATE ROOT FAMILY
        if (person.isRoot) {
            Transform rootFamily = FindChildWithTag(rootGen, "Center");
            createFamilyUnit(rootFamily, person, true);
        }

        // GENERATE SIBLINGS, NIECES, & NEPHEWS
        Transform siblings = FindChildWithTag(rootGen, familySide);
        foreach (Node sibling in person.siblings) {
            createFamilyUnit(siblings, sibling, true);
        }

        // GENERATE PARENTS, AUNTS, & UNCLES
        Transform parentGen = FindChildWithTag(parentsGen, familySide);
        
        // generating parents
        Transform parents = FindChildWithTag(parentGen, "Center");
        if (person.parentOne != null) {
            createFamilyUnit(parents, person.parentOne, false);
        }

        // generating aunts and uncles of left parent
        Transform leftParentSiblings = FindChildWithTag(parentGen, "LeftSide");
        Node leftParent = null;
        if (person.parentOne != null && person.parentOne.isLeftSpouse)
            leftParent = person.parentOne;
        else if (person.parentTwo != null && person.parentTwo.isLeftSpouse)
            leftParent = person.parentTwo;

        if (leftParent != null) {
            foreach (Node sibling in leftParent.siblings) {
                createFamilyUnit(leftParentSiblings, sibling, false);
            }
        }

        // generating aunts and uncles of right parent
        Transform rightParentSiblings = FindChildWithTag(parentGen, "RightSide");
        Node rightParent = null;
        if (person.parentOne != null && !person.parentOne.isLeftSpouse)
            rightParent = person.parentOne;
        else if (person.parentTwo != null && !person.parentTwo.isLeftSpouse)
            rightParent = person.parentTwo;

        if (rightParent != null) {
            foreach (Node sibling in rightParent.siblings) {
                createFamilyUnit(rightParentSiblings, sibling, false);
            }
        }

        // GENERATE GRANDPARENTS
        Transform grandparentGen = FindChildWithTag(grandparentsGen, familySide);

        if (leftParent != null && leftParent.parentOne != null) {
            createFamilyUnit(grandparentGen, leftParent.parentOne, false);
        }

        if (rightParent != null && rightParent.parentOne != null) {
            createFamilyUnit(grandparentGen, rightParent.parentOne, false);
        } 
    } 

    public Transform FindChildWithTag(Transform parent, string tag) {
        GameObject child = null;

        foreach(Transform transform in parent) {
            if(transform.CompareTag(tag)) {
                child = transform.gameObject;
                break;
            }
        }

        return child.transform;
    }

    public Transform createFamilyUnit(Transform parent, Node person, bool includeChildren) {
        GameObject familyUnit = new GameObject("Family Unit");
        familyUnit.transform.parent = parent;
        RectTransform rect = familyUnit.AddComponent<RectTransform>();
        ContentSizeFitter sizefitter = familyUnit.AddComponent<ContentSizeFitter>();
        VerticalLayoutGroup vertLayout = familyUnit.AddComponent<VerticalLayoutGroup>();
        
        rect.localScale = new Vector3(1,1,1);

        sizefitter.horizontalFit = ContentSizeFitter.FitMode.MinSize;
        sizefitter.verticalFit = ContentSizeFitter.FitMode.MinSize;

        vertLayout.childControlWidth = false;
        vertLayout.childControlHeight = false;
        vertLayout.childForceExpandWidth = false;
        vertLayout.childForceExpandHeight = false;
        vertLayout.childAlignment = TextAnchor.MiddleCenter;
        vertLayout.spacing = groupSpacing;

        Transform spouseGroups = createNodeGroup(familyUnit.transform, "Spouses");

        if (person.spouse == null)
        {
            Transform spouseGroup = createNodeGroup(spouseGroups, "Single");
            GameObject personNode = createNodeObject(spouseGroup, person);
        }
        else {
            Transform spouseGroup = createNodeGroup(spouseGroups, "Married");
            GameObject personNode = createNodeObject(spouseGroup, person);
            GameObject spouseNode = createNodeObject(spouseGroup, person.spouse);
            
            if (!person.isLeftSpouse) {
                spouseNode.transform.SetAsFirstSibling();
            }
        }

        if (person.exSpouse != null) {
            Transform spouseGroup = createNodeGroup(spouseGroups, "Divorced");
            GameObject exSpouseNode = createNodeObject(spouseGroup, person.exSpouse);

            if (!person.isLeftSpouse) {
                exSpouseNode.transform.SetAsFirstSibling();
            }

            if (person.isLeftSpouse && person.spouse != null) {
                exSpouseNode.transform.SetAsFirstSibling();
            }
            
            if (person.exSpouse.spouse != null) {
                spouseGroup.gameObject.name = "Remarried";
                GameObject exSpouseSpouseNode = createNodeObject(spouseGroup, person.exSpouse.spouse);

                if (person.isLeftSpouse && person.spouse != null) {
                    exSpouseSpouseNode.transform.SetAsFirstSibling();
                }
                else if (!person.isLeftSpouse && spouseGroup == null) {
                    exSpouseSpouseNode.transform.SetAsFirstSibling();
                }
            }
        }

        if (includeChildren) {
            Dictionary<int, List<Node>> childGroups = new Dictionary<int, List<Node>> {
                {0, new List<Node>()}, {1, new List<Node>()}, {2, new List<Node>()}
            };

            foreach(Node child in person.children) {
                if (child.parentTwo == null || (child.parentTwo != null && child.parentTwo == person.spouse)) {
                    childGroups[0].Add(child); // spouse children
                }
                if (child.parentTwo != null && child.parentTwo == person.exSpouse) {
                    childGroups[1].Add(child); // ex spouse children
                }
            }

            if (person.exSpouse != null) {
                foreach (Node child in person.exSpouse.children) {
                    if (child.parentOne != person && child.parentTwo != person) {
                        childGroups[2].Add(child);  // step children
                    }
                }
            }
            
            Transform childGroup = createNodeGroup(familyUnit.transform, "Children");

            for (int i = 0; i < 3; i++) {
                foreach (Node child in childGroups[i]) {
                    if (child.spouse != null) {
                        Transform childSpouseGroup = createNodeGroup(childGroup, "Spouse Group");
                        GameObject childNode = createNodeObject(childSpouseGroup, child);
                        GameObject childSpouseNode = createNodeObject(childSpouseGroup, child.spouse);

                        if (!child.isLeftSpouse)
                            childSpouseNode.transform.SetAsFirstSibling();
                            
                        if (person.isLeftSpouse && person.spouse != null)
                            childNode.transform.SetAsFirstSibling();

                        if (!person.isLeftSpouse && person.spouse == null)
                            childNode.transform.SetAsFirstSibling();
                    }
                    else {
                        Transform childSpouseGroup = createNodeGroup(childGroup, "Spouse Group");
                        GameObject childNode = createNodeObject(childSpouseGroup, child);

                        if (person.isLeftSpouse && person.spouse != null)
                            childNode.transform.SetAsFirstSibling();

                        if (!person.isLeftSpouse && person.spouse == null)
                            childNode.transform.SetAsFirstSibling();
                    }
                }
            }
        }

        return familyUnit.transform;
    }

    public GameObject createNodeObject(Transform parent, Node node) {
        GameObject nodeObject = Instantiate(nodePrefab, parent);
        nodeObject.name = node.personName;
        //Debug.Log("Intantiated: " + nodeObject.name);
        nodeObject.GetComponent<ItemSlotNode>().node = node;
        if (node.parentOne != null || node.parentTwo != null)
            nodeObject.tag = "HasParent";
        nodeTransforms.Add(nodeObject.GetComponent<RectTransform>());
        checkForWin.Add(nodeObject);
        return nodeObject;
    }

    public Transform createNodeGroup(Transform parent, string groupName) {
        GameObject nodeGroup = new GameObject(groupName);
        nodeGroup.transform.parent = parent;
        RectTransform rect = nodeGroup.AddComponent<RectTransform>();
        ContentSizeFitter sizefitter = nodeGroup.AddComponent<ContentSizeFitter>();
        HorizontalLayoutGroup horiLayout = nodeGroup.AddComponent<HorizontalLayoutGroup>();
        
        rect.localScale = new Vector3(1,1,1);

        sizefitter.horizontalFit = ContentSizeFitter.FitMode.MinSize;
        sizefitter.verticalFit = ContentSizeFitter.FitMode.MinSize;

        horiLayout.childControlWidth = false;
        horiLayout.childForceExpandWidth = false;
        horiLayout.childControlHeight = false;
        horiLayout.childForceExpandHeight = false;
        horiLayout.childAlignment = TextAnchor.MiddleCenter;
        horiLayout.spacing = nodeSpacing;

        return nodeGroup.transform;
    }

    IEnumerator RefreshLayout( GameObject tree) {

        blackoutScreen.SetActive(true);


        // jank workaround for layout groups not refreshing
        for (int i = 0; i < 4; i++) {
            yield return new WaitForEndOfFrame();

            tree.SetActive(false);

            yield return new WaitForEndOfFrame();

            tree.SetActive(true);
        }
        
        blackoutScreen.SetActive(false);

        setMedianTreePosition();
        onLoad.Invoke(medianTreePos);
        //GameObject.GetComponent<LineSpanwer>().GenerateLines()

    }

    public void setMedianTreePosition() {
        Vector3 sumPosition = new Vector3();
        foreach (RectTransform rect in nodeTransforms) {
            sumPosition += rect.position;
        }

        medianTreePos = sumPosition / nodeTransforms.Count;
        medianTreePos = new Vector3((float)(medianTreePos.x + 0.75), (float)(medianTreePos.y - 1.3), -10);
    }
}

[System.Serializable]
public class UnityVector3Event : UnityEvent<Vector3> {}
