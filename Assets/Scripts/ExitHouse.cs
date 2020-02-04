/****************************************************
	文件：ExitHouse.cs
	作者：Sam
	邮箱: 403117224@qq.com
	日期：2020/02/04 0:22   	
	功能：离开房屋
*****************************************************/


using UnityEngine;
using UnityEngine.SceneManagement;

//public class ExitHouse : MonoBehaviour
//{
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);//进入下一个场景，编号+1
//        }
//    }
//}

public class ExitHouse : MonoBehaviour
{
	public void exitHouse()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);//进入下一个场景，编号+1
	}
}
