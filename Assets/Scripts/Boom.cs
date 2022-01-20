using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public void DestroyBoom()
    {
        Destroy(gameObject);//爆炸结束后消失该爆炸组件
    }
}
