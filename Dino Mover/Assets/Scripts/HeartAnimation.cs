using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeartAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 originalScale = transform.localScale;
        transform.DOScale(originalScale * 1.1f, 0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
