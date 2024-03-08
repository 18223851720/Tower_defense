using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BuildManager : MonoBehaviour
{
    //在Inspector面板将预制体拉入，并填写其它相关数据
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    //表示当前选择的炮台（要建造的炮台）
    private TurretData selectedTurretData;
    //UI上显示和选择的炮台，写三个炮台的选择方法，
    //通过注册三个炮台的Toggle事件来识别哪个被选择了

    //表示当前选择的炮台（场景中的游戏物体）
    private MapCube selectedMapCube;

    public Text moneyText;

    public Animator moneyAnimator;

    private int money = 1000;

    public GameObject upgardeCanvas;

    public Animator upgradeCanvasAnimator;

    public Button buttonUpgrade;

    void ChangeMoney(int change = 0)
    {
        money += change;
        moneyText.text = "$" + money;
    }
    void Start()
    {
        upgradeCanvasAnimator = upgardeCanvas.GetComponent<Animator>();//得到状态机
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //IsPointerOverGameObject表示鼠标是否按在了UI上
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//把鼠标的点转化成射线
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));//射线检测得到是否碰撞到MapCube上
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();//得到点击的mapCube
                    if (selectedTurretData != null && mapCube.turretGo == null)
                    {
                        //可以创建
                        if (money > selectedTurretData.cost)
                        {
                            ChangeMoney(-selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData);
                        }
                        else
                        {
                            //提示钱不够
                            moneyAnimator.SetTrigger("Flicker");
                        }
                    }
                    else if (mapCube.turretGo != null)//如果上边有炮台，那么判断是否做升级处理
                    {
                        //升级处理
                        /*   if (mapCube.isUpgraded)
                           {
                               ShowUpgradeUI(mapCube.transform.position, true);
                           }
                           else
                           {
                               ShowUpgradeUI(mapCube.transform.position, false);
                           }*/
                        if (mapCube == selectedMapCube && upgardeCanvas.activeInHierarchy)//如果第二次点击此炮台了并且UI的激活属性是true
                        {
                            StartCoroutine(HideUpgradeUI());//将UI隐藏，用协程的方式
                        }
                        else
                        {
                            //否则显示升级/拆除UI面板，第二个参数的bool值与是否有炮台判断相符，所以不再if判断直接传即可
                            ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
                        }
                        selectedMapCube = mapCube;//把点击的炮台赋给点击的炮台
                    }
                }
            }
        }
    }

    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }
    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }
    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }
    }


    //关于画布的升级UI的显示和隐藏
    void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false)
    {
        StopCoroutine("HideUpgradeUI");//为了防止冲突故在上面加上一个暂停的协程方法。       
        upgardeCanvas.SetActive(false);
        upgardeCanvas.SetActive(true);//设置画布显示
        upgardeCanvas.transform.position = pos;//设置画布位置
        buttonUpgrade.interactable = !isDisableUpgrade;//开启或者禁用升级按钮
    }

    IEnumerator HideUpgradeUI()
    {
        upgradeCanvasAnimator.SetTrigger("Hide");
        //upgardeCanvas.SetActive(false);
        yield return new WaitForSeconds(0.8f);//消失的效果结束后再去调用下面
        upgardeCanvas.SetActive(false);//隐藏的时候不能直接把画布禁用，不然就无法播放禁用的动画了
    }

    public void OnUpgradeButtonDown()//按下升级触发的方法
    {
        if (money >= selectedMapCube.turretData.costUpgraded)//如果大于升级所需要的钱
        {
            ChangeMoney(-selectedMapCube.turretData.costUpgraded);
            selectedMapCube.UpgradeTurret();
        }
        else
        {
            moneyAnimator.SetTrigger("Flicker");
        }
        StartCoroutine(HideUpgradeUI());//把UI隐藏掉   
    }

    public void OnDestoryButtonDwon()//按下拆除触发的方法
    {
        selectedMapCube.DestoryTurret();
        StartCoroutine(HideUpgradeUI());
    }
}
