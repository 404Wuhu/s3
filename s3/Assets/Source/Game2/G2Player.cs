using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class G2Player : MonoBehaviour
{
    public float speed = 5f; // 移动速度

    private float screenWidthInUnits;

    void Start()
    {
        // 获取屏幕的宽度（单位是世界单位）
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bolck")
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (other.gameObject.tag == "Score")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
}
