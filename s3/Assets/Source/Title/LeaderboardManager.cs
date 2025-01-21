using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    private const int MaxEntries = 10; // 每个排行榜最多存储10个分数

    /// <summary>
    /// 添加分数到指定排行榜
    /// </summary>
    public static void AddScore(string gameKey, int score)
    {
        List<int> scores = GetScores(gameKey);

        // 添加分数并排序
        scores.Add(score);
        scores.Sort((a, b) => b.CompareTo(a)); // 从高到低排序

        // 保留前10个分数
        if (scores.Count > MaxEntries)
        {
            scores.RemoveAt(scores.Count - 1);
        }

        // 保存分数
        SaveScores(gameKey, scores);
    }

    /// <summary>
    /// 获取指定排行榜的分数
    /// </summary>
    public static List<int> GetScores(string gameKey)
    {
        List<int> scores = new List<int>();
        for (int i = 0; i < MaxEntries; i++)
        {
            if (PlayerPrefs.HasKey($"{gameKey}_Score_{i}"))
            {
                scores.Add(PlayerPrefs.GetInt($"{gameKey}_Score_{i}"));
            }
        }
        return scores;
    }

    /// <summary>
    /// 保存排行榜数据
    /// </summary>
    private static void SaveScores(string gameKey, List<int> scores)
    {
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetInt($"{gameKey}_Score_{i}", scores[i]);
        }

        for (int i = scores.Count; i < MaxEntries; i++)
        {
            PlayerPrefs.DeleteKey($"{gameKey}_Score_{i}");
        }

        PlayerPrefs.Save();
    }
}
