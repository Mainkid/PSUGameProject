using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionController : MonoBehaviour
{
    public bool is_animation_playing = false;
    public void crossButtonPressed()
    {
        if (!is_animation_playing)
        {
            GameObject.FindGameObjectWithTag("FrontCard").GetComponent<Card_controller>().CardMoveBack();
            
            GameObject.Find("Card_description").GetComponent<Animator>().Play("Description_disappear", -1, 0);
            GameObject.Find("Description_Fade").GetComponent<Animator>().Play("Description_fade_disappear", -1, 0);
            GameObject.Find("Buttons").GetComponent<Animator>().Play("Button_appear", -1, 0);
        }
    }

}
