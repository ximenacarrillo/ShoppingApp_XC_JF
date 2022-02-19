using Isi.ShoppingApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Presentation.ViewModels
{
    public class ShoppingAppViewModel
    {
        private Users user;

        public ShoppingAppViewModel(Users user)
        {
            this.user = user;
        }
    }
}
