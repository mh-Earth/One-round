using UnityEngine;
using System.Collections;

public class Gameover : MonoBehaviour
{

    // // Stopping from spawning multiple player or blackHole and sure there are only one player and blackHole in the game scene 


    // private void OnEnable()
    // {

    //     UiController.gameStarted += DestroyMultipleCloneCoroutine;

    // }


    // private void OnDisable()
    // {

    //     UiController.gameStarted -= DestroyMultipleCloneCoroutine;

    // }



    // void DestroyMultipleCloneCoroutine()
    // {

    //     StartCoroutine(DestroyMultipleClone());


    // }

    // IEnumerator DestroyMultipleClone()
    // {

    //     yield return new WaitForSeconds(.5f);

    //     while (true)
    //     {


    //         GameObject[] playerClone = GameObject.FindGameObjectsWithTag(TagManager.PLAYER_TAG);

    //         if (playerClone.Length > 1)
    //         {

    //             for (int i = 1; i < playerClone.Length; i++)
    //             {

    //                 Destroy(playerClone[i]);

    //             }

    //         }

    //         GameObject[] BlackHoleClone = GameObject.FindGameObjectsWithTag(TagManager.BLACK_HOLE_TAG);

    //         if (BlackHoleClone.Length > 1)
    //         {

    //             for (int i = 1; i < BlackHoleClone.Length; i++)
    //             {

    //                 Destroy(BlackHoleClone[i]);

    //             }

    //         }

    //         yield return new WaitForSeconds(.1f);

    //         if( GameObject.FindGameObjectsWithTag(TagManager.PLAYER_TAG).Length == 1 &&  GameObject.FindGameObjectsWithTag(TagManager.BLACK_HOLE_TAG).Length == 1){
    //             yield break;
    //         }


    //     }

    // }

} //class




