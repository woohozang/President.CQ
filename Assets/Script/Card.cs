using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public string CardCode = "H13";
    public RectTransform rect;
    private Vector3 wasPosition;
    private bool isInDeck;
    public User user;
    public GameManager gameManager;
    public void OnBeginDrag(PointerEventData eventData)
    {
        wasPosition = rect.position;
        rect.localScale = new Vector3(1.3f, 1.3f, 1.3f);


    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rect.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        if (isInDeck)
        {
            user.Submit(CardCode);
            gameManager.Submitted(CardCode);
            //gameManager.ArrangeCard();
            rect.parent.gameObject.GetComponent<RectTransform>().SetParent(gameManager.deck.GetComponent<RectTransform>());
            rect.GetComponentInChildren<Card>().setCardImg();
        }
        else
        {
            rect.position = wasPosition;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name == "SubmitManager")
        {
            isInDeck = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SubmitManager")
        {
            isInDeck = false;
        }
    }
    private void Start()
    {
        user = GameObject.Find("Me").GetComponent<User>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void setCardImg() {
        gameObject.GetComponent<CardVO>().setCardImg(CardCode);

    }
}
