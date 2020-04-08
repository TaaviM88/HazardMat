#region Enemy enums
public enum EnemyType
{
    standing,
    wandering,
    flying,
}

public enum EnemyState
{

}
public enum EnemyAIState
{
    Spawning,
    Idle,
    Roaming,
    ChaseTarget,
    flyforward,
    Attacking,
    TakingDamage,
    Dying,
}

public enum EnemySpawnerType
{
    Once,
    Wave,
    Boss,
    Infinite,
}
#endregion


public enum PlayerAttackState
{
    None, 
    WeaponThrow, 
    SummonFamiliar,
    GrapplingHook,
}

public enum PlayerLookDir
{
    Right,
    Left,
}

public enum GrapplinState
{
    None,
    Grappling,
}
public enum MagneticType
{
    Pull,
    Push,
}

public enum MagneticState
{
    Activate,
    Inactive,
}

public enum SealSkillState
{
    //basic movement done
    crawl, 
    fly,
    //done
    hover, 
    FloatingVertical,
    //done
    jump, 
    fat, 
    oil, 
    electricity, 
    flat,
}

public enum DoorState
{
    open,
    close,
    locked,
}

public enum GameState
{
    on,
    pause,
    menu,
}