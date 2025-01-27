using System.Collections;
using Dan.Main;
using Dan.Models;
using Dan.Demo;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class LeaderBoardManager : MonoBehaviour
{
    private TransitionManager transitionManager;
    public static LeaderBoardManager instance;

    public string[] badWords;

    [Header("Leaderboard Essentials:")]
    [SerializeField] private Transform _entryDisplayParent;
    [SerializeField] private GameObject _entryDisplayPrefab;
    [SerializeField] private CanvasGroup _leaderboardLoadingPanel;
    [Space]
    [SerializeField] private int _defaultPageNumber = 1, _defaultEntriesToTake = 10;
    [SerializeField] private string _pageNumber, _taken;

    [Header("Keys")]
    [SerializeField] private string publicKey;
    [SerializeField] private string secretKey;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        transitionManager = FindObjectOfType<TransitionManager>();  
    }

    private void Start()
    {
        InitializeComponents();
        
        //LoadEntries();        
    }

    public void GetLeaderBoard()
    {
        var timePeriod =
                0 == 1 ? Dan.Enums.TimePeriodType.Today :
                0 == 2 ? Dan.Enums.TimePeriodType.ThisWeek :
                0 == 3 ? Dan.Enums.TimePeriodType.ThisMonth :
                0 == 4 ? Dan.Enums.TimePeriodType.ThisYear : Dan.Enums.TimePeriodType.AllTime;

        var pageNumber = int.TryParse(_pageNumber, out var pageValue) ? pageValue : _defaultPageNumber;
        pageNumber = Mathf.Max(1, pageNumber);

        var take = int.TryParse(_taken, out var takeValue) ? takeValue : _defaultEntriesToTake;
        take = Mathf.Clamp(take, 1, 100);

        var leaderboard = new LeaderboardSearchQuery
        {
            Skip = (pageNumber - 1) * take,
            Take = take,
            TimePeriod = timePeriod
        };

        Leaderboards.Shin.GetEntries(leaderboard, OnLeaderboardLoaded,ErrorCallback);
        ToggleLoadingPanel(true);
    }

    /*public void SetLeaderBoardEntry(string userName, float score, string extra)
    {
        Leaderboards.Shin.UploadNewEntry(userName, score, extra, isSuccessful =>
        {
            if (isSuccessful)
            {
                //transitionManager.TransitionScene(0, GameManager.instance.timerTransition, 3);
                //InitializeComponents();
                Debug.Log("Se agrego un nombre correctamente");
            }
        });
    }*/

    public void EntryNew()
    {
        string userName = UiManager.instance.ifUserName.text;
        float score = LevelManager.instance.distance.position.x;
        string extra = LevelManager.instance.totalTime.ToString();

        if (System.Array.IndexOf(badWords, userName) == -1 && userName != "") 
        {
            StartCoroutine(NewLoad(userName, score));
        }
        else
        {
            Debug.Log("You insert a badword");
            UiManager.instance.ifUserName.text = "";
            UiManager.instance.ifUserName.placeholder.GetComponent<TextMeshProUGUI>().text = "Ingresa otro nombre";
        }

    }

    private IEnumerator NewLoad(string userName, float score)
    {
        Leaderboards.Shin.UploadNewEntry(userName, (int)score, Callback, ErrorCallback);

        yield return new WaitForSeconds(0.3f);

        GameManager.instance.isInGame = false;

        transitionManager.TransitionScene(0, GameManager.instance.timerTransition, UiManager.instance.leaderboardUiId);
        transitionManager.TransitionMusic(0, GameManager.instance.timerTransition);
    }

    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    private void OnLeaderboardLoaded(Entry[] entries)
    {
        int loopLenth = (entries.Length < names.Count) ? entries.Length : names.Count;

        for (int i = 0; i < loopLenth; i++)
        {
            names[i].text = entries[i].Username;
            scores[i].text = (entries[i].Score / 100f).ToString("F2") + "KM";
        }

        /*foreach (Transform t in _entryDisplayParent)
            Destroy(t.gameObject);

        foreach (var t in entries)
            CreateEntryDisplay(t);

        ToggleLoadingPanel(false);*/
    }

    private void CreateEntryDisplay(Entry entry)
    {
        var entryDisplay = Instantiate(_entryDisplayPrefab, _entryDisplayParent);
        entryDisplay.GetComponent<EntryDisplay>().SetEntry(entry);
    }

    /*public void LoadEntries()
    {
        Leaderboards.Shin.GetEntries(entries =>
        {
            #region Ranked
            foreach (var t in UiManager.instance.rankeds)
                t.text = "...";

            var lengthR = Mathf.Min(UiManager.instance.rankeds.Count, entries.Length);
            for (int i = 0; i < lengthR; i++)
                UiManager.instance.rankeds[i].text = $"{entries[i].Rank}";
            #endregion

            #region Names
            foreach (var t in UiManager.instance.names)
                t.text = "...";

            var lengthN = Mathf.Min(UiManager.instance.names.Count, entries.Length);
            for (int i = 0; i < lengthN; i++)
                UiManager.instance.names[i].text = $"{entries[i].Username}";
            #endregion

            #region Distance
            foreach (var t in UiManager.instance.distances)
                t.text = "...";

            var lengthD = Mathf.Min(UiManager.instance.distances.Count, entries.Length);
            for (int i = 0; i < lengthD; i++)
                UiManager.instance.distances[i].text = $"{(entries[i].Score / 100f)}KM";
            #endregion

            #region Time
            foreach (var t in UiManager.instance.times)
                t.text = "...";

            var lengthT = Mathf.Min(UiManager.instance.times.Count, entries.Length);
            for (int i = 0; i < lengthT; i++)
                UiManager.instance.times[i].text = $"{entries[i].Date}";
            #endregion
        });
    }*/

    private void InitializeComponents()
    {
        StartCoroutine(LoadingTextCoroutine(_leaderboardLoadingPanel.GetComponentInChildren<TextMeshProUGUI>()));
        GetLeaderBoard();
        GetPersonalEntry();
    }

    private void ToggleLoadingPanel(bool isOn)
    {
        _leaderboardLoadingPanel.alpha = isOn ? 1f : 0f;
        _leaderboardLoadingPanel.interactable = isOn;
        _leaderboardLoadingPanel.blocksRaycasts = isOn;
    }

    private IEnumerator LoadingTextCoroutine(TMP_Text text)
    {
        var loadingText = "Cargando";
        for (int i = 0; i < 3; i++)
        {
            loadingText += ".";
            text.text = loadingText;
            yield return new WaitForSeconds(0.25f);
        }

        StartCoroutine(LoadingTextCoroutine(text));
    }

    public void GetPersonalEntry()
    {
        Leaderboards.Shin.GetPersonalEntry(OnPersonalEntryLoaded, ErrorCallback);
    }

    private void OnPersonalEntryLoaded(Entry entry)
    {
        UiManager.instance.rankeds[0].text = entry.RankSuffix();
        UiManager.instance.names[0].text = entry.Username;
        UiManager.instance.distances[0].text = (entry.Score / 100f).ToString("F2") + "KM";

        /*float hours = Mathf.FloorToInt((entry.Extra / 3600) % 24);
        float minutes = Mathf.FloorToInt((entry.Extra / 60) % 60);
        float seconds = Mathf.FloorToInt((entry.Extra % 60));*/
        //UiManager.instance.times[0].text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    private void Callback(bool isSuccesfull)
    {
        if(isSuccesfull) 
        {
            InitializeComponents();
            Debug.Log("Se agrego un nombre correctamente");
        }
        //transitionManager.TransitionScene(0, GameManager.instance.timerTransition, 3);
    }

    private void ErrorCallback(string error)
    {
        Debug.LogError(error);
        ToggleLoadingPanel(true);
        _leaderboardLoadingPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Error: " + error;
    }
}
