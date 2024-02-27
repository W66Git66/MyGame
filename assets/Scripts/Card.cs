using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardState cardState;
    public CardPattern cardParttern;
    public GameManager gameManager;
    private float flipSpeed=2.5f;//���Ʒ����ٶ�

    private void Start()
    {
        cardState = CardState.δ����;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnMouseUp()
    {
        if(cardState.Equals(CardState.�ѷ���))
        {
            return;
        }
        if(gameManager.ReadyToCompareCards)
        {
            return;
        }
        gameManager.AddCardComparison(this);
        if (gameManager.leftSteps > 0) //������һ
        {
            gameManager.leftSteps--;
            gameManager.stepsBox.text=gameManager.leftSteps.ToString();//ʣ�ಽ��
            if(gameManager.leftSteps == 0)
            {
                Time.timeScale = 0;
                gameManager.FailBox.SetActive(true);
            }
            //Debug.Log(gameManager.leftSteps);
        }
        OpenCard();
        gameManager.CompareCardInList();
    }

    void OpenCard()
    {
        // transform.eulerAngles=new Vector3(0,180,0);
        StartCoroutine(TurnFace());
        cardState = CardState.�ѷ���;
    }

    IEnumerator TurnFace()
    {
        float t = 0f;
        while (t <1f)
        {
            t += Time.deltaTime * flipSpeed;
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(0f, 0f, 0f), Quaternion.Euler(0, 180f, 0), t);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        transform.eulerAngles= new Vector3(0, 180, 0);
    }
}

public enum CardState
{
    δ����,
    �ѷ���,
    ��Գɹ�
}

public enum CardPattern
{ 
    None,
    Monday,
    Thusday,
    Wensday,
    Thursday,
    Friday,
}