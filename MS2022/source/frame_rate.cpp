/*==================================================================================================
    MS2022
    [frame_rate.cpp]
    �E�t���[�����[�g�Ǘ�
----------------------------------------------------------------------------------------------------
    2021.09.27 @Author WATARU FUKUOKA
====================================================================================================
    History
        210927 �쐬

/*================================================================================================*/
#include "main.h"
#include "frame_rate.h"

DWORD dwExecLastTime;
DWORD dwCurrentTime;

void CFrameRate::Initialize() {
    //�t���[���J�E���g������
    timeBeginPeriod(1);
    dwExecLastTime = timeGetTime();
    dwCurrentTime = 0;
}

void CFrameRate::Finalize() {
    timeEndPeriod(1); // ����\��߂�
}

bool CFrameRate::FPS_check(float fps) {
    
    dwCurrentTime = timeGetTime();

    if ((dwCurrentTime - dwExecLastTime) >= (1000.0f / fps)) {
        dwExecLastTime = dwCurrentTime;
        return true;
    }

    return false;
}