using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DotweenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        transform.DOScale(new Vector3(2f, 2f, 2f), 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimateButton(GameObject button)
    {
       Vector3 originalScale = button.transform.localScale;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(button.transform.DOScale(originalScale * 0.7f, 0.05f));
        sequence.Append(button.transform.DOScale(originalScale, 0.05f));
        sequence.Play();

    }

    
}
