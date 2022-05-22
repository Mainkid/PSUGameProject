using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject cardStartPos;
    public Text textField;
    private GameObject Card_canvas_front;
    private GameObject Card_canvas_back;
    private GameObject charm;
    private GameObject stamina;
    private GameObject intelligence;

    public GameObject LeftButton;
    public GameObject RightButton;
    public GameObject Achievement;
    public GameObject WeekText;

    private IEnumerator dissolveCoroutine;
    private Coroutine textAnimationCoroutine;

    private Vector3 ScaleVector = new Vector3(0.5624999f, 0.5624999f, 0.5624999f);
    private bool can_pressButton = false;
    private float progressBarHeight = 200;

    public Color32 colorStandart = new Color32(187, 88, 80, 255);
    public Color32 colorDown = new Color32(144, 20, 14, 255);
    public Color32 colorUp = new Color32(15, 143, 72, 255);

    private bool isFinalCard = false;
    
    //Поменять!!!

    // Fields for mechanic to work

    Player player;

    [SerializeField]
    private Storage storage;

    ICard currentCard = null;

    public enum Actions { action1, action2 };

    public enum FinalCardTypes { IntelligenceLose, CharismaLost, StaminaLose, Win };

    int daysToWin = 28;

    // Fields for mechanic to work

    private int aCounter = 1;

    void Start()
    {
        isFinalCard = false;

        dissolveCoroutine = TextDisolve();
        GameObject.Find("Fade").GetComponent<Animator>().Play("Fade_in", -1, 0);
        GameObjInit();

        storage.Initialize();

        InitPerkValues();
        InitStartAnimation();
        
    }

    private void InitIcons()
    {

        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
        {
            MainMenuButtonPressed();           
        }
    }

    private Sprite LoadCard(string picturePath)
    {
        return Resources.Load<Sprite>(picturePath);
    }

    void GameObjInit()
    {
        Card_canvas_front = GameObject.Find("Card_Canvas_front");
        Card_canvas_back = GameObject.Find("Card_Canvas_back");
        charm = GameObject.Find("charm_UI");
        intelligence = GameObject.Find("intelligence_UI");
        stamina = GameObject.Find("stamina_UI");
    }

    void InitPerkValues()
    {
        player = new Player();
        StartCoroutine(changeProgressBarOnInit(intelligence, player.Intelligence));
        StartCoroutine(changeProgressBarOnInit(charm, player.Charisma));
        StartCoroutine(changeProgressBarOnInit(stamina, player.Stamina));
    }

    void GameOverBehaviour()
    {
        RestartButtonPressed();
    }

    IEnumerator changeProgressBar(GameObject gameobject, int Value)
    {
        int is_Moving_down = 0;
        Image progressBar =gameobject.GetComponent<Transform>().Find("progressBar").GetComponent<Image>();

        float targetValue= Value/10.0f;
        if ((progressBar.fillAmount - targetValue) > 0.09f && (progressBar.fillAmount - targetValue) >0.0f)
            is_Moving_down = 2;
        else if ((progressBar.fillAmount - targetValue) < -0.09f && (progressBar.fillAmount - targetValue) < 0.0f)
            is_Moving_down = 1;
        else
            is_Moving_down = 0;

        if (is_Moving_down == 2)
            progressBar.color = colorDown;
        else if (is_Moving_down == 1)
            progressBar.color = colorUp;
        else
            progressBar.color = colorStandart;

        Debug.Log((progressBar.fillAmount - targetValue));
        float TimeCount = 0;
        while (Mathf.Abs(progressBar.fillAmount - targetValue) > 0.01 || Vector3.Distance(new Vector3(progressBar.color.r*255, progressBar.color.g*255, progressBar.color.b*255), new Vector3(colorStandart.r, colorStandart.g, colorStandart.b))>10.0f)
        {
            Debug.Log(Vector3.Distance(new Vector3(progressBar.color.r, progressBar.color.g, progressBar.color.b), new Vector3(colorStandart.r, colorStandart.g, colorStandart.b)));
            TimeCount += Time.deltaTime;
            yield return null;
            progressBar.color = Color32.Lerp(progressBar.color, colorStandart,TimeCount*TimeCount*1.2f);
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, targetValue, Time.deltaTime*5.0f);          
        }
        Debug.Log("Ended");
    }

    IEnumerator changeProgressBarOnInit(GameObject gameobject, int Value)
    {
       
        Image progressBar = gameobject.GetComponent<Transform>().Find("progressBar").GetComponent<Image>();

        float targetValue = Value / 10.0f;
        

        while (Mathf.Abs(progressBar.fillAmount - targetValue) > 0.01)
        {
            yield return null;
            
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, targetValue, Time.deltaTime * 5.0f);
        }
    }



    void InitStartAnimation()
    {
        StartCoroutine(GenCards(0.1f));
    }

    IEnumerator MoveToCardPoint(Transform transform)
    {
        while(Vector2.Distance(transform.position,cardStartPos.transform.position)>1)
        {
            transform.position = Vector2.Lerp(transform.position, cardStartPos.transform.position, Time.deltaTime*10);
            yield return null;
        }
        transform.GetComponent<Card_controller>().can_be_clicked = true;
    }

    IEnumerator GenCards(float seconds)
    {
        int N = 10;
        Vector3 CardGenPos = new Vector3(-1000, 1000, 0);
        GameObject card;

        for (int i = 0; i < 10; i++)
        {
            int xRand = Random.Range(-2, 2) * 1000;
            //int yRand = Random.Range(-2, 2) * 1000;
            //Debug.Log(xRand);
            card = Instantiate(cardPrefab, Camera.main.ScreenToWorldPoint(CardGenPos), Quaternion.identity);


            if (i == N - 1)
            {               
                card.transform.SetParent(Card_canvas_front.transform);
                card.tag = "FrontCard";
            }
            else if (i == N - 2)
            {
                card.transform.SetParent(Card_canvas_back.transform);
                card.tag = "BackCard";
            }
            else
                card.transform.SetParent(Card_canvas_front.transform);

            card.transform.localScale = ScaleVector;
            card.transform.position = CardGenPos;
            card.GetComponent<Card_controller>().can_be_clicked = false;
            StartCoroutine(MoveToCardPoint(card.transform));

            yield return new WaitForSeconds(seconds);
        }

        yield return new WaitForSeconds(1);


        GameObject[] card_list = GameObject.FindGameObjectsWithTag("Cards");
        for (int i = 0; i < card_list.Length; i++)
            Destroy(card_list[i]);

        TakeNextCard();
        card = GameObject.FindGameObjectWithTag("FrontCard");
        card.GetComponent<Animation>().Play();
        card.GetComponent<Card_controller>().Description = currentCard.Description;

        card.transform.Find("Card_background").transform.Find("Image").GetComponent<Image>().sprite = LoadCard(currentCard.PicturePath);


        can_pressButton = true;
    }

    IEnumerator CardMovesLeft(GameObject gameobj)
    {
        can_pressButton = false;

        Transform transform = gameobj.transform;
        Vector2 EndPoint = new Vector2(transform.position.x - 3000, transform.position.y - 500);

        while (Vector2.Distance(transform.position, EndPoint) > 500)
        {
            transform.position = Vector2.Lerp(transform.position, EndPoint, Time.deltaTime * 2);
            transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0, 0, 0.3f, 1), Time.time * 0.005f);
            yield return null;
        }

        Destroy(gameobj);
        //can_pressButton = true;
    }

    IEnumerator CardMovesRight(GameObject gameobj)
    {
        can_pressButton = false;
        Transform transform = gameobj.transform;
        Vector2 EndPoint = new Vector2(transform.position.x + 3000, transform.position.y - 500);
        float TimeCounter = 0;
        while (Vector2.Distance(transform.position, EndPoint) > 500)
        {

            transform.position = Vector2.Lerp(transform.position, EndPoint, Time.deltaTime * 2);
            transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0, 0, -0.3f, 1), Time.time * 0.005f);
            yield return null;
        }

        Destroy(gameobj);

    }

    IEnumerator CardFlipWithDelay(GameObject gameobj)
    {
        yield return new WaitForSeconds(0.5f);

        if (isFinalCard)
        {
            FinalCardTypes finalCardType;

            if (player.Intelligence == 0)
                finalCardType = FinalCardTypes.IntelligenceLose;
            else if (player.Charisma == 0)
                finalCardType = FinalCardTypes.CharismaLost;
            else if (player.Stamina == 0)
                finalCardType = FinalCardTypes.StaminaLose;
            else
                finalCardType = FinalCardTypes.Win;

            TakeFinalCard(finalCardType);
        }
        else
            TakeNextCard();

        if (gameobj != null)
            gameobj.tag = "Untagged";

        GameObject card = GameObject.FindGameObjectWithTag("BackCard");

        if (card != null)
        {
            card.transform.SetParent(Card_canvas_front.transform);
            card.tag = "FrontCard";
            card.GetComponent<Animation>().Play();
            card.transform.Find("Card_background").transform.Find("Image").GetComponent<Image>().sprite = LoadCard(currentCard.PicturePath);
            card.GetComponent<Card_controller>().Description = currentCard.Description;
        }

        if (!isFinalCard)
        {
            card = Instantiate(cardPrefab, Camera.main.ScreenToWorldPoint(cardStartPos.transform.position), Quaternion.identity);
            card.transform.SetParent(Card_canvas_back.transform);
            card.transform.localScale = ScaleVector;
            card.transform.position = cardStartPos.transform.position;
            card.tag = "BackCard";
        }

        can_pressButton = true;
    }

    IEnumerator TextAnimation(string text)
    {
        int counter = 0;
        string text_str = "";

        while (counter < text.Length)
        {
            yield return new WaitForSeconds(0.04f);
            text_str += text[counter];
            textField.text = text_str;
            counter++;
        }

    }

    IEnumerator SceneTransition(string SceneName)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneName);
    }

    IEnumerator TextDisolve()
    {
        while(textField.color.a>0)
        {
            yield return null;
            Color color = textField.color;
            color.a= Mathf.Lerp(textField.color.a, 0, Time.deltaTime*5.0f);
            textField.color = color;
        }
    }

    public void StartTextAnimation()
    {
        StopCoroutine(dissolveCoroutine);
        if (textAnimationCoroutine!=null)
            StopCoroutine(textAnimationCoroutine);
        changeTextAlpha();
        textAnimationCoroutine = StartCoroutine(TextAnimation(currentCard.Text));
    }

    public void StopTextAnimation()
    {
        StopCoroutine(textAnimationCoroutine);
        textField.text = currentCard.Text;
    }

    public void StartTextDissolve()
    {
        StartCoroutine(dissolveCoroutine);
    }

    private void changeTextAlpha()
    {
        textField.text = "";
        Color color = textField.color;
        color.a = 1;
        textField.color = color;
    }

    private void ChangeDayNumber()
    {
        aCounter++;

        if (aCounter == daysToWin - 1)
            isFinalCard = true;

        WeekText.GetComponent<Text>().text = $"День: {aCounter}";
    }

    public void LeftButtonPressed()
    {
        if (isFinalCard)
            GameOverBehaviour();
            
        //Debug.Log("canPressButton: " + can_pressButton);
        //Debug.Log("can_be_clicked: " + GameObject.FindGameObjectWithTag("FrontCard").GetComponent<Card_controller>().can_be_clicked);

        if (can_pressButton && GameObject.FindGameObjectWithTag("FrontCard").GetComponent<Card_controller>().can_be_clicked&& GameObject.FindGameObjectWithTag("FrontCard").GetComponent<Card_controller>().is_flipped)
        {
            StartCoroutine(CardMovesLeft(GameObject.FindGameObjectWithTag("FrontCard")));
            StartCoroutine(CardFlipWithDelay(GameObject.FindGameObjectWithTag("FrontCard")));
            StartCoroutine(dissolveCoroutine);
            if (!isFinalCard)
            {
                ChangeDayNumber();
                ChooseAction(Actions.action1);
            }               
        }       
    }   

    public void RightButtonPressed()
    {
        if (isFinalCard)
            GameOverBehaviour();

        //Debug.Log("canPressButton: " + can_pressButton);
        //Debug.Log("can_be_clicked: " + GameObject.FindGameObjectWithTag("FrontCard").GetComponent<Card_controller>().can_be_clicked);

        if (can_pressButton && GameObject.FindGameObjectWithTag("FrontCard").GetComponent<Card_controller>().can_be_clicked && GameObject.FindGameObjectWithTag("FrontCard").GetComponent<Card_controller>().is_flipped)
        {
            StartCoroutine(CardMovesRight(GameObject.FindGameObjectWithTag("FrontCard")));
            StartCoroutine(CardFlipWithDelay(GameObject.FindGameObjectWithTag("FrontCard")));
            StartCoroutine(dissolveCoroutine);
            if (!isFinalCard)
            {
                ChooseAction(Actions.action2);
                ChangeDayNumber();
            }
        }     
    }

    public void MainMenuButtonPressed()
    {
        GameObject.Find("Fade").GetComponent<Animator>().Play("Fade_out", -1, 0);        
        StartCoroutine(SceneTransition("SampleScene"));
    }

    public void RestartButtonPressed()
    {
        GameObject.Find("Fade").GetComponent<Animator>().Play("Fade_out", -1, 0);
        StartCoroutine(SceneTransition("GameScene"));
    }

    private void ChangeTextOnButtons()
    {
        LeftButton.GetComponent<Text>().text = currentCard.Action1.Text;
        RightButton.GetComponent<Text>().text = currentCard.Action2.Text;
    }

    // Mechanic methods
    public void TakeNextCard()
    {        
        ICard nextCard = null;

        foreach (var card in storage.ActiveConditionalPool)
            card.TickTimer();

        var v = storage.ActiveConditionalPool.Select((card, index) => new { card, index }).FirstOrDefault(x => x.card.Timer == 0);

        if (v != null)
        {
            nextCard = v.card;
            storage.ActiveConditionalPool.RemoveAt(v.index);
        }

        if (nextCard == null)
        {
            int k;
            do
            {
                k = storage.DefaultPool.PeekNumberCard(random: true);

            } while (storage.DefaultPool.RNGProvider.NextDouble() > storage.DefaultPool[k].DrawProbability);

            nextCard = storage.DefaultPool.DrawCard(number: k);
        }

        currentCard = nextCard;
        ChangeTextOnButtons();
    }

    public void TakeFinalCard(FinalCardTypes finalCardType)
    {
        int id = 0;

        switch (finalCardType)
        {
            case FinalCardTypes.IntelligenceLose:
                id = 101;
                break;
            case FinalCardTypes.CharismaLost:
                id = 100;
                break;
            case FinalCardTypes.StaminaLose:
                id = 102;
                break;
            case FinalCardTypes.Win:
                id = 103;
                break;
        }

        DefaultCard finalCard = storage.FinalCards.FirstOrDefault(x => x.Id == id);

        currentCard = finalCard;
        ChangeTextOnButtons();
    }

    public void ChooseAction(Actions action)
    {
        Action chosenAction = action == Actions.action1 ? currentCard.Action1 : currentCard.Action2;

        foreach (int conditionalCardId in chosenAction.ConditionalCardIds)
        {
            ConditionalCard conditionalCardToAdd = storage.AllConditionalCards.FirstOrDefault(x => x.Id == conditionalCardId);
            if (conditionalCardToAdd != null)
                storage.ActiveConditionalPool.Add(conditionalCardToAdd);
        }

        Achievement recievedAchievement = storage.AllAchievements.FirstOrDefault(x => x.Id == chosenAction.AchievementId);
        if (recievedAchievement != null && !recievedAchievement.isReceived)
        {
            recievedAchievement.isReceived = true;
            storage.SaveProgress();
            Achievement.GetComponent<Text>().text = $"Получено достижение: {recievedAchievement.Text}";
            GameObject.Find("Achievement_panel").GetComponent<Animator>().Play("Achievement_appear", -1, 0);
        }

        player.updateCharacteristics(chosenAction.IntelligenceChange, chosenAction.CharismaChange, chosenAction.StaminaChange);

        StartCoroutine(changeProgressBar(intelligence, player.Intelligence));
        StartCoroutine(changeProgressBar(charm, player.Charisma));
        StartCoroutine(changeProgressBar(stamina, player.Stamina));

        if (player.isDead())
        {
            isFinalCard = true;
        }
    }

}
