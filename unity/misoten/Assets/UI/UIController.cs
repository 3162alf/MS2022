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

    //Canvas��render camera�ɐݒ肷��J����
    [SerializeField] private Camera targetCamera;

    void Start()
    {
        //UI�ɕK�v�ȃv���n�u�𐶐�
        Instantiate(prefabCanvasOdds, Vector3.zero, Quaternion.identity);
        Instantiate(prefabCanvasUI, Vector3.zero, Quaternion.identity);
        Instantiate(prefabOddsGaugeManager, Vector3.zero, Quaternion.identity);
        Instantiate(prefabCursorManager, Vector3.zero, Quaternion.identity);
        Instantiate(prefabDateTimeManager, Vector3.zero, Quaternion.identity);

        //renderMode�ɃJ�������A�^�b�`����
        targetCamera = GameObject.Find("Player/Camera (1)").GetComponent<Camera>();
        GameObject.Find("Canvas_Odds(Clone)").GetComponent<Canvas>().worldCamera = targetCamera;
        GameObject.Find("Canvas_UI(Clone)").GetComponent<Canvas>().worldCamera = targetCamera;
    }

    void Update()
    {
        
    }
}
