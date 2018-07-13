using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ObjType //为物品声明不同类型
{
    Weapon,
    Clothing,
    Food,
    Prop,
}
/// <summary>
/// 改脚本将在物品被加载时挂在物品上
/// </summary>
public class DragObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public ObjType type;
    public Item SelfData; //保存自身数据
    Transform father; //声明亲爹
    CreateBackpack cbk; //声明黄框框
    public void OnBeginDrag(PointerEventData eventData) //拖拽第一帧
    {
        //记录亲爹信息
        transform.position = eventData.position;
        father = transform.parent;
        transform.SetParent(GameObject.Find("Canvas").transform); //直接认Canvas当爹(显现在所有UI上面)
        transform.localScale = Vector3.one; //变为标志尺寸

    }
    public void OnDrag(PointerEventData eventData) //拖拽中
    {
        transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData) //拖拽完
    {
        //拖入其它物品槽
        switch (FindObjectOfType<PanelSwitch>().panelState)
        {
            case PanelState.Weapon:
                GetIntoSlot("WeaponSlot", ObjType.Weapon);
                break;
            case PanelState.Clothing:
                GetIntoSlot("ClothingSlot", ObjType.Clothing);
                break;
            case PanelState.Food:
                GetIntoSlot("FoodSlot", ObjType.Food);
                break;
            case PanelState.Prop:
                GetIntoSlot("PropSlot", ObjType.Prop);
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData) //按下第一帧
    {
        //根据ID显示信息
        switch (SelfData.iD / 1000)
        {
            case 1:
                WeaponsItem weaItem = SelfData as WeaponsItem;
                ShowData(weaItem.name, new Color(1, 0, 0), weaItem.description, 0, "Attack", weaItem.attack.ToString());
                GameObject.Find("Violent").GetComponent<Text>().text = string.Format("{0:P0}", weaItem.violent);
                break;
            case 2:
                ClothingItem cloItem = SelfData as ClothingItem;
                ShowData(cloItem.name, new Color(1, 1, 0), cloItem.description, 1, "Defense", cloItem.defense.ToString());
                break;
            case 3:
                FoodsItem fooItem = SelfData as FoodsItem;
                ShowData(fooItem.name, new Color(0, 1, 0), fooItem.description, 2, "Cure", fooItem.cure.ToString());
                break;
            case 4:
                PropItem proItem = SelfData as PropItem;
                ShowData(proItem.name, new Color(0, 0, 1), proItem.description, 3, "", "");
                break;
        }
        FrameSwitch();
    }
    void GetIntoSlot(string tag, ObjType _type) //拖入物品槽
    {
        GameObject[] Slots = GameObject.FindGameObjectsWithTag(tag); //查找所有该标签的物品槽
        for (int i = 0; i < Slots.Length; i++)
        {
            RectTransform slot = Slots[i].GetComponent<RectTransform>(); //获取该物品槽        
            //如果鼠标进入该物品槽内,且是该物品类型
            if (slot.rect.Contains(Input.mousePosition - slot.position) && type == _type) 
            {
                for (int j = 0; j < slot.childCount; j++)
                {
                    //如果该物品槽下有物体
                    if (slot.GetChild(j).GetComponent<DragObj>())
                        //如果该物体类型和本物体相同且名字不同
                        if (slot.GetChild(j).GetComponent<DragObj>().type == type && slot.GetChild(j).name != name)
                        {
                            Transform obj = slot.GetChild(j); //获取该物体
                            //交换位置
                            obj.SetParent(father);
                            obj.localPosition = Vector3.zero;
                            //根据槽的不同改变大小
                            if (father.name == "WeaponSlot" || father.name == "ClothingSlot")
                                obj.localScale *= 0.7f;
                            else
                                obj.localScale = Vector3.one;
                        }
                }
                //物品进入该槽
                transform.SetParent(Slots[i].transform);
                transform.localPosition = Vector3.zero;
                break;
            }
            else
            {
                //回到亲爹那里
                transform.SetParent(father);
                transform.localPosition = Vector3.zero;
            }
        }
        //进入装备物品槽图片变小
        if (transform.parent.name == "WeaponSlot" || transform.parent.name == "ClothingSlot")
            transform.localScale *= 0.7f;
        FrameSwitch();
    }
    void ShowData(string name, Color color, string description, int index, string textName, string selfData) //根据参数显示各自特有信息
    {
        //特有信息       
        FindObjectOfType<PanelSwitch>().DataPanelSwitch(index); //切换菜单
        //只有前三个拥有特有信息
        if (index < 3)
            GameObject.Find(textName).GetComponent<Text>().text = selfData;
        //名字及名字颜色
        Text weaponsName = GameObject.Find("Name").GetComponent<Text>();
        weaponsName.text = name;
        weaponsName.color = color;
        //描述
        GameObject.Find("Description").GetComponent<Text>().text = description;
    }
    void FrameSwitch() //切换黄框框
    {
        //如果黄框框已被赋值,则禁用
        if (FindObjectOfType<CreateBackpack>().frame)
            FindObjectOfType<CreateBackpack>().frame.SetActive(false);
        //启用本格黄框框,并保存信息
        transform.parent.GetChild(0).gameObject.SetActive(true);
        FindObjectOfType<CreateBackpack>().frame = transform.parent.GetChild(0).gameObject;
    }
}
