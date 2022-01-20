using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{
    public int starNums = 0;
    private bool isSelect = false;

    public GameObject stars;
    public GameObject locks;
    public GameObject level;//待进入的关卡选择
    public GameObject map;//当前地图
    public int count;
    private int levelNum = 9;//该地图关卡总数
    public Text starText;//在地图显示已获取星星数量
    public int startLevel = 1;
    public int endLevel = 3;


    /// <summary>
    /// 判断是否解锁下一关地图
    /// </summary>
    private void Start()
    {
        //PlayerPrefs.DeleteAll();

        //星星数量达到标准才可进行解锁
        if (PlayerPrefs.GetInt("totalNums", 0) >= starNums)
        {
            isSelect = true;
        }
        //解锁要将星星显示，锁标记隐藏
        if (isSelect)
        {
            stars.SetActive(true);
            locks.SetActive(false);

            //统计一整张关卡的所有获得星星数，在map上显示，每一张map的totalNums存储所有该map下关卡星星数
            count = 0;
            for (int i = 0; i < levelNum; i++)
            {
                count += PlayerPrefs.GetInt("level" + i.ToString());
            }
            PlayerPrefs.SetInt("totalNums", count);
        }

        count = 0;
        for (int i = startLevel; i <= endLevel; i++)
        {
            count += PlayerPrefs.GetInt("level" + i.ToString(), 0);
        }
        starText.text = count.ToString() + "/9";

    }

    /// <summary>
    /// 鼠标单击事件，单击某一个map后，若满足进入条件，则隐去当前界面，显示关卡界面
    /// </summary>
    public void SelectedMap()
    {
        if (isSelect)
        {
            level.SetActive(true);//显示关卡界面
            map.SetActive(false);//隐去当前地图界面
        }
    }

    /// <summary>
    /// 返回按钮，从选关卡界面回到map界面
    /// </summary>
    public void ReturnButton()
    {
        if (isSelect)
        {
            level.SetActive(false);
            map.SetActive(true);
        }
    }
}
