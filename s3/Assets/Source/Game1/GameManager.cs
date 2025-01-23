using System.Collections;
using System.Collections.Generic;
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

    private int score; // 当前分数

    private void Awake()
    {
        Application.targetFrameRate = 60; // 设置帧率为60

        Pause(); // 初始化时暂停游戏

        gameOver.SetActive(false); // 隐藏游戏结束UI

        string playerName = PlayerPrefs.GetString("PlayerName", "未知玩家");
        Debug.Log("当前玩家名字: " + playerName);
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
            playerName = "未知玩家"; // 如果名字为空，使用默认名字
        }

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
    }

    public void Pause()
    {
        Time.timeScale = 0f; // 暂停游戏时间
        player.enabled = false; // 禁用玩家控制
    }

    public void GameOver()
    {
        gameOver.SetActive(true); // 显示游戏结束UI
        playButton.SetActive(true); // 显示开始按钮
        backButton.SetActive(true); // 显示返回按钮

        // 显示名字输入框
        nameInputField.gameObject.SetActive(true);

        // 假设玩家名字存储在一个输入框中
        string playerName = PlayerPrefs.GetString("PlayerName", "未知玩家");

        // 保存分数和名字到排行榜
        LeaderboardManager.AddScore("Game1Scores", playerName, score);

        Pause(); // 暂停游戏
    }


    public void IncreaseScore()
    {
        score++; // 增加分数
        scoreText.text = score.ToString(); // 更新分数显示
    }

    // 返回到Title场景
    public void BackToTitle()
    {
        SceneManager.LoadScene("Title"); // 加载主菜单场景
    }
}
