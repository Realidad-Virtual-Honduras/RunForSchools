using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [Header("Main Menu")]
    public TextMeshProUGUI txtVersion;

    [Header("In Game Ui")]
    [SerializeField] private TextMeshProUGUI txtEnergy;
    [SerializeField] private string energyLanguage;
    [SerializeField] private Slider energySlider;
    [Space]
    [SerializeField] private TextMeshProUGUI txtDistance;
    [SerializeField] private string distanceLanguage;

    [Header("Leader Board")]
    public List<TextMeshProUGUI> rankeds;
    public List<TextMeshProUGUI> names;
    public List<TextMeshProUGUI> distances;
    public List<TextMeshProUGUI> times;
    [Space]
    public TMP_InputField ifUserName;
    [SerializeField] private TextMeshProUGUI txtTotalDistance;

    [Header("Ui IDs")]
    private const string MAINMENU_UIID = "Menu";
    private const string INSTRUCTION_UIID = "Instructions";
    private const string INGAME_UIID = "InGame";
    private const string PAUSE_UIID = "Pause";
    private const string GAMEOVER_UIID = "GameOver";
    private const string LEADERBOARD_UIID = "LeaderBoard";
    private const string TRANSITION_UIID = "Transition";
    [Space]
    public int mainMenuUiId;
    public int instructionUiId;
    public int inGameUiId;
    public int pauseUiId;
    public int gameOverUiId;
    public int leaderboardUiId;
    public int transitionUiId;

    [SerializeField]private UiMediator mMediator;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        mMediator = FindObjectOfType<UiMediator>();

        for (int i = 0; i < mMediator.Medators.Length; i++)
        {            
            if (mMediator.Medators[i].name == MAINMENU_UIID)
            {
                mainMenuUiId = i;
            }

            if (mMediator.Medators[i].name == INSTRUCTION_UIID)
            {
                instructionUiId = i;
            }

            if (mMediator.Medators[i].name == INGAME_UIID)
            {
                inGameUiId = i;
            }

            if (mMediator.Medators[i].name == PAUSE_UIID)
            {
                pauseUiId = i;
            }

            if (mMediator.Medators[i].name == GAMEOVER_UIID)
            {
                gameOverUiId = i;
            }

            if (mMediator.Medators[i].name == LEADERBOARD_UIID)
            {
                leaderboardUiId = i;
            }

            if (mMediator.Medators[i].name == TRANSITION_UIID)
            {
                transitionUiId = i;
            }
        }
    }

    public void SetEnergyText(string energyText)
    {
        txtEnergy.text = energyLanguage + ": " + energyText + "%";
    }

    public void SetEnergy(float energy)
    {
        energySlider.value = energy;
    }

    public void SetDistanceText(string distanceText)
    {
        txtDistance.text = distanceLanguage + ": " + distanceText + " KM";
        txtTotalDistance.text = distanceLanguage + ": " + distanceText + " KM";
    }
}
