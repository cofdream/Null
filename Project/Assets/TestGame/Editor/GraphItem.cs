using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class GraphItem : ScriptableObject
{
    public List<ModeLinkData> modeLinks = new List<ModeLinkData>();
    public List<ModeData> modeData = new List<ModeData>();
}

[CreateAssetMenu(menuName = "Dialogan/Dialogue")]
public class Dialogue : GraphItem
{

}


[System.Serializable]
public class ModeLinkData
{
    public string baseModeGuid;
    public string outputPortName;
    public string inputPortName;
    public string targetNodeGuid;
}

[System.Serializable]
public class ModeData
{
    public string modeGuid;
    public Vector3 position;
    public string nodeType;
    public string additionalDataJSON;
}