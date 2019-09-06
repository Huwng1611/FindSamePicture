using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour {
    [SerializeField]
    private Transform panel;
    [SerializeField]
    private GameObject buttons;
    [SerializeField]
    private int btnAmount;
    GameObject btn;
	void Awake()
    {
        for (int i = 0; i < btnAmount; i++)
        {
            //tao 8 cai buttons
            //gan 8 cai vao` panel
            btn = Instantiate(buttons);
            btn.name = "" + i;
            btn.transform.SetParent(panel, false);
        }
    }
}
