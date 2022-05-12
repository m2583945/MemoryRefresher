using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Globalization;
using UnityEngine.UI;
using TMPro;

public class CheckTime : MonoBehaviour
{
    // Start is called before the first frame update
    int numHours;
    int numDays;
    int numMonths;
    int numMinutes;
    int numYears;
    string saveFile;
    TextMeshProUGUI timeLastPlayedText;


    void Start()
    {
        
    }
    void Awake()
    {
        saveFile = Application.persistentDataPath + "/PlayerSave.json";
        if (File.Exists(saveFile))
        {
            readFile();
            // File exists!
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void loadGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void loadStart()
    {
        print("loadedstart");
        SceneManager.LoadScene("SampleScene");
    }
    public void readFile()
    {
        timeLastPlayedText = GameObject.FindWithTag("TimeLastPlayed").GetComponent<TextMeshProUGUI>();
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            print("file exists");
            
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);
            print(fileContents);
            // Work with JSON
            //make a save data instance in order to access the TimeLastSaved class and make an instance of that
            SaveData.TimeLastSaved test = new SaveData.TimeLastSaved();


            //print(fileContents);//testing
            test = JsonUtility.FromJson<SaveData.TimeLastSaved>(fileContents);
            //check if this is the first time the user is playing
            //(otherwise the previous save data can't exist),
            //it'll be created when they save for the first time
            if (test.firstTime == false)
            {

                //load the file contents into the TimeLastSaved class 
                
                //make a new DateTime object using arguments from the TimeLastSaved instance
                DateTime lastSave = new DateTime(test.saveYear, test.saveMonth, test.saveDay, test.saveHour, test.saveMinute, test.saveSecond);
                DateTime now = DateTime.Now;

                TimeSpan timePassed = DateTime.Now.Subtract(lastSave); // for some reason subtracting two DateTime objects from each other makes a TimeSpan object

                //print("time passed = " + timePassed.ToString("%h") + " hours" + timePassed.ToString("%m") +
                //" minutes" + timePassed.ToString("%s") + " seconds");//testing
                /*print("MC last save day is " + lastSave.ToString("%d"));
                print("MC subtracting " + lastSave.ToString("%d") + " days " + lastSave.ToString("%h") + " hours "+ lastSave.ToString("%m")+ "minutes "
                    + lastSave.ToString("%s") + " seconds " + "from " +
                    now.ToString("%d") + " days " + now.ToString("%h") + " hours " +
                    now.ToString("%m") + " minutes " + now.ToString("%s") + "seconds");*/ //DEBUGGING STATEMENTS
                String timeRecordString = "";//prints the time you have been away on screen
                //print("MC" + timePassed.Days); //DEBUGGING
                if(test.saveDay == null)
                {

                }
                if(timePassed.Days >= 366)//check if it's been over a year
                {
                    timeRecordString = "Wow! Over a year since you last played!";
                }
                if (timePassed.Days >= 30)//check if it has been over 30 days
                {
                    timeRecordString = "It's been over a month since you last saved the game. That's a long time!";
                }
                else
                {
                    timeRecordString = "It has been " + timePassed.ToString("%d") + " days and " + //default message
                        timePassed.ToString("%h") + " hours since you last saved the game";
                    if (timePassed.Days < 1)
                    {
                        if (timePassed.Hours >= 1)//more than one hour, less than one day
                        {
                            timeRecordString = "It's been" + timePassed.ToString("%h") + " hours and " +
                            timePassed.ToString("%m") + " minutes since you last saved the game";
                        }
                        else //less than one hour has passed
                        {
                            timeRecordString = "That was quick! It's been " + timePassed.ToString("%m") + " minutes and " + timePassed.ToString("%s") +
                                " seconds since you last saved.";
                        }
                    }
                }
                //timeLastPlayedText.text = "It has been " + timePassed.ToString("%d") + " days " + timePassed.ToString("%h") + " hours " +
                 //   timePassed.ToString("%m") + " minutes " + timePassed.ToString("%s") + " seconds since you last saved the game"; //print on screen
                timeLastPlayedText.text = timeRecordString;
            }

        }
        else
        {
            print("no file yet");
            timeLastPlayedText.text = "You have never played.";
        }
       
    }

}
