using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage = 50;

    public float speed = 20;

    public GameObject explosionEffectPrefab;

    private float distanceArriveTarget = 1.2f;//�����ж�

    private Transform target;

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }
    void Update()//�����ӵ����ƶ�
    {
        if (target == null)//���Ŀ�겻������,�����е��ӵ���������
        {
            Die();
            return;
        }
        transform.LookAt(target.position);//����Ŀ���λ��
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //ʹ�þ������ж��Ƿ�����
        Vector3 dir = target.position - transform.position;
        if (dir.magnitude < distanceArriveTarget)
        {
            //�õ��˵�Ѫ
            target.GetComponent<Enemy>().TakeDamage(damage);
            Die();
        }
    }

    //ʹ����ײ����ж��Ƿ�����
/*    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            col.GetComponent<Enemy>().TakeDamage(damage);
            Die();
        }
    }*/
    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);//������Ч
        Destroy(effect, 1);
        Destroy(this.gameObject);//���������Ϸ����,���ֻ��this��ֻ������Bullet���
    }
}
