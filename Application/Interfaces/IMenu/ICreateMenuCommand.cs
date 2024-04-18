using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IMenu
{
    public interface ICreateMenuCommand
    {
        public Menu CreateMenu()
    }
}
