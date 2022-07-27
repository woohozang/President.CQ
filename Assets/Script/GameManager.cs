using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public List<User> userList;
    List<string> attenderList;

    List<string> CardDeck = new List<string>();

    public List<string> submittedCard = new List<string>();

    public GameObject deck;
    public GameObject card;

    private void init()
    {
        int i = 1;
        foreach(string s in attenderList) {
            if ("황윤겔라" == s) //when start test on PhotonNetwork, it must change PhotonNetwork.Nickname 
            {
                userList[0].Name = s;
                userList[0].SetName();
            }
            else {
                userList[i].Name = s;
                userList[i].SetName();
                i++;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        attenderList = GameObject.Find("RoomManager").GetComponent<RoomManager>().attenderList;
        init();
        initCardDeck();
        giveCardToUser();
        foreach (User u in userList)
        {
            u.printCardList();
        }
        userList[0].SpreadCard();
        
    }

    public void initCardDeck() {
        CardDeck.Clear();
        CardDeck.Add("D01");
        CardDeck.Add("D02");
        CardDeck.Add("D03");
        CardDeck.Add("D04");
        CardDeck.Add("D05");
        CardDeck.Add("D06");
        CardDeck.Add("D07");
        CardDeck.Add("D08");
        CardDeck.Add("D09");
        CardDeck.Add("D10");
        CardDeck.Add("D11");
        CardDeck.Add("D12");
        CardDeck.Add("D13");

        CardDeck.Add("H01");
        CardDeck.Add("H02");
        CardDeck.Add("H03");
        CardDeck.Add("H04");
        CardDeck.Add("H05");
        CardDeck.Add("H06");
        CardDeck.Add("H07");
        CardDeck.Add("H08");
        CardDeck.Add("H09");
        CardDeck.Add("H10");
        CardDeck.Add("H11");
        CardDeck.Add("H12");
        CardDeck.Add("H13");

        CardDeck.Add("C01");
        CardDeck.Add("C02");
        CardDeck.Add("C03");
        CardDeck.Add("C04");
        CardDeck.Add("C05");
        CardDeck.Add("C06");
        CardDeck.Add("C07");
        CardDeck.Add("C08");
        CardDeck.Add("C09");
        CardDeck.Add("C10");
        CardDeck.Add("C11");
        CardDeck.Add("C12");
        CardDeck.Add("C13");

        CardDeck.Add("S01");
        CardDeck.Add("S02");
        CardDeck.Add("S03");
        CardDeck.Add("S04");
        CardDeck.Add("S05");
        CardDeck.Add("S06");
        CardDeck.Add("S07");
        CardDeck.Add("S08");
        CardDeck.Add("S09");
        CardDeck.Add("S10");
        CardDeck.Add("S11");
        CardDeck.Add("S12");
        CardDeck.Add("S13");

        CardDeck.Add("JB");
        CardDeck.Add("JC");
    }
    public void Submitted(string cardcode)
    {
        Debug.Log(cardcode + "제출됨");
        submittedCard.Add(cardcode);

    }
    /*public void ArrangeCard()
    {
        GameObject temp = Instantiate(card, new Vector3(1000, 720, 0), Quaternion.identity);

        temp.GetComponent<RectTransform>().SetParent(deck.GetComponent<RectTransform>());
        temp.GetComponentInChildren<Card>().setCardImg();


    }*/
    [PunRPC]
    public void giveCard(string userName, string cardcode) {
        for (int i=0; i<userList.Count; i++) {
            if (userList[i].name == userName) {
                userList[i].userCard.Add(cardcode);                
            }
        }
    }
    [PunRPC]
    void giveCardToUser() {
        for (int i = 0; i<14; i++) { 
            int r = Random.Range(0, CardDeck.Count);
            giveCard(userList[0].name, CardDeck[r]);
            CardDeck.RemoveAt(r);
        }
        for (int i = 0; i < 14; i++)
        {
            int r = Random.Range(0, CardDeck.Count);
            giveCard(userList[1].name, CardDeck[r]);
            CardDeck.RemoveAt(r);
        }
        for (int i = 0; i < 13; i++)
        {
            int r = Random.Range(0, CardDeck.Count);
            giveCard(userList[2].name, CardDeck[r]);
            CardDeck.RemoveAt(r);
        }
        for (int i = 0; i < 13; i++)
        {
            int r = Random.Range(0, CardDeck.Count);
            giveCard(userList[3].name, CardDeck[r]);
            CardDeck.RemoveAt(r);
        }

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
