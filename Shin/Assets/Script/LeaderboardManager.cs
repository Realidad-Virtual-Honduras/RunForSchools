using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;

    private List<ScoreEntry> scoreEntrys = new List<ScoreEntry>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void AddScore(string playerName, float distance)
    {
        ScoreEntry newEntry = new ScoreEntry(playerName, distance);
        scoreEntrys.Add(newEntry);
        scoreEntrys.Sort((a,b) => b.distance.CompareTo(a.distance)); // Ordena de mayor a menor

    }
    //Obtener la lista de lideres
    public List<ScoreEntry> GetScore()
    {
      return scoreEntrys; 
    }   

    [System.Serializable]
    public class ScoreEntry
    {
      public string playerName; 
      public float distance;

      public ScoreEntry (string playerName, float distance)
        {
            this.playerName = playerName;
            this.distance = distance;
        }
    }

}
