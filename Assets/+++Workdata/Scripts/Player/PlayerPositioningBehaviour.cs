using UnityEngine;

public class PlayerPositioningBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] positioningPoints;
    [SerializeField] private Transform[] frontPositioningPoints;
    [SerializeField] private Transform[] backPositioningPoints;
    [SerializeField] private Transform[] sitePositioningPoints;
    public Transform GetRandomPositioningPoint()
    {
        int randomIndex = Random.Range(0, positioningPoints.Length);
        return positioningPoints[randomIndex];
    }
    
    public Transform GetFrontPositioningPoint()
    {
        int randomIndex = Random.Range(0, frontPositioningPoints.Length);
        return frontPositioningPoints[randomIndex];
    }
    
    public Transform GetBackPositioningPoint()
    {
        int randomIndex = Random.Range(0, backPositioningPoints.Length);
        return backPositioningPoints[randomIndex];
    }
    
    public Transform GetSitePositioningPoint()
    {
        int randomIndex = Random.Range(0, sitePositioningPoints.Length);
        return sitePositioningPoints[randomIndex];
    }
}
