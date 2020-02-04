/****************************************************
	文件：Cherry.cs
	作者：Sam
	邮箱: 403117224@qq.com
	日期：2020/02/04 0:19   	
	功能：樱桃消失
*****************************************************/


using UnityEngine;

public class Cherry : MonoBehaviour
{
    public void Death()
    {
        FindObjectOfType<FinalMovement>().CherryCount();
        Destroy(gameObject);
    }
}
