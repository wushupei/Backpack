using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropItem : Item //道具数据类
{
    public PropItem(int _iD, string _name, string _description, string _image)
    {
        iD = _iD;
        name = _name;
        description = _description;
        image = _image;
    }
}
