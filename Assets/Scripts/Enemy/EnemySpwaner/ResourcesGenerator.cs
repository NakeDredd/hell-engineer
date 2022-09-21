using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourcesGenerator : Singleton<ResourcesGenerator>
{
    [SerializeField] private Transform[] leftPositions;
    [SerializeField] private Transform[] rightPositions;
    [SerializeField] private ResourcesClaster claster;
    [SerializeField] private int minResources;
    [SerializeField] private int maxResources;

    public Action ClearClasters;

    public void GenerateResources (bool left)
    {
        ClearClasters?.Invoke();

        int number = Random.Range(minResources, maxResources);

        if (left)
        {
            number = Mathf.Clamp(number, minResources, leftPositions.Length - 1);

            for (int i = 0; i < number; i++)
            {
                CreateClaster(leftPositions[Random.Range(0, leftPositions.Length)].position);
            }

            return;
        }
        else
        {
            number = Mathf.Clamp(number, minResources, rightPositions.Length - 1);

            for (int i = 0; i < number; i++)
            {
                CreateClaster(rightPositions[Random.Range(0, rightPositions.Length)].position);
            }
        }
    }

    private void CreateClaster (Vector3 position)
    {
        ResourcesClaster claster = Instantiate(this.claster, position, Quaternion.identity);
    }
}
