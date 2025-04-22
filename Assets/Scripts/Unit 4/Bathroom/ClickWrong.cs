using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;

public class ClickWrong : MonoBehaviour
{
    public GameObject wordPrefab;
    public Transform spawnPoint; // set this to a UI canvas anchor or world location
    public int maxMisses = 3;

    public MiniGameManager mgr;

    public GameObject canvas;

    private int misses = 0;
    private float spawnInterval = 2f; // start slow
    private float speedMultiplier = 1f;
    private bool gameOver = false;
    
    private List<float> recentYPositions = new List<float>();
    private float minYDistanceBetweenWords = 60f; 
    
    [Header("Difficulty Settings")]
    public float startSpawnInterval = 2f;
    public float minSpawnInterval = 0.5f;
    public float spawnRampRate = 0.95f; // closer to 1 = slower ramp
    public float baseSpeedMultiplier = 1f;
    public float speedRampAmount = 0.05f;
    
    
    
    private readonly (string spanish, string english, bool correct)[] wordPairs = new (string, string, bool)[]
    {
        ("correr", "to run", true),
        ("comer", "to eat", true),
        ("hablar", "to jump", false),
        ("vivir", "to sleep", false),
        ("leer", "to read", true),
        ("beber", "to swim", false),
        ("caminar", "to walk", true),
        ("escribir", "to fly", false)
    };

    void OnEnable()
    {
        spawnInterval = startSpawnInterval;
        speedMultiplier = baseSpeedMultiplier;
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (!gameOver)
        {
            SpawnWord();
            yield return new WaitForSeconds(spawnInterval);

            // Ramp up difficulty
            spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval * spawnRampRate);
            speedMultiplier += speedRampAmount;
        }
    }

    void SpawnWord()
    {
        // Get the RectTransform of the canvas
        Canvas canvas = GetComponentInParent<Canvas>();
        Camera cam = canvas.worldCamera;
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        // Pick a random Y position within the canvas rect
        float minY = canvasRect.rect.yMin + 100f;
        float maxY = canvasRect.rect.yMax - 100f;
        float randomY = 0f;
        int attempts = 0;
        bool found = false;

        while (!found && attempts < 10)
        {
            randomY = Random.Range(minY, maxY);
            found = true;

            foreach (float recentY in recentYPositions)
            {
                if (Mathf.Abs(recentY - randomY) < minYDistanceBetweenWords)
                {
                    found = false;
                    break;
                }
            }

            attempts++;
        }

        // Keep recent positions short
        recentYPositions.Add(randomY);
        if (recentYPositions.Count > 5) recentYPositions.RemoveAt(0);

        // Convert local canvas position to world position
        Vector3 localPos = new Vector3(canvasRect.rect.xMin - 200f, randomY, 0f); // just off the left
        Vector3 worldPos = canvas.transform.TransformPoint(localPos);

        // Instantiate and place in world space
        GameObject word = Instantiate(wordPrefab, worldPos, Quaternion.identity, transform);
        //make it different font/colors

        // Make sure it’s scaled properly
        RectTransform wordRect = word.GetComponent<RectTransform>();
        wordRect.localScale = Vector3.one;

        MoveWord mover = word.GetComponent<MoveWord>();
        if (mover != null)
        {
            mover.SetSpeedMultiplier(speedMultiplier);
            mover.SetGameManager(this);
        }
        
        // Pick a random word pair
        var pair = wordPairs[Random.Range(0, wordPairs.Length)];

// Initialize DissapearOnClick
        DissapearOnClick clicker = word.GetComponent<DissapearOnClick>();
        if (clicker != null)
        {
            TMP_Text wordText = word.GetComponentInChildren<TMP_Text>();
            clicker.wordText = wordText;
            clicker.Initialize(pair.spanish, pair.english, pair.correct, this);
        }
    }

    public void WordMissed()
    {
        misses++;

        if (misses >= maxMisses)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameOver = true;
        Debug.Log("Game over!");
        // Optionally show UI, reset, etc.
        // Stop the spawn loop
        StopAllCoroutines();
        // Destroy all remaining words
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Notify the game manager
        if (mgr != null)
        {
            mgr.CloseGame();
        }

    }
}