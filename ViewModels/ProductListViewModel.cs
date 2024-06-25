using Microsoft.EntityFrameworkCore;
using SaveUp.Library;
using SaveUp.Library.Services;
using SaveUp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SaveUp.ViewModels
{
    public class ProductListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ProductModel> products;
        private decimal totalSavings;
        private ProductService ProductService { get; set; }

        public ProductListViewModel(ProductService service)
        {
            
            try
            {
                ProductService = service;
                //Products = new ObservableCollection<ProductModel>();
                LoadProductsCommand = new Command(async () => await LoadProducts());
                DeleteAllCommand = new Command(async () => await DeleteAllProducts());

                // Load products initially
                LoadProductsCommand.Execute(null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public ObservableCollection<ProductModel> Products
        {
            get => products;
            set
            {
                if (products != value)
                {
                    products = value;
                    OnPropertyChanged(nameof(Products));
                }
            }
        }

        public decimal TotalSavings
        {
            get => totalSavings;
            set
            {
                if (totalSavings != value)
                {
                    totalSavings = value;
                    OnPropertyChanged(nameof(TotalSavings));
                }
            }
        }

        public ICommand LoadProductsCommand { get; }
        public ICommand DeleteAllCommand { get; }

        private async Task LoadProducts()
        {
            var productsList = await ProductService.GetAllAsync();
            Products = new ObservableCollection<ProductModel>(productsList ?? new List<ProductModel>());
            TotalSavings = Products.Sum(p => p.Price);
        }

        private async Task DeleteAllProducts()
        {
            var result = await ProductService.DeleteAllAsync();
            if (result)
            {
                Products.Clear();
                TotalSavings = 0;
            }
        }

        public void AddProduct(ProductModel product)
        {
            Products.Add(product);
            TotalSavings += product.Price;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
