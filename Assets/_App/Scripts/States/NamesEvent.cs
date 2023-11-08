public static class NamesEvent
{
    public const string Currency = "Currency";
    public const string Defaulter = "Defaulter";

    public const string FinishPath = "FinishPath";
    public const string EnterState = "EnterState";
    public const string ShopState = "ShopState";
    public const string WorkState = "WorkState";
    public const string NeutralState = "NeutralState";
    public const string HomeState = "HomeState";
    public const string NextState = "NextState";
    
    
    public const string OnClickSomeButton = "OnBtn";
    public const string HomeButton = "OnHomeStateClick";
    public const string WorkButton = "OnWorkStateClick";
    public const string ShopButton = "OnShopStateClick";
}

public enum States
{
    ShopState,
    WorkState,
    NeutralState,
    HomeState
}
