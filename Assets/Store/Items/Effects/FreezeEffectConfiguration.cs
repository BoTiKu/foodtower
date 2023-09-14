using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "Freeze Effect Item Configuration", menuName = "Game/Store/Effects/Freeze Effect", order = 51)]
    public class FreezeEffectConfiguration : EffectsItemConfigurate
    {
        [Range(.0f, 1f), Tooltip("Замедление в процентах от 100% - полная остановка")]
        public float SlowingDown;

        public override void Execute()
        {
            base.Execute();
            foreach (var mob in LevelController.Instance.Mobs)
            {
                if (mob.HaveEffects(out FreezeSystem system))
                {
                    system.Reload();
                    continue;
                }

                var effect = mob.gameObject.AddComponent<FreezeSystem>();
                effect.SetUp(this);
            }
        }
    }
}

