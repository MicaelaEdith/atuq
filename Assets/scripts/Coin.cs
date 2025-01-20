using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> imgs;

    private int points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ground"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Destroy(rb);

            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
            }


            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && imgs.Count > 0)
            {
                int randomIndex = Random.Range(0, imgs.Count);
                spriteRenderer.sprite = imgs[randomIndex];

                if (randomIndex == 0)
                {
                    points = Random.Range(1, 15);

                }
                else if (randomIndex == 1)
                {
                    points = Random.Range(-8, 0);
                }
            }

            SoundManager.Instance.PlaySFX("coin_animation");
            StartCoroutine(ScaleAndDestroy());
        }
    }

    private IEnumerator ScaleAndDestroy()
    {
        Vector3 originalScale = transform.localScale;
        float scaleTime = 0.5f;

        float time = 0f;
        while (time < scaleTime)
        {
            transform.localScale = Vector3.Lerp(originalScale, originalScale * 1.2f, time / scaleTime);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale * 1.2f;

        time = 0f;
        while (time < scaleTime)
        {
            transform.localScale = Vector3.Lerp(originalScale * 1.2f, originalScale, time / scaleTime);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;

        time = 0f;
        while (time < scaleTime)
        {
            transform.localScale = Vector3.Lerp(originalScale, originalScale * 1.2f, time / scaleTime);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale * 1.2f;

        time = 0f;
        while (time < scaleTime)
        {
            transform.localScale = Vector3.Lerp(originalScale * 1.2f, originalScale * 0.8f, time / scaleTime);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale * 0.8f;

        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.AddPoints(points);
        SoundManager.Instance.PlaySFX("star");
        Destroy(gameObject);
    }
}
