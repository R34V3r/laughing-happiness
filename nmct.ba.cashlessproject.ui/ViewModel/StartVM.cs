using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.ViewModel
{
    class StartVM: ObservableObject, IPage
    {

        public string Name
        {
            get { return "Medewerkers Login"; }
        }

        public ICommand ShowLoginManagement
        {
            get { return new RelayCommand(MMLoginManagement); }
        }

        public async void MMLoginManagement()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new MM_loginVM());
        }

        public ICommand ShowLoginKassa
        {
            get { return new RelayCommand(MWLogin); }
        }

        public async void MWLogin()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new MW_loginVM());
        }

        public ICommand ShowLoginKlant
        {
            get { return new RelayCommand(KLLogin); }
        }

        public async void KLLogin()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new KL_LoginVM());
        }
    }
}
