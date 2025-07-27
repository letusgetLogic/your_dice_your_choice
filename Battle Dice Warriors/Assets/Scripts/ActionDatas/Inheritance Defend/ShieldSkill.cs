public class ShieldSkill
{
    public readonly int Percentage, DamageReduction, HitEndurance, RoundEndurance;
    public readonly string BuffText;
    public ShieldSkill(int dpPercentage, int damageReduction, int hitEndurance, int roundEndurance, string buffText)
    {
        Percentage = dpPercentage;
        DamageReduction = damageReduction;
        HitEndurance = hitEndurance;
        RoundEndurance = roundEndurance;
        BuffText = buffText;
    }
}

