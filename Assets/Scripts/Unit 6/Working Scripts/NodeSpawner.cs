using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro; 
using FamilyTree;


public class NodeSpawner : MonoBehaviour
{
    //public GameObject spouseGroupPrefab;
    public GameObject nodePrefab;
    public GameObject grandparentsGen;
    public GameObject parentsGen;
    public GameObject rootGen;
    public int nodeSpacing;
    public int groupSpacing;

    public void generateNodes(Node root) {
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
        Transform parents = FindChildWithTag(parentGen.gameObject, "Center");
        if (person.parentOne != null) {
            createFamilyUnit(parents, person.parentOne, false);
        }

        // generating aunts and uncles of left parent
        Transform leftParentSiblings = FindChildWithTag(parentGen.gameObject, "LeftSide");
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
        Transform rightParentSiblings = FindChildWithTag(parentGen.gameObject, "RightSide");
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

    public Transform FindChildWithTag(GameObject parent, string tag) {
        GameObject child = null;

        foreach(Transform transform in parent.transform) {
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

        Transform spouseGroup = createNodeGroup(familyUnit.transform, "Spouses");

            
        GameObject personNode = Instantiate(nodePrefab, spouseGroup);
        personNode.name = person.personName;

        if (person.spouse != null) {
            GameObject spouseNode = Instantiate(nodePrefab, spouseGroup);
            spouseNode.name = person.spouse.personName;
            
            if (!person.isLeftSpouse) {
                spouseNode.transform.SetAsFirstSibling();
            }

        }

        if (person.exSpouse != null) {
            GameObject exSpouseNode = Instantiate(nodePrefab, spouseGroup);
            exSpouseNode.name = person.exSpouse.personName;

            if (person.isLeftSpouse) {
                exSpouseNode.transform.SetAsFirstSibling();
            }
            
            if (person.exSpouse.spouse != null) {
                GameObject exSpouseSpouseNode = Instantiate(nodePrefab, spouseGroup);
                exSpouseSpouseNode.name = person.exSpouse.spouse.personName;

                if (person.isLeftSpouse) {
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
                        GameObject childNode = Instantiate(nodePrefab, childSpouseGroup);
                        childNode.name = child.personName;
                        GameObject childSpouseNode = Instantiate(nodePrefab, childSpouseGroup);
                        childSpouseNode.name = child.spouse.personName;

                        if (!child.isLeftSpouse)
                            childSpouseNode.transform.SetAsFirstSibling();

                        if(person.isLeftSpouse)
                            childSpouseGroup.SetAsFirstSibling();
                    }
                    else {
                        GameObject childNode = Instantiate(nodePrefab, childGroup);
                        childNode.name = child.personName;
                        
                        if (person.isLeftSpouse)
                            childNode.transform.SetAsFirstSibling();
                    }
                }
            }
        }

        return familyUnit.transform;
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
}
