using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.ui.ViewModel
{
    class ApplicationVM : ObservableObject
    {
        public static TokenResponse token = null;
        public ApplicationVM()
        {
            Pages.Add(new StartVM());
            Pages.Add(new KL_LoginVM());
            Pages.Add(new KL_RegistreerVM());
            Pages.Add(new KlantenVM());
            Pages.Add(new ManagementVM());
            Pages.Add(new MedewerkersVM());
            Pages.Add(new MM_loginVM());
            Pages.Add(new MW_loginVM());

            // Add other pages

            CurrentPage = Pages[0];
        }

        

        private object currentPage;
        public object CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged("CurrentPage"); }
        }

        private List<IPage> pages;
        public List<IPage> Pages
        {
            get
            {
                if (pages == null)
                    pages = new List<IPage>();
                return pages;
            }
        }

        public ICommand ChangePageCommand
        {
            get { return new RelayCommand<IPage>(ChangePage); }
        }

        public void ChangePage(IPage page)
        {
            CurrentPage = page;
        }
    }
}
