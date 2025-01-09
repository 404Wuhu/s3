using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private void Update()
    {
        if(transform.position.y < -6.0f)
        {
            Destroy(gameObject);
        }
    }
}
