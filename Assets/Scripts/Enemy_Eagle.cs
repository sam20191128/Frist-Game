/****************************************************
	文件：Enemy_Eagle.cs
	作者：Sam
	邮箱: 403117224@qq.com
	日期：2020/01/20 1:33   	
	功能：鹰
*****************************************************/


using UnityEngine;

public class Enemy_Eagle : Enemy
{
    private Rigidbody2D rb;
    //private Animator Anim;
    public Transform Top, Bottom;
    public float Speed;//浮点型，可更改
    private float TopX, TopY, BottomX, BottomY;//浮点型

    private bool isLeft = true;
    private bool isTop = true;
    private bool isLeftTop = true;
    private bool isLeftBottom = true;

    protected override void Start()
    {
        base.Start();//调用父级的Start
        rb = GetComponent<Rigidbody2D>();
        //Anim = GetComponent<Animator>();//获得动画

        transform.DetachChildren();//断绝关系
        TopX = Top.position.x;
        TopY = Top.position.y;
        BottomX = Bottom.position.x;
        BottomY = Bottom.position.y;
        Destroy(Top.gameObject);
        Destroy(Bottom.gameObject);
    }

    void Update()
    {
        SwitchAnim();
        Movement();
    }

    void Movement()
    {
        if (TopY == BottomY)//左右
        {

            if (isLeft)
            {
                rb.velocity = new Vector2(-Speed, rb.velocity.y);
                if (transform.position.x < TopX)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    isLeft = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(Speed, rb.velocity.y);
                if (transform.position.x > BottomX)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    isLeft = true;
                }
            }
        }

        if (TopX == BottomX)//上下
        {

            if (isTop)
            {
                rb.velocity = new Vector2(rb.velocity.x, Speed);
                if (transform.position.y > TopY)
                {
                    isTop = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -Speed);
                if (transform.position.y < BottomY)
                {
                    isTop = true;
                }
            }
        }

        if (TopX < BottomX && TopY > BottomY)//左上、右下
        {
            if (isLeftTop)
            {
                rb.velocity = new Vector2(-Speed, Speed);
                if (transform.position.x < TopX && transform.position.y > TopY)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    isLeftTop = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(Speed, -Speed);
                if (transform.position.x > BottomX && transform.position.y < BottomY)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    isLeftTop = true;
                }
            }
        }
        if (TopX < BottomX && TopY < BottomY)//左下、右上
        {
            if (isLeftBottom)
            {
                rb.velocity = new Vector2(-Speed, -Speed);
                if (transform.position.x < TopX && transform.position.y < TopY)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    isLeftBottom = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(Speed, Speed);
                if (transform.position.x > BottomX && transform.position.y > BottomY)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    isLeftBottom = true;
                }
            }
        }
    }

    void SwitchAnim() 
    { 

    }
}
