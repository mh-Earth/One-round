using UnityEngine;

public class RingsParticleController : MonoBehaviour
{


    [SerializeField]
    private Transform ParentRing;
    private ParticleSystem RingsParticleSystem;


    private void Start() {

     RingsParticleSystem = this.GetComponent<ParticleSystem>();
     RingsParticleSystem.Play();   


    }

    private void LateUpdate() {
        
        transform.localScale = ParentRing.localScale;

        if(transform.localScale.x <= .5f){
            RingsParticleSystem.Stop();
        }

    }


}
