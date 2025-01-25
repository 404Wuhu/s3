using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class G2Manager : MonoBehaviour
{
    public GameObject deathPrefab; // 死亡物体预制体
    public GameObject scorePrefab; // 加分物体预制体

    public float spawnRate = 2.0f; // 固定生成间隔
    public float moveSpeed = 5f; // 初始物体移动速度
    public float moveSpeedIncrease = 0.5f; // 每次增加的移动速度
    public float maxMoveSpeed = 20f; // 最大移动速度

    public Text scoreText; // 显示分数的UI文本
    private int score; // 当前分数

    public GameObject playButton; // 开始游戏按钮
    public GameObject backButton; // 返回主菜单按钮
    public GameObject gameOver; // 游戏结束的UI对象

    public G2Player player; // 玩家对象
    public InputField nameInputField; // 玩家名字输入框

    public GameObject pauseMenu; // 暂停菜单对象
    public Button resumeButton; // 继续游戏按钮
    public Button restartButton; // 重新开始按钮
    public Button quitButton; // 返回主菜单按钮

    private Vector3 playerInitialPosition; // 玩家初始位置
    private Coroutine spawnCoroutine; // 记录生成协程的引用
    private bool isPaused = false; // 游戏是否暂停

    private void Awake()
    {
        Application.targetFrameRate = 60; // 设置帧率为60

        playerInitialPosition = player.transform.position; // 保存玩家初始位置
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
        // 检测 ESC 键，弹出或关闭暂停菜单
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

        score = 0; // 重置分数
        scoreText.text = score.ToString(); // 更新分数显示

        playButton.SetActive(false); // 隐藏开始按钮
        backButton.SetActive(false); // 隐藏返回按钮
        gameOver.SetActive(false); // 隐藏游戏结束UI

        // 重置移动速度
        moveSpeed = 5f;

        // 销毁所有物体
        DestroyAllObjectsWithTag("death");
        DestroyAllObjectsWithTag("score");

        // 重置玩家位置
        player.transform.position = playerInitialPosition;

        // 恢复时间并启用玩家控制
        Time.timeScale = 1f;
        player.enabled = true;

        // 停止旧协程并启动新协程
        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);
        spawnCoroutine = StartCoroutine(SpawnRoutine());
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

    private void DestroyAllObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnObject(); // 生成物体
            yield return new WaitForSeconds(spawnRate); // 固定生成间隔
            // 增加移动速度，但限制在最大速度以内
            moveSpeed = Mathf.Min(moveSpeed + moveSpeedIncrease, maxMoveSpeed);
        }
    }

    private void SpawnObject()
    {
        GameObject objectToSpawn = Random.value > 0.5f ? deathPrefab : scorePrefab;

        float fixedY = playerInitialPosition.y - 1.0f;
        float spawnX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 1f;

        Vector3 spawnPos = new Vector3(spawnX, fixedY, 0);
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPos, Quaternion.identity);

        ObjectMover mover = spawnedObject.GetComponent<ObjectMover>();
        if (mover != null)
        {
            mover.Init(moveSpeed);
        }
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void RestartGame()
    {
        ResumeGame(); // 先恢复时间
        Play(); // 重启游戏逻辑
    }

    public void GameOver()
    {
        // 停止生成协程
        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);

        // 重置移动速度
        moveSpeed = 5f;

        gameOver.SetActive(true);
        playButton.SetActive(true);
        backButton.SetActive(true);
        nameInputField.gameObject.SetActive(true);

        string playerName = PlayerPrefs.GetString("PlayerName", "知らない人");
        LeaderboardManager.AddScore("Game2Scores", playerName, score);

        Pause();
    }

    public void BackToTitle()
    {
        Time.timeScale = 1f; // 确保时间恢复正常
        SceneManager.LoadScene("Title");
    }
}
