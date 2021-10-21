/*==================================================================================================
    MS2022
    [model.h]
    �E���f���ǂݍ���
----------------------------------------------------------------------------------------------------
    2021.10.11 @Author HAYASE SUZUKI
====================================================================================================
    History
        211011 �쐬

/*================================================================================================*/
#pragma once

// �}�e���A���\����
struct MODEL_MATERIAL {
    char						Name[256] = { '\0' };
    MATERIAL					Material = MATERIAL();
    char						TextureName[256] = { '\0' };
};

// �`��T�u�Z�b�g�\����
struct SUBSET {
    unsigned short	StartIndex = 0;
    unsigned short	IndexNum = 0;
    MODEL_MATERIAL	Material = MODEL_MATERIAL();
};

// ���f���\����
struct MODEL {
    VERTEX_3D* VertexArray = nullptr;
    unsigned short	VertexNum = 0;
    unsigned short* IndexArray = nullptr;
    unsigned short	IndexNum = 0;
    SUBSET* SubsetArray = nullptr;
    unsigned short	SubsetNum = 0;
};

class CModel {
private:

    ID3D11Buffer* m_VertexBuffer = nullptr;
    ID3D11Buffer* m_IndexBuffer = nullptr;

    DX11_SUBSET* m_SubsetArray = nullptr;
    unsigned short	m_SubsetNum = 0;

    void LoadObj(const char* FileName, MODEL* Model);
    void LoadMaterial(const char* FileName, MODEL_MATERIAL** MaterialArray, unsigned short* MaterialNum);

public:
    void Draw();

    void Load(const char* FileName);
    void Unload();

};
