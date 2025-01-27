using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActivateManager : MonoBehaviour
{
    private static ItemActivateManager instance;

    private List<IEnumerator> coroutines = new List<IEnumerator>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public static void StartGlobalCoroutine(IEnumerator coroutine)
    {
        instance.coroutines.Add(coroutine);
        instance.StartCoroutine(coroutine);
    }

    public static void StopGlobalCoroutine(IEnumerator coroutine)
    {
        instance.StopCoroutine(coroutine);
        instance.coroutines.Remove(coroutine);
    }
}
