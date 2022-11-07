using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text fpsText;
    [SerializeField]
    private Text playerSpeed;
    [SerializeField]
    private Text ringSpeed;
    private float deltaTime;

    private void Start()
    {

        if (!(PlayerPrefs.GetInt("isFpsOn", 0) != 0))
        {
            Destroy(this);

        }


    }


    private void LateUpdate()
    {
        deltaTime = (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "Fps:" + Mathf.Ceil(fps).ToString();
        playerSpeed.text = "Player Speed: " + PlayerController.playerSpeed.ToString();
        ringSpeed.text = "Ring Speed: " + rings.shrikeSpeed.ToString();

    }
}
