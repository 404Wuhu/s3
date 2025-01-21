using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;

    public float gravity = -9.8f;

    public float strength = 5f;

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        // 鼠标点击控制上升
        if (Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }

        // 触摸屏幕控制上升
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }
        }

        // 模拟重力和移动
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 如果碰到障碍物，游戏结束
        if (other.gameObject.tag == "Obstacle")
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        // 如果碰到得分区域，增加分数
        else if (other.gameObject.tag == "Score")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
        // 如果碰到地板，游戏结束
        else if (other.gameObject.tag == "floor")
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
