using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public MessagesVM()
        {
            _messagesList = new ObservableCollection<Message>();

        }

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
    }
}
