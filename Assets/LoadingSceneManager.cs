using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject slider;
    public SpriteRenderer backgroundSpriteRenderer;
    public float fillDuration = 3f; // �����̴��� ä��� �� �ɸ��� �ð�
    public float fadeDuration = 1f; // ����� ��ο����� �� �ɸ��� �ð�
    private float minBrightness = 0.2f; // �ּ� ���� (��ο��� ����)

    [SerializeField] private GameObject StartButton;

    void Start()
    {
        // �����̴� �ʱ�ȭ
        InitializeSlider();

        // ��� ��ο����� ȿ�� ����
        StartCoroutine(FadeBackgroundOverTime());

        // �����̴��� ä��� �޼��� ����
        StartCoroutine(FillSliderOverTime());
    }

    void InitializeSlider()
    {
        // �����̴� �ʱⰪ ����
        slider.GetComponent<Slider>().value = slider.GetComponent<Slider>().minValue;
    }

    IEnumerator FadeBackgroundOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // ���� �ð��� ���� ����� ���� ������Ʈ
            float currentBrightness = Mathf.Lerp(1f, minBrightness, elapsedTime / fadeDuration);
            Color newColor = Color.HSVToRGB(0f, 0f, currentBrightness);
            backgroundSpriteRenderer.color = newColor;

            // ��� �ð� ������Ʈ
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // ����� ������ ��ο������� ����
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
            // ���� �ð��� ���� �����̴� ���� ������Ʈ
            slider.GetComponent<Slider>().value = Mathf.Lerp(startValue, endValue, elapsedTime / fillDuration);  

            // ��� �ð� ������Ʈ
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // �����̴��� ������ ä�������� ����
        slider.GetComponent<Slider>().value = endValue;  

        yield return new WaitForSeconds(0.5f);  
        slider.SetActive(false);  
        StartButton.SetActive(true);  
    }
}
