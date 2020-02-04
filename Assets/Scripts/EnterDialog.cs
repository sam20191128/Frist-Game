/****************************************************
	文件：EnterDialog.cs
	作者：Sam
	邮箱: 403117224@qq.com
	日期：2020/02/04 0:20   	
	功能：进入对话框
*****************************************************/


using UnityEngine;

public class EnterDialog : MonoBehaviour
{
    public GameObject enterDialog;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enterDialog.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enterDialog.SetActive(false);
        }
    }
}
