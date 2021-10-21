/*==================================================================================================
	MS2022
	[player.h]
	�E�v���C���[
----------------------------------------------------------------------------------------------------
	2021.10.15 @Author HAYASE SUZUKI
====================================================================================================
	History
		211015 �쐬

/*================================================================================================*/
#pragma once

#include "game_object.h"
#include "animation_model.h"
#include "shader.h"

// �v���C���[�N���X�i�Q�[���I�u�W�F�N�g�j
class CPlayer : public CGameObject {
private:
	CAnimationModel* m_AnimModel = NULL;
	CShader* m_Shader = NULL;

public:

	void Init();
	void Uninit();
	void Update();
	void Draw();

	CAnimationModel* GetAnim() { return m_AnimModel; }

};