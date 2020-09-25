using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prefabs Manager", menuName = "Prefabs Manager/Create Prefabs Bank")]
public class PrefabsBank : ScriptableObject
{
   public List<PrefabInstantiationItem> prefabs;

}
