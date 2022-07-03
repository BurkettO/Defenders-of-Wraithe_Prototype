using UnityEngine;

public class PathFollower : MonoBehaviour
{
    private int currentIndex = 0;
    private Path path;

    public float posBetweenPoints;

    public float speed;

    private bool hasReachedEnd = false;

    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        path = Path.Instance;
    }

    void Update()
    {
        if (hasReachedEnd == false)
        {
            posBetweenPoints += speed / Vector2.Distance(path.GetLevelData().pathContainer.anchorPoints[currentIndex], path.GetLevelData().pathContainer.anchorPoints[currentIndex + 1]) * Time.deltaTime;
            transform.position = Vector3.Lerp(path.GetLevelData().pathContainer.anchorPoints[currentIndex], path.GetLevelData().pathContainer.anchorPoints[currentIndex + 1], posBetweenPoints);

            if (Vector2.Distance(transform.position, path.GetLevelData().pathContainer.anchorPoints[currentIndex + 1]) < .1f)
            {
                currentIndex += 1;

                if (path.PathHasPoint(currentIndex) == false)
                {
                    hasReachedEnd = true;
                    return;
                }

                Vector2 direction = (Vector2)spriteRenderer.transform.position - path.GetLevelData().pathContainer.anchorPoints[currentIndex + 1];
                float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, rotation - 90);

                posBetweenPoints = 0;
            }
        }      
    }
}