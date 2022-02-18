using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    private Text MoneyText;                 //�e�L�X�g�ύX�p

    public int money;                       //����

    BattleControler battleController;       //BattleContorller�擾�p
    SelectCursor selectCursor;              //SelectCursor.cs�擾�p
    OddsGauge oddsGauge;                    //OddsGauge.cs�擾�p

    private int state;                      //�o�g����Ԏ擾

    private bool GetMoney;                  //�������擾���镪��̃��[�v�~��

    void Start()
    {
        MoneyText = GameObject.Find("Canvas_UI(Clone)/BackGround01/GoldText").GetComponent<Text>();
        battleController = GameObject.Find("BattleManager").GetComponent<BattleControler>();
        selectCursor = GameObject.Find("SelectCursorManager(Clone)").GetComponent<SelectCursor>();
        oddsGauge = GameObject.Find("OddsGaugeManager(Clone)").GetComponent<OddsGauge>();

        money = 3000;
        GetMoney = false;

        MoneyText.text = string.Format("{0} G", money);
    }

    void Update()
    {
        state = battleController.GetState();
        
        //�t���O�؂�ւ�
        if(state == 2 && GetMoney == true)
        {
            GetMoney = false;
        }

        //�o�g�����I������猋�ʂ�����
        if(state == 4 && GetMoney == false)
        {
            //Red��I�����āARed������������
            if(battleController.GetRedWin() == true
                &&selectCursor.GetSelectRed() == true)
            {
                int sum = oddsGauge.GetRedNum() * 1000;
                money += sum;
                GetMoney = true;
            }
            else if(battleController.GetRedWin() == false
                && selectCursor.GetSelectRed() == false)
            {
                int sum = oddsGauge.GetBlueNum() * 1000;
                money += sum;
                GetMoney = true;
            }


        }

        //�������\���X�V
        MoneyText.text = string.Format("{0} G", money);

    }
}
