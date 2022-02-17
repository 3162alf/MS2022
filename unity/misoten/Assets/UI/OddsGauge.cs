using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OddsGauge : MonoBehaviour
{
    [SerializeField] private Slider oddsGauge;   //�Q�[�W�̃I�u�W�F�N�g
    [SerializeField] private Text redtext;
    [SerializeField] private Text bluetext;

    //[SerializeField] private Camera targetCamera;

    private int StartPattern;                   //�Q�[�W�̏����ʒu�����߂�ϐ�
    private int OddsGauge_2;                    //���΂̃Q�[�W
    private int StopOddsTime;                   //Odds�̕ϓ����Ƃ܂鎞��
    private int LotteryTime;                    //Odds�̕ϓ��������鎞��
    private int LotteryCount;                   //Odds�Q�[�W��ϓ��������
    private int RedNum;                         //Red�̔{���p
    private int BlueNum;                        //Blue�̔{���p

    private float countDownTime;                //���[���Ԏ擾�p

    private bool islastOdds;


    void Start()
    {
        oddsGauge = GameObject.Find("Canvas_Odds(Clone)/OddsGauge").GetComponent<Slider>();
        redtext = GameObject.Find("Canvas_Odds(Clone)/BackGround04/RedText").GetComponent<Text>();
        bluetext = GameObject.Find("Canvas_Odds(Clone)/BackGround03/BlueText").GetComponent<Text>();

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
                BlueNum = BlueNum * (int)oddsGauge.value/10;
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
        LotteryTime = 5;                        //Odds��ϓ������鎞��
        LotteryCount = 5;                       //Odds�Q�[�W��ϓ��������
        countDownTime = Timer.CountDownTime;    //���[���Ԃ��擾����
        islastOdds = false;


        //targetCamera = GameObject.Find("Player/Camera (1)").GetComponent<Camera>();
        //renderMode�ɃJ�������A�^�b�`����
        //GameObject.Find("Canvas_Odds(Clone)").GetComponent<Canvas>().worldCamera = targetCamera;
    }

    void Update()
    {
        countDownTime = Timer.CountDownTime;

        //5�̔{�����Ƃ�Odds�Q�[�W��؂�ւ���
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

            if(oddsGauge.value == OddsGauge_2)
            {
                RedNum = 1;
                BlueNum = 1;
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

            if (oddsGauge.value == OddsGauge_2)
            {
                RedNum = 1;
                BlueNum = 1;
            }

            islastOdds = true;

        }

        //���[��(�Q�[�W���Z���ق�)�����Ȃ��ق����{�����オ��B
        redtext.text = string.Format("�~{0}",RedNum);
        bluetext.text = string.Format("�~{0}", BlueNum);
    }
}
