using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        RectTransform t =GetComponent<RectTransform>();
        if (t.rect.Contains(Input.mousePosition - t.position)) //如果鼠标进入该物品槽内
        {
            print(123);
        }
    }
}
