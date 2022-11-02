using UnityEngine;

public class MainMenuTouchEffect : MonoBehaviour
{

    [SerializeField]
    private GameObject TouchParticle;

    private Vector2 tempTouch;
    private void Start() {
        tempTouch = Vector3.zero;
    }

    private void Update()
    {

        if (Input.touchCount <= 4)
        {
            foreach (var touch in Input.touches)
            {

                Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
                GameObject touchPar = Instantiate(TouchParticle, pos, Quaternion.identity);
                Destroy(touchPar, 1f);
                

            }




        }


    }


} //class
