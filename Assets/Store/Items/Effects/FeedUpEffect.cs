using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "Feed Up Effect Item Configuration", menuName = "Game/Store/Effects/Feed Up", order = 51)]
    public class FeedUpEffect : EffectsItemConfigurate
    {
        [Range(.0f, 1f), Tooltip("Кормление в процентах, где 100% - полностью накормить")]
        public float FoodInPercent;

        public override void Execute()
        {
            base.Execute();
            foreach (var mob in LevelController.Instance.Mobs)
            {
                var calculateFood = Mathf.FloorToInt(mob.NeedFood * FoodInPercent);
                mob.TakeFood(mob.FoodEat, calculateFood);
            }
        }
    }
}