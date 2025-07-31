using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;

    public static GameAssets Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameAssets>("Game Assets");
            }

            return instance;
        }
    }

    public Transform prefabEnemy;
    public Transform enemyDieParticles;
    public Transform arrowProjectile;
    public Transform buildingDestroyedParticles;
    public Transform buildingConstruction;
    public Transform buildingPlacedParticles;
}
