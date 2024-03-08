using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 10;
    public float hp = 150;
    private float totalHp;
    public GameObject exploisonEffect;
    private Slider hpSlider;//Ѫ��
    private Transform[] positions;
    private int index = 0;//Ĭ�ϵ�λ��

    // Start is called before the first frame update
    void Start()
    {
        positions = Waypoints.positions;//��ȡ��С�����ߵ�·����
        totalHp = hp;
        hpSlider = GetComponentInChildren<Slider>();//����������Ѱ��Slider����װ����
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (index > positions.Length - 1) return;//���������һ��λ��
        //��Ŀ��λ�� - ��ǰλ�ã��õ�һ������.��λ��ÿ���ƶ�1��ȡ�õ�λ����֮����������
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        //�ж�������ľ��룬Ҳ���ǿ�������û�е���Ŀ��λ�ã�ȡ��������λ���Ƿ�С��һ������
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            index++;
        }
        if (index > positions.Length - 1)//���������һ��λ��
        {
            ReachDestination();
        }
    }
    //�����յ㣬��Ϸ��ʧ����
    void ReachDestination()
    {
        GameManger.Instance.Failed();
        GameObject.Destroy(this.gameObject);
    }

    //���������
    void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }

    //��ʾ�ܵ����˺�
    public void TakeDamage(float damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        hpSlider.value = (float)hp / totalHp;//�ٷֱȼ���Ѫ��
        if (hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GameObject effect = GameObject.Instantiate(exploisonEffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
    }
}
