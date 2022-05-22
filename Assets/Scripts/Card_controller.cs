using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card_controller : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    public bool can_be_clicked = true;
    public bool is_flipped = false;
    private Vector3 scaleValue = new Vector3(0.2f, 0.2f, 0.2f);
    private Vector3 ScaleVector = new Vector3(0.5624999f, 0.5624999f, 0.5624999f);
    private bool is_Dragging = false;
    public bool is_animating = false;
    private Vector2 StartDragPos;
    private Vector2 StartCenterDrag;
    private GameObject CardStart;
    private RectTransform rectTransform;
    private float r=100;
    public Canvas canvas;
    public string Description;

    Coroutine MoveBackCoroutine;

    public void Start()
    {
        CardStart = GameObject.Find("CardStartPos");
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Card_Canvas_front").GetComponent<Canvas>();
    }


    public void Update()
    {
        
    }

    public void StartAnimatingText()
    {
        Camera.main.GetComponent<GameController>().StartTextAnimation();
    }

    public void OnDrag(PointerEventData eventData)
    {


        Debug.Log("DRAGG");
        Debug.Log(is_flipped);
        Debug.Log(can_be_clicked);
        if (gameObject.tag == "FrontCard"&&is_flipped&&can_be_clicked&&!is_animating)
        {
            if (!is_Dragging)
            {
                
                is_Dragging = true;
                StartDragPos =eventData.delta / canvas.scaleFactor;
                StartCenterDrag = CardStart.transform.position;
            }
            Debug.Log("QQQ :" + eventData.delta / canvas.scaleFactor);
            
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
                
                rectTransform.rotation = new Quaternion(0, 0, Mathf.Abs(gameObject.transform.position.x- StartCenterDrag.x)/(Screen.width*3)*Mathf.Sign(gameObject.transform.position.x - StartCenterDrag.x)*(-1), 1);
            //gameObject.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        is_Dragging = false;
        //if (!is_Really_Dragging)
        //OnPointerClick(eventData);
        Debug.Log(gameObject.transform.position.x);
        Debug.Log(CardStart.transform.position.x);
        Debug.Log(Mathf.Abs(gameObject.transform.position.x - CardStart.transform.position.x) / Screen.width);
        Debug.Log(is_Dragging);
        if (Mathf.Abs(gameObject.transform.position.x - CardStart.transform.position.x) / Screen.width <= 0.2f && can_be_clicked&& is_flipped&&!is_animating)
            StartCoroutine(CardMoveBackToPoint());
        else if (gameObject.transform.position.x - CardStart.transform.position.x<0 && can_be_clicked&& is_flipped && !is_animating)
        {
            can_be_clicked = true;
            Camera.main.GetComponent<GameController>().LeftButtonPressed();
        }
        else if (gameObject.transform.position.x - CardStart.transform.position.x >0 && can_be_clicked&& is_flipped && !is_animating)
        {
            can_be_clicked = true;
            Camera.main.GetComponent<GameController>().RightButtonPressed();
        }
        can_be_clicked = true;
        
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        

        if ((can_be_clicked&&gameObject.tag=="FrontCard"&&is_flipped&&!is_Dragging&&!is_animating))
        {
            is_animating = true;
            GameObject.Find("Card_description").GetComponent<Animator>().Play("Description_appear", -1, 0);
            GameObject.Find("Description_Fade").GetComponent<Animator>().Play("Description_fade_appear", -1, 0);
            GameObject.Find("Buttons").GetComponent<Animator>().Play("Button_disappear", -1, 0);
            GameObject.Find("DescriptionText").GetComponent<Text>().text = Description;
            StartCoroutine(CardMoveToDescriptionPoint());
            can_be_clicked = false;
        }
    }

    IEnumerator CardMoveToDescriptionPoint()
    {
        Vector2 targetVector = GameObject.Find("CardPlace").transform.position;

        is_animating = true;
        while (Vector2.Distance(gameObject.transform.position, targetVector) > 15 || Vector2.Distance(transform.localScale,scaleValue)>0.005f)
        {
            gameObject.transform.position = Vector2.Lerp(transform.position, targetVector, Time.deltaTime * 4);
            gameObject.transform.localScale = Vector2.Lerp(transform.localScale, scaleValue, Time.deltaTime * 5);
            yield return null;
        }
    }

    IEnumerator CardMoveBackToPoint()
    {
        Vector2 targetVector = GameObject.Find("CardStartPos").transform.position;

        is_animating = true;
        while (Vector2.Distance(gameObject.transform.position, targetVector) > 4 || Vector2.Distance(transform.localScale, ScaleVector) > 0.005f)
        {
            gameObject.transform.position = Vector2.Lerp(transform.position, targetVector, Time.deltaTime * 4);
            gameObject.transform.localScale = Vector2.Lerp(transform.localScale, ScaleVector, Time.deltaTime * 5);
            gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0, 0, 0, 1), Time.deltaTime*10);
            yield return null;
        }
        is_animating = false;
        can_be_clicked = true;
    }

    public void CardMoveBack()
    {
        StartCoroutine(CardMoveBackToPoint());
        
    }


}
