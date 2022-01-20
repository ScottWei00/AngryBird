using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private bool isSelect = false;//判断该关卡是否满足解锁条件
    public Sprite levelPic;//关卡解锁后的图像
    private Image image;//获取当前未解锁图像

    public GameObject[] stars;//显示星星

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        //第一个关卡默认开放
        if (transform.parent.GetChild(0).name == gameObject.name)
        {
            isSelect = true;
        }
        //其他关卡开放前提条件是上一关卡星星数量>0
        else
        {
            int beforeLevel = int.Parse(gameObject.name) - 1;//获取当前关卡上一关卡名
            if (PlayerPrefs.GetInt("level" + beforeLevel) > 0)
            {
                isSelect = true;
            }
        }
        //若满足解锁条件，将图片更换为解锁后的图片，并且关卡数也要显示
        if (isSelect)
        {
            image.overrideSprite = levelPic;//解锁后的图片
            transform.Find("levelNum").gameObject.SetActive(true);//显示关卡数
            //查找当前关卡星星数量并进行显示
            int count = PlayerPrefs.GetInt("level" + gameObject.name);
            for (int i = 0; i < count; i++)
            {
                stars[i].SetActive(true);
            }
        }
    }

    public void SelectedLevel()
    {
        if (isSelect)
        {
            PlayerPrefs.SetString("nowLevel", "level" + gameObject.name);
            SceneManager.LoadScene(2);
        }
    }

}
