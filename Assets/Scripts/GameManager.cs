using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public GameObject endUI;
    public Text endMessage;

    public static GameManger Instance;
    private EnemySpawner enemySpawner;
    void Awake()
    {
        Instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }
    public void Win()
    {
        endUI.SetActive(true);//设置为true后，它下面的Animator动画已经勾上了，会自动播放
        endMessage.text = "胜 利";
    }
    public void Failed()
    {
        enemySpawner.Stop();//停止生成敌人
        endUI.SetActive(true);
        endMessage.text = "失 败";
    }

    public void OnButtonRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//重新加载当前场景
    }
    public void OnButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
}
