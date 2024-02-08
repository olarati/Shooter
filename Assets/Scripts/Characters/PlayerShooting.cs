using UnityEngine;

public class PlayerShooting : CharacterShooting
{

    protected override void OnInit()
    {
        base.OnInit();
    }

    protected override void Shooting()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    protected override void Reloading()
    {
        if ((!CheckHasBulletsInRow() && Input.GetMouseButton(0)) || Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

}
