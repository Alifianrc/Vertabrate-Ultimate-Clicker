using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    // JSON file
    [SerializeField] private TextAsset JSON_VERTEBRATE_DATA;

    // Class that will be converted to
    [System.Serializable]
    public class VertebrateData
    {
        public string Name;
        public string LatinName;
        public string Description;
    }
    [System.Serializable]
    public class VertebrateDataList
    {
        public VertebrateData[] List;
    }

    // Variable of that class
    [SerializeField] public VertebrateDataList Vertebrate_Data_List = new VertebrateDataList();

    // Singleton
    public static JSONReader Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Event
    public static Action OnJDataLoaded;

    private void Start()
    {
        // Load all needed data
        Vertebrate_Data_List = JsonUtility.FromJson<VertebrateDataList>(JSON_VERTEBRATE_DATA.text);

        // Invoke action
        OnJDataLoaded?.Invoke();
    }

    // Get Data
    public VertebrateData GetVertebrateData(string name)
    {
        foreach(var vertebrate in Vertebrate_Data_List.List)
        {
            if(vertebrate.Name == name)
            {
                return vertebrate;
            }
        }

        return null;
    }
}
