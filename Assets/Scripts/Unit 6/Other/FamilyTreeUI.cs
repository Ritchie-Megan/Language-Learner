using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using System.Collections.Generic;


public class FamilyTreeUI : MonoBehaviour
{
    /*
    public GameObject nodePrefab;
    public GameObject treeBase;

    public Dictionary<Node, GameObject> nodeTree = new Dictionary<Node, GameObject>();

    public void generateNodes(HashSet<Node> nodes) {
        foreach (Node person in nodes) {
            CreateTextnode(node);
        }
    }

    public void createNodeOnCanvas(Node person) {
        //avoiding multiples
        if (nodeTree.ContainsKey(person)) {
            return;
        }
        GameObject newTextObj = Instantiate(nodePrefab, treeBase);
        newTextObj.GetComponent<Text>().text = person.personName;

        nodeTree[person] = newTextObj;
    }
    */
}
