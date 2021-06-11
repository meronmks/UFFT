using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class PermissionAuthorization : MonoBehaviour
{

    private bool isRequesting;

    IEnumerator Start()
    {
        #if UNITY_IOS
            if (!Application.HasUserAuthorization (UserAuthorization.Microphone)) {
                // 権限が無いので、マイクパーミッションのリクエストをする
                yield return RequestUserAuthorization (UserAuthorization.Microphone);
            }
        #elif UNITY_ANDROID
            // マイクパーミッションが許可されているか調べる
            if (!Permission.HasUserAuthorizedPermission (Permission.Microphone)) {
                // 権限が無いので、マイクパーミッションのリクエストをする
                yield return RequestUserPermission (Permission.Microphone);
            }
        #elif UNITY_STANDALONE
            yield return null;
        #endif
    }

    #if UNITY_IOS
        IEnumerator RequestUserAuthorization(UserAuthorization mode)
        {
            isRequesting = true;
            yield return Application.RequestUserAuthorization(mode);
            // iOSではすでに権限拒否状態だとダイアログが表示されず、フォーカスイベントが発生しない。
            // その状態を判別する方法が見つからないので、タイムアウト処理をする。

            // アプリフォーカスが戻るまで待機する
            float timeElapsed = 0;
            while (isRequesting)
            {
                if (timeElapsed > 0.5f){
                    isRequesting = false;
                    yield break;
                }
                timeElapsed += Time.deltaTime;

                yield return null;
            }
            yield break;
        }
    #elif UNITY_ANDROID
        IEnumerator RequestUserPermission(string permission)
        {
            isRequesting = true;
            Permission.RequestUserPermission(permission);
            // Androidでは「今後表示しない」をチェックされた状態だとダイアログは表示されないが、フォーカスイベントは通常通り発生する模様。
            // したがってタイムアウト処理は本来必要ないが、万が一の保険のために一応やっとく。

            // アプリフォーカスが戻るまで待機する
            float timeElapsed = 0;
            while (isRequesting)
            {
                if (timeElapsed > 0.5f){
                    isRequesting = false;
                    yield break;
                }
                timeElapsed += Time.deltaTime;

                yield return null;
            }
            yield break;
        }
    #endif
}
