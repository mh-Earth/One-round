using UnityEngine;

public class UiRings : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, speed), Space.World);


    }

}
