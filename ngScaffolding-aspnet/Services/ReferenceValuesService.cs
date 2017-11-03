using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ngScaffolding.database;
using ngScaffolding.database.Models;

namespace ngScaffolding.Services
{
    public interface IReferenceValuesService
    {
        ReferenceValue GetDefinition(string name, string group = null);
        ReferenceValue GetValue(string name, string group = null);
    }

    public class ReferenceValuesService : IReferenceValuesService
    {
        private readonly ngScaffoldingContext _context;
        private readonly IConfigurationRoot _configuration;
        private readonly ICacheService _cacheService;
        private readonly IRepository<ReferenceValue> _referenceValues;

        public ReferenceValuesService(ngScaffoldingContext context,
            IConfigurationRoot configuration,
            ICacheService cacheService,
            IRepository<ReferenceValue> referenceValues)
        {
            _context = context;
            _configuration = configuration;
            _cacheService = cacheService;
            _referenceValues = referenceValues;
        }

        public ReferenceValue GetDefinition(string name, string group = null)
        {
            ReferenceValue retVal = null;

            if (!string.IsNullOrEmpty(group))
            {
                retVal = _referenceValues.GetAll().FirstOrDefault(r => r.Name == name && r.GroupName == group);
            }
            else
            {
                retVal = _referenceValues.GetAll().FirstOrDefault(r => r.Name == name);
            }

            return retVal;
        }

        public ReferenceValue GetValue(string name, string group = null)
        {
            var key = GetKey(name, group);
            var retVal = _cacheService.Get(key) as ReferenceValue;

            if (retVal == null)
            {
                retVal = this.GetDefinition(name, group);

                if (retVal != null)
                {
                    if(retVal.Type == ReferenceValue.Types_DatabaseQuery ||
                        retVal.Type == ReferenceValue.Types_List)
                    {
                        //Populate Values for non Value Types


                    }

                    _cacheService.Set(key,retVal,retVal.CacheSeconds);
                }
            }

            return retVal;
        }

        private string GetKey(string name, string group = null)
        {
            return string.Format("ReferenceValues::{0}::{1}", name, group);
        }

        private void PopulateOptionList(ReferenceValue refValue, string seed = null)
        {
            switch (refValue.Type)
            {
                case ReferenceValue.Types_SingleValue:
                {
                    break;
                }
                case ReferenceValue.Types_List:
                {
                    foreach (var refItem in _context.ReferenceValueItems.Where(r => r.ReferenceValueId == refValue.Id).OrderBy(o => o.ItemOrder))
                    {
                        if (string.IsNullOrEmpty(refItem.Display) && !string.IsNullOrEmpty(refItem.Value))
                        {
                            refItem.Display = refItem.Value;
                        }
                        refValue.ReferenceValueItems.Add(refItem);
                    }

                    break;
                }
                case ReferenceValue.Types_DatabaseQuery:
                {
                    //Setup SQL connection
                    var connectionString = _configuration.GetConnectionString(refValue.ConnectionName);
                    SqlConnection conn = new SqlConnection(connectionString);

                    conn.Open();
                    var command = refValue.Value;

                    if (!string.IsNullOrEmpty(seed))
                    {
                        command = command.Replace("@@Seed", seed);
                    }
                    else
                    {
                        command = command.Replace("@@Seed", "NULL");
                    }

                    var comm = new SqlCommand(command, conn) { CommandTimeout = 30 };

                    var result = comm.ExecuteReader(CommandBehavior.CloseConnection);

                    int order = 0;
                    while (result.Read())
                    {
                        var opt = new ReferenceValueItem()
                        {
                            ItemOrder = order++,
                        };

                        if (HasColumn(result, "Display"))
                        {
                            opt.Display = result["Display"].ToString();
                        }

                        if (HasColumn(result, "Value"))
                        {
                            opt.Value = result["Value"].ToString();
                        }

                        //if (HasColumn(result, "SubTitle1"))
                        //{
                        //    opt.SubTitle1 = result["SubTitle1"].ToString();
                        //}

                        //if (HasColumn(result, "SubTitle2"))
                        //{
                        //    opt.SubTitle2 = result["SubTitle2"].ToString();
                        //}

                        //if (HasColumn(result, "SubTitle3"))
                        //{
                        //    opt.SubTitle3 = result["SubTitle3"].ToString();
                        //}

                        refValue.ReferenceValueItems.Add(opt);
                    }

                    break;
                }
            }
        }

        public static bool HasColumn(SqlDataReader reader, string columnName)
        {
            foreach (DataRow row in reader.GetSchemaTable().Rows)
            {
                if (row["ColumnName"].ToString() == columnName)
                    return true;
            } //Still here? Column not found. 
            return false;
        }
    }
}
