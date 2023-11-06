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
using System.Windows;

namespace Database_Hospital_Application.Models.Repositories
{
    public class AddressRepo
    {
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<Address> addresses { get; set; }

        public AddressRepo()
        {
            addresses = new ObservableCollection<Address>();
        }

        public async Task<ObservableCollection<Address>> GetAllAddressesAsync()
        {
            ObservableCollection<Address> addresses = new ObservableCollection<Address>();
            string commandText = "get_all_addresses";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);


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
