namespace Assets.Scripts
{
    public static class PhaseInitialization
    {
        public static void Do()
        {
            LevelGenerator.Instance.SetData();

            BattleManager.Instance.HideAllPanel();

            BattleManager.Instance.InitializeFields();
            BattleManager.Instance.InitializeCharacter();

           
            LevelGenerator.Instance.SpawnField();
            LevelGenerator.Instance.SpawnCharacter();
        }
    }
}
