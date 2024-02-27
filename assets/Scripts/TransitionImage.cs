using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransitionImage : MonoBehaviour
{
    public Image transitionImage;
    public float transitionTime = 1.0f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void LoadNextScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeIn()
    {
        float t = 1.0f;

        while (t > 0f)
        {
            t -= Time.deltaTime / transitionTime;
            transitionImage.color = new Color(0f, 0f, 0f, t);
            yield return null;
        }
    }

    IEnumerator FadeOut(string sceneName)
    {
        float t = 0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime / transitionTime;
            transitionImage.color = new Color(0f, 0f, 0f, t);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
