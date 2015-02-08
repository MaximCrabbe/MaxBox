using System;
using System.Collections.Generic;

namespace MaxBox.Core.Helpers
{
    public interface IAssemblyHelper
    {
        IEnumerable<Type> GetAllTypes(string assemblyFullName);
        IEnumerable<Type> GetAllClassesWithInterface<T>(string assemblyFullName) where T : class;
        bool IsRealClass(Type classType);
        IEnumerable<Type> GetAllClassesFromAbstractClass<T>(string assemblyFullName) where T : class;
    }
}