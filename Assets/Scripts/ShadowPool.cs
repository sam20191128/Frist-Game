/****************************************************
	文件：ShadowPool.cs
	作者：Sam
	邮箱: 403117224@qq.com
	日期：2020/01/17 16:07   	
	功能：对象池
*****************************************************/


using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
	public static ShadowPool instance;//单例模式

	public GameObject shadowPrefab;//获取预制体来生成

	public int shadowCount;//整形，对象池里的数量

	private Queue<GameObject> availableObjects = new Queue<GameObject>();//队列

	void Awake()//赋值
	{
		instance = this;

		//初始化对象池
		FillPool();//调用
	}

	public void FillPool()//填满池子
	{
		//for循环
		for (int i = 0; i < shadowCount; i++)
		{
			var newShadow = Instantiate(shadowPrefab);//临时变量,新生成空物体
			newShadow.transform.SetParent(transform);//设置父子级，新生成的在子集里

			//取消启用，返回对象池
			ReturnPool(newShadow);//调用
		}
	}

	public void ReturnPool(GameObject gameObject)//返回对象池的方法
	{
		gameObject.SetActive(false);//取消启用

		availableObjects.Enqueue(gameObject);//放到队列末端中等待使用
	}

	public GameObject GetFormPool()//从对象池里拽出来一个
	{
		if (availableObjects.Count == 0)//对象池里不够用时,数量等于0时
		{
			FillPool();//再次填充
		}
		var outShadow = availableObjects.Dequeue();//从开头获得一个

		outShadow.SetActive(true);//启用

		return outShadow;//回到队列
	}




}
