using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 导入场景管理命名空间

public class GameManager : MonoBehaviour
{
    public Player player; // 玩家对象，控制玩家行为

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
    }

    public void Play()
    {
        score = 0; // 重置分数
        scoreText.text = score.ToString(); // 更新分数显示

        playButton.SetActive(false); // 隐藏开始按钮
        backButton.SetActive(false); // 隐藏返回按钮
        gameOver.SetActive(false); // 隐藏游戏结束UI

        Time.timeScale = 1f; // 恢复游戏时间
        player.enabled = true; // 启用玩家控制

        Pipes[] pipes = FindObjectsOfType<Pipes>(); // 查找场景中的所有管道对象

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject); // 销毁所有管道对象
        }
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

        LeaderboardManager.AddScore("Game1Scores", score);

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
