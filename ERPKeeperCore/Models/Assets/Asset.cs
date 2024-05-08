using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel;
using ERPKeeperCore.Enterprise.Models.Assets.Enums;

namespace ERPKeeperCore.Enterprise.Models.Assets
{
    public class Asset
    {
        [Key]
        public Guid Id { get; set; }


        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }








        public int No { get; set; }
        public String? Name { get; set; }
        public String? Code { get; set; }
        public AssetStatus Status { get; set; }

        public DateTime PurchaseDate { get; set; }
        public DateTime StartDeprecationDate { get; set; }
        public DateTime EndDeprecationDate => StartDeprecationDate.AddYears(AssetType?.UseFulLifeYear ?? 1).AddDays(-1);


        public int RemainAgeDay => (int)Math.Max(0, (EndDeprecationDate - DateTime.Now).TotalDays);
        private DateTime LastDateOfMonth(DateTime date) => new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        private decimal TotalDaysInYear(int year) => DateTime.IsLeapYear(year) ? 366 : 365;



        public Decimal AssetValue { get; set; }


        public Decimal SavageValue { get; set; }


        public Decimal TotalDepreciationValue => AssetValue - SavageValue;


        public Decimal DepreciationPerYear { get; set; }


        public void UpdateDeprecation()
        {
            if (AssetType.DeprecatedAble)
                DepreciationPerYear = decimal.Round(TotalDepreciationValue / (AssetType?.UseFulLifeYear ?? 1), 2, MidpointRounding.AwayFromZero);
            else
                DepreciationPerYear = 0;
        }


        public String? Memo { get; set; }
        public String? Reference { get; set; }

        public Guid? AssetTypeId { get; set; }
        [ForeignKey("AssetTypeId")]
        public virtual AssetType AssetType { get; set; }

        public virtual ICollection<AssetDeprecateSchedule> DepreciationSchedules { get; set; }

        public DateTime? LastCreateSchedule { get; set; }

        public Asset()
        {

        }

        public void RemoveSchedule()
        {

            foreach (var schedule in DepreciationSchedules.ToList())
            {
                DepreciationSchedules.Remove(schedule);
            }
        }

        public void CreateDepreciationSchedule()
        {

            this.RemoveSchedule();

            if (AssetType == null)
                return;
            if (AssetType.DeprecatedAble == false)
                return;

            UpdateDeprecation();

            var _startDate = StartDeprecationDate.Date;

            int index = 1;
            decimal _accDeprecation = 0;

            Console.WriteLine($"{Code} > {StartDeprecationDate.ToString("dd MMMM yyyy")} to {EndDeprecationDate.ToString("dd MMMM yyyy")}");

            while (_startDate < EndDeprecationDate.Date && index <= AssetType.UseFulLifeYear + 1)
            {
                var endOfYearDate = new DateTime(_startDate.Year, 1, 1).AddYears(1).AddDays(-1);
                var _endDate = new DateTime(Math.Min(endOfYearDate.Ticks, EndDeprecationDate.Ticks));

                decimal _totalDays = (decimal)Math.Abs((_endDate - _startDate).TotalDays) + 1;
                decimal _openingValue = AssetValue - _accDeprecation;
                decimal _deprecateValue = Math.Round(DepreciationPerYear * _totalDays / TotalDaysInYear(_startDate.Year), 2);
                decimal _newAccDeprecation = _accDeprecation + _deprecateValue;

                if (_endDate.Date == EndDeprecationDate.Date)
                {
                    decimal _offset = TotalDepreciationValue - _newAccDeprecation;
                    _deprecateValue = _deprecateValue + _offset;
                    _newAccDeprecation = _accDeprecation + _deprecateValue;
                }

                decimal _remainValue = AssetValue - _newAccDeprecation;

                index = NewDepreciationSchedule(_startDate, index, _endDate, _openingValue, _deprecateValue, _newAccDeprecation, _remainValue);

                _startDate = _endDate.AddDays(1);
                _accDeprecation = _newAccDeprecation;
            }

            LastCreateSchedule = DateTime.Now;
        }

        private int NewDepreciationSchedule(DateTime _startDate, int index, DateTime _endDate, decimal _openingValue, decimal _deprecateValue, decimal _newAccDeprecation, decimal _remainValue)
        {
            var _schedule = new AssetDeprecateSchedule()
            {
                AssetId = Id,
                StartDate = _startDate,
                EndDate = _endDate,
                OpeningValue = _openingValue,
                DepreciationValue = _deprecateValue,
                DepreciateAccumulation = _newAccDeprecation,
                RemainValue = _remainValue,
                No = index++,
            };
            DepreciationSchedules.Add(_schedule);
            return index;
        }

        internal void PostToTransaction()
        {
            Console.WriteLine($">Post  Assets:{this.Name}");

            if (this.Transaction == null)
                return;
            this.Transaction.ClearLedger();

            // Dr.
            this.Transaction.AddDebit(this.AssetType.AwaitDeprecateAccount, this.AssetValue);

            // Cr.
            this.Transaction.AddCredit(this.AssetType.Purchase_Asset_Account, this.AssetValue);

            this.Transaction.Date = this.PurchaseDate;
            this.Transaction.Name = this.Name;
            this.Transaction.Reference = this.Reference;
            this.Transaction.PostedDate = DateTime.Now;
            this.IsPosted = true;
            this.Transaction.UpdateBalance();
        }
    }
}
