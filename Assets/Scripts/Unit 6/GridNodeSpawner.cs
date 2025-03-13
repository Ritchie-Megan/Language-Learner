using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro; 
using FamilyTreeBackend;

public class GridNodeSpawner : MonoBehaviour
{

    public TMP_Text nameText;
    public GameObject nodePrefab;
    public Transform treeBase;
    //public GridLayoutGroup gridLayoutGroup;

    public Dictionary<Node, GameObject> nodeTree = new Dictionary<Node, GameObject>();
    private Dictionary<int, Transform> generationContainers = new Dictionary<int, Transform>();

    void Start()
    {
        HashSet<Node> nodes = new HashSet<Node>();
        generateNodes(nodes);
    }

    public void generateNodes(HashSet<FamilyTreeBackend.Node> nodes) {
        
        //adjust cell size depending on number of nodes
        int totalNodes = nodes.Count;
        //AdjustGridCellSize(totalNodes);

        //now place them in grid
        foreach (FamilyTreeBackend.Node person in nodes) {
            int generationalLevel = GetGenerationalLevel(person);
            createNodeInGrid(person, generationalLevel);
        }
    }

    public void createNodeInGrid(Node person, int generationalLevel) {
        //avoid duplicates
        if (person == null || nodeTree.ContainsKey(person)) {
            return;
        }

        //get the correct contianer for generational level
        Transform generationalLevelContainer = GetGenerationalContainer(generationalLevel);

        //set name
        GameObject newNode = Instantiate(nodePrefab, generationalLevelContainer);
        TMP_Text textComponent = newNode.GetComponentInChildren<TMP_Text>();
        if (textComponent != null) {
            textComponent.text = person.personName;
        }

        //store node
        nodeTree[person] = newNode;
    }

    public int GetGenerationalLevel(Node person) {
        if (person == null) {
            return 0;
        }

        int parentOneLevel = 0;
        int parentTwoLevel = 0;
        //recursivly go through both parents
        if (person.parentOne != null) {
            parentOneLevel = GetGenerationalLevel(person.parentOne) + 1;
        }
        if (person.parentTwo != null) {
            parentTwoLevel = GetGenerationalLevel(person.parentTwo) + 1;
        }

        //return the hgiher of the two
        return Mathf.Max(parentOneLevel, parentTwoLevel);
    }

    private Transform GetGenerationalContainer(int generationLevel) {
        if (generationContainers.ContainsKey(generationLevel)) {
            return generationContainers[generationLevel];
        }

        //else we need to create one
        GameObject newGeneration = new GameObject("Generation " + generationLevel);
        newGeneration.transform.SetParent(treeBase, false);

        RectTransform rectTransform = newGeneration.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(1000, 100);

        HorizontalLayoutGroup layoutGroup = newGeneration.AddComponent<HorizontalLayoutGroup>();
        layoutGroup.childControlWidth = true;
        layoutGroup.childForceExpandWidth = false;
        layoutGroup.childAlignment = TextAnchor.MiddleCenter;

        //add to containers
        generationContainers[generationLevel] = newGeneration.transform;
        return newGeneration.transform;
    }

    /*
    private void AdjustGridCellSize(int numOfNodes) {
        if (gridLayoutGroup == null) {
            return;
        }

        float width = 100f;
        float height = 50f;

        //adjust based on node count
        float newWidth = Mathf.Clamp(width - (numOfNodes * 2), 100f, width);
        float newHeight = Mathf.Clamp(height - (numOfNodes * 1.5f), 60f, height);
        
        gridLayoutGroup.cellSize = new Vector2(newWidth, newHeight);
        gridLayoutGroup.spacing = new Vector2(20,20);
    }
    */
}
