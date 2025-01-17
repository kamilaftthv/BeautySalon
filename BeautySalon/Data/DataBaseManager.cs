using BeautySalon.DbConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalon.Data
{
    internal class DataBaseManager
    {
        public static BeautySalonEntities DataBaseConnection = new BeautySalonEntities();

        public static List<Client> GetClients()
        {
            return DataBaseConnection.Client.ToList();
        }
        public static List<Service> GetServices()
        {
            return DataBaseConnection.Service.ToList();
        }
        public static List<ClientService> GetClientServices()
        {
            return DataBaseConnection.ClientService.ToList();
        }
        public static List<Gender> GetGenders()
        {
            return DataBaseConnection.Gender.ToList();
        }
        
        public static bool UpdateDatabase()
        {
            try
            {
                DataBaseConnection.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void SaveChanges()
        {
            DataBaseConnection.SaveChanges();
        }
    }
}
