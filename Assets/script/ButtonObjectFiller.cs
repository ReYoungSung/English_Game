using UnityEngine;
using UnityEngine.UI;

public class ButtonObjectFiller : MonoBehaviour
{
    public Button fillButton; // 채우기 버튼
    public GameObject objectPrefab; // 채울 오브젝트 프리팹
    public Transform targetSlot; // 오브젝트를 채울 칸

    private void Start()
    {
        // 채우기 버튼에 클릭 이벤트 핸들러 연결
        fillButton.onClick.AddListener(FillSlotWithObject);
    }

    private void FillSlotWithObject()
    {
        if (objectPrefab != null && targetSlot != null)
        {
            // 칸에 오브젝트 생성 및 위치 설정
            GameObject newObject = Instantiate(objectPrefab, targetSlot.position, targetSlot.rotation);

            // 새로 생성된 오브젝트를 칸의 자식으로 설정
            newObject.transform.SetParent(targetSlot);
        }
    }
}
