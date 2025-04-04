using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonNoPersistent<T> : MonoBehaviour
    where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get {
            if (_instance == null)
            {
                var objs = FindObjectOfType(typeof(T)) as T[];
                if (objs.Length > 0)
                {
                    _instance = objs[0];
                }
                if (objs.Length > 1)
                {

                }
                if (_instance != null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
}
