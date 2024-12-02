using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 引入场景管理命名空间

public class Show : MonoBehaviour
{
    public GameObject panel; // 需要在Inspector面板中绑定的Panel对象
    public Button showButton; // 显示Panel的按钮
    public Button closeButton; // 关闭Panel的按钮
    public Button switchGame1Button; // 切换场景的按钮
    public Button switchGame2Button; // 切换场景的按钮
    public Button switchGame3Button; // 切换场景的按钮

    public string targetGame1; // 目标场景的名称，在Inspector中设置
    public string targetGame2; // 目标场景的名称，在Inspector中设置
    public string targetGame3; // 目标场景的名称，在Inspector中设置

    void Start()
    {
        // 显示按钮的点击事件
        showButton.onClick.AddListener(TogglePanel);

        // 关闭按钮的点击事件
        closeButton.onClick.AddListener(ClosePanel);

        // 切换场景按钮的点击事件
        switchGame1Button.onClick.AddListener(SwitchGame1);

        // 切换场景按钮的点击事件
        switchGame2Button.onClick.AddListener(SwitchGame2);

        // 切换场景按钮的点击事件
        switchGame3Button.onClick.AddListener(SwitchGame3);

        // 默认隐藏关闭按钮，只有Panel显示时才显示
        closeButton.gameObject.SetActive(false);
    }

    void TogglePanel()
    {
        // 切换Panel的激活状态
        bool isActive = !panel.activeSelf;
        panel.SetActive(isActive);

        // 控制关闭按钮的显示状态
        closeButton.gameObject.SetActive(isActive);
    }

    void ClosePanel()
    {
        // 隐藏Panel和关闭按钮
        panel.SetActive(false);
        closeButton.gameObject.SetActive(false);
    }

    void SwitchGame1()
    {
        // 切换到目标场景
        SceneManager.LoadScene(targetGame1);
    }

    void SwitchGame2()
    {
        // 切换到目标场景
        SceneManager.LoadScene(targetGame2);
    }

    void SwitchGame3()
    {
        // 切换到目标场景
        SceneManager.LoadScene(targetGame3);
    }
}
