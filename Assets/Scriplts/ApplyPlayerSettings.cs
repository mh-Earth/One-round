using UnityEngine;

public class ApplyPlayerSettings : MonoBehaviour
{
    [SerializeField]
    private GameObject minimap;
    private bool isMinimapOn;
    private bool bloom;


    void fetchSettings(){   

        isMinimapOn = (PlayerPrefs.GetInt("isMinimapOn", 1) != 0);

    }


    private void Start() {
        fetchSettings();
        
        minimap.SetActive(isMinimapOn);

    }





}
