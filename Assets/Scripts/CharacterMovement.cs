using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum GameState
{
    MAIN_MENU, // shows main menu canvas,
    LEVEL_START,
    LEVEL_END
}

public class CharacterMovement : MonoBehaviour
{
    public Transform character;
    private bool playLevel = true;

    public float characterSpeed;

    public float constraintLeft = 60f;
    public float constraintRight;

    public float rightInputConstraint = 9f;
    public float leftInputConstraint = -9f;

    GameObject lastCameraPosition;
    public Camera mainCamera;
    public Camera secondaryCamera;

    CharacterElementControl controlScript;
    AnimateTextPopUps animateScript;
    ElementTrackingScript trackingScript;
    StoreScript storeScript;

    Transform playerContainer;
    public AudioSource clinkSound;
    public AudioSource steamSound;

    ParticleSystem fireParticles;
    ParticleSystem waterParticles;

    public TMP_Text crystalCount;
    public TMP_Text multiplierCount;
    public TMP_Text gamesPlayed;
    public TMP_Text highScore;
    public TMP_Text SCORE;

    public Canvas mainMenuCanvas;
    public Canvas gameCanvas;

    Animator animator;

    GameState currentState;

    int MultiplierOrbCount;
    int currentScore;

    public Slider slider;

    public Transform mainView;

    void Start()
    {
        StartLevel();
        UpdateMainMenuInterface();
    }

    void Update()
    {
        
        switch (currentState)
        {
            case GameState.LEVEL_START:
                // Move player forward
                playerContainer.position += new Vector3(Time.deltaTime * -0.01f * characterSpeed, 0, 0);

                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    float deltaMovementX = ((touch.position.x / constraintRight) * 2 * rightInputConstraint) - rightInputConstraint;

                    //Debug.Log(touch.position.x / constraintRight);
                    //Debug.Log(touch.position.x + " " + deltaMovementX);

                    playerContainer.position = new Vector3(character.position.x, character.position.y, deltaMovementX);

                }
                //playerContainer.position = character.position;

                break;
            case GameState.LEVEL_END:

                if(currentScore > storeScript.bestScore)
                {
                    storeScript.bestScore = currentScore;
                }
                break;
            case GameState.MAIN_MENU:
                gameCanvas.gameObject.SetActive(false);
                mainMenuCanvas.gameObject.SetActive(true);

                animator.SetBool("isVictory", false);
                animator.SetBool("isIdle", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isDead", false);


                break;
        }
            
    }

    void UpdateMainMenuInterface()
    {
        highScore.text = "BEST SCORE: " + storeScript.bestScore;
        gamesPlayed.text = "GAMES PLAYED: " + storeScript.gamesPlayed;
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "OWater" && controlScript.FetchCurrentElement() == EElements.FIRE
            || collider.tag == "OFire" && controlScript.FetchCurrentElement() == EElements.WATER)
        {
            // LOSE HP
            
            float lostHP = GetObstacleValueByName(collider.gameObject);
            trackingScript.DecrementValue(lostHP);

            gameObject.transform.Find("SteamExplosion").GetComponent<ParticleSystem>().Play();
            steamSound.Play();
            Handheld.Vibrate();

            //collider.GetComponent<BoxCollider>().enabled = false;
            Destroy(collider.gameObject);

            if (controlScript.DecreasePlayerHP(lostHP) <= 0)
            {
                // GAME OVER
                currentState = GameState.LEVEL_END;
                animator.SetBool("isVictory", false);
                animator.SetBool("isIdle", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isDead", true);

                foreach (Transform item in GameObject.Find("CurrentElementOrbs").GetComponentsInChildren<Transform>())
                {
                    item.gameObject.SetActive(false);
                }
                Debug.Log("GAME OVER");
            }
        }

        if (collider.tag == "OWater" && controlScript.FetchCurrentElement() == EElements.WATER
            || collider.tag == "OFire" && controlScript.FetchCurrentElement() == EElements.FIRE)
        {
            // GAIN HP
            float gainedHP = GetObstacleValueByName(collider.gameObject) / 2;
            trackingScript.IncrementValue(gainedHP);
            controlScript.IncreasePlayerHP(gainedHP);

            if(collider.tag == "OWater")
            {
                controlScript.PlayWater();
            }
            else
            {
                controlScript.PlayFire();
            }

            //collider.GetComponent<BoxCollider>().isTrigger = false;
            Destroy(collider.gameObject);
        }

        if (collider.tag == "InvertOrb")
        {
            controlScript.ChangeElement(collider.gameObject);
            trackingScript.SwitchSliderElement();
            Destroy(collider.gameObject);
            gameObject.GetComponentInChildren<Renderer>().material = controlScript.FetchPlayerMaterial();
        }

        if(collider.tag == "Collectible")
        {
            Destroy(collider.gameObject);
            storeScript.GainCrystal();
            crystalCount.text = storeScript.GetCrystalCount() + "×";
            animateScript.PopUpCrystal(collider.transform);
            clinkSound.Play();
        }

        if(collider.tag == "Finish")
        {
            currentState = GameState.LEVEL_END;
            GameObject.Find("Confetti!").GetComponent<ParticleSystem>().Play();

            mainCamera.enabled = false;
            secondaryCamera.enabled = true;

            animator.SetBool("isVictory", true);
            animator.SetBool("isIdle", false);
            animator.SetBool("isRunning", false);

            storeScript.AddCrystalCountToTotal(MultiplierOrbCount);

            SCORE.text = "SCORE: " + Mathf.Round(MultiplierOrbCount * slider.value * 2);

            //gameCanvas.GetComponent<UIManipulatorScript>().endCanvas.enabled = false;
        }

        if(collider.tag == "MultiplierOrb")
        {
            Destroy(collider.gameObject);
            MultiplierOrbCount++;
            string text = "";

            for (int i = 1000; MultiplierOrbCount < i; i /= 10)
            {
                if (MultiplierOrbCount < i)
                    text += "0";
            }

            multiplierCount.text = text + MultiplierOrbCount;
        }

    }

    public void StartNewGame()
    {
        storeScript.gamesPlayed += 1;
        currentState = GameState.LEVEL_START;

        /*mainCamera.GetComponent<CameraSwitchScript>().isMovable = false;
        mainCamera.GetComponent<CameraSwitchScript>().SwitchCameraToMain();*/

        characterSpeed = slider.value;

        mainCamera.transform.position = mainView.transform.position;
        mainCamera.transform.rotation = mainView.transform.rotation;
        mainCamera.transform.localScale = mainView.transform.localScale;


        mainMenuCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(true);

        animator.SetBool("isVictory", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("isRunning", true);
        animator.SetBool("isDead", false);

        //gameCanvas.GetComponent<UIManipulatorScript>().endCanvas.gameObject.SetActive(true);
    }

    private float GetObstacleValueByName(GameObject gameObject)
    {
        string objectName = gameObject.name.Split('_')[0];
        float value = 0;

        switch (objectName)
        {   
            case "Geyser":
                value = 0.1f;
                break;
            case "Wall":
                value = 0.2f;
                break;
            case "Pool":
                value = 0.3f;
                break;
            default:
                value = 0;
                break;
        }

        return value;
    }


    public void StartLevel()
    {
        currentState = GameState.MAIN_MENU;

        MultiplierOrbCount = 0;

        mainCamera.enabled = true;
        secondaryCamera.enabled = false;

        GameObject.Find("FireOrb1").SetActive(true);
        GameObject.Find("FireOrb2").SetActive(true);

        constraintRight = Screen.width - constraintLeft;
        leftInputConstraint = -rightInputConstraint;

        controlScript = character.gameObject.GetComponent<CharacterElementControl>();
        animateScript = GameObject.Find("TextPopUps").GetComponent<AnimateTextPopUps>();
        trackingScript = GameObject.Find("ElementTracker").GetComponent<ElementTrackingScript>();
        storeScript = GameObject.Find("GameCanvas").GetComponent<StoreScript>();

        controlScript.ResetPlayer();
        storeScript.ResetCrystalCount();

        playerContainer = GameObject.Find("PlayerContainer").transform;
        animator = gameObject.GetComponent<Animator>();
    }

    public void HardResetScene()
    {
        Debug.Log("YES");
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
    }
}
