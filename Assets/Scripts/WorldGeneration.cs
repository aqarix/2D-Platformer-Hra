using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGeneration : MonoBehaviour
{
    [Header("Tilemap & Tile")]
    public Tilemap tilemap;
    public TileBase tile;

    [Header("Player Safe Zone")]
    public Transform player;
    public float playerSafeRadius = 5f;

    [Header("Platform Settings")]
    public int platformLevels = 35;
    public Vector2Int radiusXRange = new Vector2Int(3, 7);
    public Vector2Int radiusYRange = new Vector2Int(2, 4);

    [Header("Platform Count")]
    public int minPlatformsPerLevel = 1;
    public int maxPlatformsPerLevel = 3;

    [Header("Spacing")]
    public float minVerticalGap = 2.0f;
    public float maxVerticalGap = 5.0f;
    public float horizontalSpread = 12f;

    [Header("Coin Settings")]
    public GameObject coinPrefab;
    public int minCoinsPerPlatform = 0;
    public int maxCoinsPerPlatform = 3;
    public float minCoinHeightAbovePlatform = 1f;
    public float maxCoinHeightAbovePlatform = 3f;

    [Header("Shop Settings")]
    public GameObject upgradeShopPrefab;
    public GameObject weaponShopPrefab;
    [Range(0f, 1f)] public float shopSpawnChance = 0.15f;
    public float shopHeightOffset = 1f;

    private void Start()
    {
        GeneratePlatforms();
    }

    void GeneratePlatforms()
    {
        float currentHeight = 0f;

        for (int level = 0; level < platformLevels; level++)
        {
            currentHeight += Random.Range(minVerticalGap, maxVerticalGap);

            float levelProgress = (float)level / (platformLevels - 1);
            int platformsThisLevel = Mathf.Max(
                1,
                Mathf.RoundToInt(Mathf.Lerp(maxPlatformsPerLevel, minPlatformsPerLevel, levelProgress))
            );

            for (int i = 0; i < platformsThisLevel; i++)
            {
                Vector3Int center = new Vector3Int(
                    Random.Range(-Mathf.RoundToInt(horizontalSpread), Mathf.RoundToInt(horizontalSpread)),
                    Mathf.RoundToInt(currentHeight),
                    0
                );

                Vector3 worldPos = tilemap.GetCellCenterWorld(center);

                if (player != null)
                {
                    if (Vector2.Distance(worldPos, player.position) <= playerSafeRadius)
                        continue;
                }

                GeneratePlatform(center);
            }
        }
    }

    void GeneratePlatform(Vector3Int center)
    {
        int radiusX = Random.Range(radiusXRange.x, radiusXRange.y + 1);
        int radiusY = Random.Range(radiusYRange.x, radiusYRange.y + 1);

        float bottomTrimAmount = 0.35f;

        for (int x = -radiusX; x <= radiusX; x++)
        {
            for (int y = -radiusY; y <= 0; y++)
            {
                float dx = (float)(x * x) / (radiusX * radiusX);
                float dy = (float)(y * y) / (radiusY * radiusY);

                if (dx + dy <= 1f)
                {
                    if (y == -radiusY && Mathf.Abs(x) > radiusX * (1f - bottomTrimAmount))
                        continue;

                    tilemap.SetTile(new Vector3Int(center.x + x, center.y + y, 0), tile);
                }
            }
        }

        SpawnCoins(center, radiusX);
        SpawnShop(center);
    }

    void SpawnCoins(Vector3Int center, int radiusX)
    {
        if (coinPrefab == null)
            return;

        int coinCount = Random.Range(minCoinsPerPlatform, maxCoinsPerPlatform + 1);

        for (int i = 0; i < coinCount; i++)
        {
            int randomX = Random.Range(-radiusX, radiusX + 1);
            float randomHeight = Random.Range(minCoinHeightAbovePlatform, maxCoinHeightAbovePlatform);

            Vector3 spawnPos = tilemap.GetCellCenterWorld(
                new Vector3Int(center.x + randomX, center.y, 0)
            );

            spawnPos.y += randomHeight;

            Instantiate(coinPrefab, spawnPos, Quaternion.identity);
        }
    }

    void SpawnShop(Vector3Int center)
    {
        if (Random.value > shopSpawnChance)
            return;

        Vector3 spawnPos = tilemap.GetCellCenterWorld(center);
        spawnPos.y += shopHeightOffset;

        GameObject shopToSpawn;

        if (Random.value < 0.5f)
            shopToSpawn = upgradeShopPrefab;
        else
            shopToSpawn = weaponShopPrefab;

        if (shopToSpawn != null)
            Instantiate(shopToSpawn, spawnPos, Quaternion.identity);
    }
}
