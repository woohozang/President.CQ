using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Me : User
{
    public Text nameText;
    public GameObject myDeck;

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

            GameObject temp = Instantiate(card, new Vector3(600 + (i*95),200,0), Quaternion.identity);

            temp.GetComponent<RectTransform>().SetParent(myDeck.GetComponent<RectTransform>());
            temp.GetComponentInChildren<Card>().name = s;
            temp.GetComponentInChildren<Card>().CardCode = s;
            temp.GetComponentInChildren<Card>().setCardImg();
            i++;
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
