
public class EnemyShootingIfInRange : EnemyShooting
{
    protected override void Shooting()
    {
        if (CheckTargetInRange() && CheckHasBulletsInRow())
        {
            Shoot();
        }
    }
}
