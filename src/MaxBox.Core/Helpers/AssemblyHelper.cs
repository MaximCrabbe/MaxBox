using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MaxBox.Core.Helpers
{
    public class AssemblyHelper : IAssemblyHelper
    {
        public IEnumerable<Type> GetAllTypes(string assemblyFullName)
        {
            Assembly assembly = Assembly.Load(assemblyFullName);
            return assembly.GetTypes().ToList();
        }

        public IEnumerable<Type> GetAllClassesWithInterface<T>(string assemblyFullName) where T : class
        {
            return GetAllTypes(assemblyFullName).Where(x => typeof (T).IsAssignableFrom(x) && IsRealClass(x)).ToList();
        }



        public bool IsRealClass(Type classType)
        {
            return classType.IsAbstract == false
                   && classType.IsGenericTypeDefinition == false
                   && classType.IsInterface == false;
        }

        //public IEnumerable<Type> GetAllTypesFromAbstractClass(Type desiredType)
        //{
        //    return AppDomain
        //        .CurrentDomain
        //        .GetAssemblies()
        //        .SelectMany(assembly => assembly.GetTypes())
        //        .Where(type => desiredType.IsAssignableFrom(type));
        //}

        //public IEnumerable<Type> GetAllTypesFromAbstractClass<T>(string assemblyFullName) where T : class
        //{
        //    return typeof (T).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof (T)) && IsRealClass(t));
        //}
    }
}