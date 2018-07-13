using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodsItem : Item //食物数据类
{
    public int cure { get; set; }
    public FoodsItem(int _iD, string _name, string _description, int _cure, string _image)
    {
        iD = _iD;
        name = _name;
        description = _description;
        cure = _cure;
        image = _image;
    }
}
