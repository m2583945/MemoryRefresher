using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class notebook : MonoBehaviour
{
    public TMP_InputField[] noteInputs = new TMP_InputField[]{};
    string[] notes = new string[] { "Note1", "Note2", "Note3", "Note4", "Note5" };
    string saveFile;


    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
        saveFile = Application.persistentDataPath + "/NotebookData.json";
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

    public void readFile()
    {
        string fileContents = File.ReadAllText(saveFile);
        print(fileContents);
        SaveData.NotebookNotes test = new SaveData.NotebookNotes();
        /*print(test.firstTime + "kdddd");
        if (test.firstTime == false)
        {   
            test = JsonUtility.FromJson<SaveData.NotebookNotes>(fileContents);

            string[] notebookdata = { test.note1, test.note2, test.note2, test.note3, test.note4, test.note5 };

            for (int x = 0; x < noteInputs.Length; x++)
            {
                noteInputs[x].text = notebookdata[x];
            }
        }*/
        test = JsonUtility.FromJson<SaveData.NotebookNotes>(fileContents);

        string[] notebookdata = { test.note1, test.note2, test.note3, test.note4, test.note5 };

        for (int x = 0; x < noteInputs.Length; x++)
        {
            noteInputs[x].text = notebookdata[x];
        }
    }
}
