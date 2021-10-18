/*==================================================================================================
    DirectX11
    [window.h]
    �E�E�B���h�E����
----------------------------------------------------------------------------------------------------
    2021.09.27 @Author WATARU FUKUOKA
====================================================================================================
    History
        210927 �쐬

/*================================================================================================*/
#pragma once
#include <windows.h>

#define CLASS_NAME      "MS2022AppClass"
#define WINDOW_NAME     "MS2022"

#define FULL_SCREEN
#define SCREEN_WIDTH    (1920)      // �E�C���h�E�̕�
#define SCREEN_HEIGHT   (1080)      // �E�C���h�E�̍���



class CWindow {
private:

public:
    static void Initialize(HINSTANCE);
    static void Display(int);

};

HWND GetWindow();
LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);