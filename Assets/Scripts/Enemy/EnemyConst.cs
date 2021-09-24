using UnityEditor;
using UnityEngine;


public static class EnemyConst 
{
    #region Animations

    // PARAMETERS
    public const string ATTACK_TRIGGER_CONST = "isAttacking";
    public const string ATTACK_BOOL_CONST = "isAttacking";
    public const string SPEED_CONST = "Speed";
    public const string DIRECTION_X_CONST = "lastMoveX";
    public const string DIRECTION_Y_CONST = "lastMoveY";
    public const string FLEE_BOOL_CONST = "isFleeing";
    public const string DEATH_BOOL_CONST = "isDying";
    public const string HIT_TRIGGER_CONST = "isHit";

    // NAME
    public const string SPAWN_ANIMATION_NAME = "Spawn";
    public const string ATTACK_ANIMATION_NAME = "Attack";
    public const string INTERRUPTION_ANIMATION_NAME = "Interrupted";

    // EVENT FUNCTIONS
    public const string SHOOT_COROUTINE_EVENT_FUNCTION_NAME = "StartShootingProcessCo";

    #endregion

    #region Player
    public const string PLAYER_TAG = "Player";
    #endregion
}
