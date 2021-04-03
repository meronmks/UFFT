using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHorizonatalLayoutGroup : MonoBehaviour
{
    private float width;
    private float height;
    private int childCount;
    void Start()
    {
        // 親オブジェクトの高さと幅を取得
        RectTransform rect = GetComponent<RectTransform> ();
        width  = rect.sizeDelta.x;
        height = rect.sizeDelta.y;
        childCount = 0;
    }

    void Update()
    {
        // 要素数が変更になったら並びを整える
        if(childCount != this.transform.childCount)
        {
            SetLayout();
        }
        childCount = this.transform.childCount;
    }

    private void SetLayout()
    {
        for (int i = 0; i < this.transform.childCount; i++){
            // 子オブジェクトを均等間隔に並べる
            Transform child = this.transform.GetChild(i);
            float left = width * (i + 1) / (this.transform.childCount+ 1) - width / 2;
            float top = height * 1 / (this.transform.childCount+ 1) - height / 2;
            child.localPosition = new Vector3(left, top, 0);
        }
    }
}
