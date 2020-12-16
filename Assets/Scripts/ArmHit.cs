internal class ArmHit : IWeapon
{
    public string Name => throw new System.NotImplementedException();

    public int Charge => throw new System.NotImplementedException();

    int IWeapon.Charge => throw new System.NotImplementedException();

    public void AddCharge(int charge)
    {
        throw new System.NotImplementedException();
    }
}