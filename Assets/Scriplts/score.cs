using UnityEngine;

public class score : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject scoreText;
    private TMPro.TextMeshProUGUI mash;
    
    public static int Score;

    public delegate void isScoreUp();
    public static isScoreUp ScoreUp;
    private int HighScore;

    private void Start() {
        mash = scoreText.GetComponent<TMPro.TextMeshProUGUI>();
        HighScore = PlayerPrefs.GetInt("highScore",0);
    }

    private void LateUpdate() {
        
        mash.text = Score.ToString();

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag(TagManager.RING_TAG)){

            Score += 1;
            ScoreUp();

            if(Score > HighScore){

                PlayerPrefs.SetInt("highScore",Score);


            }

            
        }
        
    }

}
