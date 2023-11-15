using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShowObjectsSequentially : MonoBehaviour
{
    public List<Button> buttons;      // 여러 개의 버튼을 리스트로 저장
    public List<GameObject> objects; // 버튼에 연결된 오브젝트를 리스트로 저장

    private int currentObjectIndex = 0; // 현재 보이는 오브젝트의 인덱스

    private void Start()
    {
        // 초기에 첫 번째 오브젝트만 활성화
        ShowCurrentObject();

        // 각 버튼에 클릭 이벤트 핸들러 연결
        for (int i = 0; i < buttons.Count; i++)
        {
            int buttonIndex = i; // 클로저를 사용하여 버튼 인덱스를 유지
            buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    private void OnButtonClick(int buttonIndex)
    {
        // 다음 오브젝트를 보이도록 처리
        currentObjectIndex++;

        // 모든 오브젝트를 순환하면 처음부터 다시 시작
        if (currentObjectIndex >= objects.Count)
        {
            currentObjectIndex = 0;
        }

        // 현재 인덱스의 오브젝트를 보이도록 처리
        ShowCurrentObject();
    }

    private void ShowCurrentObject()
    {
        // 모든 오브젝트를 비활성화
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].SetActive(false);
        }

        // 현재 인덱스의 오브젝트만 활성화
        objects[currentObjectIndex].SetActive(true);
    }
}
