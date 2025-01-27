using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using MEC;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Persistent Objects")]
    [SerializeField] private GameObject[] persistentObjects = null;

    [Header("Level Data")]
    public int currentLevelNumber = 0;

    [Header("Timers")]
    [SerializeField] private float timerUi;
    public float timerTransition;

    [HideInInspector] public bool isInGame;
    [HideInInspector] public bool firstTimePlay;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
            instance = this;

        foreach (GameObject obj in persistentObjects)
            Object.DontDestroyOnLoad(obj);

        Timing.RunCoroutine(StartMenu());
    }

    IEnumerator<float> StartMenu()
    {
        yield return Timing.WaitForSeconds(timerUi);
        StartGame();
    }

    public void StartGame()
    {
        isInGame = false;

        TransitionManager.instance.TransitionScene(0, timerTransition, UiManager.instance.mainMenuUiId);
        TransitionManager.instance.TransitionMusic(0, timerTransition);

        UiManager.instance.txtVersion.text = "Version: " + Application.version;   
    }

    public void LoadLevel()
    {
        isInGame = true;
        pause = false;

        currentLevelNumber = Random.Range(0, TransitionManager.instance.levels.Length);

        if(!firstTimePlay)
        {
            TransitionManager.instance.TransitionScene(currentLevelNumber, timerTransition, UiManager.instance.inGameUiId);
            TransitionManager.instance.TransitionMusic(currentLevelNumber, timerTransition);
        }
    }

    public void ResetLevel()
    {
        pause = false;
        TransitionManager.instance.TransitionScene(currentLevelNumber, timerTransition, UiManager.instance.inGameUiId);
        TransitionManager.instance.TransitionMusic(currentLevelNumber, timerTransition);

    }

    bool pause = false;
    public void TogglePause()
    {
        pause = !pause;

        if(pause)
        {
            Timing.RunCoroutine(ChangeToUiPause(UiManager.instance.pauseUiId));
        }
        else
        {
            Timing.RunCoroutine(ChangeToUiPause(UiManager.instance.inGameUiId));
        }
    }

    private IEnumerator<float> ChangeToUiPause(int idx)
    {
        UiMediator.instance.GoToUi(idx);

        yield return Timing.WaitForSeconds(0.1f);

        LevelManager.instance.PauseGame(pause);
    }

    public void GoToleaderboard()
    {
        UiMediator.instance.GoToUi(UiManager.instance.leaderboardUiId);
    }

    public void GoToMainMenu()
    {
        UiMediator.instance.GoToUi(UiManager.instance.mainMenuUiId);
    }

    public void GoToInstructions()
    {
        UiMediator.instance.GoToUi(UiManager.instance.instructionUiId);
    }
}
