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
using Database_Hospital_Application.Models.Enums;

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

                OracleParameter pPhone = new OracleParameter("p_telefon", OracleDbType.Int32)
                {
                    Value = contact.PhoneNumber,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pMail = new OracleParameter("p_email", OracleDbType.Varchar2)
                {
                    Value = contact.Email,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pFirstName = new OracleParameter("p_jmeno", OracleDbType.Varchar2)
                {
                    Value = employee.FirstName,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pLastName = new OracleParameter("p_prijmeni", OracleDbType.Varchar2)
                {
                    Value = employee.LastName,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pUserName = new OracleParameter("p_uzivatelske_jmeno", OracleDbType.Varchar2)
                {
                    Value = employee.UserName,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pBirthnumber = new OracleParameter("p_rodne_cislo", OracleDbType.Int64)
                {
                    Value = Convert.ToInt64(employee.BirthNumber),
                    Direction = ParameterDirection.Input
                };

                OracleParameter pSex = new OracleParameter("p_pohlavi", OracleDbType.Varchar2)
                {
                    Value = SexEnumParser.GetStringFromEnumCzech(employee.Sex),
                    Direction = ParameterDirection.Input
                };

                OracleParameter pIdOfDepartment = new OracleParameter("p_oddeleni_id", OracleDbType.Int32)
                {
                    Value = employee._department.Id,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pIdOfRole = new OracleParameter("p_role_id", OracleDbType.Int32)
                {
                    Value = employee.RoleID,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pPassword = new OracleParameter("p_heslo", OracleDbType.Varchar2)
                {
                    Value = employee.Password,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pSalt = new OracleParameter("p_salt", OracleDbType.Varchar2)
                {
                    Value = employee.Salt,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pIdOfSupperiorEmployee = new OracleParameter("p_nadrizeny_id", OracleDbType.Varchar2)
                {
                    Value = employee.IdOfSuperiorEmployee,
                    Direction = ParameterDirection.Input
                };

                var parameters = new List<OracleParameter> { pPhotoBlob, pFileName, pSuffix, pPhone, pMail, pUserName, pBirthnumber, pSex, pIdOfDepartment, pIdOfRole, pPassword, pSalt, pIdOfSupperiorEmployee };


                await dbTools.ExecuteNonQueryAsync(storedProcedure, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při nahrávání fotografie: {ex.Message}", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
      
    }
}
