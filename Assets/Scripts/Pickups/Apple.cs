using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float changeMoveSpeedAmount = 1f;
    
    LevelGenerator levelGenerator;

    public void Init(LevelGenerator levelGenerator)//Method for Getting dependency(LevelGenerator class) from Chunk class
    {
        this.levelGenerator = levelGenerator;
    }

    protected override void OnPickup()
    {
        levelGenerator.ChangeChunkMoveSpeed(changeMoveSpeedAmount);
    }
}
