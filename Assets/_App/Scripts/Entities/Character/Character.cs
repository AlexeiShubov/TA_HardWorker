using UnityEngine;

public class Character : MonoBehaviour
{
    private SOGlobalSettings _soGlobalSettings;

    public void Init(SOGlobalSettings soGlobalSettings)
    {
        _soGlobalSettings = soGlobalSettings;
    }
}
