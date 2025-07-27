public static class DamageCalculator
{
    /// <summary>
    /// Calculates the damage dealt by the attacker and applies it to the defender's health.
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    public static void CalculateDamage(
        CharacterAttack deals, CharacterDefense defends, CharacterHealth defenderHealth,
        Attack attack)
    {
        float variedAP = attack.VariedAP(deals.CurrentAP);
        float damage = deals.CurrentAP + variedAP - defends.CurrentDP;
        if (defends.CurrentBuffType == CharacterDefense.BuffType.DamageReduction)
        {
            damage *= 1 - defends.CurrentDamageReduction * 0.01f;
        }
        if (damage < 0)
            damage = 0;

        bool isCrit = damage > deals.OriginAP * 2;
        defenderHealth.TakeDamage(damage, isCrit);
    }
}

