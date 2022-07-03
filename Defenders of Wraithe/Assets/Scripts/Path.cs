using UnityEngine;

public class Path : MonoBehaviour
{
    public static Path Instance;
    public Grid grid;

    public LevelData levelData;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameObject obj = Instantiate(levelData.tilemapData, grid.transform).gameObject;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < levelData?.pathContainer.anchorPoints.Length; i++)
        {
            if (i == 0)
            {
                Gizmos.color = Color.blue;
                if (levelData.pathContainer.anchorPoints.Length > 1)
                {
                    Gizmos.DrawLine(levelData.pathContainer.anchorPoints[0], levelData.pathContainer.anchorPoints[1]);
                }
            }

            else if (i < levelData.pathContainer.anchorPoints.Length -1)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(levelData.pathContainer.anchorPoints[i], levelData.pathContainer.anchorPoints[i + 1]);

            }

            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(levelData.pathContainer.anchorPoints[i - 1], levelData.pathContainer.anchorPoints[i]);
            }
        }
    }

    public bool PathHasPoint(int index)
    {
        if (index < levelData.pathContainer.anchorPoints.Length - 1) { return true; }
        else { return false; }
    }

    public Vector2 ReturnPointAtIndex(int index)
    {
        return levelData.pathContainer.anchorPoints[index];
    }

    public LevelData GetLevelData()
    {
        return levelData;
    }

}