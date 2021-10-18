#pragma once
#include "shader.h"
#include "texture.h"
#include "texture_wic.h"

// �|���S���N���X�i�Q�[���I�u�W�F�N�g�j
class CPolygon : public CGameObject
{
private:

	ID3D11Buffer*	m_VertexBuffer = NULL; // ���_�o�b�t�@
	CShader*		m_Shader;              // �V�F�[�_�[

	CTexture*		m_Texture;             // �e�N�X�`��
	CTextureWIC*    m_TextureWic;

public:
	void Init();
	void Uninit();
	void Update();
	void Draw();

	void SetTexture(const char* FileName) { m_Texture->Load(FileName); }
	void SetVertex(XMFLOAT3 pos, XMFLOAT3 size, XMFLOAT2 uv = XMFLOAT2(0, 0), XMFLOAT2 uvsize = XMFLOAT2(1.0, 1.0));
};