using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{

    public GameObject enemy;
    public Canvas gameScreen;


    private bool spawn = false;
    private int wave = 1;

    private int enemiesToSpawn;
    private int enemiesSpawned;


    void Start()
    {
        wave = 1;
        spawn = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public int StartSpawn()
    {
        spawn = true;




        return enemiesSpawned;
    }

    private void StopSpawn()
    {
        spawn = false;
        
    }

    public void NextWave()
    {
        wave++;
    }

    void SpawnEnemy()
    {
        Vector2 pos = GetRandomEdgePosition();
        GameObject enemySpawned = Instantiate(enemy, pos, Quaternion.identity, gameScreen.transform);
    }


    Vector2 GetRandomEdgePosition()
    {
        float x = Random.Range(-Screen.width / 2, Screen.width / 2);
        float y = (Random.value > .5f) ? Screen.height / 2 + 50 : Screen.height / 2 - 50;
        return new Vector2(x, y);

    }
    
}
