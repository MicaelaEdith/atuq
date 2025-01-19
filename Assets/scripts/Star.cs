using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public int starType;
    public int points;

    public void Initialize(int type, Sprite sprite, Vector3 position)
    {
        starType = type;

        switch (starType)
        {
            case 0:
                points = 1;
                break;
            case 1:
                points = 6;
                break;
            case 2:
                points = Random.Range(-4, 12);
                break;
            default:
                Debug.LogWarning("Tipo de estrella no válido.");
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
}
