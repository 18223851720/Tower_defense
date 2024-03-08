using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private List<GameObject> enemys = new List<GameObject>();
    //���뵽�����¼���ֻ�������Ƚ��빥����Χ�ڵĵ���

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }

    public float attackRateTime = 1; //�����빥��һ�Σ������ܵ���
    private float timer = 0;//���ü�ʱ���ж�ʱ���Ƿ�

    public GameObject bulletPrefab;
    public Transform firePosition;//�������ڿ�λ��
    public Transform head;//������ͷ��

    public bool useLaser = false;//��ʾ�Ƿ�ʹ�ü���

    public float damageRate = 70;//������˺�ֵ/��

    public LineRenderer laserRenderer;

    public GameObject laserEffect;


    void Start()
    {
        timer = attackRateTime;//�շ��ֵ�һ�ξ��ܹ���
    }

    void Update()
    {
        //��ͷ��λ���������
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;//ʹ��̨����˵�y�ᱣ��һ��
            head.LookAt(targetPosition);
        }
        if (useLaser == false)
        {
            //�ȵ����귽���ٽ��й���
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer >= attackRateTime)//����ео����Ҵ��ڹ���ʱ�䣬��timer���㣬��ʼ���й���
            {
                //timer -= attackRateTime;//������������ӵ���BUG
                timer = 0;
                Attcak();
            }
        }
        else if (enemys.Count > 0)//����һ���жϣ����������е���
        {
            if (laserRenderer.enabled == false)
                laserRenderer.enabled = true;
                laserEffect.SetActive(true);//��Ч�������
            if (enemys[0] == null)
            {
                UpdateEnemys();//�����һ�ŵ���Ϊ�գ��͸��µ���
            }
            if (enemys.Count > 0)//��������˺����ж��Ƿ��е��ˡ�
            {
                laserRenderer.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
                enemys[0].GetComponent<Enemy>().TakeDamage(damageRate * Time.deltaTime);
                laserEffect.transform.position = enemys[0].transform.position;//��Чλ���ڵ�����
                Vector3 pos = transform.position;
                pos.y = enemys[0].transform.position.y;
                laserEffect.transform.LookAt(pos);
            }
        }
        else
        {
            laserEffect.SetActive(false);//��Ч�������
            laserRenderer.enabled = false;//�뿪��Ұ�ˣ���ʹ�õ�ʱ��Ѽ������
        }

    }
    void Attcak()
    {
        if (enemys[0] == null)//���ڿգ�����ζ�ż������Ѿ��п�Ԫ����
        {
            UpdateEnemys();
        }
        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            //ͨ��bullet���GameObject�����������ΪBullet�ű����������Ȼ���ڵ��ô˶���ķ�����
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);//����Ŀ��Ĭ��ʹ�ü����еĵ�һ��Ԫ��
        }
        else
        {
            timer = attackRateTime;//����������֮Ϊ���Թ�����״̬
            //�����ڵ�״̬���´������˾Ϳ���ֱ�ӽ��й�����
        }

    }
    void UpdateEnemys()//�Ƴ�����Ϊ�յ�Ԫ��
    {
        // enemys.RemoveAll(null); //ʹ��RemoveALL����ʱ���ܸ�һ����ֵ
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < enemys.Count; index++)
        {
            if (enemys[index] == null)
            {
                emptyIndex.Add(index);
            }
        }

        for (int i = 0; i < emptyIndex.Count; i++)
        {
            enemys.RemoveAt(emptyIndex[i] - i);//��Ϊÿɾ��һ������ߵ�ֵ�ͻ���ǰ�ƶ�һ��������Ҫ��һ�´��¶�λ��"-i"����
        }
    }
}
