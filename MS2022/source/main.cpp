/*==================================================================================================
    MS2022
    [main.cpp]
    �E���C��
----------------------------------------------------------------------------------------------------
    2021.09.27 @Author HAYASE SUZUKI
====================================================================================================
    History
        210927 �쐬

/*================================================================================================*/
#include "main.h"
#include "window.h"
#include "manager.h"


int APIENTRY WinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPSTR lpCmdLine, _In_ int nCmdShow) {

    //�t���[���J�E���g������
    DWORD dwExecLastTime;
    DWORD dwCurrentTime;
    timeBeginPeriod(1);
    dwExecLastTime = timeGetTime();
    dwCurrentTime = 0;


    CManager::Init(hInstance, nCmdShow);

    // ���b�Z�[�W���[�v
    MSG msg;
    while (1) {
        if (PeekMessage(&msg, NULL, 0, 0, PM_REMOVE)) {
            if (msg.message == WM_QUIT) {// PostQuitMessage()���Ă΂ꂽ�烋�[�v�I��
                break;
            }
            else {
                // ���b�Z�[�W�̖|��ƃf�B�X�p�b�`
                TranslateMessage(&msg);
                DispatchMessage(&msg);
            }
        }
        else {
            dwCurrentTime = timeGetTime();

            if ((dwCurrentTime - dwExecLastTime) >= (1000 / 60)) {
                dwExecLastTime = dwCurrentTime;

                // �X�V����
                CManager::Update();

                // �`�揈��
                CManager::Draw();
            }
        }
    }

    timeEndPeriod(1); // ����\��߂�

    // �I������
    CManager::Uninit();

    return (int)msg.wParam;
}
