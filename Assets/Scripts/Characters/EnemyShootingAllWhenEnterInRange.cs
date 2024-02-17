
public class EnemyShootingAllWhenEnterInRange : EnemyShooting
{
    private bool _isInRange;

    protected override void Shooting()
    {
        if(!_isInRange && CheckTargetInRange())
        {
            _isInRange = true;
        }

        if (_isInRange)
        {
            if (CheckHasBulletsInRow())
            {
                Shoot();
            }
            else
            {
                _isInRange = false;
            }
        }
    }

}
