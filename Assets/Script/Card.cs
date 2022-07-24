using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public string CardCode = "HK";
    public RectTransform rect;
    private Vector3 wasPosition;
    private bool isInDeck;
    public User user;


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
            Destroy(this.gameObject);
        }
        else
        {
            rect.position = wasPosition;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name == "SubmitManager")
        {
            isInDeck = true;
            Debug.Log("¼­ºê¹Ô¸Þ´ÏÀú ´êÀ½");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SubmitManager")
        {
            isInDeck = false;
            Debug.Log("¼­ºê¹Ô¸Þ´ÏÀú ¶³¾îÁü");
        }
    }
    private void Start()
    {
        user = GameObject.Find("Me").GetComponent<User>();
    }
}
