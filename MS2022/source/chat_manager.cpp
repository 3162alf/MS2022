/*==================================================================================================
    MS2022
    [chat_manager.cpp]
    �E�`���b�g�Ǘ�
----------------------------------------------------------------------------------------------------
    2021.11.08 @Author HAYASE SUZUKI
====================================================================================================
    History
        211108 �쐬

/*================================================================================================*/
#include "main.h"
#include "window.h"
#include "input.h"
#include "chat_manager.h"
#include "manager.h"
#include "scene.h"
#include "player.h"
#include "bubble.h"

CChatManager* CChatManager::m_Instance;

void CChatManager::Update() {
    if (CInput::GetKeyTrigger(VK_F1)) {
        // �`���b�g�{�b�N�X��\��
        if (IsWindowVisible(GetEditWindow())) {
            ShowWindow(GetEditWindow(), SW_HIDE);
            char text[100];
            GetWindowText(GetEditWindow(), text, 100);
            int len = strlen(text);
            // �`���b�g�\��
            if (len > 0) {
                CBubble* bub = CManager::GetScene()->AddGameObject<CBubble>(1);
                bub->SetParent(CManager::GetScene()->GetGameObject<CPlayer>());

                wchar_t wc[100];
                MultiByteToWideChar(CP_OEMCP, 0U, text, -1, wc, strlen(text) + 1);
                bub->SetText(wc);
            }
        }
        // �`���b�g�{�b�N�X�\��
        else {
            ShowWindow(GetEditWindow(), SW_SHOW);
            UpdateWindow(GetEditWindow());
        }
    }
}
