namespace Ladeskab
{
    public interface IChargeControl
    {
        public bool IsConnected();
        public void StartCharge();
        public void StopCharge();
        public void PluginPhone();
        public void UnplugPhone();
    }
}