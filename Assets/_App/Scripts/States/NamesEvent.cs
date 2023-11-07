public static class NamesEvent
{
    public const string Currency = "Currency";

    public const string ExitState = "ExitState";
    public const string ShopState = "ShopState";
    public const string WorkState = "WorkState";
    public const string NeutralState = "NeutralState";
    public const string HomeState = "HomeState";
    public const string NextState = "NextState";
}

public enum States
{
    ShopState,
    WorkState,
    NeutralState,
    HomeState
}
