/****************************************************
	文件：FinalMovement.cs
	作者：Sam
	邮箱: 403117224@qq.com
	日期：2020/01/20 12:27   	
	功能：角色相关
*****************************************************/


using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Collider2D coll;
    public Collider2D DisColl;
    public Transform CellingCheck;
    private Animator anim;
    public Joystick joystick;//操纵杆


    public float speed, jumpForce;
    private float horizontalMove;
    public Transform groundCheck;//地面检测点
    public LayerMask ground;//地面的layer，检测是否踩在地面上

    [Header("CD的UI组件")]
    public Image cdImage;

    [Header("Dash参数")]//冲锋
    public float dashTime;//dash 时长
    private float dashTimeLeft;//冲锋剩余时间
    private float lastDash = -10f;//上一次dash时间点
    public float dashCoolDown;//Dash的CD时间
    public float dashSpeed;//冲锋速度

    public bool isGround, isJump, isDashing, Hurt;//布尔值变量,判断按键是否按下

    bool jumpPressde;//跳跃按下
    [SerializeField]
    private int jumpCount;//跳跃次数
    [SerializeField]
    private int Cherry, Gem;//樱桃、宝石数量，整数

    public Text CherryNum, GemNum;//樱桃、宝石数量UI显示

    private bool isHurt;//判断受伤,默认false

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)//跳跃
        {
            jumpPressde = true;
        }

        if (Input.GetKeyDown(KeyCode.K))//冲锋
        {
            if (Time.time >= (lastDash + dashCoolDown))//当前时间超过了最后一次执行时间+CD冷却时间
            {
                //可以执行Dash
                ReadyToDash();
            }
        }

        cdImage.fillAmount -= 1.0f / dashCoolDown * Time.deltaTime;

        CherryNum.text = Cherry.ToString();//text是字符，int是整形，无法直接赋值，需要转换ToString
    }

    private void FixedUpdate()//调用脚本，FixedUpdate 每0.02秒会检测按键是否按完了
    {
        if (isGround)//在地面上
        {
            jumpCount = 2;//跳跃次数
            isJump = false;//跳跃默认关闭
        }

        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);//判断是否在地面上。  0.1f设置检测圆形区域的半径。  ground选择检测和哪个layermask有重叠

        Dash();//调用冲锋的方法

        if (isDashing)//如果正在执行冲锋，不执行以下的其他方法
            return;

        if (!isHurt)
        {
            GroundMovement();//调用移动的方法
            Mobile_GroundMovement();//调用移动的方法
        }

        Jump();//调用跳跃的方法

        SwitchAnim();//调用动画的方法
    }

    void GroundMovement()//移动
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");//只返回-1,0,1  Horizontal 可以判断人物的左右翻转,负值向左翻转，正值向右翻转
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);

        }

        Crouch();
    }
    void Mobile_GroundMovement()//移动
    {
        horizontalMove = joystick.Horizontal;//操纵杆控制移动
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
        if (horizontalMove > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (horizontalMove < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Mobile_Crouch();
    }

    public void Jump()//跳跃
    {
        //if (isGround)//在地面上
        //{
        //    jumpCount = 2;//跳跃次数
        //    isJump = false;//跳跃默认关闭
        //}

        if (jumpPressde && isGround)//按下跳跃
        {
            isJump = true;//跳跃开启
            isGround = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);//执行跳跃
            jumpCount--;//跳跃次数减一
            SoundManager.instance.JumpAudio();
            jumpPressde = false;//按键执行完关闭
        }
        else if (jumpPressde && jumpCount > 0 && isJump)//按下同时跳跃次数大于0同时在跳跃状态，也就是在天上
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);//再次执行跳跃
            jumpCount--;//再次跳跃次数减一
            SoundManager.instance.JumpAudio();
            jumpPressde = false;//按键执行完关闭
        }

        if (!isGround && rb.velocity.y < 0 && jumpPressde && jumpCount > 0)//下落中按跳
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);//执行跳跃
            jumpCount--;//再次跳跃次数减一
            SoundManager.instance.JumpAudio();
            jumpPressde = false;//按键执行完关闭
        }
    }

    public void Mobile_Jump()//跳跃
    {
        //if (isGround)//在地面上
        //{
        //    jumpCount = 2;//跳跃次数
        //    isJump = false;//跳跃默认关闭
        //}

        if (isGround)
        {
            isJump = true;//跳跃开启
            isGround = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);//执行跳跃
            jumpCount--;//跳跃次数减一
            SoundManager.instance.JumpAudio();
        }
        else if (jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);//再次执行跳跃
            jumpCount--;//再次跳跃次数减一
            SoundManager.instance.JumpAudio();
        }

        if (!isGround && rb.velocity.y < 0 && jumpCount > 0)//下落中按跳
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);//执行跳跃
            jumpCount--;//再次跳跃次数减一
            SoundManager.instance.JumpAudio();
        }
    }

    public void Crouch()//下蹲
    {
        if (!Physics2D.OverlapCircle(CellingCheck.position, 0.2f, ground))//没有障碍物时才执行
        {
            if (Input.GetButton("Crouch"))//按下下蹲
            {
                anim.SetBool("crouching", true);//执行下蹲动画
                DisColl.enabled = false;//方形collider不启用
            }
            else
            {
                anim.SetBool("crouching", false);//关闭下蹲动画
                DisColl.enabled = true;//方形collider启用
            }
        }
    }

    public void Mobile_Crouch()
    {
        if (!Physics2D.OverlapCircle(CellingCheck.position, 0.2f, ground))//没有障碍物时才执行
        {
            if (joystick.Vertical < -0.5f)//按下下蹲
            {
                anim.SetBool("crouching", true);//执行下蹲动画
                DisColl.enabled = false;//方形collider不启用
            }
            else
            {
                anim.SetBool("crouching", false);//关闭下蹲动画
                DisColl.enabled = true;//方形collider启用
            }
        }
    }

    void SwitchAnim()//切换动画效果
    {

        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));
        if (isGround)//在地面
        {
            anim.SetBool("falling", false);
            anim.SetBool("jumping", false);
        }
        else if (!isGround && rb.velocity.y > 0)//不在地面，y>0，说明在上升
        {
            anim.SetBool("falling", false);
            anim.SetBool("jumping", true);//执行跳跃动画
        }
        else if (rb.velocity.y < 0)//y<0,下落
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);//执行下落动画
        }
        if (isHurt)
        {
            anim.SetBool("hurt", true);//受伤动画
            if (Mathf.Abs(rb.velocity.x) < 0.1f)//x绝对值小于0.1
            {
                anim.SetBool("hurt", false);
                isHurt = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//碰撞触发器
    {

        if (collision.tag == "Cherry")//收集物品
        {
            SoundManager.instance.CherryAudio();
            collision.GetComponent<Animator>().Play("Cherry_isGot");//播放获取物品的特效动画
            //CherryNum.text = Cherry.ToString();//text是字符，int是整形，无法直接赋值，需要转换ToString
        }
        if (collision.tag == "Gem")//收集物品
        {
            Destroy(collision.gameObject);
            Gem += 1;
            SoundManager.instance.GemAudio();
            GemNum.text = Gem.ToString();//text是字符，int是整形，无法直接赋值，需要转换ToString
        }
        if (collision.tag == "DeadLine")
        {
            GetComponent<AudioSource>().enabled = false;//所有音源启动方式换为false
            Invoke("Restart", 2f);//Invoke,字符型名字+延迟时间，延迟2秒执行重置
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//消灭敌人和受伤
    {
        if (collision.gameObject.tag == "Enemy")//碰撞到敌人
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();//实例化青蛙类 创建对象 frog 将碰撞体的青蛙类脚本组件赋予 frog
            if (anim.GetBool("falling"))//消灭敌人，从上掉落
            {
                enemy.JumpOn();//摧毁
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);//执行跳跃
                anim.SetBool("jumping", true);//小跳
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)//受伤，不是掉落,角色在敌人左侧
            {
                rb.velocity = new Vector2(-10f, rb.velocity.y);//向左弹出10个位置
                isHurt = true;//受伤
                SoundManager.instance.HurtAudio();
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)//受伤，不是掉落,角色在敌人右侧
            {
                rb.velocity = new Vector2(10f, rb.velocity.y);//向右弹出10个位置
                isHurt = true;//受伤
                SoundManager.instance.HurtAudio();
            }
        }
    }

    void ReadyToDash()//准备冲锋
    {
        isDashing = true;//启用

        dashTimeLeft = dashTime;//冲锋剩余时间=设定的冲锋时间 

        lastDash = Time.time;//时间点=按下按键的时间点

        cdImage.fillAmount = 1;//冲锋后CD开始冷却
    }

    public void Dash()//冲锋速度的方法
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)//冲锋剩余时间大于0
            {
                if (rb.velocity.y > 0 && !isGround)//在空中Dash向上
                {
                    rb.velocity = new Vector2(dashSpeed * horizontalMove, jumpForce);
                    SoundManager.instance.DashAudio();
                }
                rb.velocity = new Vector2(dashSpeed * horizontalMove, rb.velocity.y);//地面Dash
                SoundManager.instance.DashAudio();

                dashTimeLeft -= Time.deltaTime;//跳出循环，因为要在FixedUpdate里调用，所以要减去这个时间的值Time.deltaTime

                ShadowPool.instance.GetFormPool();//拿出一个影子
            }
            if (dashTimeLeft <= 0)//冲锋剩余时间小于0
            {
                isDashing = false;//停止冲锋
                if (!isGround)//如果停止后在天上
                {
                    rb.velocity = new Vector2(dashSpeed * horizontalMove, jumpForce);//冲锋后会有个小跳,可以跳上台子
                    SoundManager.instance.JumpAudio();
                }
            }
        }
    }
    public void Mobile_Dash()
    {
        if (Time.time >= (lastDash + dashCoolDown))//当前时间超过了最后一次执行时间+CD冷却时间
        {
            //可以执行Dash
            ReadyToDash();
        }
    }


    void Restart()//重置场景
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CherryCount()
    {
        Cherry += 1;
    }
}
