using GameClicker.Collections;

namespace GameClicker.Services
{
    public interface IKeyManager
    {
        bool Start(ref KeyConfigCollection keyConfigCollection);
        bool Stop();
    }
}