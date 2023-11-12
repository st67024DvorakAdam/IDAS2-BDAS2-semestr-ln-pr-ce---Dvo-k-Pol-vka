using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Database_Hospital_Application.ViewModels.ViewsVM
{
    public class MessagesVM : BaseViewModel
    {
        private ObservableCollection<Message> _messagesList;
        private Message _selectedMessage;

        
        public ICommand AddMessageCommand { get; private set; }
        public ICommand EditMessageCommand { get; private set; }
        public ICommand DeleteMessageCommand { get; private set; }


        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////
        public MessagesVM()
        {
            LoadMessagesAsync();
            //_messagesList = new ObservableCollection<Message>();
            MessagesView = CollectionViewSource.GetDefaultView(MessagesList);
            MessagesView.Filter = MessagesFilter;

        }


        private async Task LoadMessagesAsync()
        {
            MessageRepo repo = new MessageRepo();
            MessagesList = await repo.GetAllMessagesAsync();
        }

        ///KONSTRUKTOR ////////////////////////////////////////////////////////////////////////////////////////

        public ObservableCollection<Message> MessagesList
        {
            get { return _messagesList; }
            set
            {
                _messagesList = value;
                OnPropertyChange(nameof(MessagesList));
            }
        }

        public Message SelectedMessage
        {
            get { return _selectedMessage; }
            set
            {
                _selectedMessage = value;
                OnPropertyChange(nameof(SelectedMessage));
                
            }
        }

        

        private void AddMessage()
        {
           
        }

        private bool CanEditMessage()
        {
            
            return _selectedMessage != null;
        }

        private void EditMessage()
        {
            
        }

        private bool CanDeleteMessage()
        {
            
            return _selectedMessage != null;
        }

        private void DeleteMessage()
        {
            
            if (_selectedMessage != null)
            {
                MessagesList.Remove(_selectedMessage);
            }
        }


        //FILTER/////////////////////////////////////////////////////////////////////

        private string _searchText;
        public ICollectionView MessagesView { get; private set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChange(nameof(SearchText));
                MessagesView.Refresh();
            }
        }

        private bool MessagesFilter(object item)
        {

            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var message = item as Message;
            if (message == null) return false;

            return message.ID.ToString().Contains(_searchText)
                || message.Sender.ToString().Contains(_searchText)
                || message.Recipient.ToString().Contains(_searchText)
                || message.Content.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || message.DateSent.ToString().Contains(_searchText, StringComparison.OrdinalIgnoreCase);
        }
        //FILTER/////////////////////////////////////////////////////////////////////


    }
}
