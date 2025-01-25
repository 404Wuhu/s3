using UnityEngine;

public class BgmManager : MonoBehaviour
{
    public static BgmManager Instance; // 单例模式

    public AudioClip titleBgm; // Title 场景的 BGM
    public AudioClip game1Bgm; // Game1 场景的 BGM
    public AudioClip game2Bgm; // Game2 场景的 BGM
    public AudioClip game3Bgm; // Game3 场景的 BGM

    private AudioSource audioSource; // 音频播放器

    private void Awake()
    {
        // 确保只有一个 BgmManager 实例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 在场景切换时不销毁
        }
        else
        {
            Destroy(gameObject); // 如果已有实例，则销毁新创建的对象
            return;
        }

        // 初始化 AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true; // 循环播放
        audioSource.playOnAwake = false; // 不自动播放
    }

    /// <summary>
    /// 播放指定场景的 BGM
    /// </summary>
    public void PlayBgmForScene(string sceneName)
    {
        AudioClip bgmToPlay = null;

        switch (sceneName)
        {
            case "Title":
                bgmToPlay = titleBgm;
                break;
            case "Game1":
                bgmToPlay = game1Bgm;
                break;
            case "Game2":
                bgmToPlay = game2Bgm;
                break;
            case "Game3":
                bgmToPlay = game3Bgm;
                break;
            default:
                Debug.LogWarning($"No BGM assigned for scene: {sceneName}");
                return;
        }

        // 如果当前播放的 BGM 不同于目标 BGM，则切换
        if (audioSource.clip != bgmToPlay)
        {
            audioSource.clip = bgmToPlay;
            audioSource.Play();
        }
    }
}
