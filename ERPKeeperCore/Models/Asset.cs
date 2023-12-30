using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel;
using KeeperCore.ERPNode.Models.Enums;

namespace KeeperCore.ERPNode.Models
{
    public class Asset
    {
        [Key]
        public Guid Id { get; set; }
        public int No { get; set; }
        public String Name { get; set; }
        public String Code { get; set; }
        public Enums.AssetStatus Status { get; set; }

        public DateTime PurchaseDate { get; set; }
        public DateTime StartDeprecationDate { get; set; }
        public DateTime EndDeprecationDate => StartDeprecationDate.AddYears(this.AssetType?.UseFulLifeYear ?? 1).AddDays(-1);
       
        
        public int RemainAgeDay => (int)Math.Max(0, (EndDeprecationDate - DateTime.Now).TotalDays);
        private DateTime LastDateOfMonth(DateTime date) => new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        private Decimal TotalDaysInYear(int year) => (decimal)(DateTime.IsLeapYear(year) ? 366 : 365);

        public Decimal AssetValue { get; set; }
        public Decimal SavageValue { get; set; }
        public Decimal TotalDepreciationValue => AssetValue - SavageValue;

        public decimal DepreciationPerYear { get; set; }


        public void UpdateDeprecation()
        {
            if (this.AssetType.DeprecatedAble)
                DepreciationPerYear = decimal.Round(this.TotalDepreciationValue / (decimal)(this.AssetType?.UseFulLifeYear ?? 1), 2, MidpointRounding.AwayFromZero);
            else
                DepreciationPerYear = 0;
        }


        public String Memo { get; set; }
        public String Reference { get; set; }

        public Guid? AssetTypeId { get; set; }
        [ForeignKey("AssetTypeId")]
        public virtual AssetType AssetType { get; set; }

        public virtual ICollection<AssetDeprecateSchedule> DepreciationSchedules { get; set; }

        public DateTime? LastCreateSchedule { get; private set; }

        public Asset()
        {
            
        }

        public void RemoveSchedule()
        {

            foreach (var schedule in this.DepreciationSchedules.ToList())
            {
                this.DepreciationSchedules.Remove(schedule);
            }
        }

        public void CreateDepreciationSchedule()
        {
            this.RemoveSchedule();

            if (this.AssetType == null)
                return;
            if (this.AssetType.DeprecatedAble == false)
                return;

            this.UpdateDeprecation();

            var _startDate = this.StartDeprecationDate.Date;

            int index = 1;
            Decimal _accDeprecation = 0;

            Console.WriteLine($"{this.Code} > {this.StartDeprecationDate.ToString("dd MMMM yyyy")} to {this.EndDeprecationDate.ToString("dd MMMM yyyy")}");

            while (_startDate < this.EndDeprecationDate.Date && index <= this.AssetType.UseFulLifeYear + 1)
            {
                var endOfYearDate = new DateTime(_startDate.Year, 1, 1).AddYears(1).AddDays(-1);
                var _endDate = new DateTime(Math.Min(endOfYearDate.Ticks, this.EndDeprecationDate.Ticks));

                decimal _totalDays = (decimal)Math.Abs((_endDate - _startDate).TotalDays) + 1;
                decimal _openingValue = this.AssetValue - _accDeprecation;
                decimal _deprecateValue = Math.Round(this.DepreciationPerYear * _totalDays / TotalDaysInYear(_startDate.Year), 2);
                decimal _newAccDeprecation = _accDeprecation + _deprecateValue;

                if (_endDate.Date == this.EndDeprecationDate.Date)
                {
                    decimal _offset = this.TotalDepreciationValue - _newAccDeprecation;
                    _deprecateValue = _deprecateValue + _offset;
                    _newAccDeprecation = _accDeprecation + _deprecateValue;
                }

                decimal _remainValue = this.AssetValue - _newAccDeprecation;

                index = NewDepreciationSchedule(_startDate, index, _endDate, _openingValue, _deprecateValue, _newAccDeprecation, _remainValue);

                _startDate = _endDate.AddDays(1);
                _accDeprecation = _newAccDeprecation;
            }

            this.LastCreateSchedule = DateTime.Now;
        }

        private int NewDepreciationSchedule(DateTime _startDate, int index, DateTime _endDate, decimal _openingValue, decimal _deprecateValue, decimal _newAccDeprecation, decimal _remainValue)
        {
            var _schedule = new AssetDeprecateSchedule()
            {
                Id = Guid.NewGuid(),
                AssetId = this.Id,
                StartDate = _startDate,
                EndDate = _endDate,
                OpeningValue = _openingValue,
                DepreciationValue = _deprecateValue,
                DepreciateAccumulation = _newAccDeprecation,
                RemainValue = _remainValue,
                No = index++,
            };
            this.DepreciationSchedules.Add(_schedule);
            return index;
        }
    }
}
