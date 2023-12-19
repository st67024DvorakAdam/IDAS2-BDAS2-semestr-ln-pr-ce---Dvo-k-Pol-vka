using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Packaging;
using System.Threading.Tasks;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Enums;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.ViewModels.ViewsVM.DoctorVM.PatientViewVM;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;

namespace Database_Hospital_Application.Models.Tools
{
    public class PdfGenerator
    {
        public static async void GeneratePdfForPatient(Patient patient)
        {
            string pdfName = $"Report_{patient.BirthNumber}_{patient.LastName} {patient.FirstName}.pdf";
            string content = "";
            // Cesta, kde bude uložen vygenerovaný PDF soubor
            string pdfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), pdfName);

            // Vytvoření nového dokumentu
            Document document = new Document();

            // Inicializace PdfWriter pro zápis do souboru
            PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));

            // Otevření dokumentu pro editaci
            document.Open();

            document.AddLanguage("cs-CZ"); // Nastavení jazyka dokumentu

            document = await EditDocument(document, patient); 

            // Přidání obsahu do PDF
            document.Add(new Paragraph(content));

            // Uzavření dokumentu
            document.Close();

            // Stažení PDF souboru
            DownloadPdf(pdfPath);
        }

        private static async Task<Document> EditDocument(Document document, Patient patient)
        {
            Font textFont = FontFactory.GetFont("Arial", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED, 10f);
            Font textBoldFont = FontFactory.GetFont("Arial", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED, 10f, Font.BOLD);
            Font titleFont = FontFactory.GetFont("Arial", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED, 15f, Font.BOLD);
            Font title2Font = FontFactory.GetFont("Arial", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED, 13f, Font.BOLD);

            string generatedDate = "Datum vygenerování: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            string titleText = "Úplný výpis o pacientovi";
            string personalDataText = "Osobní údaje";
            string nameLabelText = "Jméno a příjmení: ";
            string birthNumberLabelText = "Rodné číslo: ";
            string sexLabelText = "Pohlaví: ";
            string addressLabelText = "Adresa: ";
            string healthInsuranceLabelText = "Zdravotní pojišťovna: ";
            string contactLabelText = "Kontaktní údaje: ";
            string smokerText = "Kuřák: ";
            string allergicLabelText = "Alergik: ";
            string personalMedicalHistoriesText = "Osobní anamnéza: ";
            string performedProceduresText = "Provedené zákroky:";
            string actualIllnessesText = "Nemoci a léky:";
            string hospitalizationText = "Hospitalizace:";

            Paragraph dateParagraph = new Paragraph(generatedDate, textFont);
            dateParagraph.Alignment = Element.ALIGN_RIGHT;
            document.Add(dateParagraph);

            Paragraph titleParagraph = new Paragraph(titleText, titleFont);
            titleParagraph.Alignment = Element.ALIGN_CENTER;
            document.Add(titleParagraph);

            Paragraph titlePersonalDataParagraph = new Paragraph(personalDataText, title2Font);
            titlePersonalDataParagraph.Alignment = Element.ALIGN_LEFT;
            document.Add(titlePersonalDataParagraph);

            // Jméno a příjmení
            Chunk boldNameLabel = new Chunk(nameLabelText, textBoldFont);
            Chunk patientName = new Chunk($"{patient.FirstName} {patient.LastName}", textFont);
            Paragraph nameParagraph = new Paragraph();
            nameParagraph.Add(boldNameLabel);
            nameParagraph.Add(patientName);
            document.Add(nameParagraph);

            // Rodné číslo
            Chunk boldBirthNumberLabel = new Chunk(birthNumberLabelText, textBoldFont);
            Chunk patientBirthNumber = new Chunk(patient.BirthNumber, textFont);
            Paragraph birthNumberParagraph = new Paragraph();
            birthNumberParagraph.Add(boldBirthNumberLabel);
            birthNumberParagraph.Add(patientBirthNumber);
            document.Add(birthNumberParagraph);

            // Pohlaví
            Chunk boldSexLabel = new Chunk(sexLabelText, textBoldFont);
            Chunk patientSex = new Chunk(SexEnumParser.GetStringFromEnumCzech(patient.Sex).ToLower(), textFont);
            Paragraph sexParagraph = new Paragraph();
            sexParagraph.Add(boldSexLabel);
            sexParagraph.Add(patientSex);
            document.Add(sexParagraph);

            // Adresa
            Chunk boldAddressLabel = new Chunk(addressLabelText, textBoldFont);
            Chunk patientAddress = new Chunk(patient.Address.ToString(), textFont);
            Paragraph addressParagraph = new Paragraph();
            addressParagraph.Add(boldAddressLabel);
            addressParagraph.Add(patientAddress);
            document.Add(addressParagraph);

            //Zdravotní pojišťovna:
            Chunk boldHealthInsuranceLabel = new Chunk(healthInsuranceLabelText, textBoldFont);
            Chunk patientHealthInsurance = new Chunk(patient.HealthInsurance.ToString(), textFont);
            Paragraph healthInsuranceParagraph = new Paragraph();
            healthInsuranceParagraph.Add(boldHealthInsuranceLabel);
            healthInsuranceParagraph.Add(patientHealthInsurance);
            document.Add(healthInsuranceParagraph);

            // Kontaktní údaje
            Chunk boldContactLabel = new Chunk(contactLabelText, textBoldFont);
            Chunk patientContact = new Chunk(patient.Contact.ToString(), textFont);
            Paragraph contactParagraph = new Paragraph();
            contactParagraph.Add(boldContactLabel);
            contactParagraph.Add(patientContact);
            document.Add(contactParagraph);

            //Kuřák
            Chunk boldSmokerLabel = new Chunk(smokerText, textBoldFont);
            Chunk patientSmoker = new Chunk((patient.HealthInsurance.IsSmoker == true ? "Ano":"Ne"), textFont);
            Paragraph smokerParagraph = new Paragraph();
            smokerParagraph.Add(boldSmokerLabel);
            smokerParagraph.Add(patientSmoker);
            document.Add(smokerParagraph);

            // Alergik
            Chunk boldAllergicLabel = new Chunk(allergicLabelText, textBoldFont);
            Chunk patientAllergic = new Chunk((patient.HealthInsurance.IsAllergic ? "Ano" : "Ne"), textFont);
            Paragraph allergicParagraph = new Paragraph();
            allergicParagraph.Add(boldAllergicLabel);
            allergicParagraph.Add(patientAllergic);
            document.Add(allergicParagraph);


            //Osobní anamnéza
            document.Add(new Paragraph("\n"));
            Paragraph titlePersonalMedicalHistoriesParagraph = new Paragraph(personalMedicalHistoriesText, title2Font);
            titlePersonalMedicalHistoriesParagraph.Alignment = Element.ALIGN_LEFT;
            document.Add(titlePersonalMedicalHistoriesParagraph);

            ObservableCollection<PersonalMedicalHistory> medicalhistories = await LoadMedicalHistoryAsync(patient);
            foreach (var history in medicalhistories)
            {
                Chunk historyChunk = new Chunk($"- {history.Description}\n", textFont);
                Paragraph historyParagraph = new Paragraph(historyChunk);
                document.Add(historyParagraph);
            }

            // Provedené zákroky
            document.Add(new Paragraph("\n"));
            Paragraph titlePerformedProceduresParagraph = new Paragraph(performedProceduresText, title2Font);
            titlePerformedProceduresParagraph.Alignment = Element.ALIGN_LEFT;
            document.Add(titlePerformedProceduresParagraph);

            ObservableCollection<PerformedProcedure> performedProcedures = await LoadPerformedProcedures(patient);
            foreach (var procedure in performedProcedures)
            {
                Chunk procedureChunk = new Chunk($"- {procedure.ToString()}\n", textFont);
                Paragraph procedureParagraph = new Paragraph(procedureChunk);
                document.Add(procedureParagraph);
            }

            // Nemoci a léky na ně
            document.Add(new Paragraph("\n"));

            Paragraph titleActualIllnessesParagraph = new Paragraph(actualIllnessesText, title2Font);
            titleActualIllnessesParagraph.Alignment = Element.ALIGN_LEFT;
            document.Add(titleActualIllnessesParagraph);
            ObservableCollection<DataActualIllness> actualIllnesses = await LoadActualIllnesses(patient);
            foreach (var illness in actualIllnesses)
            {
                Chunk illnessChunk = new Chunk($"- {illness.ToString()}\n", textFont);
                Paragraph illnessParagraph = new Paragraph(illnessChunk);
                document.Add(illnessParagraph);
            }


            // Hospitalizace
            document.Add(new Paragraph("\n"));
            Paragraph titleHospitalizationParagraph = new Paragraph(hospitalizationText, title2Font);
            titleHospitalizationParagraph.Alignment = Element.ALIGN_LEFT;
            document.Add(titleHospitalizationParagraph);

            ObservableCollection<Hospitalization> hospitalizations = await LoadHospitalizations(patient);
            foreach (var hospitalization in hospitalizations)
            {
                Chunk hospitalizationChunk = new Chunk($"- {hospitalization.ToString()}\n", textFont);
                Paragraph hospitalizationParagraph = new Paragraph(hospitalizationChunk);
                document.Add(hospitalizationParagraph);
            }



            return document;
        }

        private static void DownloadPdf(string filePath)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.FileName = Path.GetFileName(filePath);

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                File.Copy(filePath, saveFileDialog.FileName, true);
            }
        }

        private static async Task<ObservableCollection<PersonalMedicalHistory>> LoadMedicalHistoryAsync(Patient CurrentPatient)
        {
            PersonalMedicalHistoriesRepo repo = new PersonalMedicalHistoriesRepo();
            return await repo.GetPersonalMedicalHistoryByPatientIdAsync(CurrentPatient.Id);
        }

        private static async Task<ObservableCollection<PerformedProcedure>> LoadPerformedProcedures(Patient CurrentPatient)
        {
            PerformedProceduresRepo repo = new PerformedProceduresRepo();
            return await repo.GetAllPerformedProceduresAsync(CurrentPatient.Id);
        }

        private static async Task<ObservableCollection<DataActualIllness>> LoadActualIllnesses(Patient CurrentPatient)
        {
            PatientRepo repo = new PatientRepo();
            return await repo.GetActualIllnessByPatientIdAsync(CurrentPatient.Id);
        }

        private static async Task<ObservableCollection<Hospitalization>> LoadHospitalizations(Patient CurrentPatient)
        {
            HospitalizationRepo repo = new HospitalizationRepo();
            return await repo.GetAllHospitalizationsAsync(CurrentPatient.Id);
        }
    }
}
