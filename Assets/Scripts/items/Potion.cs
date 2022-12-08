


public class Potion : InteractableScript
{
    public int amt;
 
    private void Start()
    {
        SetDialog(amt + " Potions Collected");
        this.isCollectable = true;
    }

    public override void ItemEffect()
    {
        inventory.AddPotions(amt);
    }



}
