using UnityEngine;
using UnityEngine.UI;

public class ButtonSystem : MonoBehaviour
{
    public GameObject panelOutMoney, panelWithdrawMoney;
    public InputField outMoney, wiredeMoney;
    public int outMoneySum, wiredeMoneySum;

    public void OutMoneyResetToZero()
    {
        outMoney.text = "";
    }
    public void WiredeMoneyResetToZero()
    {
        wiredeMoney.text = "";
    }
    public void outMoneySystem()
    {
        outMoneySum = int.Parse(outMoney.text);

        if (outMoneySum > MoneySystem.moneyOnHand)
        {
            Debug.Log("Error");
        }
        else
        {
            if (outMoneySum <= MoneySystem.moneyOnHand)
            {
                MoneySystem.moneyOnHand = MoneySystem.moneyOnHand - outMoneySum;
                MoneySystem.moneyOnCard = MoneySystem.moneyOnCard + outMoneySum;
                outMoney.text = null;
            }
            else if (outMoneySum <= 0)
            {
                Debug.Log("Error");
            }
        }
    }
    public void WiredeMoneySystem()
    {
        wiredeMoneySum = int.Parse(wiredeMoney.text);
        if(wiredeMoneySum>MoneySystem.moneyOnCard)
        {
            Debug.Log("Error");
        }
        else
        {
            if(wiredeMoneySum<= MoneySystem.moneyOnCard)
            {
                MoneySystem.moneyOnHand = MoneySystem.moneyOnHand + wiredeMoneySum;
                MoneySystem.moneyOnCard = MoneySystem.moneyOnCard - wiredeMoneySum;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
