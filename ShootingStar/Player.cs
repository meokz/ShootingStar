using System;
using DxLibDLL;

namespace Project1 {
    class Player {
        Bullet2[] bullet = new Bullet2[30];
        int White = DX.GetColor(255, 255, 255);
        int Green = DX.GetColor(0, 255, 0);
        const int MaxHP = 1000;

        public Player() {
            x = 320;
            y = 280;
            r = 16;
            Power = 0;
            HP = MaxHP;
            handle = DX.LoadGraph("Images/player.png");
            for (int i = 0; i < bullet.Length; i++) bullet[i] = new Bullet2();
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
                else if (value > 480 - r) yValue = 480 -r;
                else yValue = value;
            }
        }

        private int rValue;
        public int r {
            get { return rValue; }
            set {
                rValue = value;
            }
        }

        private int colorValue;
        public int color {
            get { return colorValue; }
            set {
                colorValue = value;
            }
        }

        private int handleValue;
        public int handle {
            get { return handleValue; }
            set {
                handleValue = value;
            }
        }

        private int hpValue;
        public int HP {
            get { return hpValue; }
            set {
                if (value < 0) {
                    hpValue = 0;
                }
                else hpValue = value;
            }
        }

        public void Draw() {
            for (int i = 0; i < bullet.Length; i++) bullet[i].Draw();
            DX.DrawGraph(x - r, y - r, handle, DX.TRUE);
            string str = "Player   " + HP.ToString().PadLeft(4) + "/" + MaxHP.ToString().PadLeft(4);
            DX.DrawString(30, 430, str, White);
            int a = (int)(30 + (((double)HP / (double)MaxHP) * 160));
            DX.DrawBox(30, 450, a, 471, Green, DX.TRUE);
        }

        public void Update(Computer com) {
            for (int i = 0; i < bullet.Length; i++) {
                bullet[i].Move();
                if (bullet[i].Hit(com)) bullet[i].Active = false;
            }
        }

        private int powerValue;
        public int Power {
            get { return powerValue; }
            set {
                powerValue = value;
            }
        }

        public void Shot() {
            for (int i = 0; i < bullet.Length; i++) {
                if (!bullet[i].Active) {
                    bullet[i].x = this.x;
                    bullet[i].y = this.y;
                    bullet[i].Active = true;
                    bullet[i].Angle = Math.PI / 180 * 270;
                    break;
                }
            }

            if(Power != 0) {
                for(int i = 0; i < bullet.Length; i++) {
                    if(!bullet[i].Active) {
                        bullet[i].x = this.x;
                        bullet[i].y = this.y;
                        bullet[i].Active = true;
                        bullet[i].Angle = Math.PI / 180 * 300;
                        break;

                    }
                }
                for(int i = 0; i < bullet.Length; i++) {
                    if(!bullet[i].Active) {
                        bullet[i].x = this.x;
                        bullet[i].y = this.y;
                        bullet[i].Active = true;
                        bullet[i].Angle = Math.PI / 180 * 240;
                        break;
                    }
                }
                this.Power -= 1;

            }
        }

    }
}
