using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BuildManager : MonoBehaviour
{
    //��Inspector��彫Ԥ�������룬����д�����������
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    //��ʾ��ǰѡ�����̨��Ҫ�������̨��
    private TurretData selectedTurretData;
    //UI����ʾ��ѡ�����̨��д������̨��ѡ�񷽷���
    //ͨ��ע��������̨��Toggle�¼���ʶ���ĸ���ѡ����

    //��ʾ��ǰѡ�����̨�������е���Ϸ���壩
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
        upgradeCanvasAnimator = upgardeCanvas.GetComponent<Animator>();//�õ�״̬��
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //IsPointerOverGameObject��ʾ����Ƿ�����UI��
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//�����ĵ�ת��������
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));//���߼��õ��Ƿ���ײ��MapCube��
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();//�õ������mapCube
                    if (selectedTurretData != null && mapCube.turretGo == null)
                    {
                        //���Դ���
                        if (money > selectedTurretData.cost)
                        {
                            ChangeMoney(-selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData);
                        }
                        else
                        {
                            //��ʾǮ����
                            moneyAnimator.SetTrigger("Flicker");
                        }
                    }
                    else if (mapCube.turretGo != null)//����ϱ�����̨����ô�ж��Ƿ�����������
                    {
                        //��������
                        /*   if (mapCube.isUpgraded)
                           {
                               ShowUpgradeUI(mapCube.transform.position, true);
                           }
                           else
                           {
                               ShowUpgradeUI(mapCube.transform.position, false);
                           }*/
                        if (mapCube == selectedMapCube && upgardeCanvas.activeInHierarchy)//����ڶ��ε������̨�˲���UI�ļ���������true
                        {
                            StartCoroutine(HideUpgradeUI());//��UI���أ���Э�̵ķ�ʽ
                        }
                        else
                        {
                            //������ʾ����/���UI��壬�ڶ���������boolֵ���Ƿ�����̨�ж���������Բ���if�ж�ֱ�Ӵ�����
                            ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
                        }
                        selectedMapCube = mapCube;//�ѵ������̨�����������̨
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


    //���ڻ���������UI����ʾ������
    void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false)
    {
        StopCoroutine("HideUpgradeUI");//Ϊ�˷�ֹ��ͻ�����������һ����ͣ��Э�̷�����       
        upgardeCanvas.SetActive(false);
        upgardeCanvas.SetActive(true);//���û�����ʾ
        upgardeCanvas.transform.position = pos;//���û���λ��
        buttonUpgrade.interactable = !isDisableUpgrade;//�������߽���������ť
    }

    IEnumerator HideUpgradeUI()
    {
        upgradeCanvasAnimator.SetTrigger("Hide");
        //upgardeCanvas.SetActive(false);
        yield return new WaitForSeconds(0.8f);//��ʧ��Ч����������ȥ��������
        upgardeCanvas.SetActive(false);//���ص�ʱ����ֱ�Ӱѻ������ã���Ȼ���޷����Ž��õĶ�����
    }

    public void OnUpgradeButtonDown()//�������������ķ���
    {
        if (money >= selectedMapCube.turretData.costUpgraded)//���������������Ҫ��Ǯ
        {
            ChangeMoney(-selectedMapCube.turretData.costUpgraded);
            selectedMapCube.UpgradeTurret();
        }
        else
        {
            moneyAnimator.SetTrigger("Flicker");
        }
        StartCoroutine(HideUpgradeUI());//��UI���ص�   
    }

    public void OnDestoryButtonDwon()//���²�������ķ���
    {
        selectedMapCube.DestoryTurret();
        StartCoroutine(HideUpgradeUI());
    }
}
