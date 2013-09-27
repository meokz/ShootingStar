using System;
using DxLibDLL;

namespace Project1 {
    class Fps {
        int count = 0;

        int[] f = new int[60];

        int time = 0;

        float ave = 0.0f;

        public void Update() {
            count += 1;

            f[count % 60] = DX.GetNowCount() - time;
            time = DX.GetNowCount();

            if(count % 60 == 59) {
                this.ave = 0.0f;
                for(int i = 0; i < 60; i++) ave += f[i];
                ave /= 60;
            }
        }

        int time_ = 0;
        public void Wait() {
            int term = DX.GetNowCount() - time_;
            if(16 - term > 0) System.Threading.Thread.Sleep(16 - term);
            time_ = DX.GetNowCount();
        }

        public float GetFps() {
            if(ave != 0)
                return 1000 / ave;
            else return 0;
        }
    }
}