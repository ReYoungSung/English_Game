using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour
{
    public SpriteRenderer backgroundSpriteRenderer;
    private float fadeDuration = 10f; // 배경이 밝아지는 데 걸리는 시간
    private float maxBrightness = 1f; // 최소 명도값 (어두워질 정도)
     
    void Start()
    {
        // 배경 밝아지는 효과 시작
        StartCoroutine(FadeBackgroundOverTime());

        //게임 시작 전 로딩으로 게임 기본 정보 초기화
        RunningTime.Instance.ResetGame();
    }

    IEnumerator FadeBackgroundOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // 현재 시간에 따라 배경의 명도를 업데이트
            float currentBrightness = Mathf.Lerp(0f, maxBrightness, elapsedTime / fadeDuration);
            Color newColor = Color.HSVToRGB(0f, 0f, currentBrightness);
            backgroundSpriteRenderer.color = newColor;

            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 배경이 완전히 밝아지도록 예외처리
        Color finalColor = Color.HSVToRGB(0f, 0f, maxBrightness);
        backgroundSpriteRenderer.color = finalColor;

        SceneManager.LoadScene("main"); 
    } 

    public void LoadMainScene()
    {
        SceneManager.LoadScene("main");

        RunningTime.Instance.isPauseTimer = false;
    }
}
