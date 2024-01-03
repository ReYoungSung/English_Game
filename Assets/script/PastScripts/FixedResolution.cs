using UnityEngine;

public class FixedResolution : MonoBehaviour
{
    public int targetWidth = 2960;  // 목표 가로 해상도
    public int targetHeight = 1440; // 목표 세로 해상도

    void Awake()
    {
        // 기기의 현재 해상도
        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;

        // 원하는 해상도 비율과 기기 해상도 비율 비교
        float targetAspect = (float)targetWidth / targetHeight;
        float deviceAspect = (float)deviceWidth / deviceHeight;

        // 원하는 비율과 기기의 비율을 비교하여, 더 높은 비율을 사용
        if (deviceAspect > targetAspect)
        {
            int newHeight = Mathf.RoundToInt(targetWidth / deviceAspect);
            Screen.SetResolution(targetWidth, newHeight, true);
        }
        else
        {
            int newWidth = Mathf.RoundToInt(targetHeight * deviceAspect);
            Screen.SetResolution(newWidth, targetHeight, true);
        }
    }
}
