using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class FriendBuilder : MonoBehaviour
{
    
    private List<Friend> friendlist;
    private int friendIndex = 0;
    public int wrongCount = 0;

    //things in the scene
    public GameObject draggablePrefab;
    public Transform activityPanel;
    public GameObject acceptButton;
    public GameObject rejectButton;
    public TextMeshProUGUI speechBox;
    public Image friendBody;
    public Image friendHair;
    public Image friendOutfit;
    public Image friendFace;

    //list of sprites for friend appearance
    public Sprite[] bodySprites;
    public Sprite[] hairSprites;
    public Sprite[] kitSprites;

    //makeFriends will be called by ScheduleBuilder
    //creates a list of friends with corresponding plan messages
    public List<Friend> makeFriends(Dictionary<string, List<int>> _acceptList, Dictionary<string, List<int>> _denyList) {
        //temp versions of lists
        //so we don't remove objects from the actual lists
        Dictionary<string, List<int>> acceptList = new Dictionary<string, List<int>>(_acceptList);
        Dictionary<string, List<int>> denyList = new Dictionary<string, List<int>>(_denyList);

        //list for friends + plans
        List<Friend> friendList = new List<Friend>();
        
        //iterate through all of them
        while(acceptList.Count > 0 || acceptList.Count > 0) {
            //choose random appearance
            System.Random rand = new System.Random();
            Sprite body = bodySprites[rand.Next(0,bodySprites.Length)];
            Sprite hair = hairSprites[rand.Next(0,hairSprites.Length)];
            Sprite kit = kitSprites[rand.Next(0,kitSprites.Length)];

            //choose randomly between accept and deny
            int isConflict = rand.Next(0, 10);
            int randomPlan;
            Friend tempFriend;

            //if we've chosen a plan with no conflicts and that plan exists,
            //OR there are no more plans with conflicts
            //in this case the plan should be accepted
            if ((isConflict < 5 && acceptList.Count > 0) || (denyList.Count() == 0)) {
                //choose from accept list
                randomPlan = rand.Next(0, acceptList.Count);
                string planKey = acceptList.ElementAt(randomPlan).Key;
                int date = acceptList[planKey][0];
                int time = acceptList[planKey][1];
                int conflict = acceptList[planKey][2];
                //make a new friend with a random appearance who stores the plan
                tempFriend = new Friend(planKey, date, time, body, hair, kit, conflict);

                //remove plan from list
                acceptList.Remove(planKey);
            }
            //in this case the plan should be denied
            else {
                //choose from deny list
                randomPlan = rand.Next(0, denyList.Count);
                string planKey = denyList.ElementAt(randomPlan).Key;
                int date = denyList[planKey][0];
                int time = denyList[planKey][1];
                int conflict = denyList[planKey][2];
                //make a new friend with a random appearance who stores the plan
                tempFriend = new Friend(planKey, date, time, body, hair, kit, conflict);

                //remove plan from list
                denyList.Remove(planKey);
            }

            //add friend to list
            friendList.Add(tempFriend);
        }
        
        friendlist = friendList;
        loadFriendsIntoScene();

        //Print out new list
        /*
        Debug.Log("Friend list from FriendBuilder:");
        foreach (Friend pal in friendlist) {
            string plan = pal._plan;
            Debug.Log(plan);
        }
        */

        return friendlist;
    }

    public void loadFriendsIntoScene() {
        friendIndex = 0;
        setFriend(0);
    }
    
    public void setFriend(int index) {
        if(index >= 0) {
            friendBody.sprite = friendlist[index]._body;
            friendOutfit.sprite = friendlist[index]._kit;
            friendHair.sprite = friendlist[index]._hair;

            speechBox.text = friendlist[index].message;
            
            //display or not display accept/reject buttons
            if(friendlist[index].accepted) {
                acceptButton.SetActive(false);
                rejectButton.SetActive(false);
            }
            else {
                acceptButton.SetActive(true);
                rejectButton.SetActive(true);
            }
        }
        else {
            speechBox.text = "";
            friendBody.color = Color.clear;
            friendHair.color = Color.clear;
            friendOutfit.color = Color.clear;
            friendFace.color = Color.clear;
        }
    }

    public void friendBack() {
        if(friendlist.Count() > 0) {
            if(friendIndex > 0) {
                friendIndex--;
            }
            else {
                friendIndex = friendlist.Count()-1;
            }
        }
        else {
            friendIndex = -1;
        }
        
        setFriend(friendIndex);
    }

    public void friendForward() {
        if(friendlist.Count() > 0) {
            if(friendIndex < friendlist.Count()-1) {
                friendIndex++;
            }
            else {
                friendIndex = 0;
            }
        }
        else {
            friendIndex = -1;
        }
        
        setFriend(friendIndex);
    }

    public void acceptActivity() {
        if(friendlist.Count > 0) {
            //conflict code 0 means no confict- should be accepted
            if(friendlist[friendIndex]._conflictCode == 0) {
                //spawn the activity
                GameObject newDraggable = Instantiate(draggablePrefab, activityPanel);
                string plan = friendlist[friendIndex]._plan;
                //assign text to show activity
                newDraggable.GetComponentInChildren<TextMeshProUGUI>().text = plan;
                //name the instance that activity
                newDraggable.name = plan;

                //mark as accepted
                friendlist[friendIndex].accepted = true;
                //reload current friend prompt (accept/reject buttons disappear)
                setFriend(friendIndex);
            }
            //wrong answer
            else {
                wrongCount++;
            }
        }
    }

    public void rejectActivity() {
        if(friendlist.Count > 0) {
            //correct answer- the plan has a conflict
            if(friendlist[friendIndex]._conflictCode != 0) {
                
                removeFriend();
            }
            //wrong answer
            else {
                wrongCount++;
            }
        }
    }

    public void removeFriend() {
        if(friendlist.Count() > 0) {
            //remove the activity from list
            friendlist.Remove(friendlist[friendIndex]);
            //load the next activity in list
            friendForward();
        }
        else {
            friendIndex = -1;
        }
    }

    public class Friend
    {
        public Sprite _body;
        public Sprite _hair;
        public Sprite _kit;

        public string _plan;
        public int _date;
        public int _time;
        public int _conflictCode;

        public string message;
        public bool accepted = false;

        public Friend(string plan, int date, int time, Sprite body, Sprite hair, Sprite kit, int conflictCode) {
            _plan = plan;
            _date = date;
            _time = time;
            _conflictCode = conflictCode;

            _body = body;
            _hair = hair;
            _kit = kit;

            //generate message based on data
            List<string> greetingOptions = new List<string>{
                "¡Hola! ¿Qué tal si vamos ",
                "¿Qué te parece ",
                "Hola amigo. ¿Quieres ",
                "¿Tienes planes? Tengo ganas de "
            };

            //choose random greeting
            System.Random rand = new System.Random();
            int randNum = rand.Next(0, greetingOptions.Count);
            bool question = randNum < 3;
            string greeting = greetingOptions[randNum];

            string dayPhrase = "";
            switch (_date) {
            case 0:
                dayPhrase = "lunes";
                break;
            case 1:
                dayPhrase = "martes";
                break;
            case 2:
                dayPhrase = "el lunes";
                break;
            case 3:
                dayPhrase = "jueves";
                break;
            case 4:
                dayPhrase = "viernes";
                break;
            case 5:
                dayPhrase = "sabado";
                break;
            case 6:
                dayPhrase = "domingo";
                break;
            }

            string timePhrase = "";
            switch(_time) {
                case 0:
                    timePhrase = " a las ocho de la mañana";
                    break;
                case 1:
                    timePhrase = " a las nueve de la mañana";
                    break;
                case 2:
                    timePhrase = " a las diez de la mañana";
                    break;
                case 3:
                    timePhrase = " a las once de la mañana";
                    break;
                case 4:
                    timePhrase = " al mediodía";
                    break;
                case 5:
                    timePhrase = " a la una de la tarde";
                    break;
                case 6:
                    timePhrase = " a las dos de la tarde";
                    break;
                case 7:
                    timePhrase = " a las treis de la tarde";
                    break;
                case 8:
                    timePhrase = " a las cuatro de la tarde";
                    break;
                case 9:
                    timePhrase = " a las cinco de la tarde";
                    break;
                case 10:
                    timePhrase = " a las seis de la noche";
                    break;
                case 11:
                    timePhrase = " a las siete de la noche";
                    break;
                case 12:
                    timePhrase = " a las ocho de la noche";
                    break;
            }

            message = greeting + _plan + " el " + dayPhrase + timePhrase;

            if(question) {
                message += "?";
            }
            else {
                message += ".";
            }
            
        }
    };

}