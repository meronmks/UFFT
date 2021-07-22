■■■Unity製FFTアナライザ　UFFT■■■

■これ何
マイクの入力をリアルタイムフーリエ変換をして表示するだけ
波形をpngとして出力する機能も付きました（実行ファイルのある場所にフォルダimgが生成されます）

■使い方（拡張子（.exeとか）は環境によっては見えないので注意）
＜デスクトップモードでの起動＞
『UFFT.exe』をダブルクリックして起動

＜SteamVRのオーバーレイアプリとして起動＞
『UFFT_VROverlayMode.bat』をダブルクリックして起動
※おまけ機能かつVR空間上にオーバレイ表示する為若干負荷が高くなります
　またSteamVRで動作する環境のみとなります

＜SteamVRのオーバーレイアプリとして自動起動登録する（β機能）＞
『UFFT_VROverlayMode_AutoRunInstall.bat』をダブルクリックして起動
UFFTが起動しますがすぐに終了します
以後SteamVR起動と同時に立ち上がってくると思います。
なおUFFTがCドライブかつ、実行ファイルのあるパスまでにマルチバイト文字（平たく言えば日本語）が
存在する場合正常に動かない場合があります。

＜SteamVRのオーバーレイアプリとして自動起動登録を解除する（β機能）＞
『UFFT_VROverlayMode_AutoRunUninstall.bat』をダブルクリックして起動
UFFTが起動しますがすぐに終了します

＜起動後の基本操作＞
・入力デバイス選択
　ドロップダウンメニューから現在ソフトで認識できている入力デバイス一覧が表示されています
　起動後にデバイスを変更した場合は左回りの矢印のボタンを押すと一覧が更新されます

・表示周波数範囲
　グラフで表示する範囲を整数で指定できます
　0～22050Hzぐらいの範囲で指定できます（範囲外も指定できますが意味はありません）

・計算ゲイン
　端的に言えばグラフ表示に倍率を掛けます
　ノイズが多いとか、全く見えないとかって時に使えます

・窓関数
　以下のものから選択できます（無しにすることはできません）
　- Rectangular
　- Triangle
　- Hamming
　- Hanning
　- Blackman
　- BlackmanHarris

・OneShot
　押した瞬間の波形を表示します
　押すと自動的に波形再生がOFFになります

・波形再生
　チェックがONの間はフーリエ変換した結果を表示し続けます
　（もっと良い感じの言い方をゆる募）

・補助線
　グラフ上にある白い線の表示非表示切り替えです

・波形をpngで記録
　もうそのままです
　実行ファイルのある場所にフォルダimgが生成され、その中に記録されます

・ファイルを読み込む
　外部の音声ファイルを読み込みます。
　Unity側の仕様上もあり"mp3","wav","ogg"のみを受け付けます。

・再生とか停止とかしそうなボタン
　そのままです、読み込んだファイルを再生しつつ波形解析します。
　停止時に自動でマイク入力に再度切り替わります。

・再生とか停止とかしそうなボタンの右横にあるバー
　ファイル読み込み時の再生音量です。
　現状解析時の波形の大きさにも影響するので可能な限り大き目をお勧め（対応予定）

＜SteamVRのオーバーレイアプリ時のみの操作＞
・ウインドウを移動させる
　表示されたウインドウをコントローラーのいずれかのボタン（Indexの握りこみ可）を押しつつ
　手を近づけるとブルッと震えると掴めます
　ボタンの押し込みをやめるとその場に固定されます
　ウインドウ位置のリセット機能なんて無いのでどっかいった場合は再起動してください

■使ってるもの（間接的な利用も含む）のライセンスとか
・SimpleSpectrum
https://assetstore.unity.com/packages/tools/audio/simplespectrum-free-audio-spectrum-generator-webgl-85294?locale=ja-JP

・680+ Simple Vector Icons
https://assetstore.unity.com/packages/2d/gui/icons/680-simple-vector-icons-163103

・やさしさゴシックボールド
http://www.fontna.com/blog/736/
* M+ (LICENSE_J,LICENSE_E)
Copyright(c) 2012 M+ FONTS PROJECT
ひらがな・カタカナ部分のデザイン Copyright(c)fontna.com

・EasyOpenVROverlayForUnity.cs
https://sabowl.sakura.ne.jp/gpsnmeajp/unity/EasyOpenVROverlayForUnity/

・EasyOpenVRUtil
https://github.com/gpsnmeajp/EasyOpenVRUtil

・UniTask
https://github.com/Cysharp/UniTask

The MIT License (MIT)

Copyright (c) 2019 Yoshifumi Kawai / Cysharp, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

・ZString
https://github.com/Cysharp/ZString/

MIT License

Copyright (c) 2020 Cysharp, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

・UnityStandaloneFileBrowser
https://github.com/gkngkc/UnityStandaloneFileBrowser

MIT License

Copyright (c) 2017 Gökhan Gökçe

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

■このソフトのソース
https://github.com/meronmks/UFFT

■スペシャルサンクス
・声質研究会のみなさん（デバッグしてくれた）
・なひたふ様（大元ネタソフトであるnfft07の作者）
　http://www.nahitech.com/nahitafu/nfft.html