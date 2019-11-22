using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static float agentSpeed = 1.5f;
    public static bool bossKnockbackBool = false;
    public static bool phase_two = false;
    public static int numBossMovesEnd = 2;
    public static float bossAngularSpeed = 120f;
    public static float slepSpeed = 2.0f;
    public static int enemyBossDamage = 15;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SavePlayer()
    {
        GlobalControl.SavePlayer();
    }

    public float getAgentSpeed()
    {
        return agentSpeed;
    }

    public void setAgentSpeed(float newSpeed)
    {
        agentSpeed = newSpeed;
    }

    public int getEnemyBossDamage()
    {
        return enemyBossDamage;
    }

    public void setEnemyBossDamage(int newDamage)
    {
        enemyBossDamage = newDamage;
    }

    public bool getBossKnockbackBool()
    {
        return bossKnockbackBool;
    }

    public void setBossKnockbackBool(bool newbool)
    {
        bossKnockbackBool = newbool;
    }

    public bool getPhase_two()
    {
        return phase_two;
    }

    public void setPhase_two(bool newbool)
    {
        phase_two = newbool;
    }

    public int getNumBossMovesEnd()
    {
        return numBossMovesEnd;
    }

    public void setNumBossMovesEnd(int numMoves)
    {
        numBossMovesEnd = numMoves;
    }

    public float getBossAngularSpeed()
    {
        return bossAngularSpeed;
    }

    public void setBossAngularSpeed(float newSpeed)
    {
        bossAngularSpeed = newSpeed;
    }

    public float getSlepSpeed()
    {
        return slepSpeed;
    }

    public void setSlepSpeed(float newSpeed)
    {
        slepSpeed = newSpeed;
    }
}
