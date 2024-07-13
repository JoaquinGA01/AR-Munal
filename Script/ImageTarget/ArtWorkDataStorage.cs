using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtWorkDataStorage", menuName = "ScriptableObjects/ArtWorkDataStorage", order = 1)]
public class ArtWorkDataStorage : ScriptableObject
{
    public List<ArtWorkData> ArtWorks;
}
