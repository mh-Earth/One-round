using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{


    // private GameObject PlayerSpawnPoint;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject BackHole;
    // [SerializeField]
    // private ParticleSystem PlayerSpawnerParticals;

    public void spanwPlayer(){

        GameObject NewBlackHole =  Instantiate(BackHole,Vector3.zero,Quaternion.identity);

        GameObject player = Instantiate(playerPrefab,transform.position,Quaternion.identity);
        float rotateAngel = Random.Range(-180f,180f);
        playerPrefab.transform.RotateAround(NewBlackHole.transform.position,Vector3.back,rotateAngel);

    }


}
