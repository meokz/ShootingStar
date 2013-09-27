using System;
using DxLibDLL;

namespace Project1 {
    class Item1 {
        public Item1() {
            picture = DX.LoadGraph("Images/Item1.png");
            sound = DX.LoadSoundMem("SE/seitem.mp3");
            setXY();
            this.Active = false;
        }

        private int xValue;
        public int x {
            get { return xValue; }
            set {
                xValue = value;
            }
        }

        private int yValue;
        public int y {
            get { return yValue; }
            set {
                yValue = value;
            }
        }

        private int r = 16;
        private int picture;

        private bool activeValue;
        public bool Active {
            get { return activeValue; }
            set {
                activeValue = value;
            }
        }

        private int sound;

        public void Draw() {
            if (!Active) return;
            DX.DrawGraph(x - r, y - r, picture, DX.TRUE);
        }

        public void setXY() {
            this.x = DX.GetRand(640 - r) + r;
            this.y = DX.GetRand(430 - r) + r;
        }

        int count = 0;
        public void Update(Player player) {
            if (Hit(player)) this.Active = false;

            if (player.Power == 0 && this.Active == false) {
                if (count > 300) {
                    setXY();
                    this.Active = true;
                    count = 0;
                }
                count++;
            }
        }

        public bool Hit(Player player) {
            if (!Active) return false;
            float R = player.r + this.r - 5; // 2つの円の半径の合計
            double X = player.x - this.x; // X座標の差
            double Y = player.y - this.y; // Y座標の差
            if (R * R >= X * X + Y * Y) {
                DX.PlaySoundMem(sound, DX.DX_PLAYTYPE_BACK);
                player.Power = 45;
                return true;
            }
            return false;

        }

    }
}
