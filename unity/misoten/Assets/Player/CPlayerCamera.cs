using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerCamera : MonoBehaviour {

	public Transform cameraTransform;   // ���삷��J����
	public float mouseSensitivity = 30.0f;  // �}�E�X���x
	public Vector3 centerOffset = new Vector3(0, 0, 0); // �J����������I�u�W�F�N�g�ʒu�̃I�t�Z�b�g

	private float distance = 3.0f;  // �J�����ƃI�u�W�F�N�g�܂ł̋���
	private float angleY = 0.0f;    // �J������Y������
	private float angleX = 0.0f;    // �J������X������

	[System.Obsolete]
	void Awake() {
		Screen.lockCursor = true;
	}

    // �S�Ă̏������I������ƂɃJ�����̈ʒu�𒲐����邽�߂�LateUpdate�ɂ���
    [System.Obsolete]
    void LateUpdate() {
		angleY += Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity * 10.0f;
		angleX += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity * 10.0f;

		if (Input.GetButtonDown("Fire1")) {
			Screen.lockCursor = true;
		}

		if (Input.GetKeyDown("1")) {
			Screen.lockCursor = false;
		}

		if (Input.GetKeyDown("q")) {
			centerOffset.y += 0.1f;
		}

		if (Input.GetKeyDown("e")) {
			centerOffset.y -= 0.1f;
		}

		if (Input.GetAxis("Mouse ScrollWheel") != 0) {
			distance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 500;
			distance = Mathf.Clamp(distance, 3.0f, 20.0f);
		}

		// �J�������I�u�W�F�N�g����p�x(20.0f, angleY, 0.0f)��distance�����ꂽ�ʒu�ɔz�u
		Vector3 center = transform.position + centerOffset;
		cameraTransform.position = center + (
			Quaternion.AngleAxis(angleY, Vector3.up) *
			Quaternion.AngleAxis(angleX, Vector3.right) *
			Quaternion.AngleAxis(20.0f, Vector3.right) *
			new Vector3(0, 0, -distance)
		);
		cameraTransform.LookAt(center);
	}
}