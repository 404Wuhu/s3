using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float force = 100; // �߉ƒ�?�͓x

    // Start is called before the first frame update
    void Start()
    {
        // �ݟ�??�n??�s�I��?�ȕ���?��
    }

    // Update is called once per frame
    void Update()
    {
        // ��?��?�X�V???�߉ƓI?��
        if (Input.GetMouseButtonDown(0))
        {
            // ���߉Ɠ_?�l?��??�C���߉ƓI?�̑��x�d�u?��C�R�@�{������I�͈�??��?����
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���߉Ɨ^����?����?��?��?�C�d�V��?��??�i
        SceneManager.LoadScene(0);
    }

}
