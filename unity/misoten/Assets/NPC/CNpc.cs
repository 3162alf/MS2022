using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CNpc : MonoBehaviour
{
	[SerializeField] private float wanderRange;
	private NavMeshAgent navMeshAgent;
	private NavMeshHit navMeshHit;
	private int waittime;
	private float counter = 0;

	// Animator �R���|�[�l���g
	private Animator animator;

	// NPC���
	enum NPC_STATE {
		WAIT,
		WALK,
    }
	private NPC_STATE state;

	void Start() {
		navMeshAgent = GetComponent<NavMeshAgent>();
		navMeshAgent.avoidancePriority = Random.Range(0, 100);

		animator = GetComponent<Animator>();

		state = NPC_STATE.WAIT;
		waittime = Random.Range(5, 10);
	}

	void Update() {
		if (state == NPC_STATE.WALK) {
			//�@�ړI�n�ɓ����������ǂ����̔���
			if (navMeshAgent.remainingDistance < 0.5f) {
				state = NPC_STATE.WAIT;
				animator.SetBool("isWalk", false);
			}
		}
		else if (state == NPC_STATE.WAIT) {
			counter += Time.deltaTime;
			// waittime�����~�܂��Ă���܂�����
			if (counter >= waittime) {
				SetDestination();

				waittime = Random.Range(5, 10);
				counter = 0;
			}
		}
	}

	void RandomWander() {
		//�w�肵���ړI�n�ɏ�Q�������邩�ǂ����A�����������B�\�Ȃ̂����m�F���Ė��Ȃ���΃Z�b�g����B
		//pathPending �o�H�T���̏����ł��Ă��邩�ǂ���
		if (!navMeshAgent.pathPending) {
			if (navMeshAgent.remainingDistance <= 0.1f) {
				//hasPath �G�[�W�F���g���o�H�������Ă��邩�ǂ���
				//navMeshAgent.velocity.sqrMagnitude�̓X�s�[�h
				if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f) {
					SetDestination();
				}
			}
		}
	}

	void SetDestination() {
		Vector3 randomPos = new Vector3(Random.Range(-wanderRange, wanderRange), 0, Random.Range(-wanderRange, wanderRange));
		//Debug.Log(randomPos);
		//SamplePosition�͐ݒ肵���ꏊ����5�͈̔͂ōł��߂�������Bake���ꂽ�ꏊ��T���B
		NavMesh.SamplePosition(randomPos, out navMeshHit, 5, 1);
		navMeshAgent.destination = navMeshHit.position;
		RandomWander();

		state = NPC_STATE.WALK;
		animator.SetBool("isWalk", true);
	}
}
