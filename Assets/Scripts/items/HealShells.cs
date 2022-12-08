

public class HealShells : InteractableScript
{
    public int amt;

    private void Start()
    {
        SetDialog(amt + " HealShells Collected");
        this.isCollectable = true;
    }

    public override void ItemEffect()
    {
        inventory.AddHealShells(amt);
    }
}
