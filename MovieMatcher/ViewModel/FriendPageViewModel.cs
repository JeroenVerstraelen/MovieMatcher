using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieMatcher.Model;
using Xamarin.Forms;

namespace MovieMatcher.ViewModel
{
    class FriendPageViewModel : BasePageViewModel
    {
        ObservableCollection<PersonViewModel> _found_Contacts = new ObservableCollection<PersonViewModel>();

        public INavigation Navigation;

        public class PersonViewModel
        {
            public string Nickname { set; get; }
            public string Emailaddress { set; get; }
            public string Id { set; get; }
            public ICommand ItemTappedCommand { set; get; }
        }

        public FriendPageViewModel()
        {
            InitializeContacts();
        }

        public ObservableCollection<PersonViewModel> Found_Contacts
        {
            get => _found_Contacts;

            set
            {
                _found_Contacts = value;
                RaisePropertyChanged();
            }
        }

        private async void updateFound()
        {
            IMobileServiceTable<Person> personTable = Database.client.GetTable<Person>();
            List<Person> entities = await personTable.Where(x => x.Nickname.StartsWith(_searchText)).Take(5).ToListAsync();
            List<PersonViewModel> viewModels = new List<PersonViewModel>();
            foreach (Person contact in entities)
            {
                PersonViewModel viewModel = new PersonViewModel() {
                    Nickname = contact.Nickname,
                    Emailaddress = contact.Emailaddress,
                    Id = contact.Id,
                    ItemTappedCommand = new Command<PersonViewModel>(async (personViewModel) =>
                    {
                        Console.WriteLine("Adding friend with id: " + personViewModel.Id);
                        bool shouldSave = await App.Current.MainPage.DisplayAlert("Add Friend", "Are you sure you would like to add " + personViewModel.Nickname + " as a friend?", "Yes", "No");
                        Console.WriteLine("Answer: " + shouldSave);
                        if (shouldSave)
                        {
                            // Save friendship.
                            Friend friend = new Friend() { userid1 = LoginPageViewModel.User.Id, userid2 = personViewModel.Id, confirmed = true};
                            Database.Save<Friend>(friend);
                            await App.Current.MainPage.DisplayAlert("Friend has been added!", "", "CONTINUE");
                            await Navigation.PopAsync();
                        }
                    })
                };
                viewModels.Add(viewModel);
            }
            Found_Contacts = new ObservableCollection<PersonViewModel>(viewModels);
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set { 
                if (_searchText != value) 
                {
                    _searchText = value ?? string.Empty;
                    RaisePropertyChanged();
                    updateFound();
                } 
            }
        }


        async void InitializeContacts()
        {
            ObservableCollection<Person> temp = new ObservableCollection<Person>();
            temp.Add(new Person() { Nickname = "John", Emailaddress="john@hotmail.com" });
            temp.Add(new Person() { Nickname = "Johnz", Emailaddress = "test@test.com" });
            temp.Add(new Person() { Nickname = "Johnze", Emailaddress = "test@test.com" });
            temp.Add(new Person() { Nickname = "Johnzet", Emailaddress = "test@test.com" });
            temp.Add(new Person() { Nickname = "Johnzetter", Emailaddress = "test@test.com" });
            temp.Add(new Person() { Nickname = "Johnzetterda", Emailaddress = "test@test.com" });
            temp.Add(new Person() { Nickname = "Johnzetterdada", Emailaddress = "test@test.com" });
            temp.Add(new Person() { Nickname = "Johnzetterdadababa", Emailaddress = "test@test.com" });
            temp.Add(new Person() { Nickname = "Johnzetterdadababathethird", Emailaddress = "test@test.com" });
            temp.Add(new Person() { Nickname = "Suzy", Emailaddress = "test@test.com" });
            temp.Add(new Person() { Nickname = "Adam", Emailaddress = "test@test.com" });
            temp.Add(new Person() { Nickname = "Jeroen", Emailaddress = "test@test.com" });
            temp.Add(new Person() { Nickname = "Celien", Emailaddress = "celien@hotmail.com" });
            temp.Add(new Person() { Nickname = "Mathias", Emailaddress = "mathias@hotmail.com" });
            temp.Add(new Person() { Nickname = "Kristof", Emailaddress = "kristof@hotmail.com" });
            temp.Add(new Person() { Nickname = "Filip", Emailaddress = "filip@hotmail.com" });

            /*
            foreach(Person contact in temp)
            {
                await Database.Save<Person>(contact);
            }*/
        }
    }
}
