using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float force = 100; // 玩家跳?力度

    // Start is called before the first frame update
    void Start()
    {
        // 在游??始??行的代?可以放在?里
    }

    // Update is called once per frame
    void Update()
    {
        // 在?一?更新???玩家的?入
        if (Input.GetMouseButtonDown(0))
        {
            // 当玩家点?鼠?左??，将玩家的?体速度重置?零，然后施加向上的力以??跳?效果
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 当玩家与其他?撞体?生?撞?，重新加?游??景
        SceneManager.LoadScene(0);
    }

}
