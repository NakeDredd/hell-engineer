using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        List<Vector3> positions = new List<Vector3>();

        ClearClasters?.Invoke();

        int number = Random.Range(minResources, maxResources);

        if (left)
        {
            number = Mathf.Clamp(number, minResources, leftPositions.Length - 1);

            foreach (var point in leftPositions)
            {
                positions.Add(point.position);
            }

            for (int i = 0; i < number; i++)
            {
                int pos = Random.Range(0, positions.Count);
                CreateClaster(positions[pos]);
                positions.RemoveAt(pos);
            }

            return;
        }
        else
        {
            number = Mathf.Clamp(number, minResources, rightPositions.Length - 1);

            foreach (var point in rightPositions)
            {
                positions.Add(point.position);
            }

            for (int i = 0; i < number; i++)
            {
                int pos = Random.Range(0, positions.Count);
                CreateClaster(positions[pos]);
                positions.RemoveAt(pos);
            }
        }
    }

    private void CreateClaster (Vector3 position)
    {
        Instantiate(claster, position, Quaternion.identity);
    }
}
