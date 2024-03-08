using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    public static Transform[] positions;
    //脚本被载入时调用(最早的执行函数)
    void Awake()
    {
        //注意这里如果用transform.GetComponent这种方法，会把自身的组件也带上，所以要用下面的方式0
        positions = new Transform[transform.childCount];//先从孩子点位里获得数组大小
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = transform.GetChild(i);//根据索引来得到每一个子位置
        }
    }
}
