using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{
    public List<Pig> blocks = new List<Pig>();

    /// <summary>
    /// 进入爆炸鸟的触发区域
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Add(collision.gameObject.GetComponent<Pig>());
        }
    }

    /// <summary>
    /// 离开爆炸鸟的触发区域
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Remove(collision.gameObject.GetComponent<Pig>()); 
        }
    }

    /// <summary>
    /// 小黑鸟（爆炸鸟）特点：在该鸟的一定范围的所有物体都会爆炸
    /// </summary>
    public override void ShowSkill()
    {
        base.ShowSkill();
        if (blocks.Count > 0 && blocks != null)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Dead();
            }
        }
        OnClear();
    }

    /// <summary>
    /// 当单击左键使爆炸鸟进行特效时，爆炸后该鸟就直接消失了，没有拖尾效果
    /// </summary>
    public void OnClear()
    {
        Instantiate(boom, transform.position, Quaternion.identity);//爆炸特效
        render.enabled = false;//爆炸鸟图片消失
        rg.velocity = Vector3.zero;//爆炸鸟速度归0
        GetComponent<CircleCollider2D>().enabled = false;//碰撞体取消
        myTrail.ClearTrails();//无追尾特效
    }

    /// <summary>
    /// 重写小鸟的结束方式，直接结束
    /// </summary>
    protected override void BirdDestroy()
    {
        GameManager._gameManager.birds.Remove(this);
        Destroy(gameObject);
        GameManager._gameManager.NextBird();
    }
}
