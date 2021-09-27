/*==================================================================================================
    DirectX11
    [window.cpp]
    �E�E�B���h�E����
----------------------------------------------------------------------------------------------------
    2021.09.27 @Author WATARU FUKUOKA
====================================================================================================
    History
        210927 �쐬

/*================================================================================================*/
#include "window.h"




HWND g_Window;

HWND GetWindow() {
    return g_Window;
}

// �E�B���h�E�v���V�[�W��
LRESULT CALLBACK WndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {
    switch (uMsg) {
    case WM_KEYDOWN:
        if (wParam == VK_ESCAPE) {
            SendMessage(hWnd, WM_CLOSE, 0, 0);
        }
        break;

    case WM_CLOSE:
        if (MessageBox(hWnd, "�{���ɏI�����Ă�낵���ł����H", "�I��", MB_OKCANCEL | MB_DEFBUTTON2) == IDOK) {
            DestroyWindow(hWnd);
        }
        return 0;

    case WM_DESTROY:
        PostQuitMessage(0);
        return 0;
    default:
        break;
    };

    return DefWindowProc(hWnd, uMsg, wParam, lParam);
}

// �E�B���h�E����
bool WINDOW_initialize(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow) {

    WNDCLASSEX wcex = {
        sizeof(WNDCLASSEX),
        CS_CLASSDC,
        WndProc,
        0,
        0,
        hInstance,
        NULL,
        LoadCursor(NULL, IDC_ARROW),
        (HBRUSH)(COLOR_WINDOW + 1),
        NULL,
        CLASS_NAME,
        NULL
    };

    // �E�B���h�E�N���X�̓o�^
    RegisterClassEx(&wcex);

    // �E�B���h�E�̍쐬
    g_Window = CreateWindowEx(
        0,
        CLASS_NAME,
        WINDOW_NAME,
        WS_OVERLAPPEDWINDOW,
        CW_USEDEFAULT,
        CW_USEDEFAULT,
        (SCREEN_WIDTH + GetSystemMetrics(SM_CXDLGFRAME) * 2),
        (SCREEN_HEIGHT + GetSystemMetrics(SM_CXDLGFRAME) * 2 + GetSystemMetrics(SM_CYCAPTION)),
        NULL,
        NULL,
        hInstance,
        NULL);
}