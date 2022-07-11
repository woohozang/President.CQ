using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Inst {get; private set;}
    void Awake() => Inst = this;

    [SerializeField] ItemSO itemSO;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] List<Prac_Card> myCards;
    [SerializeField] List<Prac_Card> otherCards;

 
    List<Item> itemBuffer;
    int itemCount = 0;

    public Item PopItem(){
        if (itemBuffer.Count == 0 && itemCount==0)
            SetupItemBuffer();
        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);
        itemCount = +1;
        return item;
    }

    void SetupItemBuffer() {
        itemBuffer = new List<Item>();
        for(int i=0; i<itemSO.items.Length; i++){
            Item item = itemSO.items[i];
            itemBuffer.Add(item);
        }
        
        for(int i=0; i<itemBuffer.Count; i++){
            int rand = Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }
    }

    void Start()
    {
        SetupItemBuffer();
        for (int i=0; i<=54; i++)
        {
            if(i>=0 && i<=13) {
                AddCard(true);
            }
            else if(i>= 14 && i <= 54)
            {
                AddCard(false);
            }
        }

    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Keypad1))
            AddCard(true);
        if(Input.GetKeyDown(KeyCode.Keypad2))
            AddCard(false);
    }

    void AddCard(bool isMine){
        var cardObject = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
        var card = cardObject.GetComponent<Prac_Card>();
        card.Setup(PopItem(), isMine);
        (isMine ? myCards : otherCards).Add(card);

        SetOriginOrder(isMine);
    }

    void SetOriginOrder(bool isMine)
    {
        int count = isMine ? myCards.Count : otherCards.Count;
        for (int i=0; i<count; i++)
        {
            var targetCard = isMine ? myCards[i] : otherCards[i];
            targetCard?.GetComponent<Prac_Order>().SetOriginOrder(i);
        }
    }
}
