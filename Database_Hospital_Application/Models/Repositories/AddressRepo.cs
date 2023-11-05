using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.ViewModels.ViewsVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Repositories
{
    public class AddressRepo
    {
        public ObservableCollection<Address> addresses { get; set; }

        public ObservableCollection<Address> GetAllAddresses()
        {
            DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();
            string commandText = "get_all_addresses";

            DataTable result = dbTools.ExecuteCommand(commandText, null);

            if (result.Rows.Count > 0)
            {
                if (addresses == null)
                {
                    addresses = new ObservableCollection<Address>();
                }

                foreach (DataRow row in result.Rows)
                {
                    Address address = new Address
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Street = row["ULICE"].ToString(),
                        City = row["MESTO"].ToString(),
                        HouseNumber = Convert.ToInt32(row["CISLO_POPISNE"]),
                        Country  = row["STAT"].ToString(),
                        ZipCode = Convert.ToInt32(row["PSC"])
                    };
                    addresses.Add(address);
                }
            }

            return addresses;
        }

        public void AddAddress(Address address)
        {

        }

        public void DeleteAddress(int id)
        {

        }
        public void UpdateAddress(Address address)
        {

        }

        public void DeleteAllAddresses()
        {

        }
    }
}
