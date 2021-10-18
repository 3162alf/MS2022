/*==================================================================================================
    MS2022
    [texture.h]
    �E�e�N�X�`���[
----------------------------------------------------------------------------------------------------
    2021.09.27 @Author HAYASE SUZUKI
====================================================================================================
    History
        210927 �쐬

/*================================================================================================*/
#pragma once

#include "main.h"
#include "renderer.h"




class CTexture
{

public:

	void Load( const char *FileName );
	void Unload();

	ID3D11ShaderResourceView* GetShaderResourceView(){ return m_ShaderResourceView; }


private:

	ID3D11Texture2D*			m_Texture;
	ID3D11ShaderResourceView*	m_ShaderResourceView;

};