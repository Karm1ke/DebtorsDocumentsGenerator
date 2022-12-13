using System;

namespace DBWorkLB
{
    public class DebtorCourtcase
    {
        public int RegisterNumber { get; set; }
        /// <summary>
        /// Cумма долга
        /// </summary>
        public decimal DebtSum { get; set; }
        public DateTime? DebtPeriodStartDate { get; set; }
        public DateTime? DebtPeriodEndDate { get; set; }
        /// <summary>
        /// Общая сумма долга
        /// </summary>
        public decimal TotalDebtSum { get; set; }
        /// <summary>
        /// Номер дела
        /// </summary>
        public string CaseNumber { get; set; }
        /// <summary>
        /// Взысканная сумма основного долга
        /// </summary>
        public decimal RecoveredMainAmountSum { get; set; }
        /// <summary>
        /// Взысканная сумма пенни
        /// </summary>
        public decimal RecoveredAmountPenny { get; set; }
        public DateTime? PennyPeriodStartDate { get; set; }
        public DateTime? PennyPeriodEndDate { get; set; }
        /// <summary>
        /// Взысканная сумма государственной пошлины
        /// </summary>
        public decimal RecoveredGovernmentDuty { get; set; }
        /// <summary>
        /// Дата принятия судом решения
        /// </summary>
        public DateTime? DesicionDate { get; set; }
        /// <summary>
        /// Дата вступления решения в законную силу
        /// </summary>
        public DateTime? DesicionStartDate { get; set; }
        /// <summary>
        /// Дата отмены решения
        /// </summary>
        public DateTime? DesicionCancelDate { get; set; }
    }
}
