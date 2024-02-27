using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SheZhi : MonoBehaviour
{
    public GameObject BoxSheZhi;
    public Button reStart;
    public Button reStart1;
    public Button reStart2;
    public Button SheZhibtn;
    public Button Home;
    public Button Home1;
    public Button Home2;
    public Button GuanBi;
    public Button Next;//ÏÂÒ»¹Ø
    void Start()
    {
        BoxSheZhi.SetActive (false);
        SheZhibtn.onClick.AddListener(showBoxSheZhi);
        reStart.onClick.AddListener (ReStart);
        reStart1.onClick.AddListener(ReStart);
        reStart2.onClick.AddListener(ReStart);
        Home.onClick.AddListener(ToHome);
        Home1.onClick.AddListener(ToHome);
        Home2.onClick.AddListener(ToHome);
        GuanBi.onClick.AddListener(CloseBox);
        if (Next != null)
        {
            Next.onClick.AddListener(ToNext);
        }

    }

    void showBoxSheZhi()
    {
        Time.timeScale = 0;
        BoxSheZhi.SetActive (true);
    }
    private void ReStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    private void ToHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene (0);
    }

    private void ToNext()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene (2);
    }

    void CloseBox()
    {
        BoxSheZhi.SetActive(false);
    }
}
