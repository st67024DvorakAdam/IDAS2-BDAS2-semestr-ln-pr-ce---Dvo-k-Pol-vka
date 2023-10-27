using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.ViewModels.EntitiesVM
{
    public class AssistantViewModel
    {
        // MODEL
        private readonly Assistant _assistant;

        //TODO

        // VIEWMODEL
        public AssistantViewModel(Assistant assistant)
        {
            _assistant = assistant;
        }
    }
}
