using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Me : User
{
    public Text nameText;

    public GameObject card;
    public override string Name { get; set; }

    public override void Pass()
    {
    }

    public override void SetName()
    {
        nameText.text = Name;
    }

    public override void SpreadCard()
    {
        int i = 0;
        foreach (string s in userCard) {
            GameObject temp = Instantiate(card, new Vector3(-620 + (i*50),-400,0), Quaternion.identity);
            temp.GetComponent<Card>().name = s;
            temp.GetComponent<Card>().CardCode = s;
            temp.GetComponent<Card>().setCardImg();

        }
    }

    public override void Submit(string cardcode)
    {
        Debug.Log(cardcode+"제출");
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
