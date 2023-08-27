namespace TowerDefense
{
    public class FoodProjectiles : Projectiles
    {
        public FoodTower OwnerTower { get; private set; }

        public void Configurate(FoodTower ownerTower, Animal targert)
        {
            _target = targert;
            OwnerTower = ownerTower;
        }

        protected override void ReachTarget()
        {
            _target.TakeFood(this);
        }
    }
}