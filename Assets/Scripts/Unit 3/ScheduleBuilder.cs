using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro; 

public class ScheduleBuilder : MonoBehaviour
{
    
    public GameObject holderPrefab;
    public GameObject draggablePrefab;
    public Transform schedulePanel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        ScheduleBuilderBackend.Schedule schedule = new ScheduleBuilderBackend.Schedule();
        /* PRINT SCHEDULE
        Debug.Log("Season: " + schedule._season);
        foreach (List<string> list in schedule._schedule) {
            string currentTimeActivity = "";
            foreach (string activity in list) {
                currentTimeActivity += activity + ", ";
            }
            Debug.Log(currentTimeActivity);
        }
        */
        //loadScheduleIntoScene(schedule._schedule);

    }
    
    /*
    public void loadScheduleIntoScene(List<List<string>> schedule) {
        foreach (List<string> list in schedule) {
            foreach (string activity in list) {
                GameObject newActivityHolder = Instantiate(holderPrefab, schedulePanel);
            }
        }
    }
    */


}


namespace ScheduleBuilderBackend {
    public class Schedule {
        public string _season = "verano";

        public List<List<string>> _schedule = new List<List<string>>();
        public Dictionary<string, List<int>>  _acceptInvitation = new Dictionary<string, List<int>>();
        public Dictionary<string, List<int>> _denyInvitation = new Dictionary<string, List<int>>();
        public List<string> _listOfClasses = new List<string> {
            "arte", "español", "filosofía", "historia", "literatura", "música",
            "periodismo", "biología", "computación", "física", "matemática", 
            "química", "contabilidad", "economía", "mercadeo", "psicología", "sociología"
        };
        public List<string> _listOfSummerSpringThings = new List<string> {
            "nadar afuera", "ir al picnic", "hacer caminata", "pasear en bicicleta",
            "ir a la playa"
        };
        public List<string> _listOfWinterThings = new List<string> {
            "esquiar", "patinar sobre hielo"
        };
        public List<string> _listOfNeutralActivitiesWithFriends = new List<string> {
            "cocinar comedia", "correr", "escuchar música nueva", "estudiar", "jugar al videojuego",
            "jugar al tenis", "jugar al fútbol", "ir al gimnasio", "ir al cine",
            "ir de compras", "practicar yoga", "tomar algo", "bailar", "pintar",
            "jugar al golf", "jugar al vóleibol", "jugar al básquetbol"
        };
        public List<string> _listOfSoloActivities = new List<string> {
            "ayudar mi mama", "hacer ejercicio", "lavar mi ropa", "limpiar mi casa", 
            "hacer una videollamada con mi abuela", "hablar mi mama"
        };


        public Schedule() {
            _season = getSeason();
            setSchedule();
        }

        private string getSeason() {
            System.Random rand = new System.Random();
            int randomNumberSeason = rand.Next(0,4);
            if (randomNumberSeason == 0) {
                return "primavera";
            }
            else if (randomNumberSeason == 1) {
                return "verano";
            }
            else if (randomNumberSeason == 2) {
                return "otoño";
            }
            else {
                return "invierno";
            }
        }

        private void setSchedule() {
            //makes the empty schedule
            //this is for the number of hours in a day(starts at 8am)
            //this creates a 2D list that is _schedule[timeOfDay][DayOfTheWeek]
            for (int i = 0; i < 13; i ++) {
                List<string> newList = new List<string>();
                //this is for the days of the week, monday-sunday
                for (int j = 0; j < 7; j++) {
                    newList.Add("");
                }
                _schedule.Add(newList);
            }

            //add classes and work
            System.Random rand = new System.Random();
            //---CLASSES---
            //ranomizes the number of classes
            int randNum = rand.Next(3,6);
            int randomDay;
            int randomTime;
            int randomLength;
            //places each class in the schedule where open
            for (int i = 0; i < randNum; i++) {
                //determines if the classes are MWF, TR, or a long nightclass
                int classDays = rand.Next(0,3);
                //MWF
                if (classDays == 0) {
                    bool foundTime = false;
                    while(!foundTime) {
                        //0-10 becuase we want regular classes to stop around 5pm
                        randomTime = rand.Next(0,10);
                        //if schedule is free at the random time of day on the MWF dates
                        //reminder: Monday=0, Tuesday=1, etc... and _schedule[timeOfDay][DayOfTheWeek]
                        if (checkIfFree(0, randomTime, 1) && checkIfFree(2, randomTime, 1) && checkIfFree(4, randomTime, 1)) {
                            int randomClass = rand.Next(0, _listOfClasses.Count);
                            _schedule[randomTime][0] = _listOfClasses[randomClass];
                            _schedule[randomTime][2] = _listOfClasses[randomClass];
                            _schedule[randomTime][4] = _listOfClasses[randomClass];
                            //so we don't reuse this class
                            _listOfClasses.RemoveAt(randomClass);
                            foundTime = !foundTime;
                        }
                    }

                }
                //TR
                else if (classDays == 1) {
                    bool foundTime = false;
                    while(!foundTime) {
                        //0-10 becuase we want regular classes to stop around 5pm
                        randomTime = rand.Next(0,10);
                        //if schedule is free at the random time of day on the TR dates
                        //reminder: Monday=0, Tuesday=1, etc...
                        if (checkIfFree(1, randomTime, 1) && checkIfFree(3, randomTime, 1)) {
                            int randomClass = rand.Next(0, _listOfClasses.Count);
                            _schedule[randomTime][1] = _listOfClasses[randomClass];
                            _schedule[randomTime][3] = _listOfClasses[randomClass];
                            //so we don't reuse this class
                            _listOfClasses.RemoveAt(randomClass);
                            foundTime = !foundTime;
                        }
                    }
                }
                //long nightclass
                else {
                    bool foundTime = false;
                    while(!foundTime) {
                        //night classses are long and only happen once a week on a random weekday
                        randomDay = rand.Next(0,5);
                        //class can be 2 or 3 hours long
                        randomLength = rand.Next(2,4);
                        //must start at 6pm if 3 hour class, can start at 7pm if 2 hour class
                        randomTime = rand.Next(10,13-randomLength);
                        
                        //if schedule is free at the random time of day on the night dates
                        //reminder: Monday=0, Tuesday=1, etc...
                        if (checkIfFree(randomDay, randomTime, randomLength)) {
                            int randomClass = rand.Next(0, _listOfClasses.Count);
                            //make sure to put it on the schedule for the full length of time
                            for (int j = 0; j < randomLength; j++) {
                                _schedule[randomTime + j][randomDay] = _listOfClasses[randomClass];
                            }
                            
                            //so we don't reuse this class
                            _listOfClasses.RemoveAt(randomClass);
                            foundTime = !foundTime;
                        }
                    }
                }
            }

            //---WORK---
            //randomizes work hours
            //randomizes the number of days this person works
            randNum = rand.Next(0,7);
            List<int> daysWorking = new List<int>();
            for (int i = 0; i < randNum; i++) {
                //for each day of work it randomizes which day, when, and for how long
                //we want to make sure its different days, so not all shifts fall on the same day
                int workDay = rand.Next(0,7);
                while (daysWorking.Contains(workDay)) {
                    workDay = rand.Next(0,7);
                }
                daysWorking.Add(workDay);
                //makes to work part time, 1-4 hours per shift
                int workLength = rand.Next(1,5);
                int workStartTime = rand.Next(0,13-workLength);
                

                //once there is a clear space, it moves on, otherwise it resets the potential work schedule
                while (!checkIfFree(workDay, workStartTime, workLength)) {
                    workDay = rand.Next(0,7);
                    while (daysWorking.Contains(workDay)) {
                        workDay = rand.Next(0,7);
                    }
                    workLength = rand.Next(1,5);
                    workStartTime = rand.Next(0,13-workLength);
                }
                //once it finds a space to work. _schedule = [TimeOfDay][DayOfWeek]
                for (int j = 0; j < workLength; j++) {
                    _schedule[workStartTime + j][workDay] = "trabajar";
                }
                
            }

            //---RANDOM ACTIVITES---
            //just a preset 2 random solo activities + a random planned study time
            //solo activites
            for (int i = 0; i < 2; i++) {
                randomDay = rand.Next(0,7);
                //random length of 1-3 hours
                randomLength = rand.Next(1,4);
                randomTime = rand.Next(0,13-randomLength);
                while(!checkIfFree(randomDay,randomTime, randomLength)) {
                    randomDay = rand.Next(0,7);
                    //random length of 1-3 hours
                    randomLength = rand.Next(1,4);
                    randomTime = rand.Next(0,13-randomLength);
                }
                int randomActivity = rand.Next(0, _listOfSoloActivities.Count);
                _schedule[randomTime][randomDay] = _listOfSoloActivities[randomActivity];
            }
            //random study time
            randomDay = rand.Next(0,7);
            //random length of 1-3 hours
            randomLength = rand.Next(1,4);
            randomTime = rand.Next(0,13 - randomLength);
            while(!checkIfFree(randomDay,randomTime, randomLength)) {
                randomDay = rand.Next(0,7);
                //random length of 1-3 hours
                randomLength = rand.Next(1,4);
                randomTime = rand.Next(0,13- randomLength);
                
            }
            for (int j = 0; j < randomLength; j++) {
                _schedule[randomTime + j][randomDay] = "estudiar";
            }
            

            //---FRIENDS ASKING---
            //randomizes friends asking
            //  first randomizes conflicting scedules
            //can have up to 3 conflicting schedules(must say no to)
            randNum = rand.Next(0,3);
            for (int i = 0; i < randNum; i++) {
                int isSeasonal = rand.Next(0,10);
                if (isSeasonal > 8 && _season != "otoño")  { // 20% chance of it being a seasonal confliction
                    randomDay = rand.Next(0,7);
                    randomTime = rand.Next(0,13);
                    //makes sure that there is already an event in the schedule at that time
                    while(checkIfFree(randomDay, randomTime, 1)) {
                        randomDay = rand.Next(0,7);
                        randomTime = rand.Next(0,13);
                    }
                    if (_season == "invierno") {
                        int randomActivity = rand.Next(0, _listOfSummerSpringThings.Count);
                        List<int> tempList = new List<int> {
                            randomDay, randomTime, 1
                        };
                        _denyInvitation[_listOfSummerSpringThings[randomActivity]] = tempList;
                    }
                    else {
                        int randomActivity = rand.Next(0, _listOfWinterThings.Count);
                        List<int> tempList = new List<int> {
                            randomDay, randomTime, 1
                        };
                        _denyInvitation[_listOfWinterThings[randomActivity]] = tempList;
                    }
                    
                }
                else { // 80% chance it is conflicting with already existing schedule things
                    randomDay = rand.Next(0,7);
                    randomTime = rand.Next(0,13);
                    //makes sure that there is already an event in the schedule at that time
                    while(checkIfFree(randomDay, randomTime, 1)) {
                        randomDay = rand.Next(0,7);
                        randomTime = rand.Next(0,13);
                    }
                    int randomActivity = rand.Next(0, _listOfNeutralActivitiesWithFriends.Count);
                    List<int> tempList = new List<int> {
                        randomDay, randomTime, 1
                    };
                    _denyInvitation[_listOfNeutralActivitiesWithFriends[randomActivity]] = tempList;
                }
            }
            //  then randomizes correct scheduling
            //can have 2 or 3 correct schedules(must say yes to)
            randNum = rand.Next(2,4); 
            for (int i = 0; i < randNum; i++) {
                randomDay = rand.Next(0,7);
                randomTime = rand.Next(0,13);
                while(!checkIfFree(randomDay,randomTime, 1)) {
                    randomDay = rand.Next(0,7);
                    randomTime = rand.Next(0,13);
                }
                int randomActivity = rand.Next(0, _listOfNeutralActivitiesWithFriends.Count);
                _schedule[randomTime][randomDay] = _listOfNeutralActivitiesWithFriends[randomActivity];
                List<int> tempList = new List<int> {
                    randomDay, randomTime, 1
                };
                _acceptInvitation[_listOfNeutralActivitiesWithFriends[randomActivity]] = tempList;

            }
            //  then friends try to set up a time to have food with you..could be during an open time, could not be
            //breakfast =0, lunch = 1, dinner = 2
            randNum = rand.Next(0,3);
            randomDay = rand.Next(0,7);
            //breakfast
            if (randNum == 0) {
                //makes sure it is breakfast time of day
                randomTime = rand.Next(0,3);
                if (checkIfFree(randomDay, randomTime, 1)) {
                    _schedule[randomTime][randomDay] = "desayunar";
                    List<int> tempList = new List<int> {
                        randomDay, randomTime, 1
                    };
                    _acceptInvitation["desayunar"] = tempList;
                }
                else {
                    List<int> tempList = new List<int> {
                        randomDay, randomTime, 1
                    };
                    _denyInvitation["desayunar"] = tempList;
                }
            }
            //lunch
            else if (randNum == 1) {
                //makes sure it is in lunch time of day
                randomTime = rand.Next(3,9);
                if (checkIfFree(randomDay, randomTime, 1)) {
                    _schedule[randomTime][randomDay] = "almorzar";
                    List<int> tempList = new List<int> {
                        randomDay, randomTime, 1
                    };
                    _acceptInvitation["almorzar"] = tempList;
                }
                else {
                    List<int> tempList = new List<int> {
                        randomDay, randomTime, 1
                    };
                    _denyInvitation["almorzar"] = tempList;
                }
            }
            //dinner
            else if (randNum == 2) {
                //makes sure it is in dinner time of day
                randomTime = rand.Next(9,13);
                if (checkIfFree(randomDay, randomTime, 1)) {
                    _schedule[randomTime][randomDay] = "cenar";
                    List<int> tempList = new List<int> {
                        randomDay, randomTime, 1
                    };
                    _acceptInvitation["cenar"] = tempList;
                }
                else {
                    List<int> tempList = new List<int> {
                        randomDay, randomTime, 1
                    };
                    _denyInvitation["cenar"] = tempList;
                }
            }
        }

        //returns if there is a free space within the given time frame
        private bool checkIfFree(int day, int time, int lengthOfTime) {
            //if the length of time exceeds the day, then obviously no
            if ((time + lengthOfTime) > 12) {
                return false;
            }
            //_schedule[timeOfDay][DayOfTheWeek]
            for (int i = 0; i < lengthOfTime; i++) {
                if (_schedule[time + i][day] != "") {
                    return false;
                }
            }
            return true;
        }
    }
}
