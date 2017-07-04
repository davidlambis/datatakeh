using DATATAKEH.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DATATAKEH.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FotoPage : ContentPage
	{
		public FotoPage ()
		{
			InitializeComponent ();

            BindingContext = new TakePictureViewModel();

        }
	}
}