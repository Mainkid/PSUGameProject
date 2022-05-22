using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Settings_menu : MonoBehaviour
{
    private bool isMoving = false;
    public GameObject settings;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void aboutButtonClick()
    {
        Debug.Log("About");
    }
    

    public void exitButtonClick()
    {
        GameObject.Find("Main_menu").GetComponent<Animator>().Play("Mainmenu_appear", -1, 0);

       settings.GetComponent<Animator>().Play("Settings_disappear", -1, 0);
    }

}
