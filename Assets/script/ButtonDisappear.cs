using UnityEngine;
using UnityEngine.UI;

public class ButtonDisappear : MonoBehaviour
{
    public Button button; // 사라질 버튼

    private void Start()
    {
        // 버튼 클릭 이벤트에 버튼 사라지기 함수 연결
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        // 버튼을 비활성화하여 사라지게 만듭니다.
        button.gameObject.SetActive(false);
    }
}
