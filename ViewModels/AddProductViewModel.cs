using Microsoft.EntityFrameworkCore;
using SaveUp.Library;
using SaveUp.Library.Enums;
using SaveUp.Library.Services;
using SaveUp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SaveUp.ViewModels
{
    public class AddProductViewModel : INotifyPropertyChanged
    {
        private Models.ProductModel product = new();

        private ProductService ProductService { get; set; }
        private readonly ProductListViewModel _productListViewModel;

        #region Properties
        public string ProductName
        {
            get => product.ProductName;
            set
            {
                if (product.ProductName != value)
                {
                    product.ProductName = value;
                    OnPropertyChanged(nameof(ProductName));
                }
            }
        }

        public string Description
        {
            get => product.Description;
            set
            {
                if (product.Description != value)
                {
                    product.Description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public Category Category
        {
            get => product.Category;
            set
            {
                if (product.Category != value)
                {
                    product.Category = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }

        public decimal Price
        {
            get => product.Price;
            set
            {
                if (product.Price != value)
                {
                    product.Price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        public DateTime DateTimeAdded
        {
            get => product.DateTimeAdded;
            set
            {
                if (product.DateTimeAdded != value)
                {
                    product.DateTimeAdded = value;
                    OnPropertyChanged(nameof(DateTimeAdded));
                }
            }
        }

        public ObservableCollection<Category> Categories { get; }

        // Commands to handle form actions
        public ICommand SaveCommand { get; }
        public ICommand ResetCommand { get; }

        #endregion


        public AddProductViewModel(ProductService service, ProductListViewModel productListViewModel) // , ProductListViewModel productListViewModel
        {
            try
            {
                ProductService = service;
                _productListViewModel = productListViewModel;
                SaveCommand = new Command(async () => await OnSave());
                ResetCommand = new Command(OnReset);
                DateTimeAdded = DateTime.Now;
                //var t = Enum.GetValues(typeof(Category));
                Categories = new ObservableCollection<Category>(Enum.GetValues(typeof(Category)).Cast<Category>());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task OnSave()
        {
            //var newProduct = new ProductModel
            //{
            //    ProductName = this.ProductName,
            //    Description = this.Description,
            //    Price = this.Price,
            //    Category = this.Category,
            //    DateTimeAdded = this.DateTimeAdded
            //};

            var result = await ProductService.CreateAsync(product);

            if (result != null)
            {
                _productListViewModel.AddProduct(product);

                OnReset();
            }
        }

        public void OnReset()
        {
            product = new ProductModel();

            OnPropertyChanged(nameof(ProductName));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(DateTimeAdded));
            //ProductName = string.Empty;
            //Description = string.Empty;
            //Category = Category.Sonstiges;
            //Price = 0;
            //DateTimeAdded = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
