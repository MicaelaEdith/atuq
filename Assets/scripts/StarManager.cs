using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> imgList;

    [SerializeField]
    private List<Transform> starPositions;

    [SerializeField]
    private GameObject starPrefab;

    private void Start()
    {
        GenerateStars();
    }

    private void GenerateStars()
    {
        ShuffleList(starPositions);

        int starsOfType0 = 11;
        int starsOfType1 = 14;
        int starsOfType2 = 8;

        int positionIndex = 0;

        for (int i = 0; i < starsOfType0; i++)
        {
            CreateStar(0, imgList[0], starPositions[positionIndex++].position);
        }

        for (int i = 0; i < starsOfType1; i++)
        {
            CreateStar(1, imgList[1], starPositions[positionIndex++].position);
        }

        for (int i = 0; i < starsOfType2; i++)
        {
            CreateStar(2, imgList[2], starPositions[positionIndex++].position);
        }
    }

    private void CreateStar(int type, Sprite sprite, Vector3 position)
    {
        GameObject starObject = Instantiate(starPrefab);

        Star star = starObject.GetComponent<Star>();
        if (star != null)
        {
            star.Initialize(type, sprite, position);
        }
        else
        {
            Debug.LogError("El prefab no tiene el componente Star.");
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        // Fisher-Yates
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}

