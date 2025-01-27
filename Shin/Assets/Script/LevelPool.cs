using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPool : MonoBehaviour
{
    private static LevelPool instance;

    public static LevelPool Instance {  get => instance; }

    //****************************************************************

    [SerializeField] private GameObject[] levelParts;
    [SerializeField] private List<GameObject> levelPartsList;
    public List<GameObject> LevelPartsList { get => levelPartsList; }
    
    private int poolSize;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        poolSize = levelParts.Length;
    }

    void Start()
    {
        AddLevelToPool(poolSize);
    }

    private void AddLevelToPool(int size)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject level = Instantiate(levelParts[i]);
            level.SetActive(false);
            levelPartsList.Add(level);
            level.transform.parent = transform;
        }
    }

    public GameObject RequestLevel()
    {
        for(int i = 0;i < levelPartsList.Count; i++)
        {
            int randomNumber = Random.Range(0, LevelPartsList.Count);
            if (!levelPartsList[randomNumber].activeSelf)
            {
                levelPartsList[randomNumber].SetActive(true);
                return levelPartsList[randomNumber];
            }
        }
        AddLevelToPool(1);
        levelPartsList[levelPartsList.Count - 1].SetActive(false);
        return levelPartsList[levelPartsList.Count - 1];
    }
}
