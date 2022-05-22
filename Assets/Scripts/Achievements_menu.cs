using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements_menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void exitButtonClicked()
    {

        GameObject.Find("Main_menu").GetComponent<Animator>().Play("Mainmenu_appear", -1, 0);

       gameObject.GetComponent<Animator>().Play("achievement_menu_reversed", -1, 0);
    }
}
