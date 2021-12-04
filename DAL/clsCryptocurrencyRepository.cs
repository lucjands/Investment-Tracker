using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class clsCryptocurrencyRepository : ICryptocurrencyRepository
    {
        public bool TableExists()
        {
            string query = @"IF EXISTS (SELECT * 
                                        FROM INFORMATION_SCHEMA.TABLES 
                                        WHERE TABLE_NAME = N'Employees')
                             BEGIN
                                RETURN ";
            return false;
        }

        public bool Delete(clsCryptocurrencyM entity)
        {
            throw new NotImplementedException();
        }

        public bool GenerateTable()
        {
            throw new NotImplementedException();
        }

        public List<clsCryptocurrencyM> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsCryptocurrencyM GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsCryptocurrencyM entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(clsCryptocurrencyM entity)
        {
            throw new NotImplementedException();
        }
    }
}
