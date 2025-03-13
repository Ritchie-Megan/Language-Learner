using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro; 

public class FamilyTree : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int maxPeople = 10;
        FamilyTreeBackend.Node root = buildTree(maxPeople);
        Debug.Log("-------Family Tree----------");
        HashSet<FamilyTreeBackend.Node> visitedNodes = new HashSet<FamilyTreeBackend.Node>();
        List<String> cluesList = new List<String>();
        FamilyTreeBackend.TreePrinter.generateAndPrintTreeAndClues(root, visitedNodes, ref cluesList);
        Debug.Log("Number of nodes in hashset: " + visitedNodes.Count);
        foreach (FamilyTreeBackend.Node person in visitedNodes) {
            Debug.Log(person.personName);
        }
        
        //send data over to the GridNodeSpawner script
        NodeSpawner nodeSpawner = FindObjectOfType<NodeSpawner>();
        if (nodeSpawner != null) {
            nodeSpawner.generateNodes(visitedNodes);
        }

        //send data over to scrollable view
        List<String> tempList = new List<String>();
        foreach (FamilyTreeBackend.Node person in visitedNodes) {
            tempList.Add(person.personName);
        }
        //but first- scrable the names
        List<string> scrambleNames = new List<String>();
        System.Random rand = new System.Random();
        while (tempList.Count > 0) {
            int randomNumber = rand.Next(0, tempList.Count);
            scrambleNames.Add(tempList[randomNumber]);
            tempList.RemoveAt(randomNumber);
        }
        
        //now send
        ScrollBarLoader imageLoader = FindObjectOfType<ScrollBarLoader>();
        if (imageLoader != null) {
            foreach (string name in scrambleNames) {
                imageLoader.AddItem(name);
            }
        }

        //sending over clues list
        ClueLoader clueLoader = FindObjectOfType<ClueLoader>();
        if (clueLoader != null) {
            foreach (string clue in cluesList) {
                clueLoader.AddItem(clue);
            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private FamilyTreeBackend.Node buildTree(int maxNumberOfPeople) {
        return FamilyTreeBackend.TreeBuilder.buildTree(maxNumberOfPeople);
    }

    private void generateAndPrintTreeAndClues(FamilyTreeBackend.Node root, HashSet<FamilyTreeBackend.Node> visited, ref List<string> clues, int depth = 0) {
        FamilyTreeBackend.TreePrinter.generateAndPrintTreeAndClues(root, visited, ref clues, depth);
    }
}

namespace FamilyTreeBackend {
    public class Node {
        public string personName;
        public string personGender;//m or f
        public string maritalStatus; //m = married, s = single, d = divorce, r = remarried

        public Node parentOne = null;
        public Node parentTwo = null;
        public Node spouse = null;
        
        public List<Node> children = new List<Node>();
        public List<Node> siblings = new List<Node>();
        private static List<string> _names = new List<string> {"Katrina", "Jamie", "Alex", "Adriana", "Leo", "Jesus", "Martin", "Jose", "Juan", "Daniela", "Maximo", "Isabel", "Julio", "Maria", "Gabriela", "Arielle", "Carlos"};

        public Node() {
            personName = getName();
            personGender = "m";
            maritalStatus = "s";
        }

        private static string getName() {
            if (_names.Count == 0) {
                return "out of names lol";
            }

            string name = _names[0];
            _names.RemoveAt(0);
            return name;
        }
    }


    public static class TreeBuilder{
        //TODO: make sure all relationships are connected to each other
        //TODO: Pet variable??
        //TODO: the most important relationships are the step/half/in-law relationships
        //TODO: when going through the queue, if a person is divorced, they have to check if they are divorced or remarried
        //also if the person is married, need to randomize if they were remarried or married
        public static Node buildTree(int maxNumberOfPeople) {
            int numberOfPeopleLeft = maxNumberOfPeople;
            List<Node> toVisitQueue = new List<Node>();

            //start node
            Node root = new Node();
            toVisitQueue.Add(root);
            numberOfPeopleLeft--;

            
            //now visit all nodes that are in the queue
            while (toVisitQueue.Count > 0 && numberOfPeopleLeft > 0) {
                Node person = toVisitQueue[0];
                toVisitQueue.RemoveAt(0);
                generateNodeInfo(person, ref numberOfPeopleLeft, ref toVisitQueue);
            }

            return root;
        }

        
        public static void generateNodeInfo(Node currentNode, ref int numPeopleLeft, ref List<Node> toVisitQueue) {
            if (numPeopleLeft <= 0 || currentNode == null) {
                return;
            }
            
            //TODO: RANDOMIZE NAME

            //RANDOMIZED GENDER
            System.Random rand = new System.Random();
            int randomNumberGender = rand.Next(0,2);
            //generate number 1 or 0
            if (randomNumberGender == 1) {
                currentNode.personGender = "f";
            }

            //RANDOMIZED MARITAL STATUS AND CHILDREN
            //just so long the node is not already married(meaning spouse = null), we can make it any type
            if (currentNode.spouse == null) {
                //generate number 0,1,2
                int randomNumberMarritalStatus = rand.Next(0,4);
                //SINGLE
                if (randomNumberMarritalStatus == 1) {
                    currentNode.maritalStatus = "s";
                    int randomNumberChildrenTwo = rand.Next(0,2);
                    //make sure it doesn't go over the people limit
                    for (int i = 0; i < randomNumberChildrenTwo; i++) {
                        if (numPeopleLeft > 0) {
                            Node newChild = new Node();
                            newChild.parentOne = currentNode;
                            //this makes sure that they don't overlap in sibling vectors
                            foreach (Node sibling in newChild.parentOne.children) {
                                if (sibling != newChild && !newChild.siblings.Contains(sibling)) {
                                    newChild.siblings.Add(sibling);
                                    sibling.siblings.Add(newChild);
                                }
                            }
                            //add child to parent vectors
                            newChild.parentOne.children.Add(newChild);
                            toVisitQueue.Add(newChild);
                            numPeopleLeft--;
                        }
                    }
                }
                //DIVORCED
                else if (randomNumberMarritalStatus == 2) {
                    currentNode.maritalStatus = "d";
                    //create a divorced spouse and update
                    //did they share kids
                    int randomNumberChildren = rand.Next(0,3);
                    if (randomNumberChildren > 0) {
                        Node oldSpouse = new Node();
                        oldSpouse.maritalStatus = "d";
                    
                        for (int i = 0; i < randomNumberChildren; i++) {
                            if (numPeopleLeft > 0) {
                                Node newChild = new Node();
                                newChild.parentOne = currentNode;
                                newChild.parentTwo = oldSpouse;
                                //this makes sure that they don't overlap in sibling vectors
                                foreach (Node sibling in newChild.parentOne.children) {
                                    if (sibling != newChild && !newChild.siblings.Contains(sibling)) {
                                        newChild.siblings.Add(sibling);
                                        sibling.siblings.Add(newChild);
                                    }
                                }
                                foreach (Node sibling in newChild.parentTwo.children) {
                                    if (sibling != newChild && !newChild.siblings.Contains(sibling)) {
                                        newChild.siblings.Add(sibling);
                                        sibling.siblings.Add(newChild);
                                    }
                                }
                                //add child to parent vectors
                                newChild.parentOne.children.Add(newChild);
                                newChild.parentTwo.children.Add(newChild);
                                toVisitQueue.Add(newChild);
                                numPeopleLeft--;
                            }
                        }
                        toVisitQueue.Add(oldSpouse);
                        numPeopleLeft--;
                    }
                }
                //REMARRIED
                else if (randomNumberMarritalStatus == 3) {
                    currentNode.maritalStatus = "r";
                    //create old spouse and new one

                    //old spouse
                    //did they share kids
                    int randomNumberChildren = rand.Next(0,3);
                    if (randomNumberChildren > 0) {
                        Node oldSpouse = new Node();
                        oldSpouse.maritalStatus = "d";
                    
                        for (int i = 0; i < randomNumberChildren; i++) {
                            if (numPeopleLeft > 0) {
                                Node newChild = new Node();
                                newChild.parentOne = currentNode;
                                newChild.parentTwo = oldSpouse;
                                //this makes sure that they don't overlap in sibling vectors
                                foreach (Node sibling in newChild.parentOne.children) {
                                    if (sibling != newChild && !newChild.siblings.Contains(sibling)) {
                                        newChild.siblings.Add(sibling);
                                        sibling.siblings.Add(newChild);
                                    }
                                }
                                foreach (Node sibling in newChild.parentTwo.children) {
                                    if (sibling != newChild && !newChild.siblings.Contains(sibling)) {
                                        newChild.siblings.Add(sibling);
                                        sibling.siblings.Add(newChild);
                                    }
                                }
                                //add child to parent vectors
                                newChild.parentOne.children.Add(newChild);
                                newChild.parentTwo.children.Add(newChild);
                                toVisitQueue.Add(newChild);
                                numPeopleLeft--;
                            }
                        }
                        toVisitQueue.Add(oldSpouse);
                        numPeopleLeft--;
                    }
                    

                    //new spouse(since they don't already have one, couldn't get into this if statement)
                    Node newSpouse = new Node();
                    currentNode.spouse = newSpouse;
                    newSpouse.spouse = currentNode;
                    newSpouse.maritalStatus = "m";
                    toVisitQueue.Add(newSpouse);
                    numPeopleLeft--;
                    //repeat from like 30 lines up
                    int randomNumberChildrenTwo = rand.Next(0,3);
                    for (int i = 0; i < randomNumberChildrenTwo; i++) {
                        if (numPeopleLeft > 0) {
                            Node newChild = new Node();
                            newChild.parentOne = currentNode;
                            newChild.parentTwo = newSpouse;
                            //this makes sure that they don't overlap in sibling vectors
                            foreach (Node sibling in newChild.parentOne.children) {
                                if (sibling != newChild && !newChild.siblings.Contains(sibling)) {
                                    newChild.siblings.Add(sibling);
                                    sibling.siblings.Add(newChild);
                                }
                            }
                            foreach (Node sibling in newChild.parentTwo.children) {
                                if (sibling != newChild && !newChild.siblings.Contains(sibling)) {
                                    newChild.siblings.Add(sibling);
                                    sibling.siblings.Add(newChild);
                                }
                            }
                            //add child to parent vectors
                            newChild.parentOne.children.Add(newChild);
                            newChild.parentTwo.children.Add(newChild);
                            toVisitQueue.Add(newChild);
                            numPeopleLeft--;
                        }
                    }
                }
                //MARRIED
                else {
                    //create a spouse, add info, add to queue
                    currentNode.maritalStatus = "m";
                    Node newSpouse = new Node();
                    currentNode.spouse = newSpouse;
                    newSpouse.spouse = currentNode;
                    newSpouse.maritalStatus = "m";
                    //repeat from like 30 lines up
                    int randomNumberChildrenTwo = rand.Next(0,3);
                    for (int i = 0; i < randomNumberChildrenTwo; i++) {
                        if (numPeopleLeft > 0) {
                            Node newChild = new Node();
                            newChild.parentOne = currentNode;
                            newChild.parentTwo = newSpouse;
                            //this makes sure that they don't overlap in sibling vectors
                            foreach (Node sibling in newChild.parentOne.children) {
                                if (sibling != newChild && !newChild.siblings.Contains(sibling)) {
                                    newChild.siblings.Add(sibling);
                                    sibling.siblings.Add(newChild);
                                }
                            }
                            foreach (Node sibling in newChild.parentTwo.children) {
                                if (sibling != newChild && !newChild.siblings.Contains(sibling)) {
                                    newChild.siblings.Add(sibling);
                                    sibling.siblings.Add(newChild);
                                }
                            }
                            //add child to parent vectors
                            newChild.parentOne.children.Add(newChild);
                            newChild.parentTwo.children.Add(newChild);
                            toVisitQueue.Add(newChild);
                            numPeopleLeft--;
                        }
                    }
                    toVisitQueue.Add(newSpouse);
                    numPeopleLeft--;

                }
            }

            //GENERATE PARENTS
            //if current already has one parent, it can add anther
            //if current doesn't have either, it can have one or both
            if (currentNode.parentOne == null && currentNode.parentTwo != null) {
                //randomly determine if they will get that parent
                int randomNumberParent = rand.Next(0,2);
                if (randomNumberParent == 1) {
                    Node newParent = new Node();
                    newParent.children.Add(currentNode);
                    currentNode.parentOne = newParent;
                    toVisitQueue.Add(newParent);
                    numPeopleLeft--;
                }
            }
            else if (currentNode.parentOne != null && currentNode.parentTwo == null) {
                //randomly determine if they will get that parent
                int randomNumberParent = rand.Next(0,2);
                if (randomNumberParent == 1) {
                    Node newParent = new Node();
                    newParent.children.Add(currentNode);
                    currentNode.parentTwo = newParent;
                    toVisitQueue.Add(newParent);
                    numPeopleLeft--;
                }
            }
            else if (currentNode.parentOne == null && currentNode.parentTwo == null) {
                int randomNumberParentOne = rand.Next(0,2);
                //do they have parentOne
                if (randomNumberParentOne == 1 && numPeopleLeft > 0) {
                    //one parent is new, add child to list of parent
                    Node newParent = new Node();
                    newParent.children.Add(currentNode);
                    currentNode.parentOne = newParent;
                    toVisitQueue.Add(newParent);
                    numPeopleLeft--;
                }
                //do they have parentTwo
                int randomNumberParentTwo = rand.Next(0,2);
                if (randomNumberParentTwo == 1 && numPeopleLeft > 0) {
                    //one parent is new, add child to list of parent
                    Node newParent = new Node();
                    newParent.children.Add(currentNode);
                    currentNode.parentTwo = newParent;
                    toVisitQueue.Add(newParent);
                    numPeopleLeft--;
                }
            }
        }
    }
    
    public static class TreePrinter {
        public static void generateAndPrintTreeAndClues(Node root, HashSet<Node> visited, ref List<string> clues, int depth = 0) {
            //base case
            if (root == null) {
                return;
            }
            //returns if has already has it
            if (visited.Contains(root)) {
                return;
            }
            //if it doesn't add it to the hash
            visited.Add(root);
            //after that it makes a clue for this new visit
            generateFamilyRelationClue(root, ref clues);
            string indent = new string(' ', depth * 4);
            //info on root
            //name/gender/marital status
            Debug.Log($"{indent}" + root.personName + "(Gender: " + root.personGender + ", MaritalStatus: " + root.maritalStatus + ")");
            //parents
            if (root.parentOne != null) {
                Debug.Log($"{indent}" + "--parentOne: " + root.parentOne.personName);
                
            }
            if (root.parentTwo != null) {
                Debug.Log($"{indent}" + "--parentTwo: " + root.parentTwo.personName);
                
            }
            //spouse
            if (root.spouse != null) {
                Debug.Log($"{indent}" + "--spouse: " + root.spouse.personName);
                
            }
            //children
            if (root.children.Count > 0) {
                foreach (Node child in root.children) {
                    Debug.Log($"{indent}" + "--child: " + child.personName);
                    
                }
                foreach (Node child in root.children) {
                    generateAndPrintTreeAndClues(child, visited, ref clues, depth + 1);
                }
            }
            //siblings
            if (root.siblings.Count > 0) {
                foreach (Node sibling in root.siblings) {
                    Debug.Log($"{indent}" + "--sibling: " + sibling.personName);
                    
                }
                foreach (Node sibling in root.siblings) {
                    generateAndPrintTreeAndClues(sibling, visited, ref clues, depth);
                }
                
            }
            generateAndPrintTreeAndClues(root.parentOne, visited, ref clues, depth);
            generateAndPrintTreeAndClues(root.parentTwo, visited, ref clues, depth);
            generateAndPrintTreeAndClues(root.spouse, visited, ref clues, depth);
            
        }  

        //generate the clues
        public static void generateFamilyRelationClue(Node currentNode, ref List<string> clues) {
            //base case: makes sure no stray nodes break the system
            if (currentNode.parentOne == null && currentNode.parentTwo == null &&
                currentNode.spouse == null && currentNode.children.Count < 1 && currentNode.siblings.Count < 1) {
                Debug.Log("No relationships for this node is found.");
                return;
            }
            //randomly decide what relationship to make the clue
            System.Random rand = new System.Random();
            int randomNumberRelationship = rand.Next(0,8);
            //in relation to a grandparent
            if (randomNumberRelationship == 0) {
                //if the current node does not have any grandparents, roll again (have to check if parent is there first)
                if ((currentNode.parentOne == null || (currentNode.parentOne.parentOne == null && currentNode.parentOne.parentTwo == null)) &&
                    (currentNode.parentTwo == null || (currentNode.parentTwo.parentOne == null && currentNode.parentTwo.parentTwo == null))) {
                    generateFamilyRelationClue(currentNode, ref clues);
                }
                else {
                    List<Node> grandparents = new List<Node>();
                    if (currentNode.parentOne != null && currentNode.parentOne.parentOne != null) {
                        grandparents.Add(currentNode.parentOne.parentOne);
                    }
                    if (currentNode.parentOne != null && currentNode.parentOne.parentTwo != null) {
                        grandparents.Add(currentNode.parentOne.parentTwo);
                    }
                    if (currentNode.parentTwo != null && currentNode.parentTwo.parentOne != null) {
                        grandparents.Add(currentNode.parentTwo.parentOne);
                    }
                    if (currentNode.parentTwo != null && currentNode.parentTwo.parentTwo != null) {
                        grandparents.Add(currentNode.parentTwo.parentTwo);
                    }
                    if (grandparents.Count == 0) {
                        generateFamilyRelationClue(currentNode, ref clues);
                    }
                    else {
                        int pickGrandparent = rand.Next(0, grandparents.Count);
                        if (grandparents[pickGrandparent].personGender == "f") {
                            Debug.Log("La abuela de " + currentNode.personName + " es " + grandparents[pickGrandparent].personName);
                            clues.Add("La abuela de " + currentNode.personName + " es " + grandparents[pickGrandparent].personName);
                        }
                        else {
                            Debug.Log("El abuelo de " + currentNode.personName + " es " + grandparents[pickGrandparent].personName);
                            clues.Add("El abuelo de " + currentNode.personName + " es " + grandparents[pickGrandparent].personName);
                        }
                    }
                }
            }
            //in relation to a parent
            //parent relationships that are importnat to remember:
                // mother/fater and step mother/father
            else if (randomNumberRelationship == 1) {
                if (currentNode.parentOne == null && currentNode.parentTwo == null) {
                    generateFamilyRelationClue(currentNode, ref clues);
                }
                else {
                    List<Node> parents = new List<Node>();
                    //if they have one parent
                    if (currentNode.parentOne != null) {
                        parents.Add(currentNode.parentOne);
                    }
                    //if they have the other parent
                    if (currentNode.parentTwo != null) {
                        parents.Add(currentNode.parentTwo);
                    }
                    //if the first parent is remarried
                    if (currentNode.parentOne != null && currentNode.parentOne.spouse != null && currentNode.parentOne.spouse != currentNode.parentTwo && currentNode.parentOne.spouse != null) {
                        parents.Add(currentNode.parentOne.spouse);
                    }
                    //if the second parent is remarried
                    if (currentNode.parentTwo != null && currentNode.parentTwo.spouse != null && currentNode.parentTwo.spouse != currentNode.parentOne && currentNode.parentTwo.spouse != null) {
                        parents.Add(currentNode.parentTwo.spouse);
                    }
                    if (parents.Count == 0) {
                        generateFamilyRelationClue(currentNode, ref clues);
                    }
                    else {
                        int pickParent = rand.Next(0, parents.Count);
                        if (parents[pickParent] == currentNode.parentOne || parents[pickParent] == currentNode.parentTwo) {
                            if (parents[pickParent].personGender == "f") {
                                Debug.Log("La madre de " + currentNode.personName + " es " + parents[pickParent].personName);
                                clues.Add("La madre de " + currentNode.personName + " es " + parents[pickParent].personName);
                            }
                            else {
                                Debug.Log("El padre de " + currentNode.personName + " es " + parents[pickParent].personName);
                                clues.Add("El padre de " + currentNode.personName + " es " + parents[pickParent].personName);
                            }
                        }
                        else {
                            if (parents[pickParent].personGender == "f") {
                                Debug.Log("La madrastra de " + currentNode.personName + " es " + parents[pickParent].personName);
                                clues.Add("La madrastra de " + currentNode.personName + " es " + parents[pickParent].personName);
                            }
                            else {
                                Debug.Log("El padrastro de " + currentNode.personName + " es " + parents[pickParent].personName);
                                clues.Add("El padrastro de " + currentNode.personName + " es " + parents[pickParent].personName);
                            }
                        }
                    }
                }
                
            }
            //in relation to an aunt/unlce
            //remember: if a child has a step parent, then step parent's siblings are their aunt/uncles too
            else if (randomNumberRelationship == 2) {
                if (currentNode.parentOne == null && currentNode.parentTwo == null) {
                    generateFamilyRelationClue(currentNode, ref clues);
                }
                else {
                    List<Node> unclesAndAunts = new List<Node>();
                    //if they have one parent, add all thier siblings
                    if (currentNode.parentOne != null) {
                        if (currentNode.parentOne.siblings.Count > 0) {
                            foreach(Node sibling in currentNode.parentOne.siblings) {
                                unclesAndAunts.Add(sibling);
                            }
                        }
                    }
                    //if they have the other parent, add all thier siblings
                    if (currentNode.parentTwo != null) {
                        if (currentNode.parentTwo.siblings.Count > 0) {
                            foreach(Node sibling in currentNode.parentTwo.siblings) {
                                unclesAndAunts.Add(sibling);
                            }
                        }
                    }
                    //if the first parent is remarried, add all step parent's siblings
                    if (currentNode.parentOne != null && currentNode.parentOne.spouse != null && currentNode.parentOne.spouse != currentNode.parentTwo && currentNode.parentOne.spouse != null) {
                        if (currentNode.parentOne.spouse.siblings.Count > 0) {
                            foreach(Node sibling in currentNode.parentOne.spouse.siblings) {
                                unclesAndAunts.Add(sibling);
                            }
                        }
                    }
                    //if the second parent is remarried, add all step parent's siblings
                    if (currentNode.parentTwo != null && currentNode.parentTwo.spouse != null && currentNode.parentTwo.spouse != currentNode.parentOne && currentNode.parentTwo.spouse != null) {
                        if (currentNode.parentTwo.spouse.siblings.Count > 0) {
                            foreach(Node sibling in currentNode.parentTwo.spouse.siblings) {
                                unclesAndAunts.Add(sibling);
                            }
                        }
                    }
                    if (unclesAndAunts.Count == 0) {
                        generateFamilyRelationClue(currentNode, ref clues);
                    }
                    else {
                        int pickUncleOrAunt = rand.Next(0, unclesAndAunts.Count);
                        if (unclesAndAunts[pickUncleOrAunt].personGender == "f") {
                            Debug.Log("La tía de " + currentNode.personName + " es " + unclesAndAunts[pickUncleOrAunt].personName);
                            clues.Add("La tía de " + currentNode.personName + " es " + unclesAndAunts[pickUncleOrAunt].personName);
                        }
                        else {
                            Debug.Log("El tío de " + currentNode.personName + " es " + unclesAndAunts[pickUncleOrAunt].personName);
                            clues.Add("El tío de " + currentNode.personName + " es " + unclesAndAunts[pickUncleOrAunt].personName);
                        }
                    }
                }
            }
            //in relation to a cousin
            else if (randomNumberRelationship == 3) {
                if (currentNode.parentOne == null && currentNode.parentTwo == null) {
                    generateFamilyRelationClue(currentNode, ref clues);
                }
                else {
                    List<Node> cousins = new List<Node>();
                    //if they have one parent, add all thier siblings' children
                    if (currentNode.parentOne != null) {
                        if (currentNode.parentOne.siblings.Count > 0) {
                            foreach(Node parentSibling in currentNode.parentOne.siblings) {
                                foreach (Node cousin in parentSibling.children) {
                                    cousins.Add(cousin);
                                }
                            }
                        }
                    }
                    //if they have the other parent, add all thier siblings' children
                    if (currentNode.parentTwo != null) {
                        if (currentNode.parentTwo.siblings.Count > 0) {
                            foreach(Node parentSibling in currentNode.parentTwo.siblings) {
                                foreach (Node cousin in parentSibling.children) {
                                    cousins.Add(cousin);
                                }
                            }
                        }
                    }
                    //if the first parent is remarried, add all step parent's siblings' children
                    if (currentNode.parentOne != null && currentNode.parentOne.spouse != null && currentNode.parentOne.spouse != currentNode.parentTwo && currentNode.parentOne.spouse != null) {
                        if (currentNode.parentOne.spouse.siblings.Count > 0) {
                            foreach(Node parentSibling in currentNode.parentOne.spouse.siblings) {
                                foreach (Node cousin in parentSibling.children) {
                                    cousins.Add(cousin);
                                }
                            }
                        }
                    }
                    //if the second parent is remarried, add all step parent's siblings' children
                    if (currentNode.parentTwo != null && currentNode.parentTwo.spouse != null && currentNode.parentTwo.spouse != currentNode.parentOne && currentNode.parentTwo.spouse != null) {
                        if (currentNode.parentTwo.spouse.siblings.Count > 0) {
                            foreach(Node parentSibling in currentNode.parentTwo.spouse.siblings) {
                                foreach (Node cousin in parentSibling.children) {
                                    cousins.Add(cousin);
                                }
                            }
                        }
                    }
                    if (cousins.Count == 0) {
                        generateFamilyRelationClue(currentNode, ref clues);
                    }
                    else {
                        int pickCousin = rand.Next(0, cousins.Count);
                        if (cousins[pickCousin].personGender == "f") {
                            Debug.Log("La prima de " + currentNode.personName + " es " + cousins[pickCousin].personName);
                            clues.Add("La prima de " + currentNode.personName + " es " + cousins[pickCousin].personName);
                        }
                        else {
                            Debug.Log("El primo de " + currentNode.personName + " es " + cousins[pickCousin].personName);
                            clues.Add("El primo de " + currentNode.personName + " es " + cousins[pickCousin].personName);
                        }
                    }
                }
            }
            //in relation to a sibling
            // make sure to inlcude: full sibling, half siblings(have one parent in common by blood), step-siblings(no parents in common by blood, only by marriage)
            else if (randomNumberRelationship == 4) {
                if (currentNode.siblings.Count == 0) {
                    generateFamilyRelationClue(currentNode, ref clues);
                }
                else {
                    //should already have the sibling vector filled out correctly, just choose a random one
                    int pickSibling = rand.Next(0, currentNode.siblings.Count);
                    Node chosenSibling = currentNode.siblings[pickSibling];
                    //if they share two parents, they are full siblings
                    if (chosenSibling.parentOne == currentNode.parentOne && chosenSibling.parentTwo == currentNode.parentTwo) {
                        if (chosenSibling.personGender == "f") {
                            Debug.Log("La hermana de " + currentNode.personName + " es " + chosenSibling.personName);
                            clues.Add("La hermana de " + currentNode.personName + " es " + chosenSibling.personName);
                        }
                        else {
                            Debug.Log("El hermano de " + currentNode.personName + " es " + chosenSibling.personName);
                            clues.Add("El hermano de " + currentNode.personName + " es " + chosenSibling.personName);
                        }
                    }
                    //if they only share on parent, they are half-siblings
                    else if (chosenSibling.parentOne == currentNode.parentOne || chosenSibling.parentTwo == currentNode.parentTwo) {
                        if (chosenSibling.personGender == "f") {
                            Debug.Log("La media hermana de " + currentNode.personName + " es " + chosenSibling.personName);
                            clues.Add("La media hermana de " + currentNode.personName + " es " + chosenSibling.personName);
                        }
                        else {
                            Debug.Log("El medio hermano de " + currentNode.personName + " es " + chosenSibling.personName);
                            clues.Add("El medio hermano de " + currentNode.personName + " es " + chosenSibling.personName);
                        }
                    }
                    //if they don't share either parent, they are step siblings, meaning their parents had them with other peopel then remarried
                    else {
                        if (chosenSibling.personGender == "f") {
                            Debug.Log("La hermanastra de " + currentNode.personName + " es " + chosenSibling.personName);
                            clues.Add("La hermanastra de " + currentNode.personName + " es " + chosenSibling.personName);
                        }
                        else {
                            Debug.Log("La hermanastro de " + currentNode.personName + " es " + chosenSibling.personName);
                            clues.Add("La hermanastro de " + currentNode.personName + " es " + chosenSibling.personName);
                        }
                    }
                }
            }
            //in relation to a spouse
            else if (randomNumberRelationship == 5) {
                if (currentNode.spouse == null) {
                    generateFamilyRelationClue(currentNode, ref clues);
                }
                else {
                    if (currentNode.spouse.personGender == "f") {
                        Debug.Log("La esposa de " + currentNode.personName + " es " + currentNode.spouse.personName);
                        clues.Add("La esposa de " + currentNode.personName + " es " + currentNode.spouse.personName);
                    }
                    else {
                        Debug.Log("El esposo de " + currentNode.personName + " es " + currentNode.spouse.personName);
                        clues.Add("El esposo de " + currentNode.personName + " es " + currentNode.spouse.personName);
                    }
                }
            }
            //in relation to a child
            else if (randomNumberRelationship == 6) {
                if (currentNode.children.Count == 0) {
                    generateFamilyRelationClue(currentNode, ref clues);
                }
                else {
                    int pickChild = rand.Next(0, currentNode.children.Count);
                    Node chosenChild = currentNode.children[pickChild];
                    //if either one of chosen child's parents are currentNode, then this is their blood child
                    if (chosenChild.parentOne == currentNode || chosenChild.parentTwo == currentNode) {
                        if (chosenChild.personGender == "f") {
                            Debug.Log("La hija de " + currentNode.personName + " es " + chosenChild.personName);
                            clues.Add("La hija de " + currentNode.personName + " es " + chosenChild.personName);
                        }
                        else {
                            Debug.Log("El hijo de " + currentNode.personName + " es " + chosenChild.personName);
                            clues.Add("El hijo de " + currentNode.personName + " es " + chosenChild.personName);
                        }
                    }
                    //if neither one of chosen child's parents are currentNode, then this child is a step child
                    else {
                        if (currentNode.spouse.personGender == "f") {
                            Debug.Log("La hijastra de " + currentNode.personName + " es " + currentNode.spouse.personName);
                            clues.Add("La hijastra de " + currentNode.personName + " es " + currentNode.spouse.personName);
                        }
                        else {
                            Debug.Log("El hijastro de " + currentNode.personName + " es " + currentNode.spouse.personName);
                            clues.Add("El hijastro de " + currentNode.personName + " es " + currentNode.spouse.personName);
                        }
                    }
                }
            }
            //in realtion to a grandchild
            else if (randomNumberRelationship == 7) {
                if (currentNode.children.Count == 0) {
                    generateFamilyRelationClue(currentNode, ref clues);
                }
                else {
                    List<Node> grandchildren = new List<Node>();
                    foreach (Node child in currentNode.children) {
                        foreach (Node grandchild in child.children) {
                            grandchildren.Add(grandchild);
                        }
                    }
                    if (grandchildren.Count < 1) {
                        generateFamilyRelationClue(currentNode, ref clues);
                    }
                    else {
                        int pickGrandchild = rand.Next(0, grandchildren.Count);
                        if (grandchildren[pickGrandchild].personGender == "f") {
                            Debug.Log("La nieta de " + currentNode.personName + " es " + grandchildren[pickGrandchild].personName);
                            clues.Add("La nieta de " + currentNode.personName + " es " + grandchildren[pickGrandchild].personName);
                        }
                        else {
                            Debug.Log("El nieto de " + currentNode.personName + " es " + grandchildren[pickGrandchild].personName);
                            clues.Add("El nieto de " + currentNode.personName + " es " + grandchildren[pickGrandchild].personName);
                        }
                    }
                }
            }
            else {
                Debug.Log("Error, could not make a clue for this relationship.");
            }
            return; 
        
        }  
    }

}
