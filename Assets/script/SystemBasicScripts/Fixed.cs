using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts
{
    //Reference https://giseung.tistory.com/19
    public class Fixed : MonoBehaviour
    {
        private bool resolutionSet = false;

        private void Awake()
        {
            if (!resolutionSet)
            {
                SetResolution();
                resolutionSet = true;
            }
        }

        public void SetUpCanvasScaler(int setWidth, int setHeight) 
        {
            CanvasScaler canvasScaler = FindObjectOfType<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(setWidth, setHeight);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        }

        /* 해상도 설정하는 함수 */
        public void SetResolution(int setWidth = 1920, int setHeight = 1080)
        {
            SetUpCanvasScaler(setWidth, setHeight);

            int deviceWidth = Screen.width; // 기기 너비 저장
            int deviceHeight = Screen.height; // 기기 높이 저장

            if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
            {
                float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
                Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
            }
            else // 게임의 해상도 비가 더 큰 경우
            {
                float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
                Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
            }
        }
    } 

    /*
    public class Fixed : MonoBehaviour
    {
        CanvasScaler canvasScaler;

        private void Awake()
        {
            canvasScaler = GetComponent<CanvasScaler>();
        }
        private void Start()
        {
            SetResolution(); // 초기에 게임 해상도 고정
        }

        // 해상도 설정하는 함수 
        public void SetResolution()
        {
            canvasScaler.referenceResolution *= new Vector2((canvasScaler.referenceResolution.x / Screen.width), (Screen.height / canvasScaler.referenceResolution.y));
        }
    }
    */
}