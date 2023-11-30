using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelUpSystem : ScriptableObject
{
    public abstract int ExpRequired(int level);
}
