/****************************************************
	文件：Enemy_Frog.cs
	作者：Sam
	邮箱: 403117224@qq.com
	日期：2020/01/20 1:33   	
	功能：青蛙
*****************************************************/


using UnityEngine;

public class Enemy_Frog : Enemy//冒号是继承，表示Enemy_Frog包含MonoBehaviour的全部
{
    private Rigidbody2D rb;
    private Collider2D Coll;
    public LayerMask Ground;//地面的layer，检测是否踩在地面上

    public Transform leftpoint, rightpoint;
    public float Speed, JumpForce;//浮点型，可更改
    private float leftx, lefty, rightx, righty;//浮点型

    private bool Faceleft = true;

    protected override void Start()//调用父级的Start
    {
        base.Start();//调用父级的Start
        rb = GetComponent<Rigidbody2D>();//获得重力
        Coll = GetComponent<Collider2D>();

        transform.DetachChildren();//断绝关系
        leftx = leftpoint.position.x;
        lefty = leftpoint.position.y;
        rightx = rightpoint.position.x;
        righty = rightpoint.position.y;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    void Update()
    {
        SwitchAnim();
    }

    void Movement()
    {
        if (Faceleft)//面向左
        {
            if (Coll.IsTouchingLayers(Ground))//在地面上
            {
                Anim.SetBool("jumping", true);//执行跳跃
                rb.velocity = new Vector2(-Speed, JumpForce);//负速度，向左移动
            }
            if (transform.position.x < leftx)//当移动超过左点
            {
                transform.localScale = new Vector3(-1, 1, 1);//掉头向右
                Faceleft = false;//面向左关闭，面向右
            }
        }
        else//面向右
        {
            if (Coll.IsTouchingLayers(Ground))//在地面上
            {
                Anim.SetBool("jumping", true);//执行跳跃
                rb.velocity = new Vector2(Speed, JumpForce);//正速度，向右移动
            }
            if (transform.position.x > rightx) //当移动超过右点
            {
                transform.localScale = new Vector3(1, 1, 1);//掉头向左
                Faceleft = true;//面向右关闭，面向左
            }
        }
    }

    void SwitchAnim()//切换动画
    {
        if (Anim.GetBool("jumping"))//跳跃状态时
        {

            if (rb.velocity.y < 0)//y<0,下落
            {
                Anim.SetBool("jumping", false);//关闭跳跃动画
                Anim.SetBool("falling", true);//执行下落动画
            }
        }
        if (Coll.IsTouchingLayers(Ground) && Anim.GetBool("falling"))//下落并碰到地面时
        {
            Anim.SetBool("falling", false);//执行下落动画
        }
    }
}
