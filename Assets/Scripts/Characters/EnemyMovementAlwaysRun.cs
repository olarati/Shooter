
public class EnemyMovementAlwaysRun : EnemyMovement
{
    protected override void Movement()
    {
        MoveToPlayer();
    }
}
