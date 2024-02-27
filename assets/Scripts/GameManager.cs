using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float flipSpeed = 2.5f;
    private float moveSpeed = 0.05f;//�غϾ���
    private float fadeSpeed = 1f;//������ʧ�ٶ�
    public GameObject gameOver;//��Ϸ�������
    public Text stepsBox;//ʣ�ಽ����ʾ
    public Text cardsBox;//ʣ���ƶ���ʾ
    public Text elmBox;//��������
    public int number;//��������
    public Button WanNeng;//���ܰ�ť
    public bool isWanNeng = false;
    private int a=0;//�����Ƽ���
    private int b=0;//�Ӳ����Ƽ���
    private SoundManager soundManager;

    //public Image image;//������

    public Button AddStep;//������

    public GameObject FailBox;//ʧ�ܽ���
    public GameObject SuccessBox;//ʤ������

    private Vector3 newPosition;

    [Header("�Ա��嵥")]
    public List<Card> CardComparison;
    [Header("���������嵥")]
    public List<CardPattern> cardsToBePutIn;

    public Transform[] positions;

    [Header("����ԵĿ�������")]
    public int matchedCardsCount = 0;
    public int cardsLeft = 0;//ʣ������
    private int elmBoxNum = 0;

    [Header("ʣ�ಽ��")]
    public int leftSteps = 30;

    WaitForSeconds waitForSeconds = new WaitForSeconds(1f);//�����ȴ�ʱ��


    void Start()
    {
        //SetupCardsToBePutIn();
        //AddNewCard(CardPattern.Thusday);
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        GenerateRandomCards();
        stepsBox.text = leftSteps.ToString();
        cardsBox.text = cardsLeft.ToString();
        elmBox.text = elmBoxNum.ToString();
        AddStep.onClick.AddListener(AddSteps);
        WanNeng.onClick.AddListener(WanNengCards);
        FailBox.SetActive(false);
        SuccessBox.SetActive(false);
    }
    private void Update()
    {
        //image.fillAmount = matchedCardsCount / cardsLeft;
    }

    void SetupCardsToBePutIn()
    {
        Array array = Enum.GetValues(typeof(CardPattern));
        foreach (var item in array)
        {
            cardsToBePutIn.Add((CardPattern)item);
        }
        cardsToBePutIn.RemoveAt(0);//ɾ��Cardpattern.��
    }

    void GenerateRandomCards()//����
    {
        int positionIndex = 0;
        SetupCardsToBePutIn();
        if (number == 9)
        {
            cardsToBePutIn.RemoveAt(2);
            cardsToBePutIn.RemoveAt(2);
            cardsToBePutIn.RemoveAt(2);
        }
        for (int i = 0; i < 8; i++)
        {
            int maxRandomNumber = cardsToBePutIn.Count;//�������

            for (int j = 0; j < maxRandomNumber; j++)
            {
                int randomNumber = UnityEngine.Random.Range(0, maxRandomNumber);//���������
                                                                                //����
                AddNewCard(cardsToBePutIn[randomNumber], positionIndex);
                //cardsToBePutIn.RemoveAt(randomNumber);
                positionIndex++;
                if (positionIndex >= number)
                {
                    positionIndex = 0;
                    return;
                }
            }
        }
    }

    void AddNewCard(CardPattern cardParttern, int positionIndex)
    {
        GameObject card = Instantiate(Resources.Load<GameObject>("Prefabs/card"));
        card.GetComponent<Card>().cardParttern = cardParttern;
        card.name = "card_" + cardParttern.ToString();
        card.transform.position = positions[positionIndex].position;

        GameObject graphic = Instantiate(Resources.Load<GameObject>("Prefabs/face"));
        graphic.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Graphics/" + cardParttern.ToString());

        graphic.transform.SetParent(card.transform);
        graphic.transform.localPosition = new Vector3(0, 0f, 0.01f);
        graphic.transform.eulerAngles = new Vector3(0, 180, 0);

    }
    void AddNewCardWhenClear(CardPattern cardParttern, Vector3 newPosition)
    {
        GameObject card = Instantiate(Resources.Load<GameObject>("Prefabs/card"));
        card.GetComponent<Card>().cardParttern = cardParttern;
        card.name = "card_" + cardParttern.ToString();
        card.transform.position = newPosition;

        GameObject graphic = Instantiate(Resources.Load<GameObject>("Prefabs/face"));
        graphic.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Graphics/" + cardParttern.ToString());

        graphic.transform.SetParent(card.transform);
        graphic.transform.localPosition = new Vector3(0, 0f, 0.01f);
        graphic.transform.eulerAngles = new Vector3(0, 180, 0);

        //card.transform.eulerAngles = new Vector3(0, 180, 0);
        //card.GetComponent<Card>().cardState = CardState.�ѷ���; 

    }

    public void AddCardComparison(Card card)
    {
        CardComparison.Add(card);
    }

    public bool ReadyToCompareCards
    {
        get
        {
            if (CardComparison.Count == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void CompareCardInList()
    {
        if (ReadyToCompareCards)
        {
            //Debug.Log("keyibidui");
            if (CardComparison[0].cardParttern == CardComparison[1].cardParttern || isWanNeng==true)
            {
                isWanNeng = false;
                //a++;
                //Debug.Log("yiyang");
                foreach (var card in CardComparison)
                {
                    card.cardState = CardState.��Գɹ�;
                }
                newPosition = CardComparison[1].transform.position;

                //CardComparison[0].transform.position = Vector3.Lerp(transform.position,newPosition,moveSpeed*Time.deltaTime);
                soundManager.PlayCardEliminationSound();
                StartCoroutine(TransCardPosition(CardComparison[0]));
                StartCoroutine(FadeOut());


                StartCoroutine(waitFor1f());
                //CardComparison[0].transform.position = newPosition;

                //AddNewCard(cardsToBePutIn[randomNumber], positionIndex);
                matchedCardsCount = matchedCardsCount + 2;
                cardsLeft = cardsLeft - 1;
                elmBoxNum += 1;
                if (cardsLeft <= 1 && leftSteps >= 0)
                {
                    Time.timeScale = 0;
                    SuccessBox.gameObject.SetActive(true);
                }
                cardsBox.text = cardsLeft.ToString();//ʣ������
                elmBox.text=elmBoxNum.ToString();//��������
                if (matchedCardsCount >= positions.Length)
                {
                    //StartCoroutine(ReloadScene());
                }
            }
            else
            {
                //Debug.Log("buyiyang");
                StartCoroutine(MissMatchCards());
            }
        }
    }

    public void ClearCardConparison()
    {
        CardComparison.Clear();
    }

    void TurnBackCards()
    {
        foreach (var card in CardComparison)
        {
            StartCoroutine(TurnBack(card));
            //card.gameObject.transform.eulerAngles = Vector3.zero;
            card.cardState = CardState.δ����;
        }
    }

    IEnumerator TurnBack(Card card)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * flipSpeed;
            card.gameObject.transform.rotation = Quaternion.Slerp(Quaternion.Euler(0f, 180f, 0f), Quaternion.Euler(0, 0f, 0), t);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        card.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    IEnumerator MissMatchCards()
    {
        yield return waitForSeconds;
        TurnBackCards();
        ClearCardConparison();
    }

    IEnumerator TransCardPosition(Card card)
    {

        float t = 0f;
        while (t < 1f)
        {
            if (card != null)
            {
                t += Time.deltaTime * moveSpeed;
                newPosition.z= 0.1f;
                card.gameObject.transform.position = Vector3.Lerp(card.gameObject.transform.position, newPosition, t);
                yield return null;
            }
            else { break; }
        }
    }
    IEnumerator waitFor1f()
    {
        yield return new WaitForSeconds(0.5f);
        if (number == 9)
        {
            int randomNumber = UnityEngine.Random.Range(0, 1);
            AddNewCardWhenClear(cardsToBePutIn[randomNumber], newPosition);
        }
        else
        {
            int randomNumber = UnityEngine.Random.Range(0, 4);
            AddNewCardWhenClear(cardsToBePutIn[randomNumber], newPosition);
        }
    }

        IEnumerator ReloadScene()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        IEnumerator FadeOut()
        {
            int i = 0;
            // ��ȡ�������Ⱦ��
            Renderer[] renderer0 = CardComparison[0].gameObject.GetComponentsInChildren<Renderer>();
            Renderer[] renderer1 = CardComparison[1].gameObject.GetComponentsInChildren<Renderer>();

            // ȷ����������Ⱦ�����
            if (renderer1 != null || renderer0 != null)
            {
                // ����ȫ��͸�����䵽��ȫ͸��
                for (float t = 1.0f; t > 0.0f; t -= fadeSpeed * Time.deltaTime)
                {
                    if (renderer0.Length > 0)
                    {
                        Color newColor0 = renderer0[0].material.color;
                        newColor0.a = t;
                        renderer0[i].material.color = newColor0;
                        Color newColor01 = renderer0[1].material.color;
                        newColor01.a = t;
                        renderer0[1].material.color = newColor01;
                    }
                    if (renderer1.Length > 0)
                    {
                        Color newColor1 = renderer1[0].material.color;
                        newColor1.a = t;
                        renderer1[0].material.color = newColor1;
                        Color newColor11 = renderer1[1].material.color;
                        newColor11.a = t;
                        renderer1[1].material.color = newColor11;
                    }
                    yield return null;
                }
                // ���ս���������

            }
            Destroy(CardComparison[0].gameObject);
            Destroy(CardComparison[1].gameObject);
            ClearCardConparison();

        }

        void AddSteps()
        {
       if (b < 5)
       {
            leftSteps += 10;
            stepsBox.text = leftSteps.ToString();
            b++;
            //Debug.Log("nihao");
        }
        else return;
        }

    void WanNengCards()
    {
           // Debug.Log("iii");
            isWanNeng = true;
    }
}
