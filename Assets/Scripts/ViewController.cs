using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    public float speed = 1;
    public float mousespeed = 60;

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");//得到鼠标滑轮的值
        Debug.Log(mouse);
        transform.Translate(new Vector3(h * speed, mouse * mousespeed, v * speed) * Time.deltaTime, Space.World);//Space.World按照世界空间移动

    }
}
