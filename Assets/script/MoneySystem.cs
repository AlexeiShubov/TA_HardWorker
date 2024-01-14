using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    public static int moneyOnHand, moneyOnCard;
    public Text OnHandText, OnCardText;
    // Start is called before the first frame update
    void Start()
    {
        moneyOnCard = 0;
        moneyOnHand = 750;
    }

    // Update is called once per frame
    void Update()
    {
        OnHandText.text = "на руке" + moneyOnHand.ToString();
        OnCardText.text = "на карте" + moneyOnCard.ToString();
    }
}
