using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCursor : MonoBehaviour
{
    [SerializeField] private GameObject cursor; //�L�����o�X����SelectCursor������
    [SerializeField] private bool SelectRed;                     //Red�ɓ��[���Ă��邩�ǂ���
    private float countDownTime;                //���[���Ԏ擾�p

    void Start()
    {
        //�J�[�\����T��
        cursor = GameObject.Find("Canvas_Odds(Clone)/SelectCursor");

        SelectRed = true;
        countDownTime = Timer.CountDownTime;
    }

    void Update()
    {
        countDownTime = Timer.CountDownTime;

        //0�b�ɂȂ�܂ł͑I���\
        if(countDownTime > 0.0f)
        {
            //1�L�[���͂�Red�I�����
            if (Input.GetKey(KeyCode.Alpha1))
            {
                cursor.transform.localPosition = new Vector2(-125.0f, 250.0f);
                SelectRed = true;
            }

            //2�L�[���͂�Blue�I�����
            if (Input.GetKey(KeyCode.Alpha2))
            {
                cursor.transform.localPosition = new Vector2(15.0f, 250.0f);
                SelectRed = false;
            }
        }

        if(cursor.transform.localPosition.x == -125.0f)
        {
            SelectRed = true;
        }

    }


    public bool GetSelectRed() {
        return SelectRed;
    }

}
