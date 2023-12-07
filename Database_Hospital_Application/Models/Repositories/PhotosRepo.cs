using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.ViewModels.ViewsVM;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Database_Hospital_Application.Models.Repositories
{
    public class PhotosRepo
    {
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<Foto> photos { get; set; }

        public PhotosRepo()
        {
            photos = new ObservableCollection<Foto>();
        }

        public async Task<ObservableCollection<Foto>> GetAllPhotosAsync()
        {
            ObservableCollection<Foto> photos = new ObservableCollection<Foto>();
            string commandText = "photo.get_all_photos";
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);


            if (result.Rows.Count > 0)
            {
                if (photos == null)
                {
                    photos = new ObservableCollection<Foto>();
                }

                foreach (DataRow row in result.Rows)
                {
                    Foto photo = new Foto
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        Name = row["NAZEV_SOUBORU"].ToString(),
                        Suffix = row["PRIPONA"].ToString(),
                        DateOfUpload = new Oracle.ManagedDataAccess.Types.OracleDate(Convert.ToDateTime(row["DATUM_NAHRANI"])),
                        DateOfModification = new Oracle.ManagedDataAccess.Types.OracleDate(Convert.ToDateTime(row["DATUM_MODIFIKACE"])),
                       // ListOfUserNamesWhichUseMeWithTheirRoles = row["Zam_Role_List"].ToString()
                    };

                    byte[] imageBytes = (byte[])row["obrazek"];
                    BitmapImage bmimg = FotoExtension.ConvertBytesToBitmapImage(imageBytes);
                    if (bmimg != null) photo.Image = bmimg;
                    else photo.Image = new BitmapImage(new Uri("https://github.com/st67024DvorakAdam/IDAS2-BDAS2-semestralni_prace-Dvorak-Polivka/raw/main/Images/no-profile-photo-icon.png"));


                    string commandText2 = "photo.get_all_photos_additional_info";
                    Dictionary<string, object> parameters2 = new Dictionary<string, object>
                {
                    { "p_foto_id", photo.Id }
                };
                    DataTable result2 = await dbTools.ExecuteCommandAsync(commandText2, parameters2);

                    if (result2.Rows.Count > 0)
                    {
                        string ListOfUserNamesWhichUseMeWithTheirRoles = result2.Rows[0]["Zam_Role_List"].ToString();
                        photo.ListOfUserNamesWhichUseMeWithTheirRoles = ListOfUserNamesWhichUseMeWithTheirRoles;
                    }
                    photos.Add(photo);
                }
            }
            return photos;
        }

        public async Task AddPhoto(byte[] picture, string filename, string suffix)
        {
            try
            {
                string storedProcedure = "photo.add_photo";

                OracleParameter pPhotoBlob = new OracleParameter("p_photo_blob", OracleDbType.Blob)
                {
                    Value = picture,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pFileName = new OracleParameter("p_nazev_souboru", OracleDbType.Varchar2)
                {
                    Value = filename,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pSuffix = new OracleParameter("p_pripona", OracleDbType.Varchar2)
                {
                    Value = suffix,
                    Direction = ParameterDirection.Input
                };


                var parameters = new List<OracleParameter> { pPhotoBlob, pFileName, pSuffix };


                await dbTools.ExecuteNonQueryAsync(storedProcedure, parameters);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Chyba při nahrávání fotografie: {ex.Message}", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task<int> DeletePhoto(int id)
        {
            string commandText = "photo.delete_photo_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };
            return await dbTools.ExecuteNonQueryAsync(commandText, parameters);
        }

        public async Task<int> UpdatePhoto(int id, byte[] picture, string filename, string suffix)
        {
            try
            {
                string storedProcedure = "photo.update_photo";

                OracleParameter pId = new OracleParameter("p_Id", OracleDbType.Int32)
                {
                    Value = id,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pPhotoBlob = new OracleParameter("p_photo_blob", OracleDbType.Blob)
                {
                    Value = picture,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pFileName = new OracleParameter("p_nazev_souboru", OracleDbType.Varchar2)
                {
                    Value = filename,
                    Direction = ParameterDirection.Input
                };

                OracleParameter pSuffix = new OracleParameter("p_pripona", OracleDbType.Varchar2)
                {
                    Value = suffix,
                    Direction = ParameterDirection.Input
                };


                var parameters = new List<OracleParameter> {pId, pPhotoBlob, pFileName, pSuffix };


                return await dbTools.ExecuteNonQueryAsync(storedProcedure, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při nahrávání fotografie: {ex.Message}", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }

        public void DeleteAllPhotos()
        {

        }

        public async Task<Foto> GetBasicPhotoAsync()
        {
            Foto photo = new Foto();
            string commandText = "photo.GetEmployeeImage";
            var parameters = new Dictionary<string, object>
            {
                { "p_id", 1 }
            };
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, parameters);


            if (result.Rows.Count > 0)
            {
                    photo = new Foto
                    {
                        Id = Convert.ToInt32(result.Rows[0]["ID"]),
                        Name = result.Rows[0]["NAZEV_SOUBORU"].ToString(),
                        Suffix = result.Rows[0]["PRIPONA"].ToString(),
                        DateOfUpload = new Oracle.ManagedDataAccess.Types.OracleDate(Convert.ToDateTime(result.Rows[0]["DATUM_NAHRANI"])),
                        DateOfModification = new Oracle.ManagedDataAccess.Types.OracleDate(Convert.ToDateTime(result.Rows[0]["DATUM_MODIFIKACE"])),
                        // ListOfUserNamesWhichUseMeWithTheirRoles = row["Zam_Role_List"].ToString()
                    };

                    byte[] imageBytes = (byte[])result.Rows[0]["obrazek"];
                    BitmapImage bmimg = FotoExtension.ConvertBytesToBitmapImage(imageBytes);
                    if (bmimg != null) photo.Image = bmimg;
                    else photo.Image = new BitmapImage(new Uri("https://github.com/st67024DvorakAdam/IDAS2-BDAS2-semestralni_prace-Dvorak-Polivka/raw/main/Images/no-profile-photo-icon.png"));
                }
            
            return photo;
        }
    }
}
