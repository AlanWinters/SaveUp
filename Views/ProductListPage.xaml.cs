using SaveUp.ViewModels;

namespace SaveUp.Views;

public partial class ProductListPage : ContentPage
{
	public ProductListPage(ProductListViewModel model)
	{
		InitializeComponent();

        BindingContext = model;
    }
}