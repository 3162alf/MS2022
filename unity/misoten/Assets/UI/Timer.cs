using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float CountDownTime;  // �J�E���g�_�E���^�C��
    public Text TimerCountDown;         //�e�L�X�g�^�̕ϐ�

    void Start()
    {
        CountDownTime = 30.0F;    // �J�E���g�_�E���J�n�b�����Z�b�g
    }

    void Update()
    {
        // �J�E���g�_�E���^�C���𐮌`���ĕ\��
        //TimerCountDown.text = String.Format("00", CountDownTime);
        TimerCountDown.text = CountDownTime.ToString("00");
        // �o�ߎ����������Ă���
        CountDownTime -= Time.deltaTime;

        if (CountDownTime <= 0.0F)
        {
            CountDownTime = 0.0F;
        }
    }

    // ���Ԏ擾
    public float GetTime() {
        return CountDownTime;
    }

    // ���ԃ��Z�b�g
    public void ResetTime() {
        CountDownTime = 30.0f;
    }
}
