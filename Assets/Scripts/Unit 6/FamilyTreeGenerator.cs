using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class FamilyTreeGenerator : MonoBehaviour
{
    public int maxPeople = 25;
    List<GameObject> toWin = new List<GameObject>();
    public GameObject winScreen;
    public int numMistakes = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FamilyTree.Node root = buildTree(maxPeople);
        Debug.Log("-------Family Tree----------");
        HashSet<FamilyTree.Node> visitedNodes = new HashSet<FamilyTree.Node>();
        List<String> cluesList = new List<String>();
        
        FamilyTree.TreePrinter.generateAndPrintTreeAndClues(root, visitedNodes, ref cluesList);
        Debug.Log("Number of nodes in hashset: " + visitedNodes.Count);
        
        
        //send data over to the NodeSpawner script
        NodeSpawner nodeSpawner = FindFirstObjectByType<NodeSpawner>();
        if (nodeSpawner != null) {
            toWin = nodeSpawner.generateNodes(root);         
            
        }

        //send data over to scrollable view
        List<String> tempList = new List<String>();
        foreach (FamilyTree.Node person in visitedNodes) {
            tempList.Add(person.personName);
        }

        //but first- scramble the names
        List<string> scrambleNames = new List<String>();
        while (tempList.Count > 0) {
            int randomNumber = Random.Range(0, tempList.Count);
            scrambleNames.Add(tempList[randomNumber]);
            tempList.RemoveAt(randomNumber);
        }
        
        //now send
        ScrollBarLoader imageLoader = FindFirstObjectByType<ScrollBarLoader>();
        if (imageLoader != null) {
            foreach (string name in scrambleNames) {
                imageLoader.AddItem(name);
            }
        }

        //sending over clues list
        ClueLoader clueLoader = FindFirstObjectByType<ClueLoader>();
        if (clueLoader != null) {
            foreach (string clue in cluesList) {
                clueLoader.AddItem(clue);
            }
        }
    }

    private FamilyTree.Node buildTree(int maxNumberOfPeople) {
        return FamilyTree.TreeBuilder.buildTree(maxNumberOfPeople);
    }

    private void generateAndPrintTreeAndClues(FamilyTree.Node root, HashSet<FamilyTree.Node> visited, ref List<string> clues, int depth = 0) {
        FamilyTree.TreePrinter.generateAndPrintTreeAndClues(root, visited, ref clues, depth);
    }

    public bool checkForTreeWin() {
        int count = 0;
        foreach (GameObject holder in toWin) {
            Transform child = holder.transform.Find(holder.name);
            if (child != null) {
                count++;
            }
        }
        Debug.Log("Total correct: " + count);
        if (count == toWin.Count) {
            Transform child = winScreen.transform.Find("Stats");
            child.GetComponent<TextMeshProUGUI>().text = "Mistakes: " + numMistakes;
            winScreen.SetActive(true);
            return true;
        }
        else {
            return false;
        }
    }

    public void addMistake() {
        numMistakes++;
    }
}

namespace FamilyTree {
    public class Node {
        public string personName;
        public string maritalStatus;    // m = married, s = single, d = divorce, r = remarried
        public bool needsParent;        // only true for root+spouse & root+spouse parents/grandparents/greatgrandparents/etc
        public bool isMale;             // stores node sex, true for males
        public bool isRoot;             // starting node, used for building tree & determining which nodes have kids/parents
        public bool isLeftSpouse;       // determines placement in tree when married
        public int gen;                 // tracks current node generation, used to stop tree building after set amount of gens

        public Node parentOne = null;
        public Node parentTwo = null;
        public Node spouse = null;
        public Node exSpouse = null;
        
        public List<Node> children = new List<Node>();
        public List<Node> siblings = new List<Node>();
        
        private static List<string> _maleNames = new List<string>
        {
            "Santiago", "Matías", "Sebastián", "Mateo", "Nicolás", "Alejandro",
            "Samuel", "Diego", "Daniel", "Benjamín", "Leonardo", "Tomás", "Joaquín",
            "Gabriel", "Emiliano", "Martín", "Lucas", "Agustín", "David", "Iker",
            "José", "Maximiliano", "Adrián", "Emmanuel", "Felipe", "Juan Pablo",
            "Andrés", "Gerónimo", "Ángel", "Rodrigo", "Bruno", "Alexander", "Thiago",
            "Pablo", "Ian", "Isaac", "Fernando", "Javier", "Emilio", "Sebastián", 
            "Alonso", "Aarón", "Rafael", "Esteban", "Juan", "Axel", "Francisco", 
            "Bautista", "Carlos", "Dylan", "Julián", "Manuel", "Facundo", "Gael",
            "Valentino", "Damián", "Santino", "Vicente", "Máximo", "Christopher", 
            "Jorge", "Luciano", "Dante", "Alan", "Cristóbal", "Jesús", "Lorenzo", 
            "Alex", "Patricio", "Pedro", "Manuel", "Matthew", "Antonio", "Iván", 
            "José", "Hugo", "Josué", "Lautaro", "Miguel", "Franco", "Kevin", "Luis",
            "Simón", "Elías", "Caleb", "Eduardo", "Ricardo", "Juan David", "Marcos",
            "Salvador", "Jacobo", "Ignacio", "Camilo", "Mauricio", "Gonzalo"
        };
        private static List<string> _femaleNames = new List<string>
        {
            "Sofía", "Isabella", "Valentina", "Emma", "Martina", "Lucía", "Victoria",
            "Luciana", "Valeria", "Camila", "Julieta", "Ximena", "Sara", "Daniela",
            "Emilia", "Xiomara", "Mía", "Catalina", "Julia", "Elena", "Olivia",
            "Paula", "Natalia", "Mariana", "Samantha", "María", "Antonella",
            "Gabriela", "Emily", "Zoe", "Alma", "Alejandra", "Andrea", "Juliana",
            "Alba", "Aaliyah", "Jahaira", "Carla", "Laura", "Ángela", "Clara",
            "Teresa", "Laura", "Fernanda", "Camila", "Inés", "Silvia", "Regina",
            "Carmen", "Teresa", "Valeria", "Marisol", "Guadalupe", "Adriana",
            "Beatriz", "Patricia", "Carmen", "Isabel", "Mariana", "Teresa", "María",
            "Susana", "Clara", "Mónica", "Viviana", "Lidia", "Dolores", "Stefanie",
            "Violeta", "Veronica", "Jocelyn", "Gloria", "Angélica", "Rosalía",
            "Silvia", "Aida", "Raquel", "Leticia"
        };


        public Node() {
            // Set odds of M or F based on number of names left in each list
            isMale = Random.Range(1, _maleNames.Count + _femaleNames.Count + 1) <= _maleNames.Count;

            if (isMale)
                personName = getName(_maleNames);
            else
                personName = getName(_femaleNames);

            needsParent = false;
            isRoot = false;
            isLeftSpouse = isMale;
        }

        public Node(bool male) {
            if (male)
                personName = getName(_maleNames);
            else
                personName = getName(_femaleNames);

            needsParent = false;
            isRoot = false;
            isMale = male;
            isLeftSpouse = male;
        }

        private static string getName(List<string> _names) {
            if (_names.Count != 0) {
                int index = Random.Range(0,_names.Count);
                string name = _names[index];
                _names.RemoveAt(index);
                return name;
            }
            else {
                return "out of names lol";
            }
        }

        public void setRandomStatus() {
            // Don't select divorce/remarriage for node that can't have kids
            int statusRange = (canHaveChildren()) ? 4 : 2;
            int status = Random.Range(0, statusRange);

            switch (status) {
                case 0:
                    maritalStatus = "s";
                    break;
                case 1:
                    maritalStatus = "m";
                    break;
                case 2:
                    maritalStatus = "d";
                    break;
                default:
                    maritalStatus = "r";
                    break;
            }

        }

        public bool canHaveChildren() {
            if (isRoot)
                return true;

            if (needsParent)
                return true;

            foreach (Node sibling in siblings) {
                if (sibling.isRoot)
                    return true;
            }

            return false;
        }

        public bool getSpouseGender() {
            // Make spouse more likely to be opposite gender
            return (Random.Range(0,6) < 5) ? !isMale : isMale;
        }
    }
    
    public static class TreeBuilder{
        public static Node buildTree(int maxNumberOfPeople) {
            int numberOfPeopleLeft = maxNumberOfPeople;
            List<Node> toVisitQueue = new List<Node>();

            //start node ()
            Node root = new Node();
            root.isRoot = true;
            root.needsParent = true;
            root.setRandomStatus();

            root.gen = 1;

            toVisitQueue.Add(root);
            numberOfPeopleLeft--;

            //now visit all nodes that are in the queue
            while (toVisitQueue.Count > 0 && numberOfPeopleLeft > 0) {
                Node person = toVisitQueue[0];
                toVisitQueue.RemoveAt(0);
                generateNodeInfo(person, ref numberOfPeopleLeft, ref toVisitQueue);
            }

            Debug.Log("NumPeopleLeft: " + numberOfPeopleLeft);

            return root;
        }

        public static void generateNodeInfo(Node currentNode, ref int numPeopleLeft, ref List<Node> toVisitQueue) {
            if (numPeopleLeft <= 0 || currentNode == null)
                return;

            //RANDOMIZED MARITAL STATUS AND CHILDREN
            // just so long the node is not already married(meaning spouse = null), we can make it any type
            if (currentNode.spouse == null) {
                //SINGLE
                if (currentNode.maritalStatus == "s") {
                    // if able, single nodes have 25% chance of having kids
                    if (currentNode.canHaveChildren()) {
                        if (Random.Range(0,4) < 1)
                            generateNodeChild(currentNode, null, ref numPeopleLeft, ref toVisitQueue);
                    }
                }
                //MARRIED
                else if (currentNode.maritalStatus == "m") {
                    // create a spouse, add info, add to queue
                    Node spouse = new Node(currentNode.getSpouseGender());
                    spouse.maritalStatus = "m";
                    spouse.gen = currentNode.gen;
                    spouse.spouse = currentNode;
                    currentNode.spouse = spouse;
                    
                    toVisitQueue.Add(spouse);
                    numPeopleLeft--;

                    // set positions in spouse group in tree
                    spouse.isLeftSpouse = !currentNode.isLeftSpouse;
                    
                    // make spouse parent/grandparent of root if current node is
                    if (currentNode.needsParent) {
                        spouse.needsParent = true;
                        foreach(Node child in currentNode.children) {
                            child.parentTwo = spouse;
                            spouse.children.Add(child);
                        }
                    }

                    // if able, married nodes have 75% chance of having kids
                    if(currentNode.canHaveChildren()) {
                        if (Random.Range(0,4) < 3)
                            generateNodeChild(currentNode, spouse, ref numPeopleLeft, ref toVisitQueue); 
                    }     
                }
                //DIVORCED
                else if (currentNode.maritalStatus == "d") {
                    // only create old spouse if not generated from previous node divorce
                    // must have kids together, so skip if only 1 person left
                    if (currentNode.exSpouse == null && numPeopleLeft > 1) {
                        Node exSpouse = new Node(currentNode.getSpouseGender());
                        exSpouse.gen = currentNode.gen;
                        exSpouse.exSpouse = currentNode;
                        currentNode.exSpouse = exSpouse;

                        // Decided to limit remarriages (step family) to bottom generation
                        if (currentNode.gen > 1)
                            exSpouse.maritalStatus = "d";
                        else
                            exSpouse.maritalStatus = Random.Range(0,2) < 1 ? "r" : "d";  //randomly decide if ex spouse remarried or not

                        toVisitQueue.Add(exSpouse);
                        numPeopleLeft--;

                        // set positions in spouse group in tree
                        exSpouse.isLeftSpouse = !currentNode.isLeftSpouse;

                        // make ex-spouse parent/grandparent of root if current node is
                        if (currentNode.needsParent) {
                            exSpouse.needsParent = true;
                            foreach(Node child in currentNode.children) {
                                child.parentTwo = exSpouse;
                                exSpouse.children.Add(child);
                            }
                        }

                        // only nodes that have kids will show divorce, always generate kids
                        generateNodeChild(currentNode, exSpouse, ref numPeopleLeft, ref toVisitQueue);
                    }
                }
                //REMARRIED
                else {
                    // create old spouse if not generated from previous node divorce
                    // must have kids together, so skip if only 1 person left
                    if (currentNode.exSpouse == null && numPeopleLeft > 1) {
                        Node exSpouse = new Node(currentNode.getSpouseGender());
                        exSpouse.maritalStatus = Random.Range(0,2) < 1 ? "r" : "d";  //randomly decide if ex spouse remarried or not
                        exSpouse.gen = currentNode.gen;
                        exSpouse.exSpouse = currentNode;  
                        currentNode.exSpouse = exSpouse;
                        
                        toVisitQueue.Add(exSpouse);
                        numPeopleLeft--;

                        exSpouse.isLeftSpouse = !currentNode.isLeftSpouse;

                        // if current node is, randomly decide if ex-spouse is parent/grandparent of root 
                        if (currentNode.needsParent && (Random.Range(0,2) < 1)) {
                            exSpouse.needsParent = true;
                            foreach(Node child in currentNode.children) {
                                child.parentTwo = exSpouse;
                                exSpouse.children.Add(child);
                            }
                        }

                        generateNodeChild(currentNode, exSpouse, ref numPeopleLeft, ref toVisitQueue);
                    }

                    // create new spouse
                    Node spouse = new Node(currentNode.getSpouseGender());
                    spouse.maritalStatus = "m";
                    spouse.gen = currentNode.gen;
                    spouse.spouse = currentNode;
                    currentNode.spouse = spouse;
                    
                    toVisitQueue.Add(spouse);
                    numPeopleLeft--;

                    // set positions in spouse group in tree
                    spouse.isLeftSpouse = !currentNode.isLeftSpouse;

                    // if current node is, and ex spouse is not, make new spouse parent/grandparent of root
                    if (currentNode.needsParent && !currentNode.exSpouse.needsParent) {
                        spouse.needsParent = true;
                        foreach(Node child in currentNode.children) {
                            if (child.parentTwo == null) {
                                child.parentTwo = spouse;
                                spouse.children.Add(child);
                            }
                        }
                    }

                    // 75% chance of having kids with new spouse
                    if (Random.Range(0,4) < 3)
                        generateNodeChild(currentNode, spouse, ref numPeopleLeft, ref toVisitQueue);
                }
                
            }

            //GENERATE PARENT (only for root, root parents, root grandparents, etc)
            if (currentNode.needsParent && currentNode.gen < 3) {
                if (numPeopleLeft > 0) {
                    Node parent = new Node();
                    parent.needsParent = true;
                    parent.gen = currentNode.gen + 1;
                    parent.children.Add(currentNode);
                    currentNode.parentOne = parent;

                    parent.setRandomStatus();

                    // Decided to limit remarriages (step family) to bottom generation
                    if (parent.gen > 1 && parent.maritalStatus == "r")
                        parent.maritalStatus = "d";

                    toVisitQueue.Add(parent);
                    numPeopleLeft--;
                }
            }
        }
        
        public static void generateNodeChild(Node parentOne, Node parentTwo, ref int numPeopleLeft, ref List<Node> toVisitQueue) {
            if (parentOne == null) {
                Debug.Log("Child generator had null parent one");
                return;
            }

            int numChildren = Random.Range(1,3);
            for (int i = 0; i < numChildren; i++) {
                if (numPeopleLeft <= 0)
                    return;

                Node newChild = new Node();
                newChild.gen = parentOne.gen - 1;
                newChild.parentOne = parentOne;
                newChild.parentTwo = parentTwo;

                // makes sure that they don't overlap in sibling lists
                foreach (Node sibling in newChild.parentOne.children) {
                    if (sibling != newChild && !newChild.siblings.Contains(sibling)) {
                        newChild.siblings.Add(sibling);
                        sibling.siblings.Add(newChild);
                    }
                }

                if (parentOne.exSpouse != null) {
                    foreach (Node sibling in newChild.parentOne.exSpouse.children) {
                        if (sibling != newChild && !newChild.siblings.Contains(sibling)) {
                            newChild.siblings.Add(sibling);
                            sibling.siblings.Add(newChild);
                        }
                    }
                }

                // add child to parent one chilren list
                parentOne.children.Add(newChild);

                if (parentTwo != null) {
                    foreach (Node sibling in newChild.parentTwo.children) {
                        if (sibling != newChild && !newChild.siblings.Contains(sibling)) {
                            newChild.siblings.Add(sibling);
                            sibling.siblings.Add(newChild);
                        }
                    }

                    if (parentTwo.exSpouse != null) {
                    foreach (Node sibling in newChild.parentTwo.exSpouse.children) {
                        if (sibling != newChild && !newChild.siblings.Contains(sibling)) {
                            newChild.siblings.Add(sibling);
                            sibling.siblings.Add(newChild);
                        }
                    }
                }
                    //add child to parent two children list
                    parentTwo.children.Add(newChild);
                }
                
                newChild.setRandomStatus();

                toVisitQueue.Add(newChild);
                numPeopleLeft--;
            }
        }
    }

    public static class TreePrinter {
        public static void generateAndPrintTreeAndClues(Node root, HashSet<Node> visited, ref List<string> clues, int depth = 0) {
            // base case
            if (root == null) {
                return;
            }
            // returns if has already has it
            if (visited.Contains(root)) {
                return;
            }
            // if it doesn't add it to the hash
            visited.Add(root);
            // after that it makes a clue for this new visit
            //
            // string indent = new string(' ', depth * 4);
            string nodeInfo = "";
            // info on root
            // name/gender/marital status
            nodeInfo += root.personName + " (Sex: " + ((root.isMale) ? "Male" : "Female") + ", MaritalStatus: " + root.maritalStatus + ", Gen: " + root.gen + ")";
            
            // parents
            if (root.parentOne != null)
                nodeInfo += "\n--Parent One: " + root.parentOne.personName;

            if (root.parentTwo != null)
                nodeInfo += "\n--Parent Two: " + root.parentTwo.personName;
                
            // spouse
            if (root.spouse != null)
                nodeInfo += "\n--Spouse: " + root.spouse.personName;

            // ex-spouse
            if (root.exSpouse != null)
                nodeInfo += "\n--Ex-Spouse: " + root.exSpouse.personName;

            //children
            if (root.children.Count > 0)
                nodeInfo += "\n--Children: ";

            foreach (Node child in root.children) 
                nodeInfo += child.personName + ", ";

            //siblings
            if (root.siblings.Count > 0)
                nodeInfo += "\n--Siblings: ";

            foreach (Node sibling in root.siblings) 
                nodeInfo += sibling.personName + ", ";

            List<int> options = new List<int>{0,1,2,3,4,5,6,7};
            generateFamilyRelationClue(root, ref clues, options);
            Debug.Log(nodeInfo);
            
            generateAndPrintTreeAndClues(root.spouse, visited, ref clues, depth);
            generateAndPrintTreeAndClues(root.exSpouse, visited, ref clues, depth);
            
            generateAndPrintTreeAndClues(root.parentOne, visited, ref clues, depth);
            generateAndPrintTreeAndClues(root.parentTwo, visited, ref clues, depth);
            
            foreach (Node child in root.children)
                generateAndPrintTreeAndClues(child, visited, ref clues, depth + 1);
            
        }  

        //generate the clues
        public static void generateFamilyRelationClue(Node currentNode, ref List<string> clues, List<int> options) {
            //base case: makes sure no stray nodes break the system
            if (currentNode.parentOne == null && currentNode.parentTwo == null &&
                currentNode.spouse == null && currentNode.children.Count < 1 && currentNode.siblings.Count < 1) {
                Debug.Log("No relationships for this node is found.");
                return;
            }
            //randomly decide what relationship to make the clue
            int randomNumberRelationship = Random.Range(0,8);
            //in relation to a grandparent
            if (randomNumberRelationship == 0) {
                //if the current node does not have any grandparents, roll again (have to check if parent is there first)
                if ((currentNode.parentOne == null || (currentNode.parentOne.parentOne == null && currentNode.parentOne.parentTwo == null)) &&
                    (currentNode.parentTwo == null || (currentNode.parentTwo.parentOne == null && currentNode.parentTwo.parentTwo == null))) {
                    options.Remove(0);
                    generateFamilyRelationClue(currentNode, ref clues, options);
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
                        options.Remove(0);
                        generateFamilyRelationClue(currentNode, ref clues, options);
                    }
                    else {
                        int pickGrandparent = Random.Range(0, grandparents.Count);
                        Node chosenGrandparent = grandparents[pickGrandparent];
                        if (chosenGrandparent.isMale) {
                            //Debug.Log("El abuelo de " + currentNode.personName + " es " + grandparents[pickGrandparent].personName);
                            clues.Add("El abuelo de " + currentNode.personName + " es " + grandparents[pickGrandparent].personName);
                        }
                        else {
                            //Debug.Log("La abuela de " + currentNode.personName + " es " + grandparents[pickGrandparent].personName);
                            clues.Add("La abuela de " + currentNode.personName + " es " + grandparents[pickGrandparent].personName);
                        }
                    }
                }
            }
            //in relation to a parent
            //parent relationships that are importnat to remember:
                // mother/fater and step mother/father
            else if (randomNumberRelationship == 1) {
                if (currentNode.parentOne == null && currentNode.parentTwo == null) {
                    options.Remove(1);
                    generateFamilyRelationClue(currentNode, ref clues, options);
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
                    if (currentNode.parentOne != null && currentNode.parentOne.spouse != null && currentNode.parentOne.spouse != currentNode.parentTwo) {
                        parents.Add(currentNode.parentOne.spouse);
                    }
                    //if the second parent is remarried
                    if (currentNode.parentTwo != null && currentNode.parentTwo.spouse != null && currentNode.parentTwo.spouse != currentNode.parentOne) {
                        parents.Add(currentNode.parentTwo.spouse);
                    }

                    int pickParent = Random.Range(0, parents.Count);
                    Node chosenParent = parents[pickParent];
                    if ( chosenParent == currentNode.parentOne || chosenParent == currentNode.parentTwo) {
                        if (chosenParent.isMale) {
                            //Debug.Log("El padre de " + currentNode.personName + " es " + parents[pickParent].personName);
                            clues.Add("El padre de " + currentNode.personName + " es " + parents[pickParent].personName);
                        }
                        else {
                            //Debug.Log("La madre de " + currentNode.personName + " es " + parents[pickParent].personName);
                            clues.Add("La madre de " + currentNode.personName + " es " + parents[pickParent].personName);
                        }
                    }
                    else {
                        if (chosenParent.isMale) {
                            //Debug.Log("El padrastro de " + currentNode.personName + " es " + parents[pickParent].personName);
                            clues.Add("El padrastro de " + currentNode.personName + " es " + parents[pickParent].personName);
                        }
                        else {
                            //Debug.Log("La madrastra de " + currentNode.personName + " es " + parents[pickParent].personName);
                            clues.Add("La madrastra de " + currentNode.personName + " es " + parents[pickParent].personName);
                        }
                    }
                }
                
            }
            //in relation to an aunt/unlce
            //remember: if a child has a step parent, then step parent's siblings are their aunt/uncles too
            else if (randomNumberRelationship == 2) {
                if (currentNode.parentOne == null && currentNode.parentTwo == null) {
                    options.Remove(2);
                    generateFamilyRelationClue(currentNode, ref clues, options);
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
                        options.Remove(2);
                        generateFamilyRelationClue(currentNode, ref clues, options);
                    }
                    else {
                        int pickUncleOrAunt = Random.Range(0, unclesAndAunts.Count);
                        Node uncleOrAunt = unclesAndAunts[pickUncleOrAunt];
                        if (uncleOrAunt.isMale) {
                            //Debug.Log("El tío de " + currentNode.personName + " es " + unclesAndAunts[pickUncleOrAunt].personName);
                            clues.Add("El tío de " + currentNode.personName + " es " + unclesAndAunts[pickUncleOrAunt].personName);
                        }
                        else {
                            
                            //Debug.Log("La tía de " + currentNode.personName + " es " + unclesAndAunts[pickUncleOrAunt].personName);
                            clues.Add("La tía de " + currentNode.personName + " es " + unclesAndAunts[pickUncleOrAunt].personName);
                        }
                    }
                }
            }
            //in relation to a cousin
            else if (randomNumberRelationship == 3) {
                if (currentNode.parentOne == null && currentNode.parentTwo == null) {
                    options.Remove(3);
                    generateFamilyRelationClue(currentNode, ref clues, options);
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
                        options.Remove(3);
                        generateFamilyRelationClue(currentNode, ref clues, options);
                    }
                    else {
                        int pickCousin = Random.Range(0, cousins.Count);
                        Node chosenCousin = cousins[pickCousin];
                        if (chosenCousin.isMale) {
                            //Debug.Log("El primo de " + currentNode.personName + " es " + cousins[pickCousin].personName);
                            clues.Add("El primo de " + currentNode.personName + " es " + cousins[pickCousin].personName);
                        }
                        else {
                            
                            //Debug.Log("La prima de " + currentNode.personName + " es " + cousins[pickCousin].personName);
                            clues.Add("La prima de " + currentNode.personName + " es " + cousins[pickCousin].personName);
                        }
                    }
                }
            }
            //in relation to a sibling
            // make sure to inlcude: full sibling, half siblings(have one parent in common by blood), step-siblings(no parents in common by blood, only by marriage)
            else if (randomNumberRelationship == 4) {
                if (currentNode.siblings.Count == 0) {
                    options.Remove(4);
                    generateFamilyRelationClue(currentNode, ref clues, options);
                }
                else {
                    //should already have the sibling vector filled out correctly, just choose a random one
                    int pickSibling = Random.Range(0, currentNode.siblings.Count);
                    Node chosenSibling = currentNode.siblings[pickSibling];
                    //if they share two parents, they are full siblings
                    if (chosenSibling.parentOne == currentNode.parentOne && chosenSibling.parentTwo == currentNode.parentTwo) {
                        if (chosenSibling.isMale) {
                            //Debug.Log("El hermano de " + currentNode.personName + " es " + chosenSibling.personName);
                            clues.Add("El hermano de " + currentNode.personName + " es " + chosenSibling.personName);
                        }
                        else {
                            //Debug.Log("La hermana de " + currentNode.personName + " es " + chosenSibling.personName);
                            clues.Add("La hermana de " + currentNode.personName + " es " + chosenSibling.personName);
                        }
                    }
                    //if they only share on parent, they are half-siblings
                    else if (chosenSibling.parentOne == currentNode.parentOne || chosenSibling.parentTwo == currentNode.parentTwo) {
                        if (chosenSibling.isMale) {
                            //Debug.Log("El medio hermano de " + currentNode.personName + " es " + chosenSibling.personName);
                            clues.Add("El medio hermano de " + currentNode.personName + " es " + chosenSibling.personName);
                        }
                        else {
                            //Debug.Log("La media hermana de " + currentNode.personName + " es " + chosenSibling.personName);
                            clues.Add("La media hermana de " + currentNode.personName + " es " + chosenSibling.personName);
                        }
                    }
                    //if they don't share either parent, they are step siblings, meaning their parents had them with other peopel then remarried
                    else {
                        if (chosenSibling.isMale) {
                            //Debug.Log("La hermanastro de " + currentNode.personName + " es " + chosenSibling.personName);
                            clues.Add("La hermanastro de " + currentNode.personName + " es " + chosenSibling.personName);
                        }
                        else {
                            //Debug.Log("La hermanastra de " + currentNode.personName + " es " + chosenSibling.personName);
                            clues.Add("La hermanastra de " + currentNode.personName + " es " + chosenSibling.personName);
                        }
                    }
                }
            }
            //in relation to a spouse
            else if (randomNumberRelationship == 5) {
                // If current node has no spouses, chose a different option
                if (currentNode.spouse == null && currentNode.exSpouse == null) {
                    options.Remove(5);
                    generateFamilyRelationClue(currentNode, ref clues, options);
                }
                else {
                    bool useCurrentSpouse;
                    // If current node has both spouse & ex-spouse, randomly choose one
                    if (currentNode.spouse != null && currentNode.exSpouse != null) {
                        useCurrentSpouse = Random.Range(0,2) < 1;
                    }
                    // Otherwise, choose whatever they do have
                    else {
                        useCurrentSpouse = (currentNode.spouse != null) ? true : false;
                    }
                    if (useCurrentSpouse) {
                        if (currentNode.spouse.isMale) {
                            //Debug.Log("El esposo de " + currentNode.personName + " es " + currentNode.spouse.personName);
                            clues.Add("El esposo de " + currentNode.personName + " es " + currentNode.spouse.personName);
                        }
                        else {
                            
                            //Debug.Log("La esposa de " + currentNode.personName + " es " + currentNode.spouse.personName);
                            clues.Add("La esposa de " + currentNode.personName + " es " + currentNode.spouse.personName);
                        }
                    }
                    else {
                        if (currentNode.exSpouse.isMale) {
                            //Debug.Log("El ex esposo de " + currentNode.personName + " es " + currentNode.exSpouse.personName);
                            clues.Add("El ex esposo de " + currentNode.personName + " es " + currentNode.exSpouse.personName);
                        }
                        else {
                            
                            //Debug.Log("La ex esposa de " + currentNode.personName + " es " + currentNode.exSpouse.personName);
                            clues.Add("La ex esposa de " + currentNode.personName + " es " + currentNode.exSpouse.personName);
                        }
                    }
                }
            }
            //in relation to a child
            else if (randomNumberRelationship == 6) {
                if (currentNode.children.Count == 0) {
                    options.Remove(6);
                    generateFamilyRelationClue(currentNode, ref clues, options);
                }
                else {
                    int pickChild = Random.Range(0, currentNode.children.Count);
                    Node chosenChild = currentNode.children[pickChild];
                    //if either one of chosen child's parents are currentNode, then this is their blood child
                    if (chosenChild.parentOne == currentNode || chosenChild.parentTwo == currentNode) {
                        if (chosenChild.isMale) {
                            //Debug.Log("El hijo de " + currentNode.personName + " es " + chosenChild.personName);
                            clues.Add("El hijo de " + currentNode.personName + " es " + chosenChild.personName);
                        }
                        else {
                            
                            //Debug.Log("La hija de " + currentNode.personName + " es " + chosenChild.personName);
                            clues.Add("La hija de " + currentNode.personName + " es " + chosenChild.personName);
                        }
                    }
                    //if neither one of chosen child's parents are currentNode, then this child is a step child
                    else {
                        if (chosenChild.isMale) {
                            //Debug.Log("El hijastro de " + currentNode.personName + " es " + currentNode.spouse.personName);
                            clues.Add("El hijastro de " + currentNode.personName + " es " + chosenChild.personName);
                        }
                        else {
                            //Debug.Log("La hijastra de " + currentNode.personName + " es " + currentNode.spouse.personName);
                            clues.Add("La hijastra de " + currentNode.personName + " es " + chosenChild.personName);
                            
                        }
                    }
                }
            }
            //in realtion to a grandchild
            else if (randomNumberRelationship == 7) {
                if (currentNode.children.Count == 0) {
                    options.Remove(7);
                    generateFamilyRelationClue(currentNode, ref clues, options);
                }
                else {
                    List<Node> grandchildren = new List<Node>();
                    foreach (Node child in currentNode.children) {
                        foreach (Node grandchild in child.children) {
                            grandchildren.Add(grandchild);
                        }
                    }
                    if (grandchildren.Count < 1) {
                        options.Remove(7);
                        generateFamilyRelationClue(currentNode, ref clues, options);
                    }
                    else {
                        int pickGrandchild = Random.Range(0, grandchildren.Count);
                        Node chosenGrandchild = grandchildren[pickGrandchild];
                        if (chosenGrandchild.isMale) {
                            //Debug.Log("El nieto de " + currentNode.personName + " es " + chosenGrandchild.personName);
                            clues.Add("El nieto de " + currentNode.personName + " es " + chosenGrandchild.personName);
                        }
                        else {
                            //Debug.Log("La nieta de " + currentNode.personName + " es " + chosenGrandchild.personName);
                            clues.Add("La nieta de " + currentNode.personName + " es " + chosenGrandchild.personName);
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
