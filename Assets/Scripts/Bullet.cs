using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage = 50;

    public float speed = 20;

    public GameObject explosionEffectPrefab;

    private float distanceArriveTarget = 1.2f;//距离判定

    private Transform target;

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }
    void Update()//控制子弹的移动
    {
        if (target == null)//如果目标不存在了,飞行中的子弹自行销毁
        {
            Die();
            return;
        }
        transform.LookAt(target.position);//面向目标的位置
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //使用距离检测判定是否命中
        Vector3 dir = target.position - transform.position;
        if (dir.magnitude < distanceArriveTarget)
        {
            //让敌人掉血
            target.GetComponent<Enemy>().TakeDamage(damage);
            Die();
        }
    }

    //使用碰撞检测判定是否命中
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
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);//增加特效
        Destroy(effect, 1);
        Destroy(this.gameObject);//销毁这个游戏物体,如果只是this，只是销毁Bullet组件
    }
}
