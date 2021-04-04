using System;


namespace ClassLibrary1
{
    class SubsidyCalculation : ISubsidyCalculation
    {
        private Volume volume;
        private Tariff tariff;

        public event EventHandler<string> OnNotify;
        public event EventHandler<Tuple<string, Exception>> OnException;

        public Charge CalculateSubsidy(Volume volumes, Tariff tariff)
        {
            OnNotify?.Invoke(this, $"Расчёт начат в {DateTime.Now}");
            Charge charge = new Charge();

            try
            {
                this.volume = volumes;
                this.tariff = tariff;

                ValidServiceID();
                ValidHouseID();
                ValidMonth();
                ValidValue();

                charge.ServiceId = volume.ServiceId;
                charge.HouseId = volume.HouseId;
                charge.Month = volume.Month;
                charge.Value = volume.Value * tariff.Value;
            }
            catch (Exception ex)
            {
                OnException?.Invoke(this, new Tuple<string, Exception>(ex.Message, ex));
                throw;
            }

            OnNotify?.Invoke(this, $"Расчёт успешно завершён в {DateTime.Now}");

            return charge;
        }

        private void ValidServiceID()
        {
            if (volume.ServiceId != tariff.ServiceId)
            {
                throw new Exception("Идентификаторы услуг не совпадают!");
            }
        }

        private void ValidHouseID()
        {
            if (volume.HouseId != tariff.HouseId)
            {
                throw new Exception("Идентификаторы домов не совпадают!");
            }
        }

        private void ValidMonth()
        {
            if ((volume.Month < tariff.PeriodBegin) || (volume.Month > tariff.PeriodEnd))
            {
                throw new Exception("Месяц объёма не входит в период действия тарифа!");
            }
        }

        private void ValidValue()
        {
            if (volume.Value < 0)
            {
                throw new Exception("Значение объёма не может быть меньше нуля!");
            }
            if (tariff.Value <= 0)
            {
                throw new Exception("Значение тарифа не может быть меньше либо равно нуля!");
            }
        }
    }
}
