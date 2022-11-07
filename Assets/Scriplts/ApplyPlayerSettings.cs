using UnityEngine;

public class ApplyPlayerSettings : MonoBehaviour
{
    [SerializeField]
    private GameObject minimap;
    private bool isMinimapOn;
    private bool bloom;

    // [SerializeField]
    // private Camera Ui_camera;
    [SerializeField]
    private GameObject Ui_effect_camera;
    private bool effect;



    void fetchSettings()
    {

        isMinimapOn = (PlayerPrefs.GetInt("isMinimapOn", 1) != 0);
        effect = (PlayerPrefs.GetInt("isBloomOn", 1) != 0);

    }


    private void Start()
    {
        fetchSettings();

        minimap.SetActive(isMinimapOn);
        Ui_effect_camera.SetActive(effect);


    }





}
