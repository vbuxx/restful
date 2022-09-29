using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    public interface IDivisionRepository
    {
        List<Division> Get();

        Division Get(int id);

        int Post(Division division);

        int Put(Division division);

        int Delete(int id);
    }
}
