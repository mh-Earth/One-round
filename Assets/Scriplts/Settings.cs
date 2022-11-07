using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
// using System.Runtime.Serialization.Formatters.Binary;

public class Settings : MonoBehaviour
{



    [SerializeField]
    private Slider SoundValue;
    [SerializeField]
    private Slider MusicValue;
    [SerializeField]
    private Toggle minimap;
    [SerializeField]
    private Toggle Vibrate;
    [SerializeField]
    private Toggle Bloom;
    [SerializeField]
    private Toggle FPS;
    [Header("Animations")]
    private Animator animator;
    [SerializeField]
    private GameObject counterBody;

    [SerializeField]
    private TextMeshProUGUI counter;
    private Animator counterAnimation;
    [SerializeField]
    private Animator ArrowAnimator;
    private float tempCounter;
    private float tempTime;

    private void Start()
    {


        animator = GameObject.FindGameObjectWithTag("Opening").GetComponent<Animator>();
        counterAnimation = counterBody.GetComponent<Animator>();


        // Load Save settings
        MusicValue.value = PlayerPrefs.GetFloat("music", 50);
        SoundValue.value = PlayerPrefs.GetFloat("sound", 50);
        Bloom.isOn = (PlayerPrefs.GetInt("isBloomOn", 1) != 0);
        minimap.isOn = (PlayerPrefs.GetInt("isMinimapOn", 1) != 0);
        Vibrate.isOn = (PlayerPrefs.GetInt("isVibrateOn", 1) != 0);
        FPS.isOn = (PlayerPrefs.GetInt("isFpsOn", 0) != 0);


        MusicValue.onValueChanged.AddListener(delegate { MusicSliderValueChanged(); });
        SoundValue.onValueChanged.AddListener(delegate { SoundSliderValueChanged(); });

        // sliderValueChanged();
    }



    IEnumerator loadMainMenu()
    {

        PlayerPrefs.SetFloat("music", MusicValue.value);
        PlayerPrefs.SetFloat("sound", SoundValue.value);
        PlayerPrefs.SetInt("isBloomOn", (Bloom.isOn ? 1 : 0));
        PlayerPrefs.SetInt("isVibrateOn", (Vibrate.isOn ? 1 : 0));
        PlayerPrefs.SetInt("isMinimapOn", (minimap.isOn ? 1 : 0));
        PlayerPrefs.SetInt("isFpsOn", (FPS.isOn ? 1 : 0));

        ArrowAnimator.SetBool("Swap",true);
        animator.SetBool(TagManager.opening_animation_tag, false);

        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(0);
    }

    public void MainMenu()
    {

        StartCoroutine(loadMainMenu());
    }

    public void reset()
    {
        // Load Save settings
        MusicValue.value = PlayerPrefs.GetFloat("music", 100);
        SoundValue.value = PlayerPrefs.GetFloat("sound", 100);
        Bloom.isOn = PlayerPrefs.GetInt("isBloomOn", 1) != 0;
        minimap.isOn = PlayerPrefs.GetInt("isMinimapOn", 1) != 0;
        Vibrate.isOn = PlayerPrefs.GetInt("isVibrateOn", 1) != 0;
        FPS.isOn = PlayerPrefs.GetInt("isFpsOn", 0) != 0;


    }



    void MusicSliderValueChanged()
    {
        counterAnimation.SetBool("Close",counterBody.activeSelf);
        if (tempTime - Time.time < .9f){
            tempTime = Time.time + .7f;
        }

        counter.text = MusicValue.value.ToString();

    }

    void SoundSliderValueChanged()
    {

        counterAnimation.SetBool("Close",counterBody.activeSelf);
        if (tempTime - Time.time < .9f){
            tempTime = Time.time + .7f;
        }
        counter.text = SoundValue.value.ToString();

    }


    private void LateUpdate()
    {

        if (counterBody.activeSelf)
        {
            if (Time.time > tempTime)
                counterAnimation.SetBool("Close",!counterBody.activeSelf);
                
        }


    }


}
