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

        if(DataController.Instance.gameData.AutoLogin ==false)
		{
            toggle.isOn = false;
               
		}
        else if(DataController.Instance.gameData.AutoLogin == true)
		{
            toggle.isOn = true;
		}
        toggle.onValueChanged.AddListener((bool isOn) =>
        {
           if (isOn)
           {
               DataController.Instance.gameData.AutoLogin = true;
           }
           else
           {
               DataController.Instance.gameData.AutoLogin = false;
           }
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
