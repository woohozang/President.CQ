using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardMgr : MonoBehaviour
{
    public Button StartBtn;
    public Sprite[] cards;
    public Image image;


    //public List<Image> ShuffleList = new List<Image>() {"card1"};





void Start()
    {
        Init_UI();
    }
   
    private void Init_UI()
    {
        StartBtn.onClick.RemoveAllListeners();
        StartBtn.onClick.AddListener(ShuffleCards);
         
    }


    // Update is called once per frame
    void Update()
    {

    }

    void ShuffleCards()
    {
        int index = Random.Range(0, cards.Length);
        Sprite select = cards[index];
        image.sprite = select;
        








        //for (int i = 0; i < 3; i++)
        //{
        //    int rand = Random.Range(0, GachaList.Count);
        //    print(GachaList[rand]);
        //    GachaList.RemoveAt(rand);
        //}
    }
}