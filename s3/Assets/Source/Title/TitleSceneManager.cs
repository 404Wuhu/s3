using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
    public Text game1LeaderboardText; // 显示 Game1 排行榜的 Text
    public Text game2LeaderboardText; // 显示 Game2 排行榜的 Text
    public Text game3LeaderboardText; // 显示 Game3 排行榜的 Text

    public Button game1Button; // 打开 Game1 排行榜的按钮
    public Button game2Button; // 打开 Game2 排行榜的按钮
    public Button game3Button; // 打开 Game3 排行榜的按钮

    public Button closeGame1Button; // 关闭 Game1 排行榜的按钮
    public Button closeGame2Button; // 关闭 Game2 排行榜的按钮
    public Button closeGame3Button; // 关闭 Game3 排行榜的按钮

    public GameObject game1Panel; // Game1 排行榜 Panel
    public GameObject game2Panel; // Game2 排行榜 Panel
    public GameObject game3Panel; // Game3 排行榜 Panel

    private void Start()
    {
        // 隐藏所有排行榜 Panel
        game1Panel.SetActive(false);
        game2Panel.SetActive(false);
        game3Panel.SetActive(false);

        // 绑定按钮事件
        game1Button.onClick.AddListener(() => ShowLeaderboard("Game1Scores", game1Panel, game1LeaderboardText));
        game2Button.onClick.AddListener(() => ShowLeaderboard("Game2Scores", game2Panel, game2LeaderboardText));
        game3Button.onClick.AddListener(() => ShowLeaderboard("Game3Scores", game3Panel, game3LeaderboardText));

        closeGame1Button.onClick.AddListener(() => game1Panel.SetActive(false));
        closeGame2Button.onClick.AddListener(() => game2Panel.SetActive(false));
        closeGame3Button.onClick.AddListener(() => game3Panel.SetActive(false));
    }

    /// <summary>
    /// 显示排行榜内容
    /// </summary>
    private void ShowLeaderboard(string gameKey, GameObject panel, Text leaderboardText)
    {
        // 获取排行榜条目
        List<LeaderboardManager.LeaderboardEntry> entries = LeaderboardManager.GetLeaderboard(gameKey);

        // 构建显示内容
        string leaderboardContent = "排行榜:\n";
        for (int i = 0; i < entries.Count; i++)
        {
            leaderboardContent += $"{i + 1}. {entries[i].playerName} - {entries[i].score}\n";
        }

        // 更新 Text 内容
        leaderboardText.text = leaderboardContent;

        // 显示对应的 Panel
        panel.SetActive(true);
    }

}
