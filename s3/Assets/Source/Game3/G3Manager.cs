using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 导入场景管理命名空间

/// <summary>
/// G3Manager 类负责控制游戏整体逻辑，包括生成物体、分数管理和游戏状态管理。
/// </summary>
public class G3Manager : MonoBehaviour
{
    public GameObject poison; // 毒物
    public GameObject food;   // 食物

    public float spawnRate = 2.0f; // 初始生成间隔
    public float spawnRateDecrease = 0.1f; // 每次减少的间隔时间
    public float minSpawnRate = 0.5f; // 最小生成间隔

    public Text scoreText; // 显示分数的UI文本

    private int score; // 当前分数

    public GameObject playButton; // 开始游戏按钮
    public GameObject backButton; // 返回主菜单按钮

    public GameObject gameOver; // 游戏结束的UI对象

    public G3Player player; // 玩家对象，控制玩家行为

    private Vector3 playerInitialPosition; // 玩家初始位置

    private void Awake()
    {
        Application.targetFrameRate = 60; // 设置帧率为60

        playerInitialPosition = player.transform.position; // 保存玩家的初始位置

        Pause(); // 初始化时暂停游戏

        gameOver.SetActive(false); // 隐藏游戏结束UI
    }

    /// <summary>
    /// 开始游戏的逻辑。
    /// </summary>
    public void Play()
    {
        score = 0; // 重置分数
        scoreText.text = score.ToString(); // 更新分数显示

        playButton.SetActive(false); // 隐藏开始按钮
        backButton.SetActive(false); // 隐藏返回按钮
        gameOver.SetActive(false); // 隐藏游戏结束UI

        // 销毁所有食物和毒物
        DestroyAllObjectsWithTag("food");
        DestroyAllObjectsWithTag("poison");

        // 重置玩家位置
        player.transform.position = playerInitialPosition; // 将玩家的位置重置为初始位置

        Time.timeScale = 1f; // 恢复游戏时间
        player.enabled = true; // 启用玩家控制

        StartSpawning(); // 开始生成物体
    }

    /// <summary>
    /// 销毁所有带有指定标签的物体
    /// </summary>
    private void DestroyAllObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag); // 查找所有带有指定标签的物体
        foreach (GameObject obj in objects)
        {
            Destroy(obj); // 销毁物体
        }
    }

    /// <summary>
    /// 开始生成物体（Poison 和 Food）。
    /// </summary>
    private void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
    }

    /// <summary>
    /// 生成物体的协程，生成间隔逐渐减少
    /// </summary>
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnObject(); // 生成物体

            // 等待当前生成间隔
            yield return new WaitForSeconds(spawnRate);

            // 减少生成间隔，直到达到最小值
            spawnRate = Mathf.Max(spawnRate - spawnRateDecrease, minSpawnRate);
        }
    }

    /// <summary>
    /// 随机生成 Poison 或 Food 并设置生成位置。
    /// </summary>
    private void SpawnObject()
    {
        // 随机选择生成的物体类型
        GameObject objectToSpawn = Random.value > 0.5f ? poison : food;

        // 获取屏幕边界
        float screenWidthInUnits = Camera.main.orthographicSize * Camera.main.aspect;
        float screenTop = Camera.main.orthographicSize;

        // 随机生成X轴位置
        float randomX = Random.Range(-screenWidthInUnits, screenWidthInUnits);

        // 确定生成位置，Y轴为屏幕顶部
        Vector3 spawnPos = new Vector3(randomX, screenTop, 0);

        // 实例化物体
        Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
    }

    /// <summary>
    /// 增加分数。
    /// </summary>
    public void IncreaseScore()
    {
        score++; // 增加分数
        scoreText.text = score.ToString(); // 更新分数显示
    }

    /// <summary>
    /// 暂停游戏。
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0f; // 暂停游戏时间
        player.enabled = false; // 禁用玩家控制
    }

    /// <summary>
    /// 游戏结束的逻辑。
    /// </summary>
    public void GameOver()
    {
        gameOver.SetActive(true); // 显示游戏结束UI
        playButton.SetActive(true); // 显示开始按钮
        backButton.SetActive(true); // 显示返回按钮

        LeaderboardManager.AddScore("Game3Scores", score);

        Pause(); // 暂停游戏
    }

    /// <summary>
    /// 返回主菜单。
    /// </summary>
    public void BackToTitle()
    {
        SceneManager.LoadScene("Title"); // 加载主菜单场景
    }
}
