using UnityEngine;

public class Block : MonoBehaviour
{
    public float fallSpeed = 2.0f;  // 设置下落速度

    private void Update()
    {
        // 模拟重力效果，逐渐让物体沿y轴下落
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        // 如果物体的y坐标小于-6.0f，则销毁物体
        if (transform.position.y < -6.0f)
        {
            Destroy(gameObject);
        }
    }
}
