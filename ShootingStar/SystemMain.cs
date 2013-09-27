// Title : ShootingStar
// 2012年の高専祭の展示会にて出展した
// CSharp / DXライブラリを使って作ったゲームです

using System;
using System.Collections.Generic;
using System.Text;

using DxLibDLL;

namespace Project1 {
  class SystemMain {

    [STAThread]
    static void Main() {
      DX.ChangeWindowMode(DX.TRUE); // ウィンドウモード
      DX.SetMainWindowText("SHOOTING STAR");
      DX.SetOutApplicationLogValidFlag(DX.FALSE);
      DX.SetDrawScreen(DX.DX_SCREEN_BACK);
      DX.SetBackgroundColor(0, 0, 0);
      DX.SetWindowSize(640, 480);

      if (DX.DxLib_Init() == -1) return;

      
      GameMain gm;
      InputController ic = new InputController();
      int titleHandle = DX.LoadGraph("Images/title.png");
      int loadHandle = DX.LoadGraph("Images/loading.png");

      while (DX.ProcessMessage() == 0) {
        ic.Read();
        if (ic.isKeyPush(DX.KEY_INPUT_SPACE)) {
          DX.DrawGraph(0, 0, loadHandle, DX.TRUE);
          DX.ScreenFlip();
          gm = new GameMain();
          gm.Main();
        } else if (ic.isKeyPush(DX.KEY_INPUT_ESCAPE)) {
          break;
        } else {
          DX.ClearDrawScreen();
          DX.DrawGraph(0, 0, titleHandle, DX.TRUE);
          DX.ScreenFlip();
        }
      }

      DX.DxLib_End();
      return;
    }

  }
}
