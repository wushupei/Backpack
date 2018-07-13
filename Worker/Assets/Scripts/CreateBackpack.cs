using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBackpack : MonoBehaviour //挂在KnapsackPanel上
{
    public Transform goodsPanel; //编辑器将goodsPanel拖进去
    public GameObject frame;
    void Start()
    {
        //实例化数据类
        ItemData itemData = FindObjectOfType<ItemData>();
        List<GameObject>[] slots = new List<GameObject>[goodsPanel.childCount];

        int iD = 0; //声明ID
        string path = ""; //路径
        string tag = ""; //标签
        ObjType type=ObjType.Weapon;  //类型
        for (int i = 0; i < slots.Length; i++)
        {           
            slots[i] = new List<GameObject>(); //将数组里的每个集合实例化           
            switch (i) //根据i的值决定ID和路径
            {
                case 0:
                    SetData(ref iD, ref path, ref tag,ref type, 1001, "Prefabs/Weapons/", "WeaponSlot", ObjType.Weapon);
                    break;
                case 1:
                    SetData(ref iD, ref path, ref tag, ref type, 2001, "Prefabs/Clothings/", "ClothingSlot", ObjType.Clothing);
                    break;
                case 2:
                    SetData(ref iD, ref path, ref tag, ref type, 3001, "Prefabs/Foods/", "FoodSlot", ObjType.Food);
                    break;
                case 3:
                    SetData(ref iD, ref path, ref tag, ref type, 4001, "Prefabs/Props/", "PropSlot", ObjType.Prop);
                    break;
            }
            for (int j = 0; j < 20; j++)
            {
                //为每个菜单创建出的物品槽物体作为该菜单子物体,并添加进集合中
                slots[i].Add(Instantiate(Resources.Load("Prefabs/BJ/Slot") as GameObject));         
                slots[i][j].transform.SetParent(goodsPanel.GetChild(i));
                slots[i][j].tag = tag; //添加标签
                //创建出黄框框,作为物品槽的子物体
                Instantiate(Resources.Load("Prefabs/BJ/Frame") as GameObject).transform.SetParent(slots[i][j].transform); 

                //根据ID获取数据实例
                Item item = itemData.AccordingToIDGetData(iD); 
                if (item != null)
                {
                    GameObject itemObj = Instantiate(Resources.Load(path + item.image) as GameObject); //创建图片实例                  
                    itemObj.transform.SetParent(slots[i][j].transform); //成为该物品框的子物体
                    itemObj.transform.position = Vector2.zero; //居中
                    itemObj.AddComponent<DragObj>().SelfData= item; //为图片对象添加可拖动脚本并保存自身信息
                    itemObj.GetComponent<DragObj>().type = type;
                }
                iD++; //ID会叠加
            }
        }
    }
    //设置参数ID和加载路径及标签
    void SetData(ref int iD, ref string path, ref string tag,ref ObjType type, int _iD, string _path, string _tag,ObjType _type)
    {
        iD = _iD;
        path = _path;
        tag = _tag;
        type = _type;
    }
}
