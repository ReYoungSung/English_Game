using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseWindow;

    [SerializeField] private GameManager gameManager;

    public void pauseGame()
    {
        SoundManager.instance.PlaySFX("SellectMenuSFX");
        SoundManager.instance.StopBGM();
        RunningTime.Instance.isPauseTimer = true;
        PauseWindow.SetActive(true);
    }

    public void unPauseGame()
    {
        SoundManager.instance.PlaySFX("SellectMenuSFX");
        SoundManager.instance.RePlayBGM();
        RunningTime.Instance.isPauseTimer = false; 
        PauseWindow.SetActive(false); 
    }

    public void LoadOtherScene(string sceneName)
    {
        SoundManager.instance.PlaySFX("SellectMenuSFX");
        SceneManager.LoadScene(sceneName);

        RunningTime.Instance.isPauseTimer = false;
    }

    public void ReStartGame()
    {
        if(gameManager.currentGameMode == GameManager.GameMode.test)
            SceneManager.LoadScene("LoadingSceneForTest"); 
        else
            SceneManager.LoadScene("LoadingScene"); 

        RunningTime.Instance.isPauseTimer = false;
        RunningTime.Instance.ResetGame(); 
    }
}
