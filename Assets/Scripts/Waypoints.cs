using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    public static Transform[] positions;
    //�ű�������ʱ����(�����ִ�к���)
    void Awake()
    {
        //ע�����������transform.GetComponent���ַ����������������Ҳ���ϣ�����Ҫ������ķ�ʽ0
        positions = new Transform[transform.childCount];//�ȴӺ��ӵ�λ���������С
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = transform.GetChild(i);//�����������õ�ÿһ����λ��
        }
    }
}
