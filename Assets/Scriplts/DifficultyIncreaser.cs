using System.Collections;
using UnityEngine;

// Increase game difficulty over time

public class DifficultyIncreaser : MonoBehaviour
{


    private float timeGapForSpeedUp = 10f;

    [SerializeField]
    private float maxPlayerSpeed = 120;

    private Coroutine diffi;
    public static float ringShrikeSpeed;

    private void OnEnable() {

        ringShrikeSpeed = 0.8f;
        // print(ringShrikeSpeed);
        
    
    }
    
    private void Start()
    {

        diffi  = StartCoroutine(speedUp());


    }


    IEnumerator speedUp()
    {


        while (true)
        {

            if (PlayerController.playerSpeed <= maxPlayerSpeed)
            {

                PlayerController.playerSpeed += .5f;
            }

            yield return new WaitForSecondsRealtime(timeGapForSpeedUp);

            if(PlayerController.playerSpeed >= maxPlayerSpeed){

                StopCoroutine(diffi);
                yield return null;

            }



        }


    }




} //class
