namespace TowerDefense
{
    public enum TowerEffectTypes
    {
        EnhancingDamage
    }

    public class EffectEntity
    {
        public float Value;
        public TowerEffectTypes Type;

        public EffectEntity(float val, TowerEffectTypes type)
        {
            Value = val;
            Type = type;
        }
    }
}