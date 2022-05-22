using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundSliderController : MonoBehaviour, IPointerClickHandler
{
    private bool state = false;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (state)
        {
            gameObject.GetComponent<Animator>().Play("sound_slider_back", -1, 0);
            state = false;
        }
        else
        {
            gameObject.GetComponent<Animator>().Play("sound_slider", -1, 0);
            state = true;

        }
    }
    public void OnMouseDown()
    {
       
    }
}
