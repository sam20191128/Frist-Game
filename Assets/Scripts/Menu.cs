/****************************************************
	文件：Menu.cs
	作者：Sam
	邮箱: 403117224@qq.com
	日期：2020/02/04 0:23   	
	功能：菜单
*****************************************************/


using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject pauseGame;//暂停游戏
    public AudioMixer audioMixer;//音量调节

    //public Slider slider;//没有Dynamic float的情况使用


    public GameObject resumeGame;//继续游戏


    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()//退出游戏
    {
        Application.Quit();
    }

    public void UIEnable()//UI显示激活
    {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
    }

    public void PauseGame()//暂停游戏
    {
        pauseGame.SetActive(true);
        Time.timeScale = 0f;//时间暂停
    }
    public void ResumeGame()//恢复游戏
    {
        pauseGame.SetActive(false);
        Time.timeScale = 1f;//结束时间暂停
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);//获取valume的临时变量
    }

    //public void SetVolume()//没有Dynamic float的情况下使用
    //{
    //    audioMixer.SetFloat("MainVolume", slider.value);
    //}

}
