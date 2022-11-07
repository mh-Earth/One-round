using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]

    [Header("Player Movements")]
    public static float playerSpeed;
    private float rotateAngel = 10;
    [SerializeField]
    private float idleSpeed;
    // [SerializeField][Range(0f,1.5f)]
    // private float playerSmoothing = 0.5f;
    // private float currentSpeed;
    // private float currentSpeedVelocity;
    // private Vector3


    [Header("Camera shaking")]
    public AnimationCurve curve;
    public float duration = .3f;

    [Header("Player Hitting Actions")]
    [SerializeField]
    private SpriteRenderer playerSprite;
    // activating and disabling player trail effect
    private TrailRenderer playerTrailEffect;
    // [SerializeField]
    // private  ParticleSystem[] playerTailPartials;
    [SerializeField]
    private ParticleSystem destroyParticals;

    [Header("Player Spawning")]
    [SerializeField]
    private ParticleSystem PlayerSpawnerParticals;
    [SerializeField]
    private Vector3 playerSpawnSize;
    private UiController uiController;
    /////////////////////////
    // PlayerController settings
    private bool isVibrate;



    public delegate void gameOver();
    public static gameOver isGameOver;


    float getRandomSpeed()
    {

        if (Random.value >= 0.5f)
        {

            return 180f;

        }
        return -180f;



    }

    IEnumerator spawningPlayer()
    {

        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(.3f);
        PlayerSpawnerParticals.Play();
        yield return new WaitForSeconds(1f);
        // yield return new WaitForSeconds(.1f);
        transform.localScale = playerSpawnSize;
        // Active player trail on spawn;
        playerTrailEffect.enabled = true;
        rotateAngel = 10;


    }


    void fetchSettings(){
        isVibrate =  (PlayerPrefs.GetInt("isVibrateOn", 1) != 0);
    }
        




    void Start()
    {   
        fetchSettings();
        StartCoroutine(spawningPlayer());
        playerSpeed = 90f;
        playerTrailEffect = this.gameObject.GetComponent<TrailRenderer>();
        // Disable by starting
        playerTrailEffect.enabled = false;

    }



    // Rotating the player using touch
    void rotate()
    {

        if (GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG) != null)
        {


            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPoint = Camera.main.ScreenToWorldPoint(touch.position);
                
                if(touchPoint.x >= 0){
                    rotateAngel = playerSpeed;
                    idleSpeed = Mathf.Abs(idleSpeed);
                    print(idleSpeed);


                }
                if(touchPoint.x < 0){

                    rotateAngel = -playerSpeed;
                    idleSpeed = Mathf.Abs(idleSpeed) * -1;
                }



            }

            if (Input.touchCount < 1)
            {

                rotateAngel = idleSpeed;


            }

        }

        transform.RotateAround(target.transform.position, Vector3.back, rotateAngel * Time.deltaTime);
    }


    public IEnumerator shaking(float duration, AnimationCurve shakingCurve)
    {
        Vector3 startPosition = Camera.main.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            Camera.main.transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        Camera.main.transform.position = startPosition;
    }

    void playerHit()
    {

        StartCoroutine(shaking(duration, curve));
        playerSpeed = 0f;
        rings.shrikeSpeed = 0;
        playerSprite.sprite = null;
        // On player hit remove player trail
        playerTrailEffect.enabled = false;

        destroyParticals.Play();

        // Effected by player settings
        if(isVibrate){
            Handheld.Vibrate();
        }

        Destroy(this.gameObject, 2f);




    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TagManager.RING_TAG))
        {

            playerHit();
            isGameOver();
            // Time.timeScale = 0;
            // score.Score = 0;

        }
    }



    // Update is called once per frame
    void Update()
    {

        rotate();
        // keyBoardControls();

    }
}
