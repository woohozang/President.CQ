using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Other :  User
{
    public Text nameText;

    public override string Name { get; set ; }

    public override void Pass()
    {
    }

    public override void SetName()
    {
        nameText.text = Name;

    }

    public override void Submit(string cardcode)
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
