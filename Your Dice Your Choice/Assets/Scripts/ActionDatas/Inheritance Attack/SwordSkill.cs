public class SwordSkill
{
    public Direction Direction;
    public int Range;   
    public int Percentage;
    public int HitEndurance;
    public int RoundEndurance;
    public string BuffAPText;

    public SwordSkill(Direction direction, int range, int percentage, int hitEndurance, int roundEndurance, string buffAPText)
    {
        Direction = direction;
        Range = range;
        Percentage = percentage;
        HitEndurance = hitEndurance;
        RoundEndurance = roundEndurance;
        BuffAPText = buffAPText;
    }
}
