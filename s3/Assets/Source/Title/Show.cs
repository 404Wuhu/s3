using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 引入场景管理命名空间

public class Show : MonoBehaviour
{
    public GameObject mainPanel; // 主面板
    public GameObject g1panel; // Game1的面板
    public GameObject g2panel; // Game2的面板
    public GameObject g3panel; // Game3的面板

    public Button showButton; // 显示主Panel的按钮
    public Button closeButton; // 关闭主Panel的按钮

    public Button game1PanelButton; // 打开Game1 Panel的按钮
    public Button game2PanelButton; // 打开Game2 Panel的按钮
    public Button game3PanelButton; // 打开Game3 Panel的按钮

    public Button g1closeButton; // 关闭Game1 Panel的按钮
    public Button g2closeButton; // 关闭Game2 Panel的按钮
    public Button g3closeButton; // 关闭Game3 Panel的按钮

    public Button switchGame1Button; // 切换到Game1场景的按钮
    public Button switchGame2Button; // 切换到Game2场景的按钮
    public Button switchGame3Button; // 切换到Game3场景的按钮

    public string targetGame1; // Game1目标场景的名称
    public string targetGame2; // Game2目标场景的名称
    public string targetGame3; // Game3目标场景的名称

    void Start()
    {
        // 显示主Panel按钮的点击事件
        showButton.onClick.AddListener(ToggleMainPanel);

        // 关闭主Panel按钮的点击事件
        closeButton.onClick.AddListener(CloseMainPanel);

        // 打开各自Panel按钮的点击事件
        game1PanelButton.onClick.AddListener(() => ToggleSpecificPanel(g1panel));
        game2PanelButton.onClick.AddListener(() => ToggleSpecificPanel(g2panel));
        game3PanelButton.onClick.AddListener(() => ToggleSpecificPanel(g3panel));

        // 关闭各自Panel按钮的点击事件
        g1closeButton.onClick.AddListener(() => CloseSpecificPanel(g1panel));
        g2closeButton.onClick.AddListener(() => CloseSpecificPanel(g2panel));
        g3closeButton.onClick.AddListener(() => CloseSpecificPanel(g3panel));

        // 切换场景按钮的点击事件
        switchGame1Button.onClick.AddListener(() => SceneManager.LoadScene(targetGame1));
        switchGame2Button.onClick.AddListener(() => SceneManager.LoadScene(targetGame2));
        switchGame3Button.onClick.AddListener(() => SceneManager.LoadScene(targetGame3));

        // 默认隐藏关闭按钮和所有子Panel
        closeButton.gameObject.SetActive(false);
        g1panel.SetActive(false);
        g2panel.SetActive(false);
        g3panel.SetActive(false);
    }

    void ToggleMainPanel()
    {
        // 切换主Panel的显示状态
        bool isActive = !mainPanel.activeSelf;
        mainPanel.SetActive(isActive);

        // 控制关闭按钮的显示状态
        closeButton.gameObject.SetActive(isActive);
    }

    void CloseMainPanel()
    {
        // 隐藏主Panel和关闭按钮
        mainPanel.SetActive(false);
        closeButton.gameObject.SetActive(false);

        // 同时隐藏所有子Panel
        g1panel.SetActive(false);
        g2panel.SetActive(false);
        g3panel.SetActive(false);
    }

    void ToggleSpecificPanel(GameObject panel)
    {
        // 显示指定的Panel，同时隐藏其他Panel
        g1panel.SetActive(false);
        g2panel.SetActive(false);
        g3panel.SetActive(false);

        // 切换指定Panel的显示状态
        panel.SetActive(true);
    }

    void CloseSpecificPanel(GameObject panel)
    {
        // 关闭指定的Panel
        panel.SetActive(false);
    }
}
