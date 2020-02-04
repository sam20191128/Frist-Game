/****************************************************
	文件：EnterHouse.cs
	作者：Sam
	邮箱: 403117224@qq.com
	日期：2020/02/04 0:21   	
	功能：进入房屋
*****************************************************/


using UnityEngine;
using UnityEngine.SceneManagement;

//public class EnterHouse : MonoBehaviour
//{
//    public void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//进入下一个场景，编号+1
//        }
//    }
//}


public class EnterHouse : MonoBehaviour
{
    public void enterHouse()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//进入下一个场景，编号+1
    }
}

