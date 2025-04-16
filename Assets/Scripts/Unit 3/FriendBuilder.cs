using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class FriendBuilder : MonoBehaviour
{
    //friend plan stuff
    private List<FriendBuilder.Friend> friendlist;
    private int friendIndex = 0;
    public TextMeshProUGUI speechBox;
    public Image friendBody;
    public Image friendHair;
    public Image friendOutfit;
    public Image friendFace;

    //list of sprites for friend appearance
    public Sprite[] bodySprites;
    public Sprite[] hairSprites;
    public Sprite[] kitSprites;

    //creates a list of friends with corresponding plan messages
    //accept and reject plans are both in this list, in a random order
    //initiates the display stuff
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
            bool shouldAccept;
            Friend tempFriend;

            //if we've chosen a plan with no conflicts and that plan exists,
            //OR there are no more plans with conflicts
            //in this case the plan should be accepted
            if ((isConflict < 5 && acceptList.Count > 0) || (denyList.Count() == 0)) {
                //choose from accept list
                shouldAccept = true;
                randomPlan = rand.Next(0, acceptList.Count);
                string planKey = acceptList.ElementAt(randomPlan).Key;
                int date = acceptList[planKey][0];
                int time = acceptList[planKey][1];
                //make a new friend with a random appearance who stores the plan
                tempFriend = new Friend(planKey, date, time, shouldAccept, body, hair, kit);

                //remove plan from list
                acceptList.Remove(planKey);
            }
            //in this case the plan should be denied
            else {
                //choose from deny list
                shouldAccept = false;
                randomPlan = rand.Next(0, denyList.Count);
                string planKey = denyList.ElementAt(randomPlan).Key;
                int date = denyList[planKey][0];
                int time = denyList[planKey][1];
                //make a new friend with a random appearance who stores the plan
                tempFriend = new Friend(planKey, date, time, shouldAccept, body, hair, kit);

                //remove plan from list
                denyList.Remove(planKey);
            }

            //add friend to list
            friendList.Add(tempFriend);
        }
        
        friendlist = friendList;
        loadFriendsIntoScene();
        return friendList;
    }

    public void loadFriendsIntoScene() {
        friendIndex = 0;
        setFriend(0);
    }
    
    public void setFriend(int index) {
        if(index >= 0) {
            friendBody.sprite = friendlist[index].getBody();
            friendOutfit.sprite = friendlist[index].getKit();
            friendHair.sprite = friendlist[index].getHair();

            speechBox.text = friendlist[index].getMessage();
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
        //spawn the activity

    }

    public void rejectActivity() {
        if(friendlist.Count() > 0) {
            //remove the activity from list
            friendlist.Remove(friendlist[friendIndex]);
            //load the next activity in list
            friendForward();
        }
    }

    public class Friend
    {
        Sprite _body;
        Sprite _hair;
        Sprite _kit;

        string _plan;
        int _date;
        int _time;
        bool _accept;

        string message;

        public Friend(string plan, int date, int time, bool accept, Sprite body, Sprite hair, Sprite kit) {
            _plan = plan;
            _date = date;
            _time = time;
            _accept = accept;

            _body = body;
            _hair = hair;
            _kit = kit;

            //generate message based on data
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


            message = "¡Hola! ¿Quieres " + _plan + " el " + dayPhrase + timePhrase + "?";
        }

        public Sprite getBody() {
            return _body;
        }
        public Sprite getHair() {
            return _hair;
        }
        public Sprite getKit() {
            return _kit;
        }
        public string getMessage() {
            return message;
        }
        public int getDate() {
            return _date;
        }
        public int getTime() {
            return _time;
        }
        public bool shouldAccept() {
            return _accept;
        }
    };
}