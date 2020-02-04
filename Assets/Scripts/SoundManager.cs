/****************************************************
	文件：SoundManager.cs
	作者：Sam
	邮箱: 403117224@qq.com
	日期：2020/02/04 0:28   	
	功能：音效管理
*****************************************************/


using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;//生成静态类
    public AudioSource audioSource;
    [SerializeField]//private
    private AudioClip jumpAudio, hurtAudio, cherryAudio, gemAudio, dashAudio;


    private void Awake()//代码一开始运行就生成一个单例
    {
        instance = this;
    }

    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }
    public void HurtAudio()
    {
        audioSource.clip = hurtAudio;
        audioSource.Play();
    }    
    public void CherryAudio()
    {
        audioSource.clip = cherryAudio;
        audioSource.Play();
    }   
    public void GemAudio()
    {
        audioSource.clip = gemAudio;
        audioSource.Play();
    }
    public void DashAudio()
    {
        audioSource.clip = dashAudio;
        audioSource.Play();
    }


}
