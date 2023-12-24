using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new WavePack", menuName = "Wave/WavePack")]
public class WavePack : ScriptableObject
{
    public WaveSO[] waves;
}
