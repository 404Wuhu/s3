using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 玩家控制脚本，负责玩家的移动和碰撞处理。
/// </summary>
public class G3Player : MonoBehaviour
{
    public float speed = 5f; // 玩家移动速度

    private float screenWidthInUnits; // 屏幕宽度（单位：世界单位）

    void Start()
    {
        // 获取屏幕的宽度，使用相机的正交尺寸和宽高比计算
        screenWidthInUnits = Camera.main.orthographicSize * Camera.main.aspect;
    }

    void Update()
    {
        // 获取水平输入（A/D或←/→键）
        float horizontalInput = Input.GetAxis("Horizontal");

        // 根据输入计算新的位置
        Vector3 newPosition = transform.position + Vector3.right * horizontalInput * speed * Time.deltaTime;

        // 限制玩家不能超出屏幕
        newPosition.x = Mathf.Clamp(newPosition.x, -screenWidthInUnits, screenWidthInUnits);

        // 更新玩家位置
        transform.position = newPosition;
    }


    /// <summary>
    /// 处理玩家与其他物体的碰撞。
    /// </summary>
    /// <param name="other">发生碰撞的物体</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "poison")
        {
            // 如果碰到的是毒物，结束游戏
            FindObjectOfType<G3Manager>().GameOver();
        }
        else if (other.gameObject.tag == "food")
        {
            // 如果碰到的是食物，增加分数
            FindObjectOfType<G3Manager>().IncreaseScore();

            // 销毁碰到的食物对象
            Destroy(other.gameObject);
        }
    }
}
