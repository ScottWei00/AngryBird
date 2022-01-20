using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    /// <summary>
    /// 赢的时候显示星星
    /// </summary>
    void ShowStar()
    {
        GameManager._gameManager.WinShow();
    }
}
