using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Scene01Events : MonoBehaviour
{
    public static int storyStateCheckpoint = 0;

    [Header("Scene Objects")]
    public GameObject fadeScreenIn;
    public GameObject charBall;
    public GameObject charPaddle2;
    public GameObject textBox;

    [Header("Audio Configurations")]
    [SerializeField] AudioSource girlSigh;
    [SerializeField] AudioSource girlGasp;
    [SerializeField] AudioSource backgroundMusicSource; 
    [SerializeField] AudioClip paddle2ConvoMusic;       
    [SerializeField] AudioClip postConvoMusic;          
    [SerializeField] float fadeDuration = 1.0f;          

    [Header("UI Shake Settings")]
    [SerializeField] RectTransform shakeContainerRect;  
    [SerializeField] float shakeDuration = 0.4f;        
    [SerializeField] float shakeMagnitude = 25.0f;      

    [Header("Text & UI Management")]
    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextObject;
    [SerializeField] GameObject nextButton;
    [SerializeField] int eventPos = 0;
    [SerializeField] GameObject charName;
    [SerializeField] GameObject FadeOut;

    private Vector2 originalContainerPos;
    private bool hasSwappedToNextSong = false;
    private float originalMusicVolume = 1.0f;
    private int selectedChoiceIndex = 0;
    private bool choiceMade = false;
    private bool showDecisionChoiceUI = false;

    void Update()
    {
        textLength = TextCreator.charCount;

        if (eventPos >= 4 && backgroundMusicSource != null && !backgroundMusicSource.isPlaying && !hasSwappedToNextSong && postConvoMusic != null)
        {
            hasSwappedToNextSong = true;
            backgroundMusicSource.clip = postConvoMusic;
            backgroundMusicSource.volume = originalMusicVolume;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }

           // Block 1: Handles the 3 Dialogue Choices Selection (Arrow keys + Enter)
    if (showDecisionChoiceUI && !choiceMade)
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedChoiceIndex = (selectedChoiceIndex + 1) % 3;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedChoiceIndex = (selectedChoiceIndex - 1 + 3) % 3;
        }
        else if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            choiceMade = true;
            if (selectedChoiceIndex == 0) StartCoroutine(ChoosePaddle2Routine());
            if (selectedChoiceIndex == 1) StartCoroutine(ChooseBallarinaRoutine());
            if (selectedChoiceIndex == 2) StartCoroutine(ChooseAloneRoutine());
        }
    }
    // Block 2: Handles advancing the normal story text paragraphs cleanly!
    else if (nextButton != null && nextButton.activeSelf && !showDecisionChoiceUI)
    {
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            NextButton(); 
        }
    }
}

    void Start()
    {
        if (shakeContainerRect != null)
        {
            originalContainerPos = shakeContainerRect.anchoredPosition;
        }
        if (backgroundMusicSource != null)
        {
            originalMusicVolume = backgroundMusicSource.volume;
        }

        if (storyStateCheckpoint == 0)
        {
            StartCoroutine(EventStarter());
        }
        else if (storyStateCheckpoint == 1)
        {
            if (GameManagerMini.playerWonMinigame)
            {
                StartCoroutine(PostMinigameWinRoutine());
            }
            else
            {
                StartCoroutine(PostMinigameLoseRoutine());
            }
        }
    }

    IEnumerator EventStarter()
    {
        yield return new WaitForSeconds(2);
        if (fadeScreenIn != null) fadeScreenIn.SetActive(false);
        if (charBall != null) charBall.SetActive(true);
        yield return new WaitForSeconds(2);
        
        if (mainTextObject != null) mainTextObject.SetActive(true);
        textToSpeak = "Oh hey Paddle1! you're already here it seems, I wonder where Paddle2 is?";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        if (girlSigh != null) girlSigh.Play();
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        if (nextButton != null) nextButton.SetActive(true);
        eventPos = 1;
    }

    IEnumerator EventOne()
    {
        nextButton.SetActive(false);
        if (charPaddle2 != null) charPaddle2.SetActive(true);
        if (textBox != null) textBox.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "Paddle2";
        textToSpeak = "Sorry I was late, What did you gather us here for Ballarina?";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        if (girlGasp != null) girlGasp.Play();
        nextButton.SetActive(true);
        eventPos = 2;
    }

    IEnumerator EventTwo()
    {
        nextButton.SetActive(false);
        if (charPaddle2 != null) charPaddle2.SetActive(true);
        if (textBox != null) textBox.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "Ballarina";
        textToSpeak = "I brought you both here to play a friendly game of PONG!";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
        eventPos = 3;
    }

    IEnumerator EventThree()
    {
        nextButton.SetActive(false);
        if (charPaddle2 != null) charPaddle2.SetActive(true);
        if (textBox != null) textBox.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "Paddle2";
        textToSpeak = "Pong? Seriously? I mean, I guess I can.. for a prize.";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        if (backgroundMusicSource != null && paddle2ConvoMusic != null)
        {
            StartCoroutine(FadeAndSwapMusicRoutine(paddle2ConvoMusic));
        }

        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
        eventPos = 4;
    }

    IEnumerator EventFour()
    {
        nextButton.SetActive(false);
        if (charPaddle2 != null) charPaddle2.SetActive(true);
        if (textBox != null) textBox.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "Paddle2";
        textToSpeak = "YOUR LOVE!";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        StartCoroutine(ShakeUIBoxRoutine());

        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
        eventPos = 5;
    }

    IEnumerator EventFive()
    {
        nextButton.SetActive(false);
        if (charPaddle2 != null) charPaddle2.SetActive(true);
        if (textBox != null) textBox.SetActive(true);
        if (FadeOut != null) FadeOut.SetActive(true); 
        yield return new WaitForSeconds(2); 
        
        storyStateCheckpoint = 1;
        SceneManager.LoadScene("Pong Mini"); 
    }

    IEnumerator PostMinigameLoseRoutine()
    {
        if (fadeScreenIn != null) fadeScreenIn.SetActive(false);
        if (charBall != null) charBall.SetActive(true);
        if (charPaddle2 != null) charPaddle2.SetActive(true);
        if (mainTextObject != null) mainTextObject.SetActive(true);
        if (textBox != null) textBox.SetActive(true);

        charName.GetComponent<TMPro.TMP_Text>().text = "Paddle2";
        textToSpeak = "Damn, you suck.";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        
        nextButton.SetActive(true);
        eventPos = 100; // Triggers total level reset
    }

    IEnumerator PostMinigameWinRoutine()
    {
        if (fadeScreenIn != null) fadeScreenIn.SetActive(false);
        if (charBall != null) charBall.SetActive(true);
        if (charPaddle2 != null) charPaddle2.SetActive(true);
        if (mainTextObject != null) mainTextObject.SetActive(true);
        if (textBox != null) textBox.SetActive(true);

        charName.GetComponent<TMPro.TMP_Text>().text = "Paddle2";
        textToSpeak = "Wow, I'm a total loser. I can't believe you actually beat me.";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        
        nextButton.SetActive(true);
        eventPos = 10; 
    }

    IEnumerator TriggerChoiceBranchMenu()
    {
        nextButton.SetActive(false);
        showDecisionChoiceUI = true;
        yield return null;
    }

    IEnumerator ChoosePaddle2Routine()
    {
        showDecisionChoiceUI = false;
        charName.GetComponent<TMPro.TMP_Text>().text = "Paddle2";
        textToSpeak = "Really, you're going to show me kindness after all I've done? Let's get out of here, maybe all I need is your love..";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(1.5f);
        storyStateCheckpoint = 0;
        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator ChooseBallarinaRoutine()
    {
        showDecisionChoiceUI = false;
        charName.GetComponent<TMPro.TMP_Text>().text = "Ballarina";
        textToSpeak = "Heh, suck it Paddle2, you got what was coming to you and now I get the SUPERIOR Paddle.";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(1.5f);
        storyStateCheckpoint = 0;
        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator ChooseAloneRoutine()
    {
        showDecisionChoiceUI = false;
        charName.GetComponent<TMPro.TMP_Text>().text = "Paddle1";
        textToSpeak = "Honestly, I think I'm better off just staying by myself.";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(1.5f);
        storyStateCheckpoint = 0;
        SceneManager.LoadScene("Main Menu");
    }

    private void ResetEntireLevelProgression()
    {
        storyStateCheckpoint = 0;
        SceneManager.LoadScene("Pong Level 3");
    }

    void OnGUI()
{
    if (showDecisionChoiceUI)
    {
        float buttonWidth = 300f;
        float buttonHeight = 45f;
        float startX = (Screen.width - buttonWidth) / 2f;
        float startY = (Screen.height - (buttonHeight * 3 + 30f)) / 2f;

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 16;
        buttonStyle.fontStyle = FontStyle.Bold;

        // Choice 1 text logic: Adds an arrow symbol if selectedIndex is 0
        string choice0Text = (selectedChoiceIndex == 0) ? "-> Show kindness to Paddle2 <-" : "Show kindness to Paddle2";
        if (GUI.Button(new Rect(startX, startY, buttonWidth, buttonHeight), choice0Text, buttonStyle))
        {
            if (!choiceMade) { choiceMade = true; StartCoroutine(ChoosePaddle2Routine()); }
        }

        // Choice 2 text logic: Adds an arrow symbol if selectedIndex is 1
        string choice1Text = (selectedChoiceIndex == 1) ? "-> Claim Ballarina's love <-" : "Claim Ballarina's love";
        if (GUI.Button(new Rect(startX, startY + buttonHeight + 15f, buttonWidth, buttonHeight), choice1Text, buttonStyle))
        {
            if (!choiceMade) { choiceMade = true; StartCoroutine(ChooseBallarinaRoutine()); }
        }

        // Choice 3 text logic: Adds an arrow symbol if selectedIndex is 2
        string choice2Text = (selectedChoiceIndex == 2) ? "-> Stay by Yourself <-" : "Stay by Yourself";
        if (GUI.Button(new Rect(startX, startY + (buttonHeight * 2) + 30f, buttonWidth, buttonHeight), choice2Text, buttonStyle))
        {
            if (!choiceMade) { choiceMade = true; StartCoroutine(ChooseAloneRoutine()); }
        }
    }
}


    IEnumerator FadeAndSwapMusicRoutine(AudioClip nextClip)
    {
        float startVolume = backgroundMusicSource.volume;
        float timeElapsed = 0;

        while (timeElapsed < fadeDuration)
        {
            backgroundMusicSource.volume = Mathf.Lerp(startVolume, 0, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        backgroundMusicSource.Stop();
        backgroundMusicSource.clip = nextClip;
        backgroundMusicSource.loop = false;
        backgroundMusicSource.volume = originalMusicVolume;
        backgroundMusicSource.Play();
    }

    IEnumerator ShakeUIBoxRoutine()
    {
        if (shakeContainerRect == null) yield break;

        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            float xOffset = Random.Range(-1f, 1f) * shakeMagnitude;
            float yOffset = Random.Range(-1f, 1f) * shakeMagnitude;

            shakeContainerRect.anchoredPosition = new Vector2(originalContainerPos.x + xOffset, originalContainerPos.y + yOffset);
            elapsed += Time.deltaTime;

            yield return null;
        }

        shakeContainerRect.anchoredPosition = originalContainerPos;
    }

    public void NextButton()
    {
        if (eventPos == 1) StartCoroutine(EventOne());
        if (eventPos == 2) StartCoroutine(EventTwo());
        if (eventPos == 3) StartCoroutine(EventThree());
        if (eventPos == 4) StartCoroutine(EventFour());
        if (eventPos == 5) StartCoroutine(EventFive());
        if (eventPos == 10) StartCoroutine(TriggerChoiceBranchMenu());
        if (eventPos == 100) ResetEntireLevelProgression();
    }
}