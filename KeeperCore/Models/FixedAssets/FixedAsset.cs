using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using System.ComponentModel;
using KeeperCore.ERPNode.Models.Accounting;
using KeeperCore.ERPNode.Models.Accounting.Enums;

namespace KeeperCore.ERPNode.Models.Assets
{
    [Table("ERP_FixedAssets")]
    public class FixedAsset
    {
        [Key]
        public Guid Id { get; set; }
        public int No { get; set; }
        public String Name { get; set; }
        public String Code { get; set; }
        public String AssetCode => $"AST-{StartDeprecationDate.ToString("yyyy")}/{this.No.ToString()}";

        public Guid? FiscalYearId { get; set; }
        [ForeignKey("FiscalYearId")]
        public virtual FiscalYear FiscalYear { get; set; }
        public Enums.FixedAssetStatus Status { get; set; }

        [Column("AccquiredDate")]
        public DateTime StartDeprecationDate { get; set; }
        public DateTime EndDeprecationDate => StartDeprecationDate.AddYears(this.FixedAssetType?.UseFulLifeYear ?? 1).AddDays(-1);
        public double RemainAgeDay => Math.Max(0, (EndDeprecationDate - DateTime.Now).TotalDays);
        private DateTime LastDateOfMonth(DateTime date) => new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        private Decimal TotalDaysInYear(int year) => (decimal)(DateTime.IsLeapYear(year) ? 366 : 365);

        public Decimal AssetValue { get; set; }

        [Column(TypeName = "money")]
        public Decimal SavageValue { get; set; }
        public Decimal TotalDepreciationValue => AssetValue - SavageValue;
        public decimal DepreciationPerYear { get; set; }


        public void UpdateDeprecation()
        {
            if (this.FixedAssetType.DeprecatedAble)
                DepreciationPerYear = decimal.Round(this.TotalDepreciationValue / (decimal)(this.FixedAssetType?.UseFulLifeYear ?? 1), 2, MidpointRounding.AwayFromZero);
            else
                DepreciationPerYear = 0;
        }


        public String Memo { get; set; }
        public String Reference { get; set; }


        [Column("FixedAssetTypeId")]
        public Guid? FixedAssetTypeId { get; set; }
        [ForeignKey("FixedAssetTypeId")]
        public virtual FixedAssetType FixedAssetType { get; set; }

        public LedgerPostStatus PostStatus { get; set; }

        public virtual ICollection<DeprecateSchedule> DepreciationSchedules { get; set; }





        public DateTime? LastCreateSchedule { get; private set; }

        public FixedAsset()
        {
            
        }






        public virtual DeprecateSchedule GetNearestSchedules(FiscalYear FiscalYear)
        {
            return DepreciationSchedules?
                         .Where(d => d.EndDate <= FiscalYear.EndDate)
                         .OrderByDescending(x => x.EndDate).FirstOrDefault();
        }
        public virtual DeprecateSchedule GetNearestSchedules(DateTime date)
        {
            return DepreciationSchedules?
                         .Where(d => d.EndDate <= date)
                         .OrderByDescending(x => x.EndDate).FirstOrDefault();
        }

        public void UpdateStatus()
        {
            throw new Exception("Update fIX ASSETStatus");
            //if (this.CurrentAssetValue == this.SavageValue)
            //{
            //    this.Status = Enums.FixedAssetStatus.Closed;
            //}
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

            if (this.FixedAssetType == null)
                return;
            if (this.FixedAssetType.DeprecatedAble == false)
                return;

            this.UpdateDeprecation();

            var _startDate = this.StartDeprecationDate.Date;

            int index = 1;
            Decimal _accDeprecation = 0;

            Console.WriteLine($"{this.Code} > {this.StartDeprecationDate.ToString("dd MMMM yyyy")} to {this.EndDeprecationDate.ToString("dd MMMM yyyy")}");

            while (_startDate < this.EndDeprecationDate.Date && index <= this.FixedAssetType.UseFulLifeYear + 1)
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
            var _schedule = new DeprecateSchedule()
            {
                Id = Guid.NewGuid(),
                FixedAssetId = this.Id,
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
