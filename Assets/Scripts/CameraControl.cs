/****************************************************
	文件：CameraControl.cs
	作者：Sam
	邮箱: 403117224@qq.com
	日期：2020/02/04 0:18   	
	功能：摄像机跟随
*****************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        transform.position = new Vector3(player.position.x, 0, -10f);
    }
}
