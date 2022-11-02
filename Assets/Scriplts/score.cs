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

    private void Start() {
        mash = scoreText.GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void LateUpdate() {
        
        mash.text = Score.ToString();

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag(TagManager.RING_TAG)){

            Score += 1;
            ScoreUp();

            
        }
        
    }

}
