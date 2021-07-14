using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class MapCase : ScriptableObject
{
    public List<string> Name;
    public List<int> WorldBiom;
    public List<int> WorldWidth;
    public List<string> DataTime;
}
