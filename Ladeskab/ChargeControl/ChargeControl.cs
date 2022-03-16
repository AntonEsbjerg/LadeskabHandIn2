using System.Data.SqlTypes;

namespace Ladeskab
{
    public class ChargeControl: IChargeControl
    {
        private bool Connected { get; set; }
        private IUsbCharger Charger { get; set; }

        //new
        public double Current;
        public ChargeControl(IUsbCharger charger)
        {
            Charger = charger;
            charger.CurrentValueEvent += HandleCurrentChangedEvent;
        }

        public bool IsConnected()
        {
            return Connected;
        }

        public void StartCharge()
        {
            Charger.StartCharge();
        }

        public void StopCharge()
        {
            Charger.StopCharge();
        }

        public void PluginPhone()
        {
            Connected = true;
        }

        public void UnplugPhone()
        {
            Connected = false;
        }
        private void HandleCurrentChangedEvent(object sender, CurrentEventArgs e)
        {
            Current = e.Current;
        }
    }
}