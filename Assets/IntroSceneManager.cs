using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour
{
    public SpriteRenderer backgroundSpriteRenderer;
    private float fadeDuration = 10f; // ����� ������� �� �ɸ��� �ð�
    private float maxBrightness = 1f; // �ּ� ���� (��ο��� ����)
     
    void Start()
    {
        // ��� ������� ȿ�� ����
        StartCoroutine(FadeBackgroundOverTime());

        //���� ���� �� �ε����� ���� �⺻ ���� �ʱ�ȭ
        RunningTime.Instance.ResetGame();
    }

    IEnumerator FadeBackgroundOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // ���� �ð��� ���� ����� ���� ������Ʈ
            float currentBrightness = Mathf.Lerp(0f, maxBrightness, elapsedTime / fadeDuration);
            Color newColor = Color.HSVToRGB(0f, 0f, currentBrightness);
            backgroundSpriteRenderer.color = newColor;

            // ��� �ð� ������Ʈ
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // ����� ������ ��������� ����ó��
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
