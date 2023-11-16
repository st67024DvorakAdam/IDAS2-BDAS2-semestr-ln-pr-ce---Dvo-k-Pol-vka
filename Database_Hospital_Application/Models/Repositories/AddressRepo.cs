using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.ViewModels.ViewsVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net;
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

        public async Task AddAddress(Address address)
        {
            string commandText = "add_address";

            var parameters = new Dictionary<string, object>
            {
                { "p_ulice", address.Street },
                { "p_mesto", address.City },
                { "p_cislo_popisne", address.HouseNumber },
                { "p_stat", (address.Country).ToUpper() },
                { "p_psc", address.ZipCode }
            };

            await dbTools.ExecuteNonQueryAsync(commandText, parameters);

        }

        public async Task<int> DeleteAddress(int id)
        {
            string commandText = "delete_address_by_id";

            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };

            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);

        }
        public async Task<int> UpdateAddress(Address address)
        {
            string commandText = "update_address";

            var parameters = new Dictionary<string, object>
            {
                {"p_id", address.Id },
                { "p_ulice", address.Street },
                { "p_mesto", address.City },
                { "p_cislo_popisne", address.HouseNumber },
                { "p_stat", (address.Country).ToUpper() },
                { "p_psc", address.ZipCode }
            };
            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public void DeleteAllAddresses()
        {

        }
    }
}
