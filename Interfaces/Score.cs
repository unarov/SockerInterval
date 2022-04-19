namespace Interfaces;
public struct Score
{
    public Score(int ownerTeam, int guestTeam)
    {
        OwnerTeam = ownerTeam;
        GuestTeam = guestTeam;
    }

    public int OwnerTeam { get; }
    public int GuestTeam { get; }

    public override string ToString() => $"({OwnerTeam}, {GuestTeam})";
}