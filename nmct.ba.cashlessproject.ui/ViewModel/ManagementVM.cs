using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nmct.ba.cashlessproject.model;

namespace nmct.ba.cashlessproject.ui.ViewModel
{
    class ManagementVM: ObservableObject, IPage
    {

        public string Name
        {
            get { return "Management"; }
        }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { _customers = value;
                  SelectedCustomer = Customers[0];
                  OnPropertyChanged("Customers"); }
        }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; OnPropertyChanged("SelectedCustomer"); }
        }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                SelectedProduct = Products[0];
                OnPropertyChanged("Products");
            }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged("SelectedProducts"); }
        }

    }
}
