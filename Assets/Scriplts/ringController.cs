using UnityEngine;

public class ringController : MonoBehaviour
{
    [SerializeField]
    private GameObject ring;

    private void OnEnable() {
        UiController.gameStarted += SpawningTheFirstRing;

    }

    private void OnDisable() {
        UiController.gameStarted -= SpawningTheFirstRing;
        
    }
    

    void SpawningTheFirstRing(){
        Instantiate(ring, Vector3.zero, Quaternion.identity);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(TagManager.RING_TAG))
        {

            Instantiate(ring, Vector3.zero, Quaternion.identity);

        }
    }

}
