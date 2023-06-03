using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static JSONReader;

public class JSONReader : Singleton<JSONReader>
{
    // JSON file
    [SerializeField] private TextAsset JSON_VERTEBRATE_DATA;
    [SerializeField] private TextAsset JSON_ACHIEVEMENT_DATA;


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

    [System.Serializable]
    public class AchevementData
    {
        public string Name;
        public AchievementType Type;
        public int Threshold;
        public int Reward;
        public AchievementState State; // This value need to be override by Player Save Data
    }
    [System.Serializable]
    public class AchievementDataList
    {
        public AchevementData[] List;
    }

    // Variable of that class
    [SerializeField] public VertebrateDataList Vertebrate_Data_List = new VertebrateDataList();
    [SerializeField] public AchievementDataList Achievement_Data_List = new AchievementDataList();

    // Other list
    [SerializeField] public Sprite[] VertebrateImage;

    // Event
    public static Action OnJDataLoaded;

    private void Start()
    {
        // Load all needed data
        Vertebrate_Data_List = JsonUtility.FromJson<VertebrateDataList>(JSON_VERTEBRATE_DATA.text);
        Achievement_Data_List = JsonUtility.FromJson<AchievementDataList>(JSON_ACHIEVEMENT_DATA.text);

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

    public Sprite GetVertebrateImage(string name)
    {
        foreach(var image in VertebrateImage)
        {
            if(name == image.name)
            {
                return image;
            }
        }
        return null;
    }
}
