using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro; 

public class ScheduleBuilder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScheduleBuilderBackend.Schedule schedule = new ScheduleBuilderBackend.Schedule();
        Debug.Log("Season: " + schedule._season);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

namespace ScheduleBuilderBackend {
    public class Schedule {
        public string _season = "summer";

        public List<List<string>> _schedule = new List<List<string>>();
        public List<string> _listOfClasses = new List<string> {
            "arte", "español", "filosofía", "historia", "literatura", "música",
            "periodismo", "biología", "computación", "física", "matemática", 
            "química", "contabilidad", "economía", "mercadeo", "psicología", "sociología"
        };

        public Schedule() {
            _season = getSeason();
            setSchedule();
        }

        private string getSeason() {
            System.Random rand = new System.Random();
            int randomNumberSeason = rand.Next(0,4);
            if (randomNumberSeason == 0) {
                return "spring";
            }
            else if (randomNumberSeason == 1) {
                return "summer";
            }
            else if (randomNumberSeason == 2) {
                return "fall";
            }
            else {
                return "winter";
            }
        }

        private void setSchedule() {
            //makes the empty schedule
            //this is for the number of hours in a day(starts at 8am)
            for (int i = 0; i < 12; i ++) {
                List<string> newList = new List<string>();
                //this is for the days of the week, monday-sunday
                for (int j = 0; j < 7; j++) {
                    newList.Add("");
                }
                _schedule.Add(newList);
            }

            //add classes and work
            System.Random rand = new System.Random();
            //ranomizes the number of classes
            int randNum = rand.Next(3,6);
            //places each class in the schedule where open
            for (int i = 0; i < randNum; i++) {
                //determines if the classes are MWF, TR, or a long nightclass
                int classDays = rand.Next(0,3);
                //MWF
                if (classDays == 0) {
                    
                }
                //TR
                else if (classDays == 1) {

                }
                //long nightclass
                else {

                }
            }
            //randomizes work hours
        }
    }
}
