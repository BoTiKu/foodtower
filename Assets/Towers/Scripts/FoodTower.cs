namespace TowerDefense
{
    public class FoodTower : AttackTower<FoodProjectiles>
    {
        protected FoodTypes _foodType;

        public override TowerTypes Type => TowerTypes.Food;
        public FoodTypes FoodType => _foodType;


        protected override FoodProjectiles CreateProjectiles()
        {
            var projectiles = base.CreateProjectiles();
            projectiles.Configurate(this, _target);
            return projectiles;
        }
    }
}