using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxSpeed = 10;
    public float minSpeed = 5;
    private SpriteRenderer render;//小猪组件
    public Sprite hurt;//受伤时的小猪图片
    public GameObject boom;//添加小猪被砸中后爆炸的组件
    public GameObject score;//添加小猪被砸中后获取分数的组件
    public bool isPig = false;

    public AudioClip pigHurt;
    public AudioClip pigDead;
    public AudioClip birdCollided;

    private void Awake()//Awake进行组件赋值
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)//碰撞函数
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioPlay(birdCollided);//播放小鸟碰撞时的音乐
            collision.transform.GetComponent<Bird>().BirdHurt();//当小鸟和猪或者木块碰撞时，变成受伤
        }
        //print(collision.relativeVelocity.magnitude);//输出碰撞时的速度

        if (collision.relativeVelocity.magnitude > maxSpeed)
        {
            Dead();//碰撞时的速度大于最大速度时，小猪消失
        }
        else if (collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed)
        {
            render.sprite = hurt;//碰撞时的速度处于最小和最大速度之间时，小猪受伤
            AudioPlay(pigHurt);//播放小猪受伤的音乐

        }
    }

    public void Dead()//小猪被砸中后爆炸和获取分数
    {
        if (isPig)
        {
            GameManager._gameManager.pigs.Remove(this);
            AudioPlay(pigDead);//播放小猪死亡的音乐
        }
        Destroy(gameObject);//小猪消失
        Instantiate(boom, transform.position, Quaternion.identity);//发生爆炸动画
        //Instantiate用来将爆炸和分数的位置放在小猪位置
        GameObject go = Instantiate(score, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);//显示分数
        Destroy(go, 1.5f);//分数消失
    }

    /// <summary>
    /// 在当前位置播放clip音乐
    /// </summary>
    /// <param name="clip"></param>
    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
