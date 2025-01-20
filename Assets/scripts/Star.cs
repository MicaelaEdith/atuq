using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public int starType;
    public int points;
    public float rotationSpeed = 60f;

    [SerializeField]
    private GameObject coinPrefab;

    public void Initialize(int type, Sprite sprite, Vector3 position)
    {
        starType = type;

        switch (starType)
        {
            case 0:
                points = 1;
                break;
            case 1:
                points = 4;
                break;
            case 2:
                points = 0;
                break;
        }

        ChangeAppearance(sprite);
        transform.position = position;
    }

    private void ChangeAppearance(Sprite sprite)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && sprite != null)
        {
            spriteRenderer.sprite = sprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (starType != 2)
            {
                GameManager.Instance.AddPoints(points);
                SoundManager.Instance.PlaySFX("star");
                Destroy(gameObject);
            }
            else
            {
                Vector3 spawnPosition = transform.position;
                Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
                Destroy(gameObject);

            }
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
