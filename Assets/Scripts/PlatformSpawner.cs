using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public float spawnInterval = 2f;
    public float spawnHeight = 10f;
    public float spawnRange = 5f;
    public float fallSpeed = 5f;
    public Color[] platformColors;

    private Transform playerTransform;

    private void Start()
    {
        // Populate the platformColors array with some random colors
        platformColors = new Color[] {
        Random.ColorHSV(),
        Random.ColorHSV(),
        Random.ColorHSV()
    };
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("SpawnPlatform", spawnInterval, spawnInterval);
    }

    private void SpawnPlatform()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();

        GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        Renderer platformRenderer = platform.GetComponent<Renderer>();

        // Randomly select a color from the platformColors array
        Color randomColor = platformColors[Random.Range(0, platformColors.Length)];
        platformRenderer.material.color = randomColor;

        Rigidbody platformRb = platform.GetComponent<Rigidbody>();
        platformRb.useGravity = false;
        platformRb.velocity = Vector3.down * fallSpeed;
    }


    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomSpawnPosition = Vector3.zero;
        bool positionFound = false;
        int maxAttempts = 10;
        int attempts = 0;

        while (!positionFound && attempts < maxAttempts)
        {
            randomSpawnPosition = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                spawnHeight,
                Random.Range(-spawnRange, spawnRange)
            );

            float distanceToPlayer = Vector3.Distance(randomSpawnPosition, playerTransform.position);
            if (distanceToPlayer > 2f) // Adjust this value to set the minimum distance from the player
            {
                positionFound = true;
            }

            attempts++;
        }

        return randomSpawnPosition;
    }
}