using ado_net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ado_net.DAL.Interfaces
{
    public interface IPersonDAL
    {
        List<Person> GetAll();
        void New(Person person);
        void Update(Person person);
        void Delete(int id);
    }
}
