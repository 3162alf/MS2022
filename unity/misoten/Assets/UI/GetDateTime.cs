using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDateTime : MonoBehaviour
{
    DateTime dt;
    private Text realtimeText;         //�e�L�X�g�^�̕ϐ�

    void Start()
    {
        //�e�L�X�g��T��
        realtimeText = GameObject.Find("Canvas_UI(Clone)/BackGround03/RealTimeText").GetComponent<Text>();

        //���ݓ��������
        dt = DateTime.Now;

        realtimeText.text = dt.ToString();
    }


    void Update()
    {
        //���ݓ��������
        dt = DateTime.Now;

        realtimeText.text = dt.ToString();
    }
}
