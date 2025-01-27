using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MEC;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;

    [Header("Music")]
    public Animator transitionMusic = null;

    [Header("Scenes & Levels")]
    [SerializeField] private string menuSceneLocation;
    [SerializeField] private string[] scenes = null;
    [Space]
    [SerializeField] private string levelSceneLocation;
    public string[] levels = null;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void TransitionScene(int sceneId, float timer, int uiId)
    {
        Timing.RunCoroutine(LoadScene(sceneId, timer, uiId));
    }

    private IEnumerator<float> LoadScene(int sceneId,float timer, int uiId)
    {
        UiMediator.instance.ShowPanel(UiMediator.instance.Medators.Length - 1);

        yield return Timing.WaitForSeconds(timer);

        UiMediator.instance.GoToUi(uiId);

        if (GameManager.instance.isInGame)
        {
            SceneManager.LoadScene(levelSceneLocation + levels[sceneId]);
        }
        else
        {
            SceneManager.LoadScene(menuSceneLocation + scenes[sceneId]);
        }

        UiMediator.instance.HidePanel(UiMediator.instance.Medators.Length - 1);
    }

    public void TransitionMusic(int musicId, float timer)
    {
        Timing.RunCoroutine(LoadMuisc(musicId, timer));
    }

    private IEnumerator<float> LoadMuisc(int musicId, float timer)
    {
        transitionMusic.SetTrigger("FadeStart");
        yield return Timing.WaitForSeconds(timer);
        SoundManager.instance.musicAudioSource.Stop();

        if (GameManager.instance.isInGame)
        {
            SoundManager.instance.musicAudioSource.clip = SoundManager.instance.mainMenuAudioClip[musicId];
            //SceneManager.LoadScene(levelSceneLocation + levels[sceneId]);
        }
        else
        {
            SoundManager.instance.musicAudioSource.clip = SoundManager.instance.levelsAudioClip[musicId];
            //SceneManager.LoadScene(menuSceneLocation + scenes[sceneId]);
        }
        SoundManager.instance.musicAudioSource.Play();
        transitionMusic.SetTrigger("FadeEnd");
    }
}
