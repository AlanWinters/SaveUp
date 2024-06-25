using Microsoft.EntityFrameworkCore;
using SaveUp.Library;
using SaveUp.Library.Services;
using SaveUp.ViewModels;

namespace SaveUp.Views;

public partial class AddProductPage : ContentPage
{
	public AddProductPage(AddProductViewModel model)
	{
		InitializeComponent();

        BindingContext = model;
    }
}