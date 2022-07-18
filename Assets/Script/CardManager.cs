using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Inst {get; private set;}
    void Awake() => Inst = this;

    [SerializeField] ItemSO itemSO;
    [SerializeField] List<Item> firstPlayer;
    [SerializeField] List<Item> secondPlayer;
    [SerializeField] List<Item> thirdPlayer;
    [SerializeField] List<Item> fourthPlayer;

    List<Item> itemBuffer;

    public Item PopItem(){
        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);
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
        for (int i = 1; i <= 4; i++)
		{
            for(int j=0; j<=12; j++)
			{
                AddCard(i);
			}
		}
        List<int> list = new List<int>() { 1, 2, 3, 4 };
        for(int i=0; i<=1; i++)
		{
            int randNum = Random.Range(0, list.Count);
            AddCard(list[randNum]);
            list.RemoveAt(randNum);
        }
    }

    void AddCard(int playerID){
        Item item = PopItem();
		if (playerID == 1)
		{
            firstPlayer.Add(item);
		}
        else if (playerID == 2)
        {
            secondPlayer.Add(item);
        }
        else if (playerID == 3)
        {
            thirdPlayer.Add(item);
        }
        else if (playerID == 4)
        {
            fourthPlayer.Add(item);
        }
    }
}
