using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 引入场景管理命名空间

public class Game2Button : MonoBehaviour
{
    public Button switchSceneButton; // 切换场景的按钮

    public string targetScene; // 目标场景的名称，在Inspector中设置

    // Start is called before the first frame update
    void Start()
    {
        // 切换场景按钮的点击事件
        switchSceneButton.onClick.AddListener(SwitchScene);
    }


    void SwitchScene()
    {
        // 切换到目标场景
        SceneManager.LoadScene(targetScene);
    }
}
