/*==================================================================================================
	MS2022
	[bubble.h]
	�E�����o��
----------------------------------------------------------------------------------------------------
	2021.11.07 @Author HAYASE SUZUKI
====================================================================================================
	History
		211107 �쐬

/*================================================================================================*/
#pragma once
#include <string>
#include "shader.h"
#include "texttexture.h"

// �|���S���N���X�i�Q�[���I�u�W�F�N�g�j
class CBubble : public CGameObject {
private:
	std::wstring m_Text;
	CGameObject* m_Parent;

	ID3D11Buffer* m_VertexBuffer = NULL; // ���_�o�b�t�@
	CShader* m_Shader = NULL;       // �V�F�[�_�[

	ID3D11Texture2D* m_Texture;
	ID3D11ShaderResourceView* m_SRV;
	ID2D1Bitmap1* m_TargetBitMap;

	int m_Counter = 0;

public:
	void Init();
	void Uninit();
	void Update();
	void Draw();

	void SetParent(CGameObject* p) { m_Parent = p; }
	void SetVertex(XMFLOAT3 pos, XMFLOAT3 size, XMFLOAT2 uv = XMFLOAT2(0, 0), XMFLOAT2 uvsize = XMFLOAT2(1.0, 1.0));
	void SetText(const wchar_t* text) { m_Text = text; }
};