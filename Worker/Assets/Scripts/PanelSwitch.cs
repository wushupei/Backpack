using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum PanelState //声明菜单不同状态
{
    Weapon,
    Clothing,
    Food,
    Prop,
}
public class PanelSwitch : MonoBehaviour //挂KnapsackPanel上
{
    public PanelState panelState = PanelState.Weapon;
    public GameObject DataPanel; //把信息面板拖进去
    public GameObject[] allPanel; //把所有背包菜单拖进去
    public GameObject[] dataPanel; //把所有物品个性信息panel拖进去
    GameObject newPanel; //当前菜单
    Vector3 offset; //声明偏差值
    void OnEnable()
    {
        //将此刻开着的菜单作为当前菜单
        for (int i = 0; i < allPanel.Length; i++)
        {
            if (allPanel[i].activeInHierarchy)
            {
                newPanel = allPanel[i];
                GetComponent<Image>().color = newPanel.GetComponent<Image>().color;
            }
        }
    }
    void OpenPanel(int index) //打开菜单
    {
        newPanel.SetActive(false); //关闭当前菜单
        allPanel[index].SetActive(true); //打开索引对应的菜单
        newPanel = allPanel[index]; //将刚打开的菜单设为当前菜单
        GetComponent<Image>().color = newPanel.GetComponent<Image>().color; //将当前菜单的颜色给背景
        DataPanel.SetActive(false); //关闭信息面板
        //如果黄框框已被赋值,则禁用
        if (FindObjectOfType<CreateBackpack>().frame)
            FindObjectOfType<CreateBackpack>().frame.SetActive(false);
    }
    public void OpenWeaponPanel() //打开武器菜单
    {
        OpenPanel(0);
        panelState = PanelState.Weapon;
    }
    public void OpenClothingPanel() //打开衣服菜单
    {
        OpenPanel(1);
        panelState = PanelState.Clothing;
    }
    public void OpenFoodPanel() //打开食物菜单
    {
        OpenPanel(2);
        panelState = PanelState.Food;
    }
    public void OpenPropPanel() //打开道具菜单
    {
        OpenPanel(3);
        panelState = PanelState.Prop;
    }
    public void DataPanelSwitch(int index)
    {
        DataPanel.SetActive(true); //关闭信息面板
        foreach (var item in dataPanel)
        {
            item.SetActive(false);
        }
        if (index < dataPanel.Length)
            dataPanel[index].SetActive(true);
    }

    public void BeginDrag() //拖动第一帧调用
    {
        //获取鼠标点下那一刻的偏差
        offset = Input.mousePosition - GetComponent<RectTransform>().position;
    }
    public void Drag() //拖动时持续调用
    {
        //保持偏差随鼠标移动
        GetComponent<RectTransform>().position = Input.mousePosition - offset;
    }
    public void EndDrag() //拖动结束时调用
    {

    }
}
