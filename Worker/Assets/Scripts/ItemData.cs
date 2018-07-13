using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;

public class ItemData : MonoBehaviour //数据读取类,随便挂
{
    JsonData itemData; //声明Json
    List<Item> itemList = new List<Item>(); //声明一个集合来保存所有json数据
    void Awake()
    {
        //读取本地Json文件内容
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();
    }

    void ConstructItemDatabase() //读取Json每一个数据创建一个对象,并将对象添加进集合中
    {
        string path = Application.dataPath + "!/assets/文件.json";
        for (int i = 0; i < itemData.Count; i++)
        {
            int iD = (int)itemData[i]["ID"]; //获取ID
            switch (iD / 1000) //根据ID决定创建什么数据
            {
                case 1: //武器
                    //(太长了,一段拆成三段写)
                    itemList.Add(new WeaponsItem((int)itemData[i]["ID"], itemData[i]["Name"].ToString(),
                    itemData[i]["Description"].ToString(), (int)itemData[i]["Attack"],
                    float.Parse(itemData[i]["Violent"].ToString()), itemData[i]["Image"].ToString()));
                    break;
                case 2: //衣服
                    itemList.Add(new ClothingItem((int)itemData[i]["ID"], itemData[i]["Name"].ToString(),
                    itemData[i]["Description"].ToString(), (int)itemData[i]["Defense"], itemData[i]["Image"].ToString()));
                    break;
                case 3: //食物
                    itemList.Add(new FoodsItem((int)itemData[i]["ID"], itemData[i]["Name"].ToString(),
                    itemData[i]["Description"].ToString(), (int)itemData[i]["Cure"], itemData[i]["Image"].ToString()));
                    break;
                case 4: //道具
                    itemList.Add(new PropItem((int)itemData[i]["ID"], itemData[i]["Name"].ToString(),
                    itemData[i]["Description"].ToString(), itemData[i]["Image"].ToString()));
                    break;
            }
        }
        //WeaponsItem wp = itemList[1] as WeaponsItem;
        //print(wp.attack);

    }
    public Item AccordingToIDGetData(int _iD) //根据ID拿取数据
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (_iD == itemList[i].iD) //如果有这个ID
            {
                return itemList[i]; //返回该ID的数据
            }
        }
        return null; //没有则返回空
    }
}
