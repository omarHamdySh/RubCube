using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When you use this class you must put instantiateData class on the prefab and give it the name that you want to instantiate your game object with
/// if there are many prefabs in the list with the same name it pick the first one so please name your prefabs carfully 
/// </summary>
public class InstantiationManager : MonoBehaviour
{
    #region Singleton
    public static InstantiationManager instance { private set; get; }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public List<PrefabsBank> prefabsBanks;

    /// <summary>
    /// Seare and instantiate a prefab, while not having a reference to the prefabs bank that contains this prefab.
    /// </summary>
    /// <param name="objName"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject InstantiateGameobject(string objName, Transform parent = null) {

        foreach (var prefabsBank in prefabsBanks)
        {
            InstantiateGameobject(prefabsBank, objName,parent);
        }

        return null;
    }

    /// <summary>
    /// Instatiate prefab by its name in a specific prefab
    /// </summary>
    /// <param name="prefabsBank"></param>
    /// <param name="objName"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject InstantiateGameobject(PrefabsBank prefabsBank, string objName, Transform parent = null)
    {
        if (prefabsBank.prefabs.Count > 0)
        {
            PrefabInstantiationItem prefab = prefabsBank.prefabs.Find(x => x.name.Equals(objName));
            if (prefab)
            {
                GameObject go = Instantiate(prefab.gameObject, parent);
#if UNITY_EDITOR
                DestroyImmediate(go.GetComponent<PrefabInstantiationItem>());
#else
                Destroy(go.GetComponent<PrefabInstantiationItem>());
#endif

                return go;
            }
        }

        return null;
    }

    /// <summary>
    /// Instatiate prefab by its name in a specific prefab
    /// </summary>
    /// <param name="prefabsBank"></param>
    /// <param name="objName"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject InstantiateGameobject(PrefabsBank prefabsBank, string objName, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (prefabsBank.prefabs.Count > 0)
        {
            PrefabInstantiationItem prefab = prefabsBank.prefabs.Find(x => x.name.Equals(objName));
            if (prefab)
            {
                GameObject go = Instantiate(prefab.gameObject, position, rotation, parent);
#if UNITY_EDITOR
                DestroyImmediate(go.GetComponent<PrefabInstantiationItem>());
#else
                Destroy(go.GetComponent<PrefabInstantiationItem>());
#endif
                return go;
            }
        }

        return null;
    }
}
