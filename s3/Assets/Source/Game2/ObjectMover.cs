using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    private float targetX; // 玩家 x 轴位置（或屏幕左边界）
    private float moveSpeed; // 移动速度

    /// <summary>
    /// 初始化移动目标和速度
    /// </summary>
    public void Init(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;

        // 目标 X 设置为屏幕左边界外一点
        targetX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - 1f;
    }

    private void Update()
    {
        // 向目标 X 轴方向移动
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, 0), step);

        // 如果物体超出屏幕左侧边界则销毁
        if (transform.position.x <= targetX)
        {
            Destroy(gameObject);
        }
    }
}
