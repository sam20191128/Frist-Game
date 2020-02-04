/****************************************************
	文件：Parallax.cs
	作者：Sam
	邮箱: 403117224@qq.com
	日期：2020/02/04 0:26   	
	功能：摄像机Y轴锁定
*****************************************************/


using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform Cam;
    public float moveRate;
    private float startPointX, startPointY;
    public bool lockY;//默认false


    void Start()
    {
        startPointX = transform.position.x;
        startPointY = transform.position.y;

    }

    void Update()
    {
        if (lockY)//锁定Y轴，左右移动
        {
            transform.position = new Vector2(startPointX + Cam.position.x * moveRate, transform.position.y);
        }
        else//可上下左右移动
        {
            transform.position = new Vector2(startPointX + Cam.position.x * moveRate, startPointY+Cam.position.y*moveRate);
        }
    }
}
