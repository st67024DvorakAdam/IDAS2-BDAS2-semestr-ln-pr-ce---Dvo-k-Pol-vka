using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string commandText = "get_all_photos";
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


                    string commandText2 = "get_all_photos_additional_info";
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

        public void AddPhoto(Foto photo)
        {

        }

        public void DeletePhoto(int id)
        {

        }
        public void UpdatePhoto(Foto photo)
        {

        }

        public void DeleteAllPhotos()
        {

        }
    }
}
