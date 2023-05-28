using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    [SerializeField] private TextAsset JSON_VERTEBRATE_DATA;

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


    [SerializeField] public VertebrateDataList Vertebrate_Data_List = new VertebrateDataList();
    private void Start()
    {
        // Load all needed data
        Vertebrate_Data_List = JsonUtility.FromJson<VertebrateDataList>(JSON_VERTEBRATE_DATA.text);
    }
}
