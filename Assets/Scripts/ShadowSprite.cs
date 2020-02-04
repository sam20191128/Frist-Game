/****************************************************
	文件：ShadowSprite.cs
	作者：Sam
	邮箱: 403117224@qq.com
	日期：2020/02/04 0:27   	
	功能：幻影
*****************************************************/


using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;//用private方便日后更换Player也能自动获得

    private SpriteRenderer thisSprite;//当前的图像
    private SpriteRenderer playerSprite;//player的图像

      private Color color;

    [Header("时间控制参数")]
    public float activeTime;//浮点型。显示时间
    public float activeStart;//开始显示的时间点

    [Header("不透明度控制")]
    private float alpha;//浮点型。不透明度
    public float alphaSet;//不透明度的初始值
    public float alphaMultiplier;//透明度乘数


    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprite = GetComponent<SpriteRenderer>();//获得当前物体的Sprite
        playerSprite = player.GetComponent<SpriteRenderer>();//获得player的Sprite

        alpha = alphaSet;//初始值给alpha

        thisSprite.sprite = playerSprite.sprite; //获取player的sprite

        transform.position = player.position;//获取player的坐标
        transform.localScale = player.localScale;//获取player的大小
        transform.rotation = player.rotation;//获取player的旋转

        activeStart = Time.time;//开始的时间点等于系统的时间
    }





    void Update()
    {
        alpha *= alphaMultiplier;//透明度乘等于设置的alphaMultiplier，越乘越小

        color = new Color(0.5f, 0.5f, 1, alpha);
            
        thisSprite.color = color;

        if (Time.time>= activeStart + activeTime)//当时间超过显示时间+开始时间(应该显示的时间)
        {
            //返回对象池
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}
