using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class StartGame : MonoBehaviour
{
    public GameObject eventObj;
    public Button start;
    public Animator animator;

    private void Start()
    {
        //DontDestroyOnLoad (this.gameObject);
        //DontDestroyOnLoad (this.eventObj);

        start.onClick.AddListener(LoadScene1);
    }

    private void LoadScene1()
    {
        StartCoroutine(LoadScene(1));
    }
    IEnumerator LoadScene(int index)
    {
        animator.SetBool("FadeIn", false);
        animator.SetBool("FadeOut", true);

        yield return new WaitForSeconds(1);

        AsyncOperation async = SceneManager.LoadSceneAsync(index);
      
    }

   /* private void OnLoadedScene(AsyncOperation obj)
    {
        animator.SetBool("FadeIn", true);
        animator.SetBool("FadeOut", false);
    }*/
}
