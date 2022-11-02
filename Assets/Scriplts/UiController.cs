using UnityEngine;
using TMPro;
using System.Collections;
// using UnityEngine.Rendering.
public class UiController : MonoBehaviour

{

    private GameObject player;


    [Header("Main Menu")]
    [SerializeField]
    private Animator animator;

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
        toggleTouchEffect(); //on






    }

    public void GameStart()
    {
        // using IF for Preventing multiple player to spawning
        if (GameObject.FindGameObjectsWithTag(TagManager.PLAYER_TAG).Length == 0)
        {

            if (isFirst)
            {
                restoreCullingMaskOnUICamera();
                playerSpawner.spanwPlayer();
                gameStarted();
                StarScrollingParticleEffect.Play();
                animator.SetBool("Start", true);
                toggleTouchEffect(); // off  
                isFirst = false;
                return;

            }

            restart();
        }

    }


    IEnumerator PlayerDeadScreenAnimationWithDelay()
    {
        StarScrollingParticleEffect.Pause();

        yield return new WaitForSeconds(.8f);


        GameoverAnimator.SetBool("opening", true);
        GameoverAnimator.SetBool("closing", false);

        yield return new WaitForSeconds(.5f);
        toggleTouchEffect(); // on
        changingCameraCullingMask();




    }


    public void GameOver()
    {
        StartCoroutine(PlayerDeadScreenAnimationWithDelay());
        ScoreText.text = score.Score.ToString();
        Destroy(GameObject.FindGameObjectWithTag(TagManager.BLACK_HOLE_TAG), 2f);


    }

    public void exit()
    {
        Application.Quit();

    }

    public void restart()
    {
        // using if for Preventing multiple player to spawning
        if (GameObject.FindGameObjectsWithTag(TagManager.PLAYER_TAG).Length == 0)
        {


            toggleTouchEffect(); // off
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

            if (!isFirst)
            {
                animator.SetBool("Start", true);
            }
        }


    }

    void toggleTouchEffect()
    {
        if (!mainMenuTouch.enabled)
        {

            // Toggling touch effects when game play is running (On)
            mainMenuTouch.enabled = true;
            return;

        }

        // Toggling touch effects when game play is running (Off)
        mainMenuTouch.enabled = false;
    }

    void changingCameraCullingMask()
    {


        // UI_Camera.cullingMask = (1 << LayerMask.NameToLayer("Default") | LayerMask.GetMask("UI"));
        UI_Camera.cullingMask = LayerMask.GetMask("UI", "Default");

    }

    void restoreCullingMaskOnUICamera()
    {
        UI_Camera.cullingMask = oldMask;
    }




    IEnumerator MainMenuAnimation()
    {

        animator.SetBool("Start", false);

        yield return new WaitForSeconds(.65f);
        GameoverAnimator.SetBool("opening", false);
        GameoverAnimator.SetBool("closing", true);
    }

    public void MainMenu()
    {
        StartCoroutine(MainMenuAnimation());
    }





}//class
