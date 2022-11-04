using UnityEngine;

// [RequireComponent(typeof(ParticleSystem))]
public class BackgroundStarEffectManeger : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem scrollingStar;
    [SerializeField]
    private Color[] colorList;
    private int index = 0;
    private float scoreColorBrake = 10f;


    private void OnEnable() {
        
        score.ScoreUp += changingColor;
        PlayerController.isGameOver += reSetIndex;

    }

    private void OnDisable() {
        
        score.ScoreUp -= changingColor;
        PlayerController.isGameOver -= reSetIndex;

    }

    private void Start() {
        
        var main = scrollingStar.main;
        main.startColor = colorList[index];
        index++;

    }

    void changingColor()
    {

        if (score.Score % scoreColorBrake == 0)
        {

            if(index > colorList.Length-1){
                index = 0;

            }

            var main = scrollingStar.main;
            main.startColor = colorList[index];
            scoreColorBrake += 10;
            index++;

        }


    }

    void reSetIndex(){

        index = 0;
        var main = scrollingStar.main;
        main.startColor = colorList[index];
        scoreColorBrake = 10;

    } 



}

