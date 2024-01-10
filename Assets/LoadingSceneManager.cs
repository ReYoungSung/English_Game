using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject slider;
    public SpriteRenderer backgroundSpriteRenderer;
    public float fillDuration = 3f; // 슬라이더를 채우는 데 걸리는 시간
    public float fadeDuration = 1f; // 배경이 어두워지는 데 걸리는 시간
    private float minBrightness = 0.2f; // 최소 명도값 (어두워질 정도)

    [SerializeField] private GameObject StartButton;

    void Start()
    {
        // 슬라이더 초기화
        InitializeSlider();

        // 배경 어두워지는 효과 시작
        StartCoroutine(FadeBackgroundOverTime());

        // 슬라이더를 채우는 메서드 시작
        StartCoroutine(FillSliderOverTime());
    }

    void InitializeSlider()
    {
        // 슬라이더 초기값 설정
        slider.GetComponent<Slider>().value = slider.GetComponent<Slider>().minValue;
    }

    IEnumerator FadeBackgroundOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // 현재 시간에 따라 배경의 명도를 업데이트
            float currentBrightness = Mathf.Lerp(1f, minBrightness, elapsedTime / fadeDuration);
            Color newColor = Color.HSVToRGB(0f, 0f, currentBrightness);
            backgroundSpriteRenderer.color = newColor;

            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 배경이 완전히 어두워지도록 보장
        Color finalColor = Color.HSVToRGB(0f, 0f, minBrightness);
        backgroundSpriteRenderer.color = finalColor;
    }

    IEnumerator FillSliderOverTime()
    {
        yield return new WaitForSeconds(fadeDuration);

        float elapsedTime = 0f;
        float startValue = slider.GetComponent<Slider>().value;
        float endValue = slider.GetComponent<Slider>().maxValue;

        while (elapsedTime < fillDuration)
        {
            // 현재 시간에 따라 슬라이더 값을 업데이트
            slider.GetComponent<Slider>().value = Mathf.Lerp(startValue, endValue, elapsedTime / fillDuration);  

            // 경과 시간 업데이트
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 슬라이더가 완전히 채워지도록 보장
        slider.GetComponent<Slider>().value = endValue;  

        yield return new WaitForSeconds(0.5f);  
        slider.SetActive(false);  
        StartButton.SetActive(true);  
    }
}
