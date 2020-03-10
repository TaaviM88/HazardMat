#region Enemy enums
public enum EnemyState
{

}
public enum EnemyAIState
{
    Spawning,
    Idle,
    Roaming,
    ChaseTarget,
    Attacking,
    Dying,
}
#endregion

public enum PlayerAttackState
{
    None, 
    WeaponThrow, 
    SummonFamiliar,
}

public enum SealSkillState
{
    crawl, 
    fly, 
    hover, 
    FloatingVertical, 
    jump, 
    fat, 
    oil, 
    electricity, 
    flat
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