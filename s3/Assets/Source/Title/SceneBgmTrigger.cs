using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBgmTrigger : MonoBehaviour
{
    private void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 调用 BgmManager 切换 BGM
        if (BgmManager.Instance != null)
        {
            BgmManager.Instance.PlayBgmForScene(currentSceneName);
        }
    }
}
