using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G3Player : MonoBehaviour
{
    public float speed = 5f; // 玩家移动速度
    private float screenWidthInUnits; // 屏幕宽度（单位：世界单位）

    private Animator animator; // 动画控制器

    void Start()
    {
        // 获取屏幕的宽度
        screenWidthInUnits = Camera.main.orthographicSize * Camera.main.aspect;

        // 获取动画控制器组件
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 获取水平输入（A/D或←/→键）
        float horizontalInput = Input.GetAxis("Horizontal");

        // 根据输入设置动画状态
        if (horizontalInput > 0)
        {
            animator.Play("G2PlayerMove");
        }
        else if (horizontalInput < 0)
        {
            animator.Play("G2PlayerLMove");
        }
        else
        {
            animator.Play("G2Player");
        }

        // 根据输入计算新的位置
        Vector3 newPosition = transform.position + Vector3.right * horizontalInput * speed * Time.deltaTime;

        // 限制玩家不能超出屏幕
        newPosition.x = Mathf.Clamp(newPosition.x, -screenWidthInUnits, screenWidthInUnits);

        // 更新玩家位置
        transform.position = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "poison")
        {
            FindObjectOfType<G3Manager>().GameOver();
        }
        else if (other.gameObject.tag == "food")
        {
            FindObjectOfType<G3Manager>().IncreaseScore();
            Destroy(other.gameObject);
        }
    }
}
