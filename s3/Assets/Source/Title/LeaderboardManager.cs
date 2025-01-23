using System.Collections.Generic;
using UnityEngine;

public static class LeaderboardManager
{
    [System.Serializable]
    public class LeaderboardEntry
    {
        public string playerName; // 玩家名字
        public int score;         // 玩家分数

        public LeaderboardEntry(string name, int score)
        {
            playerName = name;
            this.score = score;
        }
    }

    private static Dictionary<string, List<LeaderboardEntry>> leaderboards = new Dictionary<string, List<LeaderboardEntry>>();

    public static void AddScore(string gameKey, string playerName, int score)
    {
        if (!leaderboards.ContainsKey(gameKey))
        {
            leaderboards[gameKey] = new List<LeaderboardEntry>();
        }

        // 添加新的条目
        leaderboards[gameKey].Add(new LeaderboardEntry(playerName, score));

        // 按分数从高到低排序
        leaderboards[gameKey].Sort((a, b) => b.score.CompareTo(a.score));

        // 只保留前10名
        if (leaderboards[gameKey].Count > 10)
        {
            leaderboards[gameKey].RemoveAt(leaderboards[gameKey].Count - 1);
        }
    }

    public static List<LeaderboardEntry> GetLeaderboard(string gameKey)
    {
        if (!leaderboards.ContainsKey(gameKey))
        {
            leaderboards[gameKey] = new List<LeaderboardEntry>();
        }

        return leaderboards[gameKey];
    }
}
