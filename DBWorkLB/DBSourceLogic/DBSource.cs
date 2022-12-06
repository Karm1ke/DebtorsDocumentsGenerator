using System;
using System.Data;

namespace DBWorkLB
{
    public static class DBSource
    {
        public static DataSet mainDataSet;

        public static void UpdateReferences()
        {
            debtorsCourtcasesDt = mainDataSet.Tables[TableNames.debtors_courtcases];
            debtorsPassportDataDt = mainDataSet.Tables[TableNames.debtors_passport_data];
            generationLogDt = mainDataSet.Tables[TableNames.generation_log];
            templatesDt = mainDataSet.Tables[TableNames.templates];
            usersDt = mainDataSet.Tables[TableNames.users];
            debtorsCommonDt = mainDataSet.Tables[TableNames.debtors_common_local];
        }

        public static DataTable debtorsCourtcasesDt;
        public static DataTable debtorsPassportDataDt;
        public static DataTable debtorsCommonDt;
        public static DataTable generationLogDt;
        public static DataTable templatesDt;
        public static DataTable usersDt;
    }
}
