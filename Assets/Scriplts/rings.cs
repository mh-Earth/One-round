using UnityEngine;

public class rings : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float RingRotateSpeed = .1f;
    private float rotateSpeed;
    private float rotateAngel;
    public static float shrikeSpeed;

    [SerializeField]
    private float maxShrikeSpeed = 1.5f;
    // [SerializeField]
    // private SpriteRenderer spriteRenderer;
    // private float alpha = 255f;
    // private Color TempColor;

    

    void Start()
    {
        initialRotate();
        shrikeSpeed = DifficultyIncreaser.ringShrikeSpeed;
        // TempColor = spriteRenderer.color;
        // spriteRenderer = this.GetComponent<SpriteRenderer>();
        rotateSpeed = 0f;
        if(score.Score > 10){
            if(doRotation()){

                initialRingRotation();
            }
        }


    }

    void initialRingRotation(){

        if(RandomBool()){
            rotateSpeed = RingRotateSpeed;
            return;

        }

        rotateSpeed = -RingRotateSpeed;


    }
    // 50 % change of spawning a ring with Rotating
    bool doRotation(){

        if (Random.value >= 0.5)
        {
            return true;
        }
        return false;

    }


    bool RandomBool()
    {

        if (Random.value >= 0.5)
        {
            return true;
        }
        return false;
    }


    // initial rotation

    void initialRotate()
    {
        rotateAngel = Random.Range(0, 360f);
        transform.Rotate(new Vector3(0, 0, rotateAngel), Space.World);
    }


    // rotating the ring in z axis
    void rotate()
    {   
        transform.Rotate(new Vector3(0, 0, rotateSpeed), Space.World);


    }

    // shirking the ring towards to center
    void shrike()
    {
        transform.localScale = new Vector3(transform.localScale.x - shrikeSpeed * Time.deltaTime, transform.localScale.y - shrikeSpeed * Time.deltaTime, transform.localScale.z);

        if (transform.localScale.x <= 0)
        {
            // Increasing ring speed with w
            if (shrikeSpeed <= maxShrikeSpeed ){

                DifficultyIncreaser.ringShrikeSpeed += .002f;

            }
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        shrike();
        rotate();


    }


}

