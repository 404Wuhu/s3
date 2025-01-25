using UnityEngine;

public class G2Player : MonoBehaviour
{
    public float jumpForce = 10f; // 跳跃力度
    public int maxJumpCount = 2; // 最大跳跃次数
    private int jumpCount; // 当前跳跃次数
    public float gravity = -9.8f; // 模拟重力
    private Rigidbody2D rb; // 刚体组件

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = maxJumpCount; // 初始化跳跃次数
    }

    void Update()
    {
        // 只有在游戏进行状态下才检测跳跃输入
        if (Time.timeScale > 0 && Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // 施加跳跃力
            jumpCount--; // 跳跃次数减少
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 碰到地面时重置跳跃次数
        if (collision.contacts[0].normal.y > 0)
        {
            jumpCount = maxJumpCount;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("death")) // 碰到死亡物体
        {
            FindObjectOfType<G2Manager>().GameOver();
        }
        else if (other.CompareTag("score")) // 碰到加分物体
        {
            FindObjectOfType<G2Manager>().IncreaseScore();
            Destroy(other.gameObject); // 销毁加分物体
        }
    }
}
