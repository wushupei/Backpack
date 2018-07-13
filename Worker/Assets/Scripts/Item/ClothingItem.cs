using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingItem : Item //衣服数据类
{
    public int defense { get; set; }
    public ClothingItem(int _iD, string _name, string _description, int _defense, string _image)
    {
        iD = _iD;
        name = _name;
        description = _description;
        defense = _defense;
        image = _image;
    }
}
