using DxLibDLL;
using System;

namespace Project1 {
    /// <summary>
    /// キーボード押下状態制御
    /// </summary>
    class InputController {
        // キーボード押下状態を保存するバッファ
        private byte[,] keyStateBuf = new byte[2, 256];

        // バッファのフレーム（どちら側が有効か）
        private int keyStateBufNo = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InputController() {
            // バッファの初期化
            keyStateBuf.Initialize();

            // フレームの初期化
            keyStateBufNo = 0;
        }

        /// <summary>
        /// キーボード押下状態を読み込む
        /// </summary>
        public void Read() {
            // フレームを切り替える
            keyStateBufNo ^= 1;

            // 今のフレームにキーボード押下状態を読み込む
            DX.GetHitKeyStateAll(out keyStateBuf[keyStateBufNo, 0]);
        }

        /// <summary>
        /// キーボード押下状態の判定（押し下げ状態かどうか）
        /// </summary>
        /// <param name="keycode">判定するキーコード</param>
        /// <returns>true:押されている false:放されている</returns>
        public bool isKeyPress(int keycode) {
            // 今のフレーム状態で判定する
            return (keyStateBuf[keyStateBufNo, keycode] == 1);
        }

        /// <summary>
        /// キーボード押下状態の判定（今、押されたかどうか）
        /// </summary>
        /// <param name="keycode">判定するキーコード</param>
        /// <returns>true:押された false:押されていない or 押しっぱなし</returns>
        public bool isKeyPush(int keycode) {
            // 今のフレーム：押されている AND 前のフレーム：放されている のとき true
            return (
                (keyStateBuf[keyStateBufNo, keycode] == 1) &&
                (keyStateBuf[keyStateBufNo ^ 1, keycode] == 0));
        }
    }
}
