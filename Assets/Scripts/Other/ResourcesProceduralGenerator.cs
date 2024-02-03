using UnityEngine;

public class ResourcesProceduralGenerator : MonoBehaviour
{
    [SerializeField] EnemyWavesManager wavesManager;
    [SerializeField] private GameObject[] woodPrefab;
    [SerializeField] private GameObject[] stonePrefab;
    [SerializeField] private GameObject[] goldPrefab;
    private GameObject nothingPrefab;
    [SerializeField] private GameObject grassPrefab;
    private GameObject spawn;

    [SerializeField] int[,] map;
    const int width = 13;
    const int height = 13;
    int center = width/2;
    [SerializeField] private Vector2 noiseScale;
    int x, y;


    private void Awake()
    {
        map = new int[width, height];
        x = width / 2;
        y = x;
        map[x, y] = (int)ResourceTypes.Nothing;
        int round = 1;
        int radius = 3;
        int closedCellsCout = 1;
        int cellsInRound = radius * radius - closedCellsCout;
        int closedCellsInRoundCout = 0;
        int direction = 0; // 0 - down, 1 - right, 2 - up, 3 - left
        int step = 1;
        x--;
        y--;
        bool spawnResource = false;
        int resourceAmountMax = Mathf.RoundToInt(Mathf.Sqrt(cellsInRound));
        int resourcesAmount = resourceAmountMax;
        int resourceSpread = cellsInRound / resourceAmountMax;
        int woodAmount = 1;
        int stoneAmount = 1;
        int goldAmount = 1;
        int lastResourceDistance = Random.Range(-2, 2);

        while (radius < width + 1)
        {

            if (radius * radius == closedCellsCout)
            {
                round++;
                radius += 2;
                cellsInRound = radius * radius - closedCellsCout;
                closedCellsInRoundCout = 0;
                resourceSpread = radius - round;
                resourceAmountMax = Mathf.RoundToInt(Mathf.Sqrt(cellsInRound)) + round;
                resourcesAmount = resourceAmountMax;
                woodAmount = Mathf.RoundToInt(resourceAmountMax * 0.5f);
                stoneAmount = Mathf.RoundToInt(resourceAmountMax * 0.3f);
                goldAmount = Mathf.RoundToInt(resourceAmountMax * 0.2f);
            }
            else
            {
                if (resourcesAmount > 0)
                {
                    float neiborShtraph = 0;
                    if (width / 2 - round > 1)
                    {
                        if (map[x - 1, y - 1] == 1) neiborShtraph += 0.1f;
                        if (map[x + 1, y + 1] == 1) neiborShtraph += 0.1f;
                        if (map[x + 1, y - 1] == 1) neiborShtraph += 0.1f;
                        if (map[x - 1, y + 1] == 1) neiborShtraph += 0.1f;
                        if (map[x, y + 1] == 1) neiborShtraph += 0.15f;
                        if (map[x, y - 1] == 1) neiborShtraph += 0.15f;
                        if (map[x + 1, y] == 1) neiborShtraph += 0.15f;
                        if (map[x - 1, y] == 1) neiborShtraph += 0.15f;
                        if (neiborShtraph < 0.05f) { neiborShtraph = -0.5f; }
                        else if (neiborShtraph < 0.1f) { neiborShtraph = -0.4f; }
                        else if (neiborShtraph < 0.15f) { neiborShtraph = -0.25f; }
                    }

                    float noise;
                    float chance;
                    float random = Random.Range(-0.3f, 0.3f);
                    noise = Mathf.PerlinNoise1D(Random.Range(noiseScale.x * x, noiseScale.y * y) / noiseScale.x * noiseScale.y * x + 1);
                    chance = noise + random - neiborShtraph + lastResourceDistance / (resourceSpread + Random.Range(0, 1)) - Random.Range(random, 1 - closedCellsInRoundCout / cellsInRound);
                    if (chance > 0.2f)
                    {
                        spawnResource = true;
                    }
                }
                if (spawnResource)
                {
                    float woodChanse = woodAmount / resourcesAmount;
                    float stoneChanse = stoneAmount / resourcesAmount;
                    float goldChanse = goldAmount / resourcesAmount;

                    if (woodChanse > stoneChanse && woodChanse > goldChanse && woodAmount > 0)
                    {
                        spawn = woodPrefab[Random.Range(0,woodPrefab.Length)];
                        woodAmount--;
                    }
                    else if (stoneChanse > woodChanse && stoneChanse > goldChanse && stoneAmount > 0)
                    {
                        spawn = stonePrefab[Random.Range(0, stonePrefab.Length)];
                        stoneAmount--;
                    }
                    else if (goldChanse > woodChanse && goldChanse > stoneChanse && goldAmount > 0)
                    {
                        spawn = goldPrefab[Random.Range(0, goldPrefab.Length)];
                        goldAmount--;
                    }
                    else
                    {
                        if (woodAmount > 0)
                        {
                            spawn = woodPrefab[Random.Range(0, woodPrefab.Length)];
                            woodAmount--;
                        }
                        else if (stoneAmount > 0)
                        {
                            spawn = stonePrefab[Random.Range(0, stonePrefab.Length)];
                            stoneAmount--;
                        }
                        else if (goldAmount > 0)
                        {
                            spawn = goldPrefab[Random.Range(0, goldPrefab.Length)];
                            goldAmount--;
                        }
                    }
                    spawn.name = closedCellsCout.ToString();
                    Instantiate(spawn, new Vector3((x - center)*15, (y - center)*15, 0), Quaternion.identity);               
                    map[x, y] = 1;
                    resourcesAmount--;
                    lastResourceDistance = -2;
                    spawnResource = false;
                }
                else
                {
                    if (Random.Range(1, 100) > 85)
                    {
                        spawn = spawn = woodPrefab[Random.Range(0, woodPrefab.Length)];
                    }
                    else if (Random.Range(1, 200) > 180)
                    {
                        spawn = stonePrefab[Random.Range(0, stonePrefab.Length)];
                    }
                    else if(Random.Range(10,200) > 170)
                    {
                        spawn = goldPrefab[Random.Range(0, goldPrefab.Length)];
                    }
                    else
                    {
                        spawn = nothingPrefab;
                    }
                    if(spawn != null)
                    {
                        spawn.name = closedCellsCout.ToString();
                        Instantiate(spawn, new Vector3( (x - center) * 15, (y - center) * 15, 0), Quaternion.identity);
                        map[x, y] = 1;
                        lastResourceDistance++;
                    }
                }

                if (direction == 0)
                {
                    if (step < radius)
                    {
                        y++;
                        step++;
                    }
                    else
                    {
                        x++;
                        step = 2;
                        direction = 1;
                    }
                    closedCellsCout++;
                    closedCellsInRoundCout++;
                }
                else if (direction == 1)
                {
                    if (step < radius)
                    {
                        x++;
                        step++;
                    }
                    else
                    {
                        y--;
                        step = 2;
                        direction = 2;
                    }
                    closedCellsCout++;
                    closedCellsInRoundCout++;
                }
                else if (direction == 2)
                {
                    if (step < radius)
                    {
                        y--;
                        step++;
                    }
                    else
                    {
                        x--;
                        step = 3;
                        direction = 3;
                    }
                    closedCellsCout++;
                    closedCellsInRoundCout++;
                }
                else
                {
                    if (step < radius)
                    {
                        x--;
                        step++;
                    }
                    else
                    {
                        x -= 2;
                        y -= 1;
                        step = 1;
                        direction = 0;
                    }
                    closedCellsCout++;
                    closedCellsInRoundCout++;
                }

            }
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (map[i, j] == 0)
                {
                    Instantiate(grassPrefab, new Vector3((i - center) * 15, (j - center) * 15, 0), Quaternion.identity);
                }
            }
        }

        wavesManager.SetSpawnPoints(ref map, 15);
        Invoke("Kill", 3f);
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
