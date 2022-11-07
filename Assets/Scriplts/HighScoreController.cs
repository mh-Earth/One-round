using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HighScoreController : MonoBehaviour
{

    // private 


    private Animator animator;

    private void Start() {
        
        animator = GameObject.FindGameObjectWithTag("Opening").GetComponent<Animator>();

    }



    IEnumerator loadMainMenu(){
        animator.SetBool(TagManager.opening_animation_tag,false);
        yield return new WaitForSecondsRealtime(.45f);
        SceneManager.LoadScene(0);

    }



    public void MeinMenu(){

        StartCoroutine(loadMainMenu());

    }



} // class
