/*==================================================================================================
	MS2022
	[camera.h]
	�E�J����
----------------------------------------------------------------------------------------------------
	2021.10.15 @Author HAYASE SUZUKI
====================================================================================================
	History
		211015 �쐬

/*================================================================================================*/
#pragma once
#include "game_object.h"

// �J�����N���X
class CCamera : public CGameObject
{
private:

	static CCamera* m_Instance;      // �C���X�^���X

	RECT m_Viewport = RECT();		 // �r���[�|�[�g

	XMFLOAT4X4	m_ViewMatrix = XMFLOAT4X4();		 // �r���[�}�g���N�X
	XMFLOAT4X4	m_InvViewMatrix = XMFLOAT4X4();      // �r���[�]�u�}�g���N�X
	XMFLOAT4X4	m_ProjectionMatrix = XMFLOAT4X4();   // �v���W�F�N�V�����}�g���N�X


public:
	CCamera() { m_Instance = this; }
	static CCamera* GetInstance() { return m_Instance; }

	void Init();
	void Uninit();
	void Update();
	void Draw();
	//void DrawShadow();
	//void DrawWater();


	XMFLOAT4X4	GetViewMatrix() { return m_ViewMatrix; }
	XMFLOAT4X4	GetInvViewMatrix() { return m_InvViewMatrix; }
	XMFLOAT4X4	GetProjectionMatrix() { return m_ProjectionMatrix; }

};