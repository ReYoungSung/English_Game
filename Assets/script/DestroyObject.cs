using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyObject : MonoBehaviour
{
    private void Awake()
    {
        // 부모 GameObject를 찾습니다
        GameObject parent = transform.parent.gameObject;

        // 부모 GameObject가 씬 전환시에도 유지되도록 설정합니다
        Destroy(parent);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "gameover") // gameover 씬의 이름에 따라 조정
        {
            // 여기에서 'DontDestroyOnLoad'로 설정된 오브젝트를 파기
            GameObject[] objectsToDestroy = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in objectsToDestroy)
            {
                DontDestroyOnLoad(obj); // 'DontDestroyOnLoad'로 설정된 오브젝트 파기
            }
        }
    }
}
