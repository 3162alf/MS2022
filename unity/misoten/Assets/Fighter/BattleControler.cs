using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControler : SingletonMonoBehaviour<BattleControler> {
    [SerializeField] GameObject prb_fighter1, prb_fighter2;    // �t�@�C�^�[�v���n�u
    [SerializeField] GameObject effect;                // �X�|�[���G�t�F�N�g
    [SerializeField] int hp1, hp2;                     // �t�@�C�^�[��HP
    [SerializeField] int maxhp = 100;                  // �t�@�C�^�[�̍ő�HP

    GameObject fighter1, fighter2;                      // �t�@�C�^�[�I�u�W�F�N�g
    Animator animator1, animator2;                     // �t�@�C�^�[�̃A�j���[�^�[
    bool move, damage;                                 // �t�@�C�^�[�̍s���t���O
    bool play = false;
    float timer = 0;                                   // �^�C�}�[


    // �o�g���t���[�X�e�[�g
    enum BATTLESTATE {
        SPAWN,
        WAIT,
        FIGHT,
        END,
    }
    BATTLESTATE state;

    // Start is called before the first frame update
    void Start() {       
        state = BATTLESTATE.SPAWN;
        fighter1 = null;
        fighter2 = null;
    }

    // Update is called once per frame
    void Update() {
        switch (state) {
            case BATTLESTATE.SPAWN:
                move = false;

                if (play == false) {
                    // �X�|�[���G�t�F�N�g�Đ�
                    Instantiate(effect, new Vector3(0.8f, 2.0f, 0.0f), Quaternion.identity);
                    Instantiate(effect, new Vector3(-0.8f, 2.0f, 0.0f), Quaternion.identity);
                    play = true;
                }

                if (fighter2 != null) {
                    // �ҋ@�X�e�[�g�Ɉڍs
                    state = BATTLESTATE.WAIT;
                    play = false;
                }
                break;
            case BATTLESTATE.WAIT:

                // �t�@�C�g�X�e�[�g�Ɉڍs
                if (Input.GetKey(KeyCode.R)) {
                    animator1.SetBool("Wait", false);
                    animator2.SetBool("Wait", false);
                    state = BATTLESTATE.FIGHT;
                }
                break;
            case BATTLESTATE.FIGHT:
                if (animator1.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Fist Fight B") {

                    if (animator1.GetBool("Fight")) {
                        animator1.SetBool("Fight", false);
                        animator2.SetBool("Fight", false);

                        int rand = Random.Range(1, 100);
                        Debug.Log(rand);
                        if (rand < 50) {
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

                if(animator1.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Two Handed Sword Death") {
                    hp1 = 0;
                }
                if (animator2.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Two Handed Sword Death") {
                    hp2 = 0;
                }

                if (animator1.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Boxing") {
                    if (!damage) {
                        damage = true;
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


                if (move) {
                    Vector3 vec = fighter1.transform.position - fighter2.transform.position;
                    fighter2.transform.position += vec.normalized * 0.02f;
                    fighter1.transform.position -= vec.normalized * 0.02f;
                    Debug.Log(vec.magnitude);
                    if (vec.magnitude <= 1.55f) {
                        animator1.SetBool("Fight", true);
                        animator2.SetBool("Fight", true);
                        move = false;
                    }
                }

                if(hp1 == 0 || hp2 == 0) {
                    timer += Time.deltaTime;

                    if (timer >= 5) {
                        state = BATTLESTATE.END;
                        timer = 0;
                    }
                }
                break;
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

                if(fighter2 == null) {
                    // �t�@�C�g�X�e�[�g�Ɉڍs
                    if (Input.GetKey(KeyCode.R)) {                       
                        state = BATTLESTATE.SPAWN;
                        play = false;
                    }
                }
                break;

        }       
    }

    public void CreateorDestroy() {
        if (fighter1 == null && fighter2 == null) {
            // �t�@�C�^�[��̂𐶐�
            fighter1 = Instantiate(prb_fighter1, new Vector3(0.8f, 1.0f, 0.0f), Quaternion.Euler(0.0f, -90.0f, 0.0f));
            // �A�j���[�^�[�擾
            animator1 = fighter1.GetComponent<Animator>();
            // �ҋ@���[�V����true
            animator1.SetBool("Wait", true);
            // HP������
            hp1 = hp2 = maxhp;

        }
        else if(fighter2 == null) {
            fighter2 = Instantiate(prb_fighter2, new Vector3(-0.8f, 1.0f, 0.0f), Quaternion.Euler(0.0f, 90.0f, 0.0f));
            animator2 = fighter2.GetComponent<Animator>();
            animator2.SetBool("Wait", true);
        }
        else if (fighter1 != null){
            Destroy(fighter1);
            fighter1 = null;
        }
        else {
            Destroy(fighter2);
            fighter2 = null;
        }
    }
}
