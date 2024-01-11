using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AudioData
{
    public string title;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f; // Volume for each audio clip
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public List<AudioData> bgmList;
    public List<AudioData> sfxList;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    private Dictionary<string, AudioClip> bgmClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();

    private float bgmVolume = 1f; // Variable to control overall BGM volume
    private float sfxVolume = 1f; // Variable to control overall SFX volume

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            bgmSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();

            foreach (AudioData bgmData in bgmList)
            {
                bgmClips[bgmData.title] = bgmData.clip;
            }

            foreach (AudioData sfxData in sfxList)
            {
                sfxClips[sfxData.title] = sfxData.clip;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 각 씬에 따라 BGM 변경
        string sceneName = scene.name;

        if (sceneName == "IntroScene")
        {
            StopBGM();
            PlayBGM("IntroBGM");
        }
        else if (sceneName == "main")
        {
            StopBGM();
            PlayBGM("MainMenuBGM");
        }
        else if (sceneName == "LoadingScene")
        {
            StopBGM();
            PlayBGM("LoadingBGM", false);
        }
        else if (sceneName == "clear")
        {
            StopBGM();
            PlayBGM("ClearSceneBGM", false);
        }
        //인게임 BGM은 Timer 스크립트에 넣어둠
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PlayBGM(string bgmTitle, bool loop = true)
    {
        if (!bgmClips.ContainsKey(bgmTitle))
        {
            Debug.LogError("Invalid BGM title");
            return;
        }

        bgmSource.clip = bgmClips[bgmTitle];
        bgmSource.volume = bgmVolume;
        bgmSource.loop = loop;
        bgmSource.Play();
    }


    public void PlaySFX(string sfxTitle)
    {
        if (!sfxClips.ContainsKey(sfxTitle))
        {
            Debug.LogError("Invalid SFX title");
            return;
        }

        sfxSource.PlayOneShot(sfxClips[sfxTitle], sfxVolume);
    }

    // BGM 정지 메서드
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // 전체 BGM 볼륨 조절 메서드
    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmSource.volume = bgmVolume;
    }

    // 전체 SFX 볼륨 조절 메서드
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }
}
