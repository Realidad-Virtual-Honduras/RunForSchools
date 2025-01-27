using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    public TMP_Text leaderboardText;
    public Button backButton;
    

    void Start()
    {
        UpdateLeaderboard();
        backButton.onClick.AddListener(GoBackToGame);    
    }

    private void UpdateLeaderboard()
    {
        var scores = LeaderboardManager.Instance.GetScore();
        leaderboardText.text = "Tabla de lideres:\n";
        foreach (var score in scores)
        {
            leaderboardText.text += $"{score.playerName}: {score.distance / 100f:F2} km\n";
        }
    }

    private void GoBackToGame()
    {
        SceneManager.LoadScene("Game");
    }

}
