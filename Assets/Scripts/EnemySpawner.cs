using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int CountEnemyAlive = 0;//��ǰ���˴���������Ĭ��Ϊ0
    public Wave[] waves;//�м�������
    public Transform START;//���ɵĿ�ʼλ��
    public float waveRate = 0.2f;//ÿ����һ����ͣ0.2��
    private Coroutine coroutine;

    void Start()
    {
       coroutine =  StartCoroutine(SpawnEnemy());
    }
    public void Stop()
    {
        StopCoroutine(coroutine);
    }
    //����Э��
    IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves)//����ÿһ�����ˣ���count���������ɣ���rate���зָ�
        {
            for (int i = 0; i < wave.count; i++)
            {   
                GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);//Quaternion.identity��ʾ����ת
                CountEnemyAlive++;
                if (i != wave.count - 1)//���������ǲ����Ⲩ���һ�����ˣ���������һ������ֱ���ߺ����Ǹ��ȴ�ʱ��
                    yield return new WaitForSeconds(wave.rate);//���rateʱ�����������һ��
            }
            while(CountEnemyAlive > 0)
            {
                yield return 0;//������е��˴��ڣ���ô��ͣ0֡
            }
            yield return new WaitForSeconds(waveRate);
        }
        while (CountEnemyAlive > 0)//��Ϸʤ���������ǵ��˶����ɡ�����Ҳ��������. ����0˵�����˻��д���ô��return 0����ͣ0֡
        {
            yield return 0;
        }
        GameManger.Instance.Win();
    }


}
