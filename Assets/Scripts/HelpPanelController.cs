using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanelController : MonoBehaviour
{
    public GameObject help_canvas;
    public GameObject finger;
    public GameObject char_description;
    public GameObject swap_description;

    private Coroutine CloseHelpPanel;
    private bool isCloseHelpPanelWorks;
    IEnumerator HelpRequest()
    {
        isCloseHelpPanelWorks = true;
        help_canvas.SetActive(true);
        finger.GetComponent<Animation>().Play("swipe_Animation");
        char_description.GetComponent<Animation>().Play("help_description_start");
        swap_description.GetComponent<Animation>().Play("help_description_start");
        yield return new WaitForSeconds(7.0f);
        char_description.GetComponent<Animation>().Play("help_description_stop");
        swap_description.GetComponent<Animation>().Play("help_description_stop");
        yield return new WaitForSeconds(0.5f);
        help_canvas.SetActive(false);
        isCloseHelpPanelWorks = false;
    }

    public void HelpClick()
    {
        CloseHelpPanel = StartCoroutine(HelpRequest());
    }

    IEnumerator CloseRequest()
    {
        char_description.GetComponent<Animation>().Play("help_description_stop");
        swap_description.GetComponent<Animation>().Play("help_description_stop");
        if (isCloseHelpPanelWorks) StopCoroutine(CloseHelpPanel);
        yield return new WaitForSeconds(0.5f);
        help_canvas.SetActive(false);
    }
    public void CloseClick()
    {
        StartCoroutine(CloseRequest());
    }
}
