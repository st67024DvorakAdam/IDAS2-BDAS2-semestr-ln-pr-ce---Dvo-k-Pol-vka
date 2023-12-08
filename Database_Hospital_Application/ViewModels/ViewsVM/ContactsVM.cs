using Database_Hospital_Application.Commands;
using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using Database_Hospital_Application.Models.Tools;
using Database_Hospital_Application.ViewModels.Dialogs.Edit;
using Database_Hospital_Application.Views.Lists.Dialogs.Contact;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class ContactsVM : BaseViewModel
    {
        private ObservableCollection<Contact> _contactsList;

        public ObservableCollection<Contact> ContactsList
        {
            get { return _contactsList; }
            set
            {
                _contactsList = value;
                OnPropertyChange(nameof(ContactsList));
            }
        }

        private ObservableCollection<Employee> _employeesList;

        public ObservableCollection<Employee> EmployeesList
        {
            get { return _employeesList; }
            set
            {
                _employeesList = value;
                OnPropertyChange(nameof(EmployeesList));
            }
        }

        private void LoadEmployeesFromEmployeeVM()
        {
            EmployeeVM employeeVM = new EmployeeVM();
            _employeesList = employeeVM.EmployeesList;
        }



        private ObservableCollection<Patient> _patientsList;

        public ObservableCollection<Patient> PatientsList
        {
            get { return _patientsList; }
            set
            {
                _patientsList = value;
                OnPropertyChange(nameof(PatientsList));
            }
        }

        private void LoadPatientsFromPatientVM()
        {
            PatientVM patientVM = new PatientVM();
            _patientsList = patientVM.PatientsList;
        }


        // BUTTONS
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        private Contact _selectedContact;
        public Contact SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                if (_selectedContact != value)
                {
                    _selectedContact = value;
                    OnPropertyChange(nameof(SelectedContact));
                    (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private Contact _newContact = new Contact();
        public Contact NewContact
        {
            get { return _newContact; }
            set
            {
                _newContact = value;
                OnPropertyChange(nameof(NewContact));
            }
        }

        public ContactsVM()
        {
            LoadEmployeesFromEmployeeVM();
            LoadPatientsFromPatientVM();

            LoadContactsAsync();
            InitializeCommands();
  
        }

        private async Task LoadContactsAsync()
        {
            ContactRepo repo = new ContactRepo();
            ContactsList = await repo.GetAllContactsAsync();
            if (ContactsList != null)
            {
                ContactsView = CollectionViewSource.GetDefaultView(ContactsList);
                ContactsView.Filter = ContactFilter;
            }
        }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddNewContactAction);
            DeleteCommand = new RelayCommand(DeleteContactAction, CanExecuteDelete);
            EditCommand = new RelayCommand(EditAction, CanEdit);
        }

        private bool CanExecuteDelete(object parameter)
        {
            return SelectedContact != null;
        }

        private async void DeleteContactAction(object parameter)
        {
            if (SelectedContact == null) return;

            ContactRepo contactRepo = new ContactRepo();
            await contactRepo.DeleteContact(SelectedContact.Id);
            await LoadContactsAsync();
        }

        private async void AddNewContactAction(object parameter)
        {
            if (!IsContactValidAndFilled(NewContact))
            {
                
                    MessageBox.Show("Vyplňte správně všechna pole", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                
            }
            ContactRepo contactRepo = new ContactRepo();
            await contactRepo.AddContact(NewContact);
            await LoadContactsAsync();
            NewContact = new Contact();
        }

        private bool CanEdit(object parameter)
        {
            return SelectedContact != null;
        }

        private void EditAction(object parameter)
        {
            if (!CanEdit(parameter)) return;

            
            EditContactVM editVM = new EditContactVM(SelectedContact);
            EditContactDialog editDialog = new EditContactDialog(editVM);

            editDialog.ShowDialog();

            
            LoadContactsAsync();
            
        }

        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView ContactsView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                ContactsView.Refresh();
            }
        }

        private bool ContactFilter(object item)
        {
            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var contact = item as Contact;
            if (contact == null) return false;

            
            return contact.PhoneNumber.ToString().Contains(_searchText)
                   || contact.Email.Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsContactValidAndFilled(Contact contact)
        {
            return (!string.IsNullOrEmpty(contact.Email) && contact.Email.Contains("@") && contact.Email.Contains(".") && 
                (contact.PhoneNumber != 0 && contact.PhoneNumber != null && contact.PhoneNumber.ToString().Length == 9) && ((contact.IdOfEmployee != 0 && contact.IdOfEmployee != null) ||
                (contact.IdOfPatient != 0 && contact.IdOfPatient!= null)));
        }
    }
}
