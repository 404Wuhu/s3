using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawner类负责在场景中周期性地生成物体（如管道）。
/// 可以设置生成频率以及生成物体的随机高度范围。
/// </summary>
public class Spawner : MonoBehaviour
{
    // 要生成的预制体对象
    public GameObject prefab;

    // 生成的时间间隔
    public float spawnRate = 1.0f;

    // 生成物体的最小高度
    public float minHeight = -200.0f;

    // 生成物体的最大高度
    public float maxHeight = 200.0f;

    /// <summary>
    /// 当该脚本所在的对象被启用时，开始周期性调用Spawn方法生成物体。
    /// </summary>
    private void OnEnable()
    {
        // 使用InvokeRepeating按指定间隔周期性调用Spawn方法
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    /// <summary>
    /// 当该脚本所在的对象被禁用时，取消生成操作。
    /// </summary>
    private void OnDisable()
    {
        // 停止周期性调用Spawn方法
        CancelInvoke(nameof(Spawn));
    }

    /// <summary>
    /// 生成物体的方法。每次生成一个物体，并在其Y轴位置上添加随机偏移量。
    /// </summary>
    private void Spawn()
    {
        // 实例化预制体对象，生成位置为当前Spawner的坐标
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);

        // 调整生成物体的Y轴位置，增加一个随机高度偏移
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }
}
