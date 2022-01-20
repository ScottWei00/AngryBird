using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour
{
    private bool isClick = false;//当前鼠标是否点击
    public float maxDis = 3;//小鸟的最大距离
    [HideInInspector]
    public SpringJoint2D sp;//让小鸟飞出弹弓；是否使得弹簧力失活
    protected Rigidbody2D rg;//刚体操作，Kinematic取消物理作用
    public GameObject boom;
    protected TestMyTrail myTrail;//处理小鸟尾部拖尾
    [HideInInspector]
    public bool canMove = false;//小鸟飞出后便无法对其进行移动
    //小鸟受伤的动画替换
    public Sprite birdHurt;
    protected SpriteRenderer render;


    public LineRenderer right;
    public Transform rightPos;
    public LineRenderer left;
    public Transform leftPos;

    public float smooth = 3;

    //小鸟选择和飞行时的音效
    public AudioClip birdSelect;
    public AudioClip birdFly;
    
    private bool isFly = false;//对黄色小鸟的特殊能力操作(飞得更快)
    [HideInInspector]
    public bool isReleased = false;//暂停时无法对小鸟操作

    //获取该组件的值
    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
    }

    //鼠标按下时
    private void OnMouseDown()
    {
        if (canMove)
        {
            isClick = true;
            rg.isKinematic = true;//无物理作用（重力）
            AudioPlay(birdSelect);//播放小鸟选择时的音乐
        }
    }

    //鼠标抬起时
    private void OnMouseUp()
    {
        if (canMove)
        {
            isClick = false;
            rg.isKinematic = false;//有物理作用，小鸟飞出后会根据重力掉下来
            Invoke("Fly", 0.1f);
            right.enabled = false;
            left.enabled = false;
            canMove = false;
        }
    }

    //鼠标一直按下时的处理
    private void Update()
    {
        //判断点击的是UI还是界面，若是UI则返回，不继续进行
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//鼠标当前位置赋给小鸟
            transform.position -= new Vector3(0, 0, Camera.main.transform.position.z);//去除深度坐标信息
            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;
                transform.position = pos * maxDis + rightPos.position;
            }
            DrawLine();
        }

        //相机跟随
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX, 7, 25), Camera.main.transform.position.y
            , Camera.main.transform.position.z), smooth * Time.deltaTime);

        //当小鸟处于飞行状态时，按下鼠标左键开启该小鸟的特殊功能
        if (isFly)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }
    }

    //让小鸟飞出去
    void Fly()
    {
        isReleased = true;
        isFly = true;//表示小鸟在飞行中，可以进行特殊能力
        myTrail.StartTrails();//小鸟飞出后开始拖尾动画
        sp.enabled = false;//弹簧力消失
        Invoke("BirdDestroy", 3f);//鸟飞出后至消失的过程
        AudioPlay(birdFly);//播放小鸟飞出的音乐
    }

    //绘制弹弓的线条
    void DrawLine()
    {
        left.enabled = true;
        right.enabled = true;

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);
    }

    //用爆炸的方式消去已发射的小鸟
    protected virtual void BirdDestroy()
    {
        GameManager._gameManager.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._gameManager.NextBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFly = false;//当碰撞时，黄色小鸟的特殊能力就用不了了
        myTrail.ClearTrails();//当小鸟碰上小猪时，拖尾结束
    }

    /// <summary>
    ///在当前位置播放clip音乐
    /// </summary>
    /// <param name="clip"></param>
    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    /// <summary>
    /// 进行小鸟的特殊能力（在重构中进行各小鸟的特殊操作）
    /// </summary>
    public virtual void ShowSkill()
    {
        isFly = false;
    }

    /// <summary>
    /// 小鸟碰撞时受伤的动画
    /// </summary>
    public void BirdHurt()
    {
        render.sprite = birdHurt;//小鸟碰撞时受伤
    }
}

