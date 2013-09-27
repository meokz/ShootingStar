using System;
using DxLibDLL;

namespace Project1 {
    class Computer {
        Bullet1[] bullet = new Bullet1[130];
        int White = DX.GetColor(255, 255, 255);
        int Green = DX.GetColor(0, 255, 0);
        const int MaxHP = 1000;

        public Computer() {
            x = 320;
            y = 60;
            r = 16;
            this.HP = MaxHP;
            handle = DX.LoadGraph("Images/com.png");
            for(int i = 0; i < bullet.Length; i++) bullet[i] = new Bullet1();
        }

        // ウィンドウサイズ 640×480
        private int xValue;
        public int x {
            get { return xValue; }
            set {
                if (value < 0 + r) xValue = 0 + r;
                else if (value > 640 - r) xValue = 640 - r;
                else xValue = value;

            }
        }

        private int yValue;
        public int y {
            get { return yValue; }
            set {
                if (value < 0 + r) yValue = 0 + r;
                else if (value > 480 - r) yValue = 480 - r;
                else yValue = value;
            }
        }

        public int r = 16;
        private int handle;

        private int hpValue;
        public int HP {
            get { return hpValue; }
            set {
                if (value < 0) hpValue = 0;
                else hpValue = value;
            }
        }

        public void Draw() {
            for (int i = 0; i < bullet.Length; i++) bullet[i].Draw();
            DX.DrawGraph(x - r, y - r, handle, DX.TRUE);

            string str = "Computer " + HP.ToString().PadLeft(4) + "/" + MaxHP.ToString().PadLeft(4);
            DX.DrawString(430, 430, str, White);
            int a = (int)(430 + (((double)HP / (double)MaxHP) * 160));
            DX.DrawBox(430, 450, a, 471, Green, DX.TRUE);
        }

        int count = 0;
        int patarn = DX.GetRand(2);
        int vec = DX.GetRand(2);
        int speed = 0;

        bool flag1 = false;
        public void Update(Player player) {
            x += speed;
            if (count % 10 == 0) {
                switch (patarn) {
                    case 0:
                        Shot(player);
                        break;
                    case 1:
                        ShotDiffusion();
                        break;
                    case 2:
                        ShotCircleD();
                        break;
                    default:
                        ShotCircleD();
                        break;
                }
            }

            if (count % 50 == 0) {
                if (this.x < 20) speed += 3;
                else if (this.x > 620) speed -= 3;
                else vec = DX.GetRand(2);
                switch (vec) {
                    case 0:
                        speed = 0;
                        break;
                    case 1:
                        speed += 1;
                        break;
                    case 2:
                        speed -= 1;
                        break;
                }

                

            }

            if (count % 100 == 0) {
                if (flag1)  if (DX.GetRand(3) == 3) ShotFocus(player);
            }

            if (count % 300 == 0) {
                count = 0;
                patarn = DX.GetRand(2);
            }

            count++;

            for (int i = 0; i < bullet.Length; i++) {
                bullet[i].Move();
                if (bullet[i].Hit(player)) bullet[i].Active = false;
            }
        }

       

        public void Shot(Player player) {
            flag1 = false;
            for (int i = 0; i < bullet.Length; i++) {
                if (!bullet[i].Active) {
                    bullet[i].x = this.x;
                    bullet[i].y = this.y;
                    bullet[i].Active = true;
                    bullet[i].Focus(player);
                    break;
                }
            }
        }

        public void ShotDiffusion() {
            flag1 = false;
            for (int j = 0; j < 12; j++) {
                for (int i = 0; i < bullet.Length; i++) {
                    if (!bullet[i].Active) {
                        bullet[i].x = this.x;
                        bullet[i].y = this.y;
                        bullet[i].Active = true;
                        bullet[i].Angle = 15 * i;
                        break;
                    }
                }
            }
        }

        public void ShotCircle() {
            for (int j = 0; j < 12; j++)
                for (int i = 0; i < bullet.Length; i++) {
                    if (!bullet[i].Active) {
                        bullet[i].x = this.x;
                        bullet[i].y = this.y;
                        bullet[i].Active = true;
                        bullet[i].Angle = (float)Math.PI / 180 * (j * 30);
                        break;
                    }
                }
        }

        public void ShotCircleD() {
            flag1 = true;
            for (int j = 0; j < 12; j++)
                for (int i = 0; i < bullet.Length; i++) {
                    if (!bullet[i].Active) {
                        bullet[i].x = this.x;
                        bullet[i].y = this.y;
                        bullet[i].Active = true;
                        bullet[i].Angle = (float)Math.PI / 180 * (j * 30 + (count & 360));
                        break;
                    }
                }
        }

        public void ShotRandom() {
            for (int j = 0; j < 12; j++)
                for (int i = 0; i < bullet.Length; i++) {
                    if (!bullet[i].Active) {
                        bullet[i].x = this.x;
                        bullet[i].y = this.y;
                        bullet[i].Active = true;
                        bullet[i].Angle = DX.GetRand(360);
                        break;
                    }
                }
        }

        public void ShotFocus(Player player) {
            flag1 = false;
            for (int i = 0; i < bullet.Length; i++) {
                if (bullet[i].Active) {
                    bullet[i].Focus(player);
                }
            }
        }
    }

}
