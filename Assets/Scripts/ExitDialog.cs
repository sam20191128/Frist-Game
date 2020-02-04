/****************************************************
	文件：ExitDialog.cs
	作者：Sam
	邮箱: 403117224@qq.com
	日期：2020/02/04 0:22   	
	功能：离开对话框
*****************************************************/


using UnityEngine;

public class ExitDialog : MonoBehaviour
{
    public GameObject exitDialog;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            exitDialog.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            exitDialog.SetActive(false);
        }
    }
}
