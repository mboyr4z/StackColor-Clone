using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectDropdown;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    public Vector3 startingPoint;

    public ColorCategory startingCategory;

    public GameObject prefab;

    [ScriptableObjectDropdown] public LevelTheme levelTheme;

}
