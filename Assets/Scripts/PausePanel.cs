using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    private Animator anim;
    public GameObject button;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// 按钮添加事件，重新开始本关
    /// </summary>
    public void Retry()
    {
        Time.timeScale = 1;//运行状态
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// 按钮添加事件，回到主界面
    /// </summary>
    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 暂停按钮，暂停游戏
    /// </summary>
    public void Pause()
    {
        //print("Pause");
        //1.播放暂停动画
        //2.游戏暂停，弹出暂停窗口 
        anim.SetBool("isPause", true);
        button.SetActive(false);

        if (GameManager._gameManager.birds.Count > 0)
        {
            //暂停时小鸟若在起飞阶段，无法移动
            if (GameManager._gameManager.birds[0].isReleased == false)
            {
                GameManager._gameManager.birds[0].canMove = false;
            }
        }
    }

    /// <summary>
    /// 按钮添加事件，继续本关游戏
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1;
        anim.SetBool("isPause", false);

        //暂停时若小鸟在起飞阶段，动画结束后才可移动
        if (GameManager._gameManager.birds.Count > 0)
        {
            if (GameManager._gameManager.birds[0].isReleased == false)
            {
                GameManager._gameManager.birds[0].canMove = true;
            }
        }
    }

    public void PauseAnimEnd()
    {
        Time.timeScale = 0;//静止状态
    }

    public void ResumeAnimEnd()
    {
        button.SetActive(true);
    }
}
