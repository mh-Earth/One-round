using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
// using UnityEngine.Rendering.
public class UiController : MonoBehaviour

{

    private GameObject player;


    [Header("Main Menu")]
    [SerializeField]
    private Animator MainMenuAnimator;

    [Header("Game Over Menu")]
    [SerializeField]
    private Animator GameoverAnimator;
    [Header("Score")]
    [SerializeField]
    private TextMeshProUGUI ScoreText;
    //////////////////////////////
    [Header("Background")]
    [SerializeField]
    private ParticleSystem StarScrollingParticleEffect;
    [SerializeField]
    private PlayerSpawner playerSpawner;
    [Header("Ul Effects")]
    [SerializeField]
    private GameObject TouchParticleObject;
    private MainMenuTouchEffect mainMenuTouch;
    private Animator OpenningAnimation;
    [SerializeField]
    private Animator GameStartTransition;
    [SerializeField]
    private Animator UiRings;


    /////////////////////////////
    private bool isFirst = true;
    [SerializeField]
    private Camera UI_Camera;
    private int oldMask;

    private void OnEnable()
    {

        PlayerController.isGameOver += GameOver;


    }

    private void OnDisable()
    {

        PlayerController.isGameOver -= GameOver;

    }

    public delegate void isGameStarted();
    // Clint:ringController
    public static isGameStarted gameStarted;

    private void Awake()
    {
        oldMask = UI_Camera.cullingMask;
        changingCameraCullingMask();
    }

    // initial state of game
    void Start()
    {
        // Time.timeScale = 0;
        mainMenuTouch = TouchParticleObject.GetComponent<MainMenuTouchEffect>();
        playerSpawner = playerSpawner.GetComponent<PlayerSpawner>();
        // StarScrollingParticleEffect.Pause();
        toggleTouchEffect(true); //on
        OpenningAnimation = GameObject.FindGameObjectWithTag("Opening").GetComponent<Animator>();

    }

    void toggleGameStartTransition()
    {

        if (GameStartTransition.GetBool(TagManager.GameStartTransition))
        {

            GameStartTransition.SetBool(TagManager.GameStartTransition, false);
            return;
        }

        GameStartTransition.SetBool(TagManager.GameStartTransition, true);

    }



    IEnumerator LoadGame()
    {


        // using IF for Preventing multiple player to spawning
        if (GameObject.FindGameObjectsWithTag(TagManager.PLAYER_TAG).Length == 0)
        {

            if (isFirst)
            {
                restoreCullingMaskOnUICamera();
                StarScrollingParticleEffect.Play();
                toggleTouchEffect(false); // off  
                isFirst = false;

                // true or false both start the transition

                toggleGameStartTransition();
                UiRings.SetBool("Zoom",true);
                yield return new WaitForSeconds(.4f);
                MainMenuAnimator.SetBool("Start", true);
                yield return new WaitForSeconds(.1f);

                // spawning player
                playerSpawner.spanwPlayer();
                // spawning first ring
                gameStarted();

                yield return null;
            }
            else
            {

                toggleGameStartTransition();
                UiRings.SetBool("Zoom",true);
                yield return new WaitForSecondsRealtime(.4f);
                restart();
                yield return new WaitForSecondsRealtime(2f);
                UiRings.SetBool("Zoom",false);
            }

        }

    }
    public void GameStart()
    {

        StartCoroutine(LoadGame());

    }


    IEnumerator PlayerDeadScreenAnimationWithDelay()
    {


        StarScrollingParticleEffect.Pause();

        yield return new WaitForSeconds(1f);


        GameoverAnimator.SetBool("opening", true);
        GameoverAnimator.SetBool("closing", false);

        yield return new WaitForSeconds(1f);
        changingCameraCullingMask();




    }


    public void GameOver()
    {

        toggleTouchEffect(false);
        StartCoroutine(PlayerDeadScreenAnimationWithDelay());
        ScoreText.text = score.Score.ToString();
        Destroy(GameObject.FindGameObjectWithTag(TagManager.BLACK_HOLE_TAG), 3f);


    }

    IEnumerator loadExit()
    {
        OpenningAnimation.SetBool(TagManager.opening_animation_tag, false);
        yield return new WaitForSeconds(.9f);
        Application.Quit();
    }

    public void exit()
    {

        StartCoroutine(loadExit());

    }

    public void restart()
    {
        // using if for Preventing multiple player to spawning
        if (GameObject.FindGameObjectsWithTag(TagManager.PLAYER_TAG).Length == 0)
        {


            GameoverAnimator.SetBool("opening", false);
            GameoverAnimator.SetBool("closing", true);
            playerSpawner.spanwPlayer();
            GameObject[] inGameRings = GameObject.FindGameObjectsWithTag(TagManager.RING_TAG);
            foreach (var rings in inGameRings)
            {
                Destroy(rings);
            }
            gameStarted();
            score.Score = 0;
            DifficultyIncreaser.ringShrikeSpeed = 0.8f;
            restoreCullingMaskOnUICamera();
            StarScrollingParticleEffect.Play();
            toggleTouchEffect(false);

            if (!isFirst)
            {
                MainMenuAnimator.SetBool("Start", true);
            }
        }


    }

    void toggleTouchEffect(bool status)
    {
        mainMenuTouch.enabled = status;

    }

    void changingCameraCullingMask()
    {
        UI_Camera.cullingMask = LayerMask.GetMask("UI", "Default");

    }

    void restoreCullingMaskOnUICamera()
    {
        UI_Camera.cullingMask = oldMask;
    }




    IEnumerator MainMenuAnimation()
    {
        toggleGameStartTransition();
        StarScrollingParticleEffect.Play();
        yield return new WaitForSeconds(.1f);
        UiRings.SetBool("Zoom",false);
        yield return new WaitForSeconds(.4f);

        MainMenuAnimator.SetBool("Start", false);

        yield return new WaitForSeconds(.65f);
        GameoverAnimator.SetBool("opening", false);
        GameoverAnimator.SetBool("closing", true);
        toggleTouchEffect(true); //On
    }

    IEnumerator AboutSceneLoad()
    {
        OpenningAnimation.SetBool(TagManager.opening_animation_tag, false);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("About");



    }

    IEnumerator SettingSceneLoad()
    {

        OpenningAnimation.SetBool(TagManager.opening_animation_tag, false);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Settings");
    }

    IEnumerator loadHighScoreScene(){

        OpenningAnimation.SetBool(TagManager.opening_animation_tag, false);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("High Score");


    }

    public void MainMenu()
    {
        StartCoroutine(MainMenuAnimation());
    }


    public void about()
    {

        StartCoroutine(AboutSceneLoad());
    }



    public void Setting()
    {

        StartCoroutine(SettingSceneLoad());

    }

    public void HighScore(){

        StartCoroutine(loadHighScoreScene());

    }




}//class
