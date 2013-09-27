using System;
using System.Collections.Generic;
using System.Text;

using DxLibDLL;
using System.Windows.Forms;

namespace Project1 {
    class GameMain {
        int White = DX.GetColor(255, 255, 255);
        int Black = DX.GetColor(0, 0, 0);
        int Green = DX.GetColor(0, 255, 0);

        const int windowWidth = 640;
        const int windowHeight = 480;

        InputController ic;
        Fps fps;
        Player player;
        Computer com;
        Item1 item;
        int back = DX.LoadGraph("Images/haikei1.png");
        // int bgm = DX.LoadSoundMem("SE/bgm.mp3");

        // コンストラクタ
        public GameMain() {
            fps = new Fps();
            ic = new InputController();
            player = new Player();
            com = new Computer();
            item = new Item1();
        }


        public void Main() {
            // DX.PlaySoundMem(bgm, DX.DX_PLAYTYPE_LOOP);

            while(DX.ProcessMessage() == 0) {
                fps.Wait();
                if(Update() == -1) return;
                if(Result()) {
                    // DX.StopSoundMem(bgm);
                    return;
                }

                DX.ClearDrawScreen();
                Draw();
                DX.ScreenFlip();
                fps.Update();
            }
        }

        private void Draw() {
          DX.DrawGraph(0, 0, back, DX.TRUE);
            item.Draw();
            com.Draw();
            player.Draw();
            DX.DrawString(0, 0, fps.GetFps().ToString() + "/" + "60", White);

        }

        int speed = 4;
        private int Update() {
            ic.Read();
            if (DX.CheckHitKey(DX.KEY_INPUT_ESCAPE) != 0) return -1;
            if (ic.isKeyPress(DX.KEY_INPUT_UP)) player.y -= speed;
            if (ic.isKeyPress(DX.KEY_INPUT_DOWN)) player.y += speed;
            if (ic.isKeyPress(DX.KEY_INPUT_RIGHT)) player.x += speed;
            if (ic.isKeyPress(DX.KEY_INPUT_LEFT)) player.x -= speed;
            if (ic.isKeyPush(DX.KEY_INPUT_SPACE))  player.Shot();

            player.Update(com);
            com.Update(player);
            item.Update(player);
            return 0;
        }

        private bool Result() {
            if (player.HP <= 0) {
                MessageBox.Show("あなたの負けです", "");
                MessageBox.Show("あなたの負けです", "");
                MessageBox.Show("あなたの負けです", "");
                return true;
            }
            else if (com.HP <= 0) {
                MessageBox.Show("あなたの勝ちです", "");
                MessageBox.Show("あなたの勝ちです", "");
                MessageBox.Show("あなたの勝ちです", "");
                return true;
            }
            return false;
        }


    }

}
