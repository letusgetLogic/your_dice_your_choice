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
        GameManager.Instance.Log(
            $"{deals.gameObject.name} attacks {defends.gameObject.name} with varied AP {variedAP} from base AP {deals.CurrentAP}."
        );
        float damage = deals.CurrentAP + variedAP - defends.CurrentDP;
        GameManager.Instance.Log(
            $"Calculated damage before damage reduction: {damage}."
        );
        if (defends.CurrentBuffType == CharacterDefense.BuffType.DamageReduction)
        {
            damage *= 1 - defends.CurrentDamageReduction * 0.01f;
            GameManager.Instance.Log(
                $"Applied damage reduction of {defends.CurrentDamageReduction}%, resulting in final damage: {damage}."
            );
        }
        if (damage < 0)
            damage = 0;

        bool isCrit = damage > deals.OriginAP * 2;
        defenderHealth.TakeDamage(damage, isCrit);
    }
}

