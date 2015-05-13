using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using WPF45_Commanding_Enable_Disable.Commands;
using WPF45_Commanding_Enable_Disable.ModelClasses;

namespace WPF45_Commanding_Enable_Disable.ViewModels
{
    /// <summary>
    /// The ViewModel class
    /// </summary>
    public class PersonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string pName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(pName));
            }
        }

        ObservableCollection<PersonInfo> _Persons;

        public ObservableCollection<PersonInfo> Persons
        {
            get { return _Persons; }
            set { _Persons = value; }
        }


        string _Name;

        public string Name
        {
            get { return _Name; }
            set 
            { 
                _Name = value;
                OnPropertyChanged("Name");
            }
        }

         

        public RelayActionCommand SearchPersonCommnad { get; set; }

        DataAccess objds;
        public PersonViewModel()
        {
            Persons = new ObservableCollection<PersonInfo>();
            objds = new DataAccess();


            Persons = new ObservableCollection<PersonInfo>(objds.GetPersonData());

            var defaultView = CollectionViewSource.GetDefaultView(Persons);

            //The Command object is initialized where the 'CanExecuteAction' will be set True or False
            //based upon the State of the 'Name' property
            //The 'ExecuteAction' will accepts the data filtered from the collection
            //based upon the data entered in the TextBox
            SearchPersonCommnad = new RelayActionCommand()
            {
                CanExecuteAction = n=> !String.IsNullOrEmpty(Name),
                 ExecuteAction = n => defaultView.Filter = name => ((PersonInfo)name).FirstName.StartsWith(Name) 
                     || ((PersonInfo)name).LastName.StartsWith(Name) 
                     || ((PersonInfo)name).City==Name
            };
        }

    }
}
