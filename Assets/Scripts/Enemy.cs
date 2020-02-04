/****************************************************
	文件：Enemy.cs
	作者：Sam
	邮箱: 403117224@qq.com
	日期：2020/01/22 2:42   	
	功能：父级类,包含青蛙、鹰
*****************************************************/


using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator Anim;
    protected AudioSource deathAudio;


    protected virtual void Start()//virtual虚拟，随时可变，父子级可互相改变
    {
        Anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    public void Death()
    {
        GetComponent<Collider2D>().enabled = false;//禁用碰撞,这样只会碰撞一次
        Destroy(gameObject);//摧毁
    }

    public void JumpOn()//公开的函数，被角色跳在身上
    {
        Anim.SetTrigger("death");
        deathAudio.Play();
    }
}
