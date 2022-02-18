using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    //�����̃v���n�u�̂݃A�^�b�`
    [SerializeField] private GameObject prefabCanvasOdds;
    [SerializeField] private GameObject prefabCanvasUI;
    [SerializeField] private GameObject prefabOddsGaugeManager;
    [SerializeField] private GameObject prefabCursorManager;
    [SerializeField] private GameObject prefabDateTimeManager;
    [SerializeField] private GameObject prefabMoneyManager;

    // �C���X�^���X
    private GameObject UITimer;
    private GameObject UIVote;
    private GameObject UICursor;
    

    //Canvas��render camera�ɐݒ肷��J����
    [SerializeField] private Camera targetCamera;

    void Start()
    {
        //UI�ɕK�v�ȃv���n�u�𐶐�
        GameObject CanvasOdds = Instantiate(prefabCanvasOdds, Vector3.zero, Quaternion.identity);
        Instantiate(prefabCanvasUI, Vector3.zero, Quaternion.identity);
        Instantiate(prefabOddsGaugeManager, Vector3.zero, Quaternion.identity);
        Instantiate(prefabCursorManager, Vector3.zero, Quaternion.identity);
        Instantiate(prefabDateTimeManager, Vector3.zero, Quaternion.identity);
        Instantiate(prefabMoneyManager, Vector3.zero, Quaternion.identity);

        // �L�����o�X�̃^�C�}�[�Ɠ��[����
        UITimer = CanvasOdds.transform.Find("BackGround01").gameObject;
        UIVote = CanvasOdds.transform.Find("BackGround02").gameObject;
        UICursor = CanvasOdds.transform.Find("SelectCursor").gameObject;

        //renderMode�ɃJ�������A�^�b�`����
        targetCamera = GameObject.Find("Player/Camera (1)").GetComponent<Camera>();
        GameObject.Find("Canvas_Odds(Clone)").GetComponent<Canvas>().worldCamera = targetCamera;
        GameObject.Find("Canvas_UI(Clone)").GetComponent<Canvas>().worldCamera = targetCamera;
    }

    void Update()
    {
        
    }

    // UI�̕\��/��\��
    public void SetUIActive(bool b) {
        UITimer.SetActive(b);
        UIVote.SetActive(b);
        UICursor.SetActive(b);
    }

    // TimerGetter
    public GameObject GetTimer() {
        return UITimer;
    }
}
