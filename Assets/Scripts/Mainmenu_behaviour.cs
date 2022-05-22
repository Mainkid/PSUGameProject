using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu_behaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        GameObject.Find("Fade").GetComponent<Animator>().Play("Fade_in", -1, 0);
    }

    

    public void settingsButtonClicked()
    {
        GameObject.Find("Settings_menu").GetComponent<Animator>().Play("Settings_appear", -1, 0);
        gameObject.GetComponent<Animator>().Play("Mainmenu_disappear",-1,0);
    }

    public void achievementsButtonClicked()
    {
        GameObject.Find("Achievement_menu").GetComponent<Animator>().Play("achievement_menu",-1,0);
        gameObject.GetComponent<Animator>().Play("Mainmenu_disappear", -1, 0);
    }

    IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(0.5f);
        //Application.LoadLevel("GameScene");
        SceneManager.LoadScene("GameScene");
    }

    public void playButtonClicked()
    {

        GameObject.Find("Fade").GetComponent<Animator>().Play("Fade_out", -1, 0);
        StartCoroutine(SceneTransition());
    }
}
