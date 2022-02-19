using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OddsGauge : MonoBehaviour
{
    [SerializeField] private Slider oddsGauge;      //�Q�[�W�̃I�u�W�F�N�g
    [SerializeField] private Text redtext;          //Red���{���̃e�L�X�g�p
    [SerializeField] private Text bluetext;         //Blue���{���̃e�L�X�g�p

    private int StartPattern;                       //�Q�[�W�̏����ʒu�����߂�ϐ�
    private int OddsGauge_2;                        //���΂̃Q�[�W
    private int StopOddsTime;                       //Odds�̕ϓ����Ƃ܂鎞��
    private int LotteryTime;                        //Odds�̕ϓ��������鎞��
    [SerializeField] private int LotteryCount;      //Odds�Q�[�W��ϓ��������
    private int RedNum;                             //Red�̔{���p
    private int BlueNum;                            //Blue�̔{���p

    private float countDownTime;                    //���[���Ԏ擾�p

    private bool islastOdds;                        //�Ō��Odds�ύX������Ƃ��̃t���O

    private int state;                              //�o�g����Ԃ̎擾�p

    private bool isInit;                            //���������������ǂ���
    Money money_cs;                                 //Money.cs�擾�p
    BattleControler battleController;               //BattleController.cs�擾�p

    void Start()
    {
        oddsGauge = GameObject.Find("Canvas_Odds(Clone)/OddsGauge").GetComponent<Slider>();
        redtext = GameObject.Find("Canvas_Odds(Clone)/BackGround04/RedText").GetComponent<Text>();
        bluetext = GameObject.Find("Canvas_Odds(Clone)/BackGround03/BlueText").GetComponent<Text>();

        money_cs = GameObject.Find("MoneyManager(Clone)").GetComponent<Money>();
        battleController = GameObject.Find("BattleManager").GetComponent<BattleControler>();

        isInit = false;

        //������
        InitOddsGauge();

        //targetCamera = GameObject.Find("Player/Camera (1)").GetComponent<Camera>();
        //renderMode�ɃJ�������A�^�b�`����
        //GameObject.Find("Canvas_Odds(Clone)").GetComponent<Canvas>().worldCamera = targetCamera;
    }

    void Update()
    {
        //�o�g����Ԃ̎擾
        state = battleController.GetState();
        //���[���Ԏ擾
        countDownTime = Timer.CountDownTime;

        //Odds�Q�[�W��؂�ւ���
        //10�b�ȉ��ɂȂ�����Odds�Q�[�W�̕ϓ��͌����Ȃ��Ȃ�
        if ((int)countDownTime == LotteryTime * LotteryCount
            && (int)countDownTime >= StopOddsTime)
        {
            float randOdds = 1.0f;

            if (Random.Range(1, 3) == 2) randOdds = -1.0f;
            

            oddsGauge.value += (float)(Random.Range(1, 3) * 10) * randOdds;
            OddsGauge_2 = 100 - (int)oddsGauge.value;
            RedNum = OddsGauge_2 / 10;
            BlueNum = (int)oddsGauge.value / 10;

            //�����䗦�ɂȂ�����m���ŕЕ��̔{����������
            if(oddsGauge.value == OddsGauge_2)
            {
                //�Ԃ̔{�����኱�グ��
                if(Random.Range(1,3) == 1)
                {
                    RedNum = 2;
                    BlueNum = 1;
                }
                else
                {
                    RedNum = 1;
                    BlueNum = 2;
                }
            }

            //Odds�Q�[�W��10:0�ɂȂ��Ă��܂����ꍇ�̑[�u
            if(oddsGauge.value >= 100)
            {
                oddsGauge.value = 90;
                OddsGauge_2 = 100 - (int)oddsGauge.value;
                RedNum = OddsGauge_2 / 10;
                BlueNum = (int)oddsGauge.value / 10;
            }

            //Odds�Q�[�W��0:10�ɂȂ��Ă��܂����ꍇ�̑[�u
            if (oddsGauge.value <= 0)
            {
                oddsGauge.value = 10;
                OddsGauge_2 = 100 - (int)oddsGauge.value;
                RedNum = OddsGauge_2 / 10;
                BlueNum = (int)oddsGauge.value / 10;
            }


            LotteryCount--;
        }

        //���[���Ԃ��O�ɂȂ�����Ō�ɂ�����x�ϓ�������
        if((int)countDownTime == 0 && islastOdds == false)
        {
            float randOdds = 1.0f;
            if (Random.Range(1, 3) == 2) randOdds = -1.0f;

            oddsGauge.value += (float)(Random.Range(1, 3) * 10) * randOdds;
            OddsGauge_2 = 100 - (int)oddsGauge.value;
            RedNum = OddsGauge_2 / 10;
            BlueNum = (int)oddsGauge.value / 10;

            //�����䗦�ɂȂ�����m���ŕЕ��̔{����������
            if (oddsGauge.value == OddsGauge_2)
            {
                //�Ԃ̔{�����኱�グ��
                if (Random.Range(1, 3) == 1)
                {
                    RedNum = 2;
                    BlueNum = 1;
                }
                else
                {
                    RedNum = 1;
                    BlueNum = 2;
                }
            }

            //Odds�Q�[�W��10:0�ɂȂ��Ă��܂����ꍇ�̑[�u
            if (oddsGauge.value >= 100)
            {
                oddsGauge.value = 90;
                OddsGauge_2 = 100 - (int)oddsGauge.value;
                RedNum = OddsGauge_2 / 10;
                BlueNum = (int)oddsGauge.value / 10;
            }

            //Odds�Q�[�W��0:10�ɂȂ��Ă��܂����ꍇ�̑[�u
            if (oddsGauge.value <= 0)
            {
                oddsGauge.value = 10;
                OddsGauge_2 = 100 - (int)oddsGauge.value;
                RedNum = OddsGauge_2 / 10;
                BlueNum = (int)oddsGauge.value / 10;
            }

            //���ڈȍ~�ʂ�Ȃ��悤��
            islastOdds = true;

            //�|������1000�~
            money_cs.money -= 1000;

        }

        //��A�̎������I�������ēx�Q�[�W�̏�����
        //state��interval�ɂȂ����珉����
        if(state == 0 && isInit == false)
        {
            InitOddsGauge();
        }


        //���[��(�Q�[�W���Z���ق�)�����Ȃ��ق����{�����オ��B
        redtext.text = string.Format("�~{0}",RedNum);
        bluetext.text = string.Format("�~{0}", BlueNum);
    }

    //Red�̔{���擾
    public int GetRedNum() {
        return RedNum;
    }

    //Blue�̔{���擾
    public int GetBlueNum() {
        return BlueNum;
    }

    //�Q�[�W���Odds�̏�����
    private void InitOddsGauge() {

        //������
        RedNum = 1;
        BlueNum = 1;

        //�Q�[�W�̏����ʒu�����߂�B3�p�^�[��
        StartPattern = Random.Range(1, 4);

        switch (StartPattern)
        {
            case 1:
                //��:�� 5:5
                oddsGauge.value = 50;
                OddsGauge_2 = 100 - (int)oddsGauge.value;
                break;

            case 2:
                //��:�� 3:7
                oddsGauge.value = 30;
                OddsGauge_2 = 100 - (int)oddsGauge.value;

                RedNum = RedNum * OddsGauge_2 / 10;
                BlueNum = BlueNum * (int)oddsGauge.value / 10;
                break;

            case 3:
                //��:�� 7:3
                oddsGauge.value = 70;
                OddsGauge_2 = 100 - (int)oddsGauge.value;

                RedNum = RedNum * OddsGauge_2 / 10;
                BlueNum = BlueNum * (int)oddsGauge.value / 10;
                break;
        }

        StopOddsTime = 10;                      //�c��10�b�ɂȂ�����ϓ����~�߂�
        LotteryTime = 2;                        //Odds��ϓ������鎞��
        LotteryCount = 14;                       //Odds�Q�[�W��ϓ��������
        countDownTime = Timer.CountDownTime;    //���[���Ԃ��擾����
        islastOdds = false;
        isInit = true;
    }

    //�������̃t���O�̃Z�b�g
    public void SetisInit(bool isinit) {
        isInit = isinit;
    }
}
