using System.Data.SqlTypes;
using Ladeskab.EventArgs;

namespace Ladeskab
{
    public class ChargeControl: IChargeControl
    {
        public bool Connected { get; set; }
        private IUsbCharger Charger { get; set; }
        private IDisplay Display { get; set; }
        private string lastMessage { get; set; }

        //new
        public double Current;
        public ChargeControl(IUsbCharger charger,IDisplay display)
        {
            Charger = charger;
            charger.CurrentValueEvent += HandleCurrentChangedEvent;
            Display = display;
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
            lastMessage = null;
        }

        private void DiplayChargeMessage()
        {
            string message;
            if (Current > 0 && Current <= 5)
            {
                message = "Batteriet er fuldt opladt og kan tages fra strømmen";
            }
            else if (Current> 5 && Current <=500)
            {
                message = "Batteriet oplader";
            }
            else if (Current > 500)
            {
                message = "Fejl: Opladning skal afsluttes med det sammer";
            }
            else
            {
                message = null;
            }
            if (message != null  && message != lastMessage)
            {
                Display.Print(message);
                lastMessage = message;
            }
        }

        private void HandleCurrentChangedEvent(object sender, CurrentEventArgs e)
        {
            Current = e.Current;
            DiplayChargeMessage();
        }
    }
}