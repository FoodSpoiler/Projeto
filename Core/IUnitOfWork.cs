using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgilFood.Core
{
    public interface IUnitOfWork //Fiz esse cara aqui para poder salvar tudo de uma vez no repositorio, seguir o padrao de que um repositorio por ser tipo uma lista, nao deve ter o saveChanges()
    {
        Task CompleteAsync();
    }
}

