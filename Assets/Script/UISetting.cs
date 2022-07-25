using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    public Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        if(DataController.Instance.gameData.AutoLogin ==0)
		{
            toggle.isOn = false;
		}
        else if(DataController.Instance.gameData.AutoLogin == 1)
		{
            toggle.isOn = true;
		}
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
