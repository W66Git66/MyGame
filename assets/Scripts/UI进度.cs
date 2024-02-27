using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI进度 : MonoBehaviour
{
    public Image image;
    public GameManager gameManager;
    private float speed=3f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int intValue = gameManager.number-int.Parse(gameManager.cardsBox.text);
        
        if (gameManager.number == 9)
        {
            image.fillAmount = Mathf.Lerp(image.fillAmount,(float)intValue / 9,speed*Time.deltaTime);
        }
        else 
        {
            image.fillAmount = Mathf.Lerp(image.fillAmount, (float)intValue / 36, speed * Time.deltaTime);
        }
    }


}
