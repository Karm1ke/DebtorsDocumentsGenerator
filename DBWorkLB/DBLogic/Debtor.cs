using System;
using System.Collections.Generic;

namespace DBWorkLB
{
    public class Debtor
    {
        /// <summary>
        /// Номер лицевого счёта
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Lastname { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string Secondname { get; set; }
        /// <summary>
        /// Пункт проживания
        /// </summary>
        public string ResidencePlace { get; set; }
        /// <summary>
        /// Номер дома
        /// </summary>
        public int HouseNumber { get; set; }
        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Номер квартиры
        /// </summary>
        public int RoomNumber { get; set; }
        /// <summary>
        /// Доля в праве
        /// </summary>
        public int ShareRight { get; set; }
        /// <summary>
        /// Судебные дела
        /// </summary>
        public List<DebtorCourtcase> Courtcases { get; set; }
    }
}
