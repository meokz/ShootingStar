using System;
using DxLibDLL;

namespace Project1 {
    class Bullet1 {
        //もし処理が重くなったらdouble → floatに

        private int se;
        public Bullet1() {
            this.Active = false;
            this.Speed = 5;
            this.Angle = 180;
            handle = DX.LoadGraph("Images/bullet1.png");
            se = DX.LoadSoundMem("SE/hit.mp3");
        }

        // Bullet1のX座標
        private double xValue;
        public double x {
            get { return xValue; }
            set {
                if (value < 0) this.Active = false;
                else if (value > 640) this.Active = false;
                else xValue = value;
                
            }
        }

        // Bullet1のY座標
        private double yValue;
        public double y {
            get { return yValue; }
            set {
                if (value < 0 - r) this.Active = false;
                else if (value > 480 + r) this.Active = false;
                else yValue = value;
            }
        }
        // Bullet1の半径
        private int r = 8;
        // Bullet1の画像をメモリにロードするためのハンドル
        private int handle;

        private bool activeValue;
        public bool Active {
            get { return activeValue; }
            set {
                activeValue = value;
            }
        }

        private float speedValue;
        public float Speed {
            get { return speedValue; }
            set {
                speedValue = value;
            }
        }

        private float angleValue;
        public float Angle {
            get { return angleValue; }
            set {
                angleValue = value;
            }
        }

        // Bullet1の描画
        public void Draw() {
            if (!Active) return;
            DX.DrawGraph((int)x - r, (int)y - r, handle, DX.TRUE);
        }
        // Bullet1とPlayerのあたり判定
        public bool Hit(Player player) {
            if (!Active) return false;
            float R = player.r + this.r - 5; // 2つの円の半径の合計
            double X = player.x - this.x; // X座標の差
            double Y = player.y - this.y; // Y座標の差
            if (R * R >= X * X + Y * Y) {
                DX.PlaySoundMem(se, DX.DX_PLAYTYPE_BACK);
                player.HP -= 20;
                return true;
            }
            return false;

            //returnで返してもいいけど、処理を書いておいたほうが楽かもね
            // そのときは参照渡しで
        }
        // 移動
        public void Move() {
            if (!Active) return;
            x = x + this.Speed * Math.Cos(this.Angle);
            y = y + this.Speed * Math.Sin(this.Angle);
        }

        public void SetXY() {
            int h = DX.GetRand(3);
            switch (h) {
                case 0:
                    x = DX.GetRand(640);
                    y = 0;
                    break;
                case 1:
                    x = 640;
                    y = DX.GetRand(480);
                    break;
                case 2:
                    x = 0;
                    y = DX.GetRand(480);
                    break;
                case 3:
                    x = DX.GetRand(640);
                    y = 480;
                    break;
            }

        }

        public void Focus(Player player) {
            int dx = player.x - (int)this.x;
            int dy = player.y - (int)this.y;
            float angle = (float)Math.Atan2(dy, dx);
            this.Angle = angle;
        }
    }
}
