using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird
{
    /// <summary>
    /// 绿色小鸟的特殊能力，飞行时点击左键可以回旋
    /// </summary>
    public override void ShowSkill()
    {
        base.ShowSkill();
        Vector3 speed = rg.velocity;
        speed.x = -speed.x;
        rg.velocity = speed;
    }
}
