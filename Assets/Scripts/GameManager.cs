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
        endUI.SetActive(true);//����Ϊtrue���������Animator�����Ѿ������ˣ����Զ�����
        endMessage.text = "ʤ ��";
    }
    public void Failed()
    {
        enemySpawner.Stop();//ֹͣ���ɵ���
        endUI.SetActive(true);
        endMessage.text = "ʧ ��";
    }

    public void OnButtonRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//���¼��ص�ǰ����
    }
    public void OnButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
}
