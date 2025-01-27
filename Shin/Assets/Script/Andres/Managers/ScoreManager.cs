using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public UnityEvent<string, float> submitScoreEvent;

    public void SubmitScore()
    {
        submitScoreEvent.Invoke(UiManager.instance.ifUserName.text, LevelManager.instance.distance.position.x);
    }
}
