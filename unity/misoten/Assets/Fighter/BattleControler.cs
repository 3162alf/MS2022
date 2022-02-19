using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleControler : SingletonMonoBehaviour<BattleControler> {
    [SerializeField] GameObject prb_fighter1, prb_fighter2;    // �t�@�C�^�[�v���n�u
    [SerializeField] GameObject effect;                // �X�|�[���G�t�F�N�g
    [SerializeField] int hp1, hp2;                     // �t�@�C�^�[��HP
    [SerializeField] int maxhp = 100;                  // �t�@�C�^�[�̍ő�HP
    [SerializeField] int interval = 10;                // �C���^�[�o������
    [SerializeField] int spawntime = 15;               // �t�@�C�^�[��������

    GameObject fighter1, fighter2;                     // �t�@�C�^�[�I�u�W�F�N�g
    Animator animator1, animator2;                     // �t�@�C�^�[�̃A�j���[�^�[
    bool move, damage;                                 // �t�@�C�^�[�̍s���t���O
    bool play = false;                                 // �G�t�F�N�g�Đ��t���O
    float timer = 0;                                   // �^�C�}�[
    int ratio = 50;


    ReadyController Readycontroller; 
    RedWinController RedWincontroller; 
    BlueWinController BlueWincontroller; 

    // UIManager
    UIController cs_uictrl;
    Timer cs_timer;
    OddsGauge cs_oddsgauge;

    // �Q�[�W�p
    [SerializeField] private Image[] Fighter1greenGauge;
    [SerializeField] private Image[] Fighter2greenGauge;

    [SerializeField] private Color[] color = new Color[4];
    private int gaugeNum = 4;

    // �o�g���t���[�X�e�[�g
    enum BATTLESTATE {
        INTERVAL,
        SPAWN,
        WAIT,
        READY,
        FIGHT,
        END,
    }
    BATTLESTATE state;

    // Start is called before the first frame update
    void Start() {
        state = BATTLESTATE.INTERVAL;
        fighter1 = null;
        fighter2 = null;

        // UIManager�擾
        GameObject uim = GameObject.Find("UIManager");
        cs_uictrl = uim.GetComponent<UIController>();
        GameObject uitimer = cs_uictrl.GetTimer();
        cs_timer = uitimer.transform.Find("Timer").gameObject.GetComponent<Timer>();

        cs_oddsgauge = GameObject.Find("OddsGaugeManager(Clone)").GetComponent<OddsGauge>();

        GameObject word = GameObject.Find("ready");
        Readycontroller = word.GetComponent<ReadyController>();

        word = GameObject.Find("redwin");
        RedWincontroller = word.GetComponent<RedWinController>();

        word = GameObject.Find("bluewin");
        BlueWincontroller = word.GetComponent<BlueWinController>();
    }

    // Update is called once per frame
    void Update() {
        switch (state) {
            // ���[����-----------------------------------------------------------
            case BATTLESTATE.INTERVAL:
                //timer += Time.deltaTime;

                // interval�o�ߌ�L��������
                if (cs_timer.GetTime() <= spawntime) {
                    state = BATTLESTATE.SPAWN;
                    timer = 0;
                }
                break;
            // �t�@�C�^�[����-----------------------------------------------------
            case BATTLESTATE.SPAWN:
                move = false;

                if (play == false) {
                    // �X�|�[���G�t�F�N�g�Đ�
                    Instantiate(effect, new Vector3(0.8f, 3.0f, 0.0f), Quaternion.identity);
                    Instantiate(effect, new Vector3(-0.8f, 3.0f, 0.0f), Quaternion.identity);
                    play = true;
                }

                if (fighter2 != null) {
                    // �ҋ@�X�e�[�g�Ɉڍs
                    state = BATTLESTATE.WAIT;
                    play = false;
                }
                break;
            // �ҋ@---------------------------------------------------------------
            case BATTLESTATE.WAIT:

                // �t�@�C�g�X�e�[�g�Ɉڍs
                if (cs_timer.GetTime() <= 0.0f) {                   
                    Readycontroller.StartExpansion();
                    state = BATTLESTATE.READY;
                    // UI��\��
                    cs_uictrl.SetUIActive(false);
                }
                break;
            // �����\��-----------------------------------------------------------
            case BATTLESTATE.READY:
                break;
            // �t�@�C�^�[�퓬-----------------------------------------------------
            case BATTLESTATE.FIGHT:
                if (animator1.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Fist Fight B") {

                    if (animator1.GetBool("Fight")) {
                        animator1.SetBool("Fight", false);
                        animator2.SetBool("Fight", false);

                        // �_���[�W���󂯂���𗐐��Ō��߂�
                        int rand = Random.Range(1, 100);
                        if (rand < ratio) {
                            animator1.SetBool("Boxing", false);
                            animator2.SetBool("Boxing", true);
                            animator1.SetBool("Wait", true);

                            if (hp2 <= 10) {
                                animator2.SetBool("Death", true);
                                animator1.SetBool("Victory", true);
                            }
                        }
                        else {
                            animator1.SetBool("Boxing", true);
                            animator2.SetBool("Boxing", false);
                            animator2.SetBool("Wait", true);

                            if (hp1 <= 10) {
                                animator1.SetBool("Death", true);
                                animator2.SetBool("Victory", true);
                            }
                        }
                    }
                    damage = false;
                }

                // ���S�������HP��0��
                if (animator1.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Two Handed Sword Death") {
                    // �Q�[�W�p
                    GaugeReducationFighter1(maxhp);

                    if (hp1 != 0) {
                        BlueWincontroller.StartExpansion();
                        hp1 = 0;
                    }
                }
                if (animator2.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Two Handed Sword Death") {
                    // �Q�[�W�p
                    GaugeReducationFighter2(maxhp);

                    if (hp2 != 0) {
                        RedWincontroller.StartExpansion();
                        hp2 = 0;
                    }
                }

                // �_���[�W���[�V�����Đ�
                if (animator1.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Boxing") {
                    if (!damage) {
                        damage = true;

                        // �Q�[�W�p
                        GaugeReducationFighter1(10f);

                        hp1 -= 10;
                    }

                    AnimatorStateInfo animInfo = animator1.GetCurrentAnimatorStateInfo(0);
                    if (animInfo.normalizedTime >= 1.0f) {
                        move = true;
                        fighter1.transform.LookAt(fighter2.transform.position);
                        fighter2.transform.LookAt(fighter1.transform.position);
                        animator1.SetBool("Boxing", false);
                        animator2.SetBool("Wait", false);
                    }
                }
                else if (animator2.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Boxing") {
                    if (!damage) {
                        damage = true;

                        // �Q�[�W�p
                        GaugeReducationFighter2(10f);

                        hp2 -= 10;
                    }

                    AnimatorStateInfo animInfo = animator2.GetCurrentAnimatorStateInfo(0);
                    if (animInfo.normalizedTime >= 1.0f) {
                        move = true;
                        fighter1.transform.LookAt(fighter2.transform.position);
                        fighter2.transform.LookAt(fighter1.transform.position);
                        animator1.SetBool("Wait", false);
                        animator2.SetBool("Boxing", false);
                    }
                }


                // �݂��ɋ߂Â��Đ퓬���[�V�������Đ�
                if (move) {
                    Vector3 vec = fighter1.transform.position - fighter2.transform.position;
                    fighter2.transform.position += vec.normalized * 0.02f;
                    fighter1.transform.position -= vec.normalized * 0.02f;
                    if (vec.magnitude <= 1.55f) {
                        animator1.SetBool("Fight", true);
                        animator2.SetBool("Fight", true);
                        move = false;
                    }
                }

                // �ǂ��炩��HP��0�ɂȂ����玎���I��
                if (hp1 == 0 || hp2 == 0) {
                    timer += Time.deltaTime;

                    if (timer >= 5) {
                        state = BATTLESTATE.END;
                        timer = 0;
                    }
                }
                break;
            // �����I��-----------------------------------------------------------
            case BATTLESTATE.END:
                if (play == false) {
                    Vector3 pos1, pos2;
                    pos1 = fighter1.transform.position;
                    pos2 = fighter2.transform.position;

                    if (hp1 == 0) {
                        pos1.y += 1.0f;
                        pos2.y += 1.0f;
                    }
                    else {
                        pos1.y += 1.0f;
                        pos2.y += 1.0f;
                    }

                    // �X�|�[���G�t�F�N�g�Đ�
                    Instantiate(effect, pos1, Quaternion.identity);
                    Instantiate(effect, pos2, Quaternion.identity);
                    play = true;
                }

                if (fighter2 == null) {
                    state = BATTLESTATE.INTERVAL;
                    play = false;

                    // �^�C�}�[���Z�b�g
                    cs_timer.ResetTime();
                    // UI��\��
                    cs_uictrl.SetUIActive(true);

                    //�Q�[�W�̏������p
                    cs_oddsgauge.SetisInit(false);
                }
                break;

        }
    }

    // �v���C���[�������폜
    public void CreateorDestroy() {
        if (fighter1 == null && fighter2 == null) {
            // �t�@�C�^�[��̂𐶐�
            fighter1 = Instantiate(prb_fighter1, new Vector3(0.8f, 2.0f, 0.0f), Quaternion.Euler(0.0f, -90.0f, 0.0f));
            // �A�j���[�^�[�擾
            animator1 = fighter1.GetComponent<Animator>();
            // �ҋ@���[�V����true
            animator1.SetBool("Wait", true);
            // HP������
            hp1 = hp2 = maxhp;

        }
        else if (fighter2 == null) {
            fighter2 = Instantiate(prb_fighter2, new Vector3(-0.8f, 2.0f, 0.0f), Quaternion.Euler(0.0f, 90.0f, 0.0f));
            animator2 = fighter2.GetComponent<Animator>();
            animator2.SetBool("Wait", true);
        }
        else if (fighter1 != null) {
            Destroy(fighter1);
            fighter1 = null;
        }
        else {
            Destroy(fighter2);
            fighter2 = null;
        }
    }

    public void StartFight() {
        animator1.SetBool("Wait", false);
        animator2.SetBool("Wait", false);
        state = BATTLESTATE.FIGHT;
    }

    // ���s���擾
    public bool GetRedWin() {
        if (hp1 <= 0) {
            return false;
        }
        else {
            return true;
        }
    }

    // �Q�[�W�p
    public void GaugeReducationFighter1(float reducationValue, float time = 0.8f) {

        var valueTo = (hp1 - reducationValue) / maxhp;

        for (int i = 0; i < gaugeNum; i++) {
            if (valueTo > 0.75f)
                Fighter1greenGauge[i].color = Color.Lerp(color[1], color[0], (valueTo - 0.75f) * 4f);
            else if (valueTo > 0.25f)
                Fighter1greenGauge[i].color = Color.Lerp(color[2], color[1], (valueTo - 0.25f) * 4f);
            else
                Fighter1greenGauge[i].color = Color.Lerp(color[3], color[2], valueTo * 4f);

            Fighter1greenGauge[i].fillAmount = valueTo;
        }
    }

    public void GaugeReducationFighter2(float reducationValue, float time = 0.8f) {

        var valueTo = (hp2 - reducationValue) / maxhp;


        for (int i = 0; i < gaugeNum; i++) {
            if (valueTo > 0.75f)
                Fighter2greenGauge[i].color = Color.Lerp(color[1], color[0], (valueTo - 0.75f) * 4f);
            else if (valueTo > 0.25f)
                Fighter2greenGauge[i].color = Color.Lerp(color[2], color[1], (valueTo - 0.25f) * 4f);
            else
                Fighter2greenGauge[i].color = Color.Lerp(color[3], color[2], valueTo * 4f);

            Fighter2greenGauge[i].fillAmount = valueTo;
        }
    }

    public int GetState() {
        int a;

        a = (int)state;

        return a;
    }
}
