using UnityEngine;
using UnityEngine.UI;

namespace InGameScript
{
    public class WordButton : MonoBehaviour
    {
        private Button button;

        private void Start()
        {
            button = GetComponent<Button>();

            // 버튼 클릭 이벤트에 버튼 사라지기 함수 연결
            button.onClick.AddListener(HideButton);
        }

        // 버튼을 비활성화하여 사라지게 만듭니다.
        public void HideButton()
        {
            button.gameObject.SetActive(false);
        }
    }
}
