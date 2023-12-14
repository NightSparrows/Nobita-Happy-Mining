using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LinearLevelUpSystem", menuName = "Level Up System/Linear")]
public class LinearLevelUpSystem : LevelUpSystem
{
    public int baseExp = 1;
    public int increment = 1;

    public override int ExpRequired(int level)
    {
        return baseExp + level * increment;
    }
}
