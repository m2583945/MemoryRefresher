using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveData : MonoBehaviour
{
    DateTime saveTime;

    // Start is called before the first frame update
    [SerializeField] private TimeLastSaved tls = new TimeLastSaved();
    [SerializeField] private NotebookNotes nn = new NotebookNotes();

    public void SaveIntoJson()
    {
        string lastSaveTime = JsonUtility.ToJson(tls);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerSave.json", lastSaveTime);
    }
    public void SaveIntoJson2()
    {
        string noteText = JsonUtility.ToJson(nn);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/NotebookData.json", noteText);
    }
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setSaveTime()
    {
        tls.firstTime = false;//the first time the user saves
        //DateTime.Now returns the time it is now
        saveTime = DateTime.Now;
        tls.dt = DateTime.Now;

        //save the year/month/time individually 
        tls.saveYear = tls.dt.Year;
        tls.saveMonth = tls.dt.Month;
        tls.saveDay = tls.dt.Day;
        tls.saveHour = tls.dt.Hour;
        tls.saveMinute = tls.dt.Minute;
        tls.saveSecond = tls.dt.Second;
        print("Saving game at" + tls.dt); //testing
        SaveIntoJson();
    }
    public void saveNotebookNotes()
    {
        //nn.firstTime = false;
        notebook nb = GameObject.Find("NOTEBOOK").GetComponent<notebook>();
        nn.note1 = nb.noteInputs[0].text;//keeps track of 5 different notebook components
        nn.note2 = nb.noteInputs[1].text;
        nn.note3 = nb.noteInputs[2].text;
        nn.note4 = nb.noteInputs[3].text;
        nn.note5 = nb.noteInputs[4].text;
        SaveIntoJson2();
        //notes.notes[noteNum] = noteText;
    }
    public void test()
    {
        print("Last saved at" + saveTime);//DEBUGGING
    }
    [System.Serializable]
    public class TimeLastSaved
    {
        public DateTime dt;
        public int saveYear;
        public int saveMonth;
        public int saveDay;
        public int saveHour;
        public int saveMinute;
        public int saveSecond;

        public bool firstTime = true;
    }
    [System.Serializable]
    public class NotebookNotes
    {
        public String note1;
        public String note2;
        public String note3;
        public String note4;
        public String note5;

    }
}
