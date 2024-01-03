using UnityEngine;

public class ObjectSwitch : MonoBehaviour
{
    public GameObject originalObject; // 원래 오브젝트를 가리키는 GameObject
    public GameObject newObject; // 새로운 오브젝트를 가리키는 GameObject

    // 버튼을 클릭했을 때 호출될 함수
    public void SwitchObject()
    {
        // 원래 오브젝트를 비활성화(숨김)
        originalObject.SetActive(false);

        // 새로운 오브젝트를 활성화(보임)
        newObject.SetActive(true);
    }
}
