public class AmmoCrate : ResourceCrate
{

    public override void UseMeOn(Tank tank)
    {
        tank.RestoreAmmo(_capacity);
        DestroyThis();
    }
}
