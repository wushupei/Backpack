using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsItem :Item //武器数据类
{
    public int attack { get; set; }
    public float violent { get; set; }
    public WeaponsItem(int _iD, string _name, string _description, int _attack, float _violent, string _image)
    {
        iD = _iD;
        name = _name;
        description = _description;
        attack = _attack;
        violent = _violent;
        image = _image;     
    }
}
