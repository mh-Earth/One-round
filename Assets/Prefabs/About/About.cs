using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class About : MonoBehaviour
{

    private Animator OpeningAnimator;

    private void Start() {
        
        OpeningAnimator = GameObject.FindGameObjectWithTag("Opening").GetComponent<Animator>();

    }

    IEnumerator loadHome(){

        OpeningAnimator.SetBool("Open",false);
        yield return new WaitForSeconds(.8f);
        SceneManager.LoadScene(0);
        yield return null;

    }
    

    public void Home(){

        StartCoroutine(loadHome());

    }


}
