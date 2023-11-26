using Database_Hospital_Application.Models.Entities;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Database_Hospital_Application.Models.Repositories
{
    public class AddNewUserEasyWayRepo
    {
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public async Task AddEmployee(Employee employee, Foto foto, Contact contact)
        {
            try
            {
                string storedProcedure = "add_complete_employee";

                OracleParameter pPhotoBlob = new OracleParameter("p_photo_blob", OracleDbType.Blob)
                {
                    Value = FotoExtension.BitmapImageToBytes(foto.Image),
                    Direction = ParameterDirection.Input
                };

                OracleParameter pFileName = new OracleParameter("p_nazev_souboru", OracleDbType.Varchar2)
                {
                    Value = foto.Name,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pSuffix = new OracleParameter("p_pripona", OracleDbType.Varchar2)
                {
                    Value = foto.Suffix,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pPhone = new OracleParameter("p_telefon", OracleDbType.Blob)
                {
                    Value = contact.PhoneNumber,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pMail = new OracleParameter("p_email", OracleDbType.Blob)
                {
                    Value = contact.Email,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pFirstName = new OracleParameter("p_jmeno", OracleDbType.Blob)
                {
                    Value = employee.FirstName,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pLastName = new OracleParameter("p_prijmeni", OracleDbType.Blob)
                {
                    Value = employee.LastName,
                    Direction = ParameterDirection.Input
                };

                //TODO ostatní atributy ze zaměstnance + proceduru v sql

                var parameters = new List<OracleParameter> { pPhotoBlob, pFileName, pSuffix, pPhone, pMail };


                await dbTools.ExecuteNonQueryAsync(storedProcedure, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při nahrávání fotografie: {ex.Message}", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
      
    }
}
