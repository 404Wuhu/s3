using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 导入场景管理命名空间

public class G2Manager : MonoBehaviour
{
    public GameObject block; // 方块游戏对象，用于生成障碍

    public GameObject Score;

    public float maxX; // 障碍物生成的最大水平偏移量

    public Transform spawnPoint; // 障碍物生成位置的初始点

    public float spawnRate; // 障碍物生成的间隔时间

    public Text scoreText; // 显示分数的UI文本

    private int score; // 当前分数

    public GameObject playButton; // 开始游戏按钮

    public GameObject backButton; // 返回主菜单按钮

    private bool gameStarted = false; // 游戏是否开始的标志

    public GameObject gameOver; // 游戏结束的UI对象

    public G2Player player; // 玩家对象，控制玩家行为

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
    }

    private void StartSpawning()
    {
        InvokeRepeating("SpawnBlock", 0.5f, spawnRate); // 定时生成障碍物
    }

    private void SpawnBlock()
    {
        Vector3 spawnPos = spawnPoint.position; // 获取生成位置

        spawnPos.x = Random.Range(-maxX, maxX); // 随机水平偏移

        Instantiate(block, spawnPos, Quaternion.identity); // 实例化障碍物
    }

    public void IncreaseScore()
    {
        score++; // 增加分数
        scoreText.text = score.ToString(); // 更新分数显示
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

        Pause(); // 暂停游戏
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("Title"); // 加载主菜单场景
    }
}
