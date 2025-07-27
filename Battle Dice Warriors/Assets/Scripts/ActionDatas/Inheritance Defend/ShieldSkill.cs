public class ShieldSkill
{
    public readonly int Percentage, DamageReduction, HitEndurance, RoundEndurance;
    public ShieldSkill(int dpPercentage, int damageReduction, int hitEndurance, int roundEndurance)
    {
        Percentage = dpPercentage;
        DamageReduction = damageReduction;
        HitEndurance = hitEndurance;
        RoundEndurance = roundEndurance;
    }
}

