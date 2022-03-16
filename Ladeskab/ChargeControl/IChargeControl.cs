namespace Ladeskab
{
    public interface IChargeControl
    { 
        bool Connected { get; set; }
        void StartCharge();
        void StopCharge();
        void PluginPhone();
        void UnplugPhone();
    }
}