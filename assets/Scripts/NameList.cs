using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameList : MonoBehaviour
{
    public GameObject nameList;
    void Start()
    {
        nameList.SetActive(false);
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            HideImage();
        }
    }

    void HideImage()
    {
        nameList.SetActive(false);
    }

    public void ShowImage()
    {
        nameList.SetActive(true);
    }
}
