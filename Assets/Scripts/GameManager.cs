using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;//存储所有小鸟
    public List<Pig> pigs;//存储所有小猪
    public static GameManager _gameManager;
    private Vector3 originPos;//小鸟在弹簧处的初始位置
    public GameObject win;
    public GameObject lose;
    public GameObject[] stars;//星星的数量
    private int starNums = 0;//存储星星的数量

    private void Awake()
    {
        _gameManager = GetComponent<GameManager>();
        if (birds.Count > 0)
        {
            originPos = birds[0].transform.position;
        }
    }

    private void Start()
    {
        Initialized();
    }

    /// <summary>
    /// 初始化，bird组件和Spring Joint2D组件，分别控制着小鸟和小鸟的弹簧力
    /// </summary>
    private void Initialized()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            if (i == 0)
            {
                birds[i].transform.position = originPos;//确保每只小鸟在弹簧出的位置相同
                birds[i].enabled = true;//bird组件
                birds[i].sp.enabled = true;//SpringJoint2D组件
                birds[i].canMove = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
                birds[i].canMove = false;
            }
        }
    }

    /// <summary>
    /// 判断游戏的进展
    /// </summary>
    public void NextBird()
    {
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                //下一只小鸟继续游戏
                Initialized();
            }
            else
            {
                //游戏输了
                lose.SetActive(true);
            }
        }
        else
        {
            //游戏胜利
            win.SetActive(true);
            //WinShow();
        }
    }

    /// <summary>
    /// 赢的时候显示星星
    /// </summary>
    public void WinShow()
    {
        StartCoroutine("Show");
    }

    IEnumerator Show()
    {
        while (starNums < birds.Count + 1)
        {
            if (starNums > stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.1f);
            stars[starNums].SetActive(true);
            starNums++;
        }
        //print(starNums);
    }

    /// <summary>
    /// 按钮添加事件，重新进行本关游戏
    /// </summary>
    public void Retry()
    {
        SaveStarNums();//存储本官完成后星星数
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// 按钮添加事件，回到主界面
    /// </summary>
    public void Home()
    {
        SaveStarNums();//存储本官完成后星星数
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 存储当前关卡的星星数
    /// 并且星星数也要实时更新，存储关卡最多的星星
    /// </summary>
    public void SaveStarNums()
    {
        if (starNums > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starNums);
        }
    }

}



