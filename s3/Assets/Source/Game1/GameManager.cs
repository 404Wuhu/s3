using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 导入场景管理命名空间

public class GameManager : MonoBehaviour
{
    public Player player; // 玩家对象，控制玩家行为

    public InputField nameInputField; // 玩家名字输入框
    public Text scoreText; // 显示分数的UI文本

    public GameObject playButton; // 开始游戏按钮
    public GameObject backButton; // 返回主菜单按钮
    public GameObject gameOver; // 游戏结束的UI对象

    public GameObject pauseMenu; // 暂停菜单对象
    public Button resumeButton; // 继续游戏按钮
    public Button restartButton; // 重新开始按钮
    public Button quitButton; // 返回主菜单按钮

    private int score; // 当前分数
    private bool isPaused = false; // 是否暂停

    private void Awake()
    {
        Application.targetFrameRate = 60; // 设置帧率为60

        Pause(); // 初始化时暂停游戏
        gameOver.SetActive(false); // 隐藏游戏结束UI
        pauseMenu.SetActive(false); // 隐藏暂停菜单

        string playerName = PlayerPrefs.GetString("PlayerName", "知らない人");
        Debug.Log("当前玩家名字: " + playerName);

        // 为暂停菜单按钮绑定功能
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(BackToTitle);
    }

    private void Update()
    {
        // 检测 ESC 键，控制暂停和恢复
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                ShowPauseMenu();
            }
        }
    }

    public void Play()
    {
        // 保存玩家名字
        SavePlayerName();

        // 隐藏名字输入框
        nameInputField.gameObject.SetActive(false);

        // 初始化分数
        score = 0;
        scoreText.text = score.ToString();

        // 隐藏UI
        playButton.SetActive(false);
        backButton.SetActive(false);
        gameOver.SetActive(false);

        // 启用玩家控制
        Time.timeScale = 1f;
        player.enabled = true;

        // 清除旧的管道
        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void SavePlayerName()
    {
        string playerName = nameInputField.text;
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "知らない人"; // 如果名字为空，使用默认名字
        }

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
    }

    public void Pause()
    {
        Time.timeScale = 0f; // 暂停游戏时间
        player.enabled = false; // 禁用玩家控制
    }

    public void ResumeGame()
    {
        isPaused = false; // 更新暂停状态
        Time.timeScale = 1f; // 恢复游戏时间
        pauseMenu.SetActive(false); // 隐藏暂停菜单
    }

    public void ShowPauseMenu()
    {
        isPaused = true; // 更新暂停状态
        Time.timeScale = 0f; // 暂停游戏时间
        pauseMenu.SetActive(true); // 显示暂停菜单
    }

    public void RestartGame()
    {
        ResumeGame(); // 恢复游戏时间
        Play(); // 重启游戏逻辑
    }

    public void GameOver()
    {
        gameOver.SetActive(true); // 显示游戏结束UI
        playButton.SetActive(true); // 显示开始按钮
        backButton.SetActive(true); // 显示返回按钮

        // 显示名字输入框
        nameInputField.gameObject.SetActive(true);

        // 假设玩家名字存储在一个输入框中
        string playerName = PlayerPrefs.GetString("PlayerName", "知らない人");

        // 保存分数和名字到排行榜
        LeaderboardManager.AddScore("Game1Scores", playerName, score);

        Pause(); // 暂停游戏
    }

    public void IncreaseScore()
    {
        score++; // 增加分数
        scoreText.text = score.ToString(); // 更新分数显示
    }

    public void BackToTitle()
    {
        Time.timeScale = 1f; // 确保时间恢复正常
        SceneManager.LoadScene("Title"); // 加载主菜单场景
    }
}
