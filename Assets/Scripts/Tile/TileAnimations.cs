using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAnimations : MonoBehaviour
{
    
    public void PlayIdleAnimation()
    {
        StartCoroutine(InitAnimation());
    }
    public void PlaySelectAnimation()
    {
        transform.DOScale(Vector2.one * 1.2f, 0.3f)
            .SetEase(Ease.InOutBounce)
            .SetLink(gameObject);
    }
    public void PlayUnSelectAnimation()
    {
        transform.DOScale(Vector2.one, 0.3f)
            .SetEase(Ease.InOutBounce)
            .SetLink(gameObject);
    }

    private IEnumerator InitAnimation()
    {
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(0.3f);
        Vector2 oldPosition = transform.position;
        Vector2 newPosition = transform.position + new Vector3(0, 20);
        transform.position = newPosition;
        transform.DOMove(oldPosition, 0.3f).SetEase(Ease.Flash).SetLink(gameObject);
        transform.DOScale(Vector2.one, 0.5f)
        .SetEase(Ease.InOutBounce)
        .SetLink(gameObject);
    }  
}
