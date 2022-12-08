

public class Coin : InteractableScript
{
    public int amt;

    private void Start()
    {
        SetDialog(amt + " Gold Found");
        this.isCollectable = true;
    }

    public override void ItemEffect()
    {
        inventory.AddGold(amt);
    }


}
